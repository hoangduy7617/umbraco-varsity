using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UmbracoMVC.Models
{
    public class PagingModel
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPage { get; set; }
        public List<CourseModel> Data{ get; set; }
    }
}