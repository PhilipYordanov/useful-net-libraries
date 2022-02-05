using Microsoft.AspNetCore.Mvc;
using MiniProfilerDemo.Data;
using MiniProfilerDemo.Models;
using MiniProfilerDemo.Services;
using StackExchange.Profiling;

namespace MiniProfilerDemo.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService postService;
        private readonly IHttpClientFactory factory;
        private readonly ITagService tagService;

        public PostController(IPostService postService, IHttpClientFactory factory, ITagService tagService)
        {
            this.postService = postService;
            this.factory = factory;
            this.tagService = tagService;
        }

        public async Task<IActionResult> Details(int postId)
        {
            // MiniProfiler.Current.Inline || MiniProfiler.Current.Step for important segmnets and wrap them with timer
            // Step --> can lead to scope problem, might need some refactoring.
            // TODO:[MiniProfiler][Step 3]: Uncomment this code in order to create time segment for current step
            // Post result = null;
            // using (MiniProfiler.Current.Step("SQL call"))
            // {
            //     result = await this.postService.GetById(postId);
            // }

            // TODO:[MiniProfiler][Step 4]: Create time segment that will show time spent for postService call
            var firstCall = await MiniProfiler.Current.Inline(() => this.postService.GetAll(), "postService call");
            var secondCall = await MiniProfiler.Current.Inline(() => this.tagService.GetAll(), "tagService call");

            Post result;
            using (MiniProfiler.Current.Step("get post by Id"))
            {
                result = await this.postService.GetById(postId);
            }

            List<Tag> tags = result.PostTags
                .Select(x => x.Tag)
                .ToList();

            var tagViewModels = new List<TagViewModel>();
            foreach (var tag in tags)
            {
                tagViewModels.Add(new TagViewModel
                {
                    Name = tag.Name
                });
            }

            // can measure calls of eternal API's and web services
            // Caching services (e.g. Redis); 
            // Application services ( e.g. ElasticSearch)
            await CalllingExternalApi();

            var vm = new PostDetailsViewModel()
            {

                Title = result.Title,
                Content = result.Content,
                Tags = tagViewModels
            };

            return View(vm);
        }

        private async Task CalllingExternalApi()
        {
            var url = "https://api.github.com/users/PhilipYordanov";
            var httpClient = factory.CreateClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.UserAgent.TryParseAdd("request");

            // TODO:[MiniProfiler][Step 5]: Custom timing provided by MiniProfiler, gives us a way to monitor external calls
            // category name/column name    (e.g. http)
            // command that is beign run    (e.g. url of the API)
            // execute type                 (e.g. HTTP verb)
            using (CustomTiming timing = MiniProfiler.Current.CustomTiming("http", string.Empty, "GET"))
            {
                var response = await httpClient.GetAsync(url);
                timing.CommandString = $"URL: {url}\n\n REPONSE CODE: {response.StatusCode}";
            }
        }
    }
}
