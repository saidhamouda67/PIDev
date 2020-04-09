
using Solution.Domain.Entities;
using Solution.Presentation.Models.Post;
using Solution.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using Solution.Data;

namespace Solution.Presentation.Controllers
{
    public class PostController : Controller
    {
        // GET: Post

        private readonly IPostService _postService;
        private readonly IForumService _forumService;
        private readonly IUserService us;

        private  MyContext _context=new MyContext();
        private  MyContext _ocontenxt=new MyContext();

        public PostController()
        {   
            _postService = new PostService();
            _forumService = new ForumService();
            us = new UserService();
        }
        public ActionResult Index(int id)
        {
            var post = _postService.GetById(id);
            var replies = BuildPostReplies(post.Replies);
            var model = new PostIndexModel
            {
                Id=post.Id,
                Title=post.Title,
                AuthorId=post.UserId,
                AuthorName=post.User.FirstName,
                AuthorImageUrl=post.User.image,
                Created=post.Created,
                AuthorRating=post.User.Rating,
                PostContennt=post.Content,
                Replies=replies
            };
            return View(model);
            
        }


        [HttpPost]
        public ActionResult Create(NewPostModel model)
        {
            var userId = User.Identity.GetUserId();
            Console.WriteLine(model.ForumId);
       
            var post = BuildPost(model, userId);

            _postService.Add(post);
            _postService.Commit();

            //TODO implement user rating management
            return RedirectToAction("Index", "Post", new { id = post.Id });
        }

        private Post BuildPost(NewPostModel model,string  userid)
        {
            return new Post
            {
                Title = model.Title,
                Content = model.Content,
                Created = DateTime.Now,
                UserId=userid,
                ForumId=model.ForumId
            };
        }

        [HttpGet]
        public ActionResult Create(int id)
        {
            var forum = _forumService.GetById(id);
            var userId = User.Identity.GetUserId();
            if (userId == null) return RedirectToAction("Login", "Account");

            var model = new NewPostModel
            {
                ForumName = forum.Title,
                ForumId = forum.Id,
                ForumImageUrl = forum.ImageUrl,
                AuthorName = us.GetById(User.Identity.GetUserId()).FirstName
            };

            return View(model);
        } 

        private IEnumerable<PostReplyModel> BuildPostReplies(ICollection<PostReply> replies)
        {

            return replies.Select(reply => new PostReplyModel
            {
                Id = reply.Id,
                AuthorName = reply.User.FirstName,
                AuthorId = reply.UserId,
                AuthorImageUrl = reply.User.image,
                AuthorRating = reply.User.Rating,
                Created = reply.Created,
                ReplyContent = reply.Content

            });
    
        }





    }
}