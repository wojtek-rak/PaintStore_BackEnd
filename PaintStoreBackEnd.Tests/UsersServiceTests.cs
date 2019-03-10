﻿using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
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
    public class UsersServiceTests
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
        public void GetUser_ValidUserId_ReturnUser()
        {
            var init = new InitializeMockContext();
            var mock = init.mock;

            var usersService = new UsersService(mock.Object, new PostService(mock.Object), new FollowersService(mock.Object, mapper));
            var result = usersService.GetUser(2, 1);
            var expected2 = true;
            var expected = "Kasia";
            Assert.AreEqual(expected2, result.Followed);
            Assert.AreEqual(result.Name, expected);
        }

        [Test]
        public void GetUser_NoLoggedUser_ReturnUserNoFOllowed()
        {
            var init = new InitializeMockContext();
            var mock = init.mock;

            var usersService = new UsersService(mock.Object, new PostService(mock.Object), new FollowersService(mock.Object, mapper));
            var result = usersService.GetUser(-1, 1);
            var expected2 = false;
            var expected = "Kasia";
            Assert.AreEqual(expected2, result.Followed);
            Assert.AreEqual(result.Name, expected);
        }
        //[Test]
        //public void GetUser_MostCategoryTool_Test()
        //{
        //    var init = new InitializeMockContext();
        //    var mock = init.mock;

        //    var controller = new UsersService(mock.Object);
        //    var result = controller.GetUser(1);
        //    var expected = "pencil";
        //    Assert.AreEqual(result.MostUsedCategoryToolName, expected);
        //}

        [Test]
        public void EditUser_AllProperties_Test()
        {
            var init = new InitializeMockContext();
            var mock = init.mock;

            var controller = new UsersService(mock.Object, new PostService(mock.Object), new FollowersService(mock.Object, mapper));
            var expectedAvatarImgLink = "Testowy Komentarz";
            var expectedAbout = "abouut";
            var expectedBackgroundImgLink = "bacckgf";
            var expectedName = "namee";
            var expectedLink = "linkdd";
            var editedUser = controller.EditUser(new Users { Id = 1, AvatarImgLink = expectedAvatarImgLink,
                About = expectedAbout, BackgroundImgLink = expectedBackgroundImgLink, Name = expectedName, Link = expectedLink});

            mock.Verify(m => m.SaveChanges(), Times.Once());
            Assert.AreEqual(expectedAvatarImgLink, editedUser.AvatarImgLink);
            Assert.AreEqual(expectedAbout, editedUser.About);
            Assert.AreEqual(expectedBackgroundImgLink, editedUser.BackgroundImgLink);
            Assert.AreEqual(expectedName, editedUser.Name);
            Assert.AreEqual(expectedLink, editedUser.Link);
        }

        [Test]
        public void AddUser_ValidUser_Test()
        {
            var init = new InitializeMockContext();
            var mock = init.mock;

            var controller = new UsersService(mock.Object, new PostService(mock.Object), new FollowersService(mock.Object, mapper));
            var editedCom = controller.AddUser(new AddUserCommand() { Email  = "Mail", Password = "Passwd", Name = "Loxin"});
            mock.Verify(m => m.SaveChanges(), Times.Once());
            init.mockSetUsers.Verify(m => m.Add(It.IsAny<Users>()), Times.Once());
        }

        [Test]
        public void GetImages_UserImagesTheNewest_Test()
        {
            var init = new InitializeMockContext();
            var mock = init.mock;

            var controller = new UsersService(mock.Object, new PostService(mock.Object), new FollowersService(mock.Object, mapper));
            var result = controller.GetPosts(1, "the_newest");
            var expected = 4;
            Assert.AreEqual(expected, result.First().Id);
        }

        [Test]
        public void GetImages_UserImagesMostPopularTest()
        {
            var init = new InitializeMockContext();
            var mock = init.mock;

            var controller = new UsersService(mock.Object, new PostService(mock.Object), new FollowersService(mock.Object, mapper));
            var result = controller.GetPosts(1, "most_popular");
            var expected = 5;
            Assert.AreEqual(expected, result.First().Id);
        }

        [Test]
        public void EditUserCredentials_ChangeValues_Changed()
        {
            var init = new InitializeMockContext();
            var mock = init.mock;

            var controller = new UsersService(mock.Object, new PostService(mock.Object), new FollowersService(mock.Object, mapper));
            var expectedEmail = "Testowy Komentarz";
            var expectedHash = "hashSW@";
            var editedUser = controller.EditUserCredentials(new Users { Id = 1, Email = expectedEmail, PasswordHash = expectedHash });

            mock.Verify(m => m.SaveChanges(), Times.Once());
            Assert.AreEqual(expectedEmail, editedUser.Email);
            Assert.AreEqual(expectedHash, editedUser.PasswordHash);
        }
        [Test]
        public void RemoveUser_ValidPassword_Remove()
        {
            var init = new InitializeMockContext();
            var mock = init.mock;

            //var actorSystem = ActorSystem.Create("PSActorSystem");
            //var actorRemove = actorSystem.ActorOf(Props.Create(() => new RemoveAccountImagesActor()));
            //var actorSupervisor = actorSystem.ActorOf(Props.Create(() => new SupervisorActor(actorRemove)));

            var controller = new UsersService(mock.Object, new PostService(mock.Object), new FollowersService(mock.Object, mapper));
            var removeAccountt = controller.RemoveUser(new Users { Id = 1, PasswordHash = "!@#sdaAWEDAFSFDSAE" });

            //mock.Verify(m => m.SaveChanges(), Times.Once());
            Thread.Sleep(100);
            init.mockSetUsers.Verify(m => m.Remove(It.IsAny<Users>()), Times.Once());
            init.mockSetImages.Verify(m => m.Remove(It.IsAny<Posts>()), Times.Exactly(5));
            init.mockSetUserFollowers.Verify(m => m.Remove(It.IsAny<UserFollowers>()), Times.Exactly(2));
        }

        [Test]
        public void RemoveUser_WithBadPassword_NoRemove()
        {
            var init = new InitializeMockContext();
            var mock = init.mock;

            //var actorSystem = ActorSystem.Create("PSActorSystem");
            //var actorRemove = actorSystem.ActorOf(Props.Create(() => new RemoveAccountImagesActor()));
            //var actorSupervisor = actorSystem.ActorOf(Props.Create(() => new SupervisorActor(actorRemove)));

            var controller = new UsersService(mock.Object, new PostService(mock.Object), new FollowersService(mock.Object, mapper));
            //var controller = new AccountRemoveController(mock.Object);
            var removeAccountt = controller.RemoveUser(new Users { Id = 1, PasswordHash = "!@#sawdasd" });
            var expectedMsg = "Password incorrect";
            Assert.AreEqual(expectedMsg, removeAccountt.PasswordHash);
        }

        [Test]
        public void RemoveUser_PerformanceTest()
        {
            var init = new InitializeMockContext();
            var mock = init.mock;

            //var actorSystem = ActorSystem.Create("PSActorSystem");
            //var actorRemove = actorSystem.ActorOf(Props.Create(() => new RemoveAccountImagesActor()));
            //var actorSupervisor = actorSystem.ActorOf(Props.Create(() => new SupervisorActor(actorRemove)));

            var controller = new UsersService(mock.Object, new PostService(mock.Object), new FollowersService(mock.Object, mapper));
            //var controller = new AccountRemoveController(mock.Object);

            var timespan = 10; // can be 0 for result

            Assert.That(Time(() => controller.RemoveUser(new Users { Id = 1, PasswordHash = "!@#sdaAWEDAFSFDSAE" })), Is.LessThanOrEqualTo(TimeSpan.FromSeconds(timespan)));

            //mock.Verify(m => m.SaveChanges(), Times.Once());
            init.mockSetUsers.Verify(m => m.Remove(It.IsAny<Users>()), Times.Once());
            init.mockSetImages.Verify(m => m.Remove(It.IsAny<Posts>()), Times.Exactly(5));
            init.mockSetUserFollowers.Verify(m => m.Remove(It.IsAny<UserFollowers>()), Times.Exactly(2));

        }



        private TimeSpan Time(Action toTime)
        {
            var timer = Stopwatch.StartNew();
            toTime();
            timer.Stop();
            return timer.Elapsed;
        }




        /// <summary>
        /// Memory test
        /// </summary>
        //[Test]
        //public void AccountRemovePerformenceMemoryTest()
        //{
        //    var init = new InitializeMockContext();
        //    var mock = init.mock;
        //    var accSystem = ActorSystem.Create("PSActorSystem");
        //    var actorRemove = accSystem.ActorOf(Props.Create(() => new RemoveAccountImagesActor()));
        //    var actorSupervisor = accSystem.ActorOf(Props.Create(() => new SupervisorActor(actorRemove)));
        //    var controller = new AccountRemoveController(mock.Object, new FollowersRemoveController(mock.Object), actorSupervisor);

        //    var memoryCheckPoint = dotMemory.Check();

        //    controller.RemoveAccount(new Accounts { Id = 1, PasswordHash = "!@#sdaAWEDAFSFDSAE" });
        //    for (int i = 0; i < 10000; i++)
        //    {
        //        controller.RemoveAccount(new Accounts { Id = 2, PasswordHash = "!@#sdaAWEDAFdsdsSFDSAE" });
        //    }

        //    dotMemory.Check(memory =>
        //    {
        //        Assert.That(memory.GetDifference(memoryCheckPoint)
        //            .GetSurvivedObjects()
        //            .GetObjects(where => where.Namespace.Like("backEnd"))
        //            .ObjectsCount, Is.EqualTo(0));
        //    });

        //    //controller.RemoveAccount(new Accounts { Id = 1, PasswordHash = "!@#sdaAWEDAFSFDSAE" })
        //}
    }
}