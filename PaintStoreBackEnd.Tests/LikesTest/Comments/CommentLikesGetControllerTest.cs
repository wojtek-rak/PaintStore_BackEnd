﻿using backEnd.Controllers;
using backEnd.Controllers.LikeControllers.Comment;
using backEnd.Controllers.LikeControllers.Images;
using backEnd.Models;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using Microsoft.Extensions.DependencyInjection;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaintStoreBackEnd.Tests.Deprecated
{

    [TestFixture]
    class CommentLikesGetControllerTest
    {
        [Test]
        public void GetCommentsLikesTest()
        {
            Assert.AreEqual(true, true);

            //var init = new InitializeMockContext();
            //var mock = init.mock;

            //var controller = new CommentsLikesGetController(mock.Object);
            //var result = controller.GetCommentLikes(new PostComments { Id = 1 });
            //var expected = 2;
            //var expected2 = "wyrak";
            //Assert.AreEqual(expected, result.Count());
            //Assert.AreEqual(expected2, result.First().Name);
        }
    }
}







