namespace BusinessManagement.WebSite.Models.Blog
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using BusinessManagement.Resources.LanguageResources;

    public class AddEditNewTagViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(BlogResource), ErrorMessageResourceName = "TagReq")]
        [Display(Name = "TagName", ResourceType = typeof(BlogResource))]
        public string Name { get; set; }
    }
}