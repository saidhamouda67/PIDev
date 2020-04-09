using Solution.Domain.Entities;
using Solution.Presentation.Models.Forum;
using Solution.Presentation.Models.Post;
using Solution.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Solution.Presentation.Controllers
{
    public class ForumController : Controller
    {
        private readonly IForumService _forumService;
        private readonly IPostService _postService;
     

        public ForumController()
        {
            _forumService = new ForumService();
            _postService = new PostService();
        }
        // GET: Forum
        public ActionResult Index()
        {

            var  forums = _forumService.GetAll()
            .Select(forum=>new ForumListingModel {
            Id=forum.Id,
            Name=forum.Title,
            Description=forum.Descrition
            });
            var model = new ForumIndexModel
            {
                ForumList = forums
            };
       
            return View(model);


        }


        public ActionResult Topic(int id)
        {
            Forum forum = _forumService.GetById(id);
           
            var postListings = _postService.GetPostsByForum(forum).Select(post => new PostListingModel
            {
                Id = post.Id,
                AuthorId = post.UserId,
                AuthorRating = post.User.Rating,
                AuthorName=post.User.FirstName,
                Title = post.Title,
                DatePosted = post.Created.ToString(),
                RepliesCount = post.Replies.Count(),
                Forum = BuildForumListing(post)
            });

            var model = new ForumTopicModel
            {
                Posts = postListings,
                Forum = BuildForumListing(forum)
            };
            return View(model);
        }

        private ForumListingModel BuildForumListing(Post post)
        {
            var forum = post.Forum;
           return  BuildForumListing(forum);
        }


        private ForumListingModel BuildForumListing(Forum forum)
        {
             return new ForumListingModel
            {
                Id = forum.Id,
                Name = forum.Title,
                Description = forum.Descrition,
                ForumImageUrl = forum.ImageUrl
            };
        }
    }
}