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
        public async Task<IActionResult> NewPost(string title)
        {
            var redditService = new RedditService();
            await redditService.PostOrComment("Duplicate Post Detection",
                "Hey this post should only appear once, then this same text as a new comment the second time around.");

            return RedirectToAction("Index", "Home");
        }
    }
}
