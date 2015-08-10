using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.AspNet.Authentication.Twitter.Messages;
using Microsoft.AspNet.Mvc;
using RedditDL.Data.Entities;
using RedditDL.Services;
using RedditSharp;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace RedditDL.Controllers
{
    public class MailgunController : Controller
    {
        private readonly IRedditService _redditService;
        private readonly TelemetryClient _telemetryClient;

        public MailgunController(IRedditService redditService, TelemetryClient telemetryClient)
        {
            _redditService = redditService;
            _telemetryClient = telemetryClient;
        }

        [HttpPost]
        public async Task<IActionResult> Webhook()
        {
            try
            {
                // This is done manually thanks to MVC6's weird model binding that won't pick up dashes in names.
                var form = await Request.ReadFormAsync();

                // Mailgun sends two subject fields, we only want the first one
                var subject = form.GetValues("subject").FirstOrDefault();
                var text = form["stripped-text"];

                await _redditService.PostOrComment(subject, text);
            }
            catch (Exception ex)
            {
                _telemetryClient.TrackException(ex);

                throw new HttpRequestException("Could not parse the webhook.", ex);
            }

            return new HttpStatusCodeResult((int)HttpStatusCode.OK);
        }
    }
}
