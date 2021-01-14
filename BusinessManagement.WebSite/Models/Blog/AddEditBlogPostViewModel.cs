namespace BusinessManagement.WebSite.Models.Blog
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using Services.Model.DTO;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using BusinessManagement.Resources.LanguageResources;

    public class AddEditBlogPostViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "CovPic", ResourceType = typeof(BlogResource))]
        public IFormFile CoverImage { get; set; }

        public string ImageStr { get; set; }

        public bool IsCoverImageChoised { get; set; }

        //[Required(ErrorMessage = "Полето Видео на корицата YouTube Линк е задължително")]
        [Display(Name = "CovVidLink", ResourceType = typeof(BlogResource))]
        public string VideoUrl { get; set; }

        public bool IsVideoUrlChoised { get; set; }

        [Required(ErrorMessageResourceType = typeof(BlogResource), ErrorMessageResourceName = "HeadlineReq")]      
        [Display(Name = "Headline", ResourceType = typeof(BlogResource))]
        public string Title { get; set; }

        [Required(ErrorMessageResourceType = typeof(BlogResource), ErrorMessageResourceName = "ShortTxtReq")]
        [Display(Name = "ShortTxt", ResourceType = typeof(BlogResource))]
        public string ShortText { get; set; }

        [Required(ErrorMessageResourceType = typeof(BlogResource), ErrorMessageResourceName = "FullTxtReq")]
        [Display(Name = "FullTxt", ResourceType = typeof(BlogResource))]
        public string FullText { get; set; }

        [Display(Name = "Tags", ResourceType = typeof(BlogResource))]
        public List<BlogTagDto> Tags { get; set; }

        [Required(ErrorMessageResourceType = typeof(BlogResource), ErrorMessageResourceName = "TagSelectReq")]
        public string TagsIds { get; set; }

        [Required(ErrorMessageResourceType = typeof(BlogResource), ErrorMessageResourceName = "ProductReq")]
        [Display(Name = "Product", ResourceType = typeof(BlogResource))]
        public string Product { get; set; }

        public List<SelectListItem> Products { get; set; }

        [Required(ErrorMessageResourceType = typeof(BlogResource), ErrorMessageResourceName = "PubDateReq")]
        [Display(Name = "PubDate", ResourceType = typeof(BlogResource))]
        public DateTime? Date { get; set; }

        [Required(ErrorMessageResourceType = typeof(BlogResource), ErrorMessageResourceName = "StatusReq")]
        [Display(Name = "Status", ResourceType = typeof(BlogResource))]
        public string Status { get; set; }
        public List<SelectListItem> Statuses { get; set; }
    }
}
