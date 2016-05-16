using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace ToshkentGullari.Services
{
   public class reCaptchaServices
    {
        public reCaptchaResponse GetCaptchaResponse()
        {
            var response = System.Web.HttpContext.Current.Request["g-recaptcha-response"];
            //secret that was generated in key value pair
            string secret = WebConfigurationManager.AppSettings["reCaptchaSecretKey"];

            var client = new WebClient();
            var reply =
                client.DownloadString(
                    string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));

            var captchaResponse = JsonConvert.DeserializeObject<reCaptchaResponse>(reply);
            return captchaResponse;
        }
    }
}
