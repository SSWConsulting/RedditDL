using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedditDL.Models
{
    public class AppSettings
    {
        public string RedditUsername { get; set; }
        public string RedditPassword { get; set; }
        public string DefaultSubreddit { get; set; }
    }
}
