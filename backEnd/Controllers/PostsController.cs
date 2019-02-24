﻿using Microsoft.AspNetCore.Mvc;
using PaintStore.Application.Interfaces;
using PaintStore.Domain.Entities;

namespace backEnd.Controllers
{
    [Produces("application/json")]
    [Route("api/Posts")]
    public class PostsController : Controller
    {
        private readonly IPostsService _postsService;

        public PostsController(IPostsService postsService)
        {
            _postsService = postsService;
        }

        /// <summary>
        /// Get Post's Details
        /// </summary>
        /// <param name="userId">actually logged user</param>
        /// <param name="postId"></param>
        /// <returns></returns>
        [HttpGet("{userId}/{postId}")]
        public IActionResult GetPost(int userId, int postId)
        {
            return Ok(_postsService.GetPost(userId, postId));
        }

        [HttpGet("{userId}/GetFollowingPosts")]
        public IActionResult GetFollowingPosts(int userId)
        {
            return Ok(_postsService.GetFollowingPosts(userId));

        }

        /// <summary>
        /// Get AllPosts
        /// </summary>
        /// <param name="message">"the_newest" for newest
        /// "most_popular" for most popular
        /// </param>
        [HttpGet("AllPosts/{message}")]
        public IActionResult GetAllPosts(string message)
        {
            return Ok(_postsService.GetAllPosts(message));
        }

        [HttpGet("AllPostsByTag/{tag}")]
        public IActionResult GetPostsByTag(string tag)
        {
            return Ok(_postsService.GetPostsByTag(tag));
        }

        [HttpPost("AddPost")]
        public IActionResult AddImage([FromBody] Posts post)
        {
            return Ok(_postsService.AddImage(post));
        }

        [HttpPut("EditPost")]
        public IActionResult EditPost([FromBody] Posts post)
        {
            
            return Ok(_postsService.EditPost(post));
        }

        [HttpDelete("DeletePost/{postId}")]
        public IActionResult PostRemove(int postId)
        {
            return Ok(_postsService.PostRemover(postId));
        }
    }
}