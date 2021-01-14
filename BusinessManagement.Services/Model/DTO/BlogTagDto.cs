namespace BusinessManagement.Services.Model.DTO
{
    using System;
    using System.Collections.Generic;

    public class BlogTagDto
    {
        public BlogTagDto()
        {
            this.BlogPosts = new List<BlogPostDto>();
        }


        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<BlogPostDto> BlogPosts { get; set; }
    }
}
