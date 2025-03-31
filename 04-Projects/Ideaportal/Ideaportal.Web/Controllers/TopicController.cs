using Application.Utils;
using Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ideaportal.Web.Controllers
{
    [Route("/")]
    [Route("topics/")]
    [Authorize]
    public class TopicController : Controller
    {
        
        public IActionResult Index()
        {
            var topics = TopicUtil.GetTopics();     
            var viewModel = new TopicViewModel { Topics = topics };
            return View(viewModel);
        }


        [HttpGet("{topicId}")]
        public IActionResult Ideas(int topicId)
        {
            var ideas = new IdeaViewModel
            {
                Topic = TopicUtil.GetTopics()[topicId - 1],
                Ideas = TopicUtil.GetIdeas(topicId),
            };
            return View(ideas);
        }

        [HttpPost]
        public IActionResult Logout()
        {
            // Logout logic here
            return RedirectToAction("Index", "Home");
        }
    }

}
