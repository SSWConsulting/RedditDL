using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedditDL.Services
{
    public interface IRedditService
    {
        Task PostOrComment(string title, string body);
    }
}
