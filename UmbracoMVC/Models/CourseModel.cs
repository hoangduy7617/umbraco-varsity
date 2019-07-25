using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UmbracoMVC.Models
{
    public class CourseModel
    {        
        public string Key { get; set; }
        public string Name { get; set; }
        public HtmlString Description { get; set; }
        public string ShortDescription { get; set; }
        public string Url { get; set; }
        public string Image { get; set; }
        public int Duration { get; set; }
        public int Price { get; set; }
        public string[] CourseCategory { get; set; }
        public IEnumerable<CourseModel> CourseRelateds { get; set; }

    }
    
}