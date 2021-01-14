using BusinessManagement.Resources.LanguageResources;
using System.ComponentModel.DataAnnotations;

namespace BusinessManagement.Services.Enum
{
    public enum StatusEnum
    {
        [Display(Name = "Visible", ResourceType = typeof(BlogResource))]
        visible = 0,
        [Display(Name = "Drafts", ResourceType = typeof(BlogResource))]
        draft = 1,
        [Display(Name = "Inactive", ResourceType = typeof(BlogResource))]
        inactive = 2
    }
}
