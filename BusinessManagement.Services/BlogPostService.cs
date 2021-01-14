namespace BusinessManagement.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;
    using ApiClient;

    using HydraWebUi.Services.Model;
    using Interfaces;
    using Model;
    using Model.DTO;
    using Model.StatusCodes;
    using Newtonsoft.Json;
    using System.Linq;
    using Enum;

    public class BlogPostService : BaseApiClient, IBlogPostService
    {
        private readonly string _blogUrl;
        public BlogPostService(string baseUrl) : base(baseUrl)
        {
            _blogUrl = "odata/BlogPost";
        }

        public async Task<PageResult<BlogPostDto>> GetSimilarPosts(Guid postId, int take)
        {
            var postResponce = await GetAsync(postId);
            var post = postResponce.Result;
            var tags = post.BlogTags;

            var tagsId = (from t in tags
                          select t.Id).ToList();

            //foreach (var blogTagDto in tags)
            //{
            //    tagsId.Add(blogTagDto.Id);
            //}

            var similarPostsResponce = await GetByTagsIds(tagsId);
            var similarPosts = similarPostsResponce.Result.Where(p => p.Id != postId).ToList();

            var countOfSimilarPosts = similarPosts.Count;

            var result = new PageResult<BlogPostDto>()
            {
                IsFailed = false,
                Massage = string.Empty,
                StatusCode = 200
            };

            if (countOfSimilarPosts <= take)
            {
                result.Result = similarPosts;
            }
            else
            {
                //var randomNum = DateTime.Now.Second;
                //bool isRndNumEven = randomNum % 2 == 0;
                Random rnd = new Random();

                var diff = countOfSimilarPosts - take;
                var skip = rnd.Next(0, diff);
                result.Result = similarPosts.Skip(skip).Take(take).ToList();
            }

            return result;
        }


        private string GetUrlTagsIds(List<Guid> ids)
        {
            string url = string.Empty;

            int i = 0;
            foreach (var tagId in ids)
            {

                url += $"BlogTags/any(t:t/id eq {tagId})";
                if (i + 1 != ids.Count)
                {
                    url += " or ";
                }

                i++;
            }
            return url;
        }

        public async Task<PageResult<BlogPostDto>> GetByTagsIds(List<Guid> ids)
        {
            string url = $"{_blogUrl}?$select=*&$Expand=BlogTags";

            url += $"&$filter=Status eq 0 and {GetUrlTagsIds(ids)}&$orderby=Date desc";
            var response = await GetResponseAsync(url);

            var result = new PageResult<BlogPostDto>
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
            result.Result = JsonConvert.DeserializeObject<OData<BlogPostDto>>(jsonResult).Value;
            return result;
        }

        public async Task<ItemResult<BlogPostDto>> GetAsync(Guid id)
        {
            string url = $"{_blogUrl}?$select=*&$filter=Id eq {id}&$Expand=BlogTags";
            var response = await GetResponseAsync(url);

            var result = new ItemResult<BlogPostDto>
            {
                StatusCode = (int)response.StatusCode
            };

            if (!response.IsSuccessStatusCode)
            {
                result.IsFailed = true;
                result.Massage = "Възникна грешка моля опитайте отново";
                return result;
            }
            var jsonResult = await response.Content.ReadAsStringAsync();
            result.Result = JsonConvert.DeserializeObject<OData<BlogPostDto>>(jsonResult).Value.FirstOrDefault();
            return result;
        }


        public async Task<PageResult<BlogPostDto>> AllAsync(int skip, int take, BlogPostDateVisibilityEnum dateVisibility = BlogPostDateVisibilityEnum.all, int status = -1, List<Guid> tagsIds = null, string searchString = null)
        {

            if ((int)dateVisibility < 0 || (int)dateVisibility > 2)
            {
                throw new IndexOutOfRangeException("the vidibility date is invalid");
            }

            string url = $"{_blogUrl}?$select=*&$Skip=" + skip + "&$Top=" + take;
            bool passFierstFilter = false;

            if (status != -1 || searchString != null || tagsIds != null || (int)dateVisibility > 0)
            {
                url += "&$filter=";
            }

            if (status != -1)
            {
                if (passFierstFilter)
                {
                    url += " and ";
                }
                else
                {
                    passFierstFilter = true;
                }

                url += $"Status eq {status}";
            }

            if (searchString != null)
            {
                if (passFierstFilter)
                {
                    url += " and ";
                }
                else
                {
                    passFierstFilter = true;
                }

                url += $"contains(Title, '{searchString}')";
            }

            if (tagsIds != null)
            {
                if (passFierstFilter)
                {
                    url += " and ";
                }
                else
                {
                    passFierstFilter = true;
                }

                url += $"({GetUrlTagsIds(tagsIds)})";
            }

            var nowDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ");
            if (dateVisibility == BlogPostDateVisibilityEnum.visibleToToday)
            {
                if (passFierstFilter)
                {
                    url += " and ";
                }
                else
                {
                    passFierstFilter = true;
                }

                url += $"Date le {nowDate}";
            }
            else if (dateVisibility == BlogPostDateVisibilityEnum.visibleAfterToday)
            {
                if (passFierstFilter)
                {
                    url += " and ";
                }
                else
                {
                    passFierstFilter = true;
                }

                url += $"Date gt {nowDate}";
            }

            url += "&$orderby=Date desc";

            var response = await GetResponseAsync(url);

            var result = new PageResult<BlogPostDto>
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
            result.Result = JsonConvert.DeserializeObject<OData<BlogPostDto>>(jsonResult).Value;
            return result;
        }

        public async Task<ItemResult<BlogPostDto>> AddAsync(BlogPostDto blogPost)
        {
            var url = $"{_blogUrl}";
            var postStr = JsonConvert.SerializeObject(blogPost, new JsonSerializerSettings()
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = DateTimeZoneHandling.Local
            });


            var response = await PostResponseAsync(url, postStr);

            var result = new ItemResult<BlogPostDto>
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
                result.Massage = "Поста беше добавен успешно";
                var jsonResult = await response.Content.ReadAsStringAsync();
                result.Result = JsonConvert.DeserializeObject<BlogPostDto>(jsonResult);
            }

            return result;
        }

        public async Task<NoDataResult> EditAsync(BlogPostDto blogPost, Guid id)
        {
            var url = $"{_blogUrl}({id})";
            var postStr = JsonConvert.SerializeObject(blogPost, new JsonSerializerSettings()
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = DateTimeZoneHandling.Local
            });
            var response = await PutResponseAsync(url, postStr);
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
                result.Massage = "Поста беше редактиран успешно";
            }

            return result;
        }

        public NoDataResult Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}