using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.Framework.OptionsModel;
using RedditDL.Models;
using RedditSharp;
using Serilog;

namespace RedditDL.Services
{
    public class RedditService : IRedditService
    {
        private readonly ILogger _logger = Log.ForContext(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IOptions<AppSettings> _appSettings;

        public RedditService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings;
        }

        public async Task PostOrComment(string title, string body)
        {            
            var reddit = GetReddit();
            var sub = await reddit.GetSubredditAsync(_appSettings.Options.DefaultSubreddit);

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
            var reddit = new Reddit(_appSettings.Options.RedditUsername, _appSettings.Options.RedditPassword, true);
            reddit.CaptchaSolver = new CaptchaIgnorer();

            return reddit;
        }
    }
}
