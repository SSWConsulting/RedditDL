using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using RedditSharp;

namespace RedditDL.Services
{
    public class RedditService : IRedditService
    {
        public async Task PostOrComment(string title, string body)
        {
            var subreddit = ConfigurationManager.AppSettings["DefaultSubreddit"];

            var reddit = GetReddit();
            var sub = await reddit.GetSubredditAsync(subreddit);

            title = title
                .Replace("RE:", "").Replace("FW:", "")
                .Replace("Re:", "").Replace("Fw:", "")
                .Replace("re:", "").Replace("fw:", "")
                .Trim();
            var existingPost = sub.Posts.FirstOrDefault(x => x.Title == title);

            if (existingPost != null)
            {
                existingPost.Comment(body);
            }
            else
            {
                sub.SubmitTextPost(title, body);
            }
        }

        private Reddit GetReddit()
        {
            var username = ConfigurationManager.AppSettings["RedditUsername"];
            var password = ConfigurationManager.AppSettings["RedditPassword"];
            return new Reddit(username, password, true);
        }
    }
}
