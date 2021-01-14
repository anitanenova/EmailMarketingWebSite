namespace BusinessManagement.Services
{
    using System;
    using System.Net;
    using System.Threading.Tasks;

    using ApiClient;
    using HydraWebUi.Services.Model;
    using Interfaces;
    using Model;
    using Model.DTO;
    using Model.StatusCodes;
    using Newtonsoft.Json;

    public class BlogTagService: BaseApiClient, IBlogTagService
    {
        private readonly string _blogUrl;
        public BlogTagService(string baseUrl) : base(baseUrl)
        {
            _blogUrl = "odata/blogtag";
        }

        public async Task<PageResult<BlogTagDto>> AllAsync(int skip, int take, string searchString = null)
        {
            string url = $"{_blogUrl}?$select=*&$Skip=" + skip + "&$Top=" + take;
            if (searchString != null)
            {
                url += $"&$filter=contains(Name, '{searchString}')";
            }
            var response = await GetResponseAsync(url);

            var result = new PageResult<BlogTagDto>
            {
                StatusCode = (int)response.StatusCode
            };


            if (!response.IsSuccessStatusCode)
            {
                result.IsFailed = true;
                switch (response.StatusCode)
                {
                    case HttpStatusCode.Conflict:
                        {
                            result.Massage = "Вече съществува такъв пост";
                            break;
                        }
                    default:
                        {
                            result.Massage = "Възникна грешка моля опитайте отново";
                            break;
                        }
                }
                return result;
            }

            var jsonResult = await response.Content.ReadAsStringAsync();
            result.Result = JsonConvert.DeserializeObject<OData<BlogTagDto>>(jsonResult).Value;
            return result;
        }

        public async Task<NoDataResult> AddAsync(BlogTagDto tag)
        {
            var url = $"{_blogUrl}";
            var response = await PostResponseAsync(url, JsonConvert.SerializeObject(tag));

            var result = new NoDataResult
            {
                StatusCode = (int)response.StatusCode
            };

            if (!response.IsSuccessStatusCode)
            {
                result.IsFailed = true;
                switch ((int)response.StatusCode)
                {
                    case StatusCode.Status422UnprocessableEntity:
                        {
                            result.Massage = "Има некоректно попълнени данни! Моля попълнете всички полета коректно";
                            break;
                        }
                    default:
                        {
                            result.Massage = "Възникна грешка моля опитайте отново";
                            break;
                        }
                }
            }
            else
            {
                result.Massage = "Тага беше добавен успешно";
            }

            return result;
        }

        public async Task<NoDataResult> EditAsync(BlogTagDto blogTag, Guid id)
        {
            var url = $"{_blogUrl}({id})";
            var response = await PutResponseAsync(url, JsonConvert.SerializeObject(blogTag));

            var result = new NoDataResult
            {
                StatusCode = (int)response.StatusCode
            };
            if (!response.IsSuccessStatusCode)
            {
                result.IsFailed = true;
                switch ((int)response.StatusCode)
                {
                    case StatusCode.Status422UnprocessableEntity:
                        {
                            result.Massage = "Има некоректно попълнени данни! Моля попълнете всички полета коректно";
                            break;
                        }
                    default:
                        {
                            result.Massage = "Възникна грешка моля опитайте отново";
                            break;
                        }
                }
            }
            else
            {
                result.Massage = "Тага беше редактиран успешно";
            }

            return result;
        }

        public NoDataResult Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
