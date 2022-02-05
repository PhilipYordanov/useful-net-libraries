using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiniProfilerDemo.Models;
using MiniProfilerDemo.Services;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MiniProfilerDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostService postService;

        public HomeController(ILogger<HomeController> logger, IPostService postService)
        {
            this.postService = postService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await postService.GetAll();
            var vm = new List<PostViewModel>();

            foreach (var item in result)
            {
                vm.Add(new PostViewModel
                {
                    PostId = item.PostId,
                    Content = item.Content,
                    Title = item.Title,
                });
            }

            return View(vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
