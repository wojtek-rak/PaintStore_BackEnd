﻿using System;
using System.Linq;
using AutoMapper;
using Moq;
using NUnit.Framework;
using PaintStore.Application.Services;
using PaintStore.BackEnd;
using PaintStore.Domain.Entities;
using PaintStore.Domain.InputModels;

namespace PaintStoreBackEnd.Tests
{
    [TestFixture]
    public class PostCommentsServiceTests
    {
        private IMapper mapper;
        [SetUp]
        public void Startup()
        {
            var config = new MapperConfiguration(cfg => { 
                cfg.AddProfile<MappingProfile>();
            });
            mapper = config.CreateMapper();
        }
        [Test]
        public void GetComments_ValidPostId_CheckNumber()
        {

            var init = new InitializeMockContext();
            var mock = init.mock;

            var controller = new PostCommentsService(mock.Object, mapper);
            var result = controller.GetComments(1 ,3).Count();
            var expected = 2;
            Assert.AreEqual(expected, result);
        }
        [Test]
        public void GetComments_MostPopular_CheckValid()
        {
            var init = new InitializeMockContext();
            var mock = init.mock;
            
            var controller = new PostCommentsService(mock.Object, mapper);
            var result = controller.GetComments(1, 3).First();
            var expected = 1;
            var expected2 = "Zosia";
            Assert.AreEqual(expected2, result.UserName);
            Assert.AreEqual(expected, result.LikeCount);
        }
        [Test]
        public void GetComments_CommentsLiked_CheckNumber()
        {
            var init = new InitializeMockContext();
            var mock = init.mock;

            var controller = new PostCommentsService(mock.Object, mapper);
            var result = controller.GetComments(2, 2).First();
            var expected = true;
            Assert.AreEqual(expected, result.Liked);
        }

        [Test]
        public void GetComments_NoUser_NotLiked()
        {
            var init = new InitializeMockContext();
            var mock = init.mock;

            var controller = new PostCommentsService(mock.Object, mapper);
            var result = controller.GetComments(-1, 2).First();
            var expected = false;
            Assert.AreEqual(expected, result.Liked);
        }

        [Test]
        public void GetComments_NoCommentsLiked_CheckNumber()
        {
            var init = new InitializeMockContext();
            var mock = init.mock;

            var controller = new PostCommentsService(mock.Object, mapper);
            var result = controller.GetComments(1, 2).First();
            var expected = false;
            Assert.AreEqual(expected, result.Liked);
        }

        [Test]
        public void CommentRemove_ValidId_Test()
        {
            var init = new InitializeMockContext();
            var mock = init.mock;

            var controller = new PostCommentsService(mock.Object, mapper);
            var editedCom = controller.CommentRemove(1);
            mock.Verify(m => m.SaveChanges(), Times.Once());
            init.mockSetComments.Verify(m => m.Remove(It.IsAny<PostComments>()), Times.Once());
            init.mockSetCommentLikes.Verify(m => m.Remove(It.IsAny<CommentLikes>()), Times.Exactly(2));
        }

        [Test]
        public void RemoveComment_Counting_Test()
        {
            var init = new InitializeMockContext();
            var mock = init.mock;
            var imageId = 2;
            var expectedCommentCountInt = mock.Object.Posts.Where(x => x.Id == imageId).First().CommentsCount;

            var controller = new PostCommentsService(mock.Object, mapper);
            controller.CommentRemove(1);
            mock.Verify(m => m.SaveChanges(), Times.Once());

            Assert.AreEqual(expectedCommentCountInt - 1, mock.Object.Posts.Where(x => x.Id == imageId).First().CommentsCount);
        }

        [Test]
        public void EditComment_ValidComment_Test()
        {
            var init = new InitializeMockContext();
            var mock = init.mock;

            var controller = new PostCommentsService(mock.Object, mapper);
            var expectedString = "Testowy Komentarz";
            var editedCom = controller.EditComment(new EditPostCommentCommand { Id = 1, Content = expectedString });
            mock.Verify(m => m.SaveChanges(), Times.Once());
            Assert.AreEqual(expectedString, editedCom.Content);
        }

        [Test]
        public void AddComment_ValidComment_Test()
        {
            var init = new InitializeMockContext();
            var mock = init.mock;

            var controller = new PostCommentsService(mock.Object, mapper);
            controller.AddComment(new AddPostCommentCommand {  Content = "Testowy Komentarz", PostId = 1, UserId = 1 });
            init.mockSetComments.Verify(m => m.Add(It.IsAny<PostComments>()), Times.Once());
            mock.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Test]
        public void AddComment_Counting_Test()
        {
            var init = new InitializeMockContext();
            var mock = init.mock;
            var imageId = 1;
            var expectedCommentCountInt = mock.Object.Posts.Where(x => x.Id == imageId).First().CommentsCount;

            var controller = new PostCommentsService(mock.Object, mapper);
            controller.AddComment(new AddPostCommentCommand { UserId = 2, PostId = imageId });
            mock.Verify(m => m.SaveChanges(), Times.Once());

            Assert.AreEqual(expectedCommentCountInt + 1, mock.Object.Posts.Where(x => x.Id == imageId).First().CommentsCount);
        }
        [Test]
        public void EditComment_BoolEdited_ChangeToTrue()
        {
            var init = new InitializeMockContext();
            var mock = init.mock;

            var postCommentsService = new PostCommentsService(mock.Object, mapper);
            var expectedBool = mock.Object.PostComments.First(x => x.Id == 1).Edited;
            var editedPost = postCommentsService.EditComment(new EditPostCommentCommand { Id = 1});
            mock.Verify(m => m.SaveChanges(), Times.Once());
            Assert.AreEqual(expectedBool, null);
            Assert.AreEqual(true, editedPost.Edited);
        }
    }
}