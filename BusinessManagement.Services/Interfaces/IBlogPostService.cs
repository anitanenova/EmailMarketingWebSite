namespace BusinessManagement.Services.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Enum;
    using HydraWebUi.Services.Model;
    using Model;
    using Model.DTO;

    public interface IBlogPostService
    {
        Task<PageResult<BlogPostDto>> GetSimilarPosts(Guid postId, int take);

        Task<PageResult<BlogPostDto>> AllAsync(int skip, int take,
            BlogPostDateVisibilityEnum dateVisibility = BlogPostDateVisibilityEnum.all, int status = -1,
            List<Guid> tagsIds = null, string searchString = null);

        Task<ItemResult<BlogPostDto>> AddAsync(BlogPostDto blogPost);
        Task<NoDataResult> EditAsync(BlogPostDto blogPost, Guid id);
        NoDataResult Delete(Guid id);
        Task<ItemResult<BlogPostDto>> GetAsync(Guid id);
        Task<PageResult<BlogPostDto>> GetByTagsIds(List<Guid> ids);
    }
}