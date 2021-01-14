namespace BusinessManagement.WebSite.Models.Blog
{
    using System.Collections.Generic;
    using BusinessManagement.Services.Enum;
    using BusinessManagement.Services.Model.DTO;
    
    public class BlogViewModel
    {
        public List<BlogPostDto> Posts { get; set; }
        public int Status { get; set; }
        public BlogPostDateVisibilityEnum DateVisibility { get; set; }
        public string DomainUrl { get; set; }

        public List<BlogTagDto> BlogPostTags { get; set; }
        public int Take { get; set; }
    }
}
