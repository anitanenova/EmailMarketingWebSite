namespace BusinessManagement.Services.Model.DTO
{
    using System;
    using System.Collections.Generic;

    public class BlogPostDto
    {
        public BlogPostDto()
        {
            if (BlogTags == null)
            {
                BlogTags = new List<BlogTagDto>();
            }
        }

      public Guid Id { get; set; }
        public string Title { get; set; }
        public string CoverImage { get; set; }
        public string VideoUrl { get; set; }
        public string ShortText { get; set; }
        public string FullText { get; set; }
        public DateTime Date { get; set; }
        public int Product { get; set; }
        public int Status { get; set; }
        public virtual List<BlogTagDto> BlogTags { get; set; }
       
    }
}