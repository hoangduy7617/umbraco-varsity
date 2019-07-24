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
    public class CourseApiController : UmbracoApiController
    {
        //[Route("api/course/list")]
        [System.Web.Http.HttpGet]
        public object List(int page = 1, int pageSize = 2, string category = "")
        {
            List<CourseModel> courseModels = new List<CourseModel>();
            dynamic selection;
            if (!string.IsNullOrEmpty(category))
            {
                selection = Umbraco.TagQuery.GetContentByTag(category);
            }
            else
            {
                selection = Umbraco.Content(Guid.Parse("a4d9628e-6ee3-46ac-aebf-550d8b6c0687"))
            .ChildrenOfType("course")
            .Where(x => x.IsVisible());
            }


            var allCategory = Umbraco.TagQuery.GetAllTags("CourseCategory");
            foreach (var item in selection)
            {
                CourseModel _courseModel = new CourseModel();
                _courseModel.Key = item.Key.ToString();
                _courseModel.Name = item.Name;
                _courseModel.Price = item.Price;
                _courseModel.CourseCategory = item.CourseCategory;
                _courseModel.Description = item.Description;
                _courseModel.ShortDescription = item.ShortDescription;
                _courseModel.Url = item.Url;
                _courseModel.Duration = item.Duration;
                _courseModel.Image = item.Image.ToString();
                courseModels.Add(_courseModel);
            }
            int totalItem = courseModels.Count;
            int totalPage = totalItem % pageSize == 0 ? totalItem / pageSize : totalItem / pageSize + 1;
            PagingModel rt = new PagingModel();
            rt.Page = page;
            rt.PageSize = pageSize;
            rt.TotalPage = totalPage;
            rt.Data = courseModels.Skip((page-1) * pageSize).Take(pageSize).ToList();
            //var objTemp = new { page = page, pageSize = pageSize, totalPage = totalPage, data = courseModels.Skip(page * pageSize).Take(pageSize) };
            //var body = RenderViewToString("Course", "~/Views/Course/List.cshtml", new { page = page});
            return rt;
        }
    }
}
