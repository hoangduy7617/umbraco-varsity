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
using Umbraco.Web.Routing;
using Umbraco.Core.Services.Implement;
using UmbracoMVC.Models;

namespace UmbracoMVC.Controllers
{
    public class CourseController : SurfaceController
    {
        // GET: Course
        public ActionResult List(int page = 1, int pageSize = 2, string category = "")
        {
            List<CourseModel> courseModels = new List<CourseModel>();
            dynamic selection;
            if (!string.IsNullOrEmpty(Request.QueryString["category"]))
            {
                selection = Umbraco.TagQuery.GetContentByTag(Request.QueryString["category"]);
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
            rt.Data = courseModels.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            //var objTemp = new { page = page, pageSize = pageSize, totalPage = totalPage, data = courseModels.Skip(page * pageSize).Take(pageSize) };
            return PartialView("List", rt);
        }
        public ActionResult Detail(string guid)
        {
            List<dynamic> arrRelated = new List<dynamic>();
            dynamic thisCourse = Umbraco.Content(Guid.Parse(guid));
            foreach (var item in thisCourse.CourseRelated)
            {
                var thisCourseRelated = Umbraco.Content(item.Udi.Guid);
                arrRelated.Add(thisCourseRelated);
            }
            return View("Detail", arrRelated);
        }

        public ActionResult CourseRelatedDetail(string guid)
        {
            var thisCourse = Umbraco.Content(Guid.Parse(guid));
            return View("CourseRelatedDetail", thisCourse);
        }

    }
}