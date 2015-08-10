using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RedditSharp;
using Serilog;

namespace RedditDL.Services
{
    public class CaptchaIgnorer : ICaptchaSolver
    {
        private readonly ILogger _logger = Log.ForContext(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public CaptchaIgnorer()
        {
        }

        public CaptchaResponse HandleCaptcha(Captcha captcha)
        {
            _logger.Warning($"Captcha was raised with ID: {captcha.Id} found at {captcha.Url}", captcha);

            // Returning an empty response cancels the captcha
            return new CaptchaResponse();
        }
    }
}
