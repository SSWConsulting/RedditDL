using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using RedditDL.Services;
using RedditSharp;
using RedditSharp.Things;

namespace RedditDL.Controllers
{
    public class RedditApiController : Controller
    {
        private readonly IRedditService _redditService;

        public RedditApiController(IRedditService redditService)
        {
            _redditService = redditService;
        }

        public async Task<IActionResult> NewPost(string title)
        {
            await _redditService.PostOrComment("Duplicate Post Detection",
                "Hey this post should only appear once, then this same text as a new comment the second time around.");

            return RedirectToAction("Index", "Home");
        }
    }
}
