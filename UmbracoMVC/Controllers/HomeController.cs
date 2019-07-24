using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using UmbracoMVC;

using Umbraco.Web.Mvc;
using Umbraco.Web;


namespace UmbracoMVC.Controllers
{
    public class HomeController : SurfaceController
    {
        IContentService _contentService;
        public HomeController(IContentService contentService)
        {
            _contentService = contentService;
        }
        public ActionResult Index()
        {
            var X = Umbraco.ContentAtRoot().FirstOrDefault()
             .ChildrenOfType("course")
             .Where(x => x.IsVisible());

                    return View("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact(string name ="", string email = "", string subject = "", string message="")
        {
            bool feedback = false;
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(email))
            {
                var contentService = Services.ContentService;
                var parent = contentService.GetById(1099);
                var newDocument = contentService.CreateContent($"feedback-{DateTime.Now.ToString("ddMMyyyyhhmmss")}", parent.GetUdi(), "feedBack");

                //IContent newDocument = contentService.Create("FeedBack" + DateTime.Now.ToString("ddMMyyyyhhmmss"), 1096, null);
                newDocument.Name = name;
                newDocument.SetValue("subject", subject);
                newDocument.SetValue("email", email);
                newDocument.SetValue("message", message);
                contentService.SaveAndPublish(newDocument);
                feedback = true;
            }
            return PartialView("_Contact", feedback);
        }
    }
}