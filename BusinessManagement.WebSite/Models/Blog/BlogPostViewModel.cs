using System;
namespace BusinessManagement.WebSite.Models.Blog
{
    using BusinessManagement.Services.Model.DTO;
    using System.Collections.Generic;

    public class BlogPostViewModel
    {
        public BlogPostDto Post { get; set; }
      
        public string DomainUrl { get; set; }

        public string RightSidePostsTitle { get; set; }

        public List<BlogPostDto> RightSidePosts { get; set; }
    }
}
