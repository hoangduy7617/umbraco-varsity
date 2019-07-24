using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Umbraco.Web;
using Umbraco.Web.WebApi;
using UmbracoMVC.Models;
using System.Web.Mvc;
using System.IO;
using System.Web.Routing;
using System.Web;
using Umbraco.Web.Mvc;
namespace UmbracoMVC.Api
{
    public class ContactApiController : UmbracoApiController
    {
        //[Route("api/course/list")]
        [System.Web.Http.HttpGet]
        public object getAll()
        {
            List<dynamic> rt = new List<dynamic>();
            dynamic selection  = Umbraco.Content(Guid.Parse("1e09ae43-77f6-47ac-936e-c5f616cc7544"))
            .ChildrenOfType("feedback")
            .Where(x => x.IsVisible());

            foreach (var item in selection)
            {
                rt.Add(new { name = item.Name, email = item.Email, subject = item.Subject, message = item.Message });
            }
           
            return rt;
        }
    }
}
