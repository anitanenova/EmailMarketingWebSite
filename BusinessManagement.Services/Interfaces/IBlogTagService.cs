namespace BusinessManagement.Services.Interfaces
{
    using System;
    using System.Threading.Tasks;

    using HydraWebUi.Services.Model;
    using Model;
    using Model.DTO;

    public interface IBlogTagService
    {
        Task<PageResult<BlogTagDto>> AllAsync(int skip, int take, string searchString = null);
        Task<NoDataResult> AddAsync(BlogTagDto tag);
        Task<NoDataResult> EditAsync(BlogTagDto blogTag, Guid id);
        NoDataResult Delete(Guid id);
    }
}