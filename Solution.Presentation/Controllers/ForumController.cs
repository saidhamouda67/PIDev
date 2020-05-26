using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PM_Dashboard.App_Start;
using Solution.Data;
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
        private readonly IUserService us;
        private readonly ApplicationUserManager _userManager;
        private MyContext _context = new MyContext();


        public ForumController()
        {
            _forumService = new ForumService();
            _postService = new PostService();
            _userManager = new ApplicationUserManager(new UserStore<User>(_context));

            us = new UserService();
        }

        private void theUserInfo()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (val1)
            {
                var user = us.GetById(User.Identity.GetUserId());

                if (_userManager.IsInRole(user.Id, "Admin") || _userManager.IsInRole(user.Id, "Manager"))
                {
                    ViewBag.Admin = true;
                }
                string Phone2 = user.Phone2;
                string mail = user.Email;
                ViewBag.home = mail;
                ViewBag.phone = Phone2;
                ViewBag.authenticated = val1;
                ViewBag.ImageUrlProfile = user.image;

            }
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
            theUserInfo();
            return View(model);


        }


         
        public ActionResult Topic(int id)
        {
            Forum forum = _forumService.GetById(id);
           
            var postListings = _postService.GetPostsByForum(forum).Select(post => new PostListingModel
            {
                Id = post.Id,
                AuthorId = post.UserId,
                AuthorName=post.User.FirstName,
                Title = post.Title,
                DatePosted = post.Created.ToString(),
                RepliesCount = post.Replies.Count(),
                Forum = BuildForumListing(post),
                ImageUrl=post.ImageUrl
            });

            var model = new ForumTopicModel
            {
                Posts = postListings,
                Forum = BuildForumListing(forum)
            };
            theUserInfo();

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