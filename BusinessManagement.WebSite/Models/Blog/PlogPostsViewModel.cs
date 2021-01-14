namespace BusinessManagement.WebSite.Models.Blog
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BusinessManagement.Services.Model.DTO;
    public class PlogPostsViewModel
    {        
        public List<BlogPostDto> Posts { get; set; }
        public string DomainUrl { get; set; }
    }
}
