
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
using System.IO;
using System.Data.Entity;
using System.Collections.ObjectModel;
using PM_Dashboard.App_Start;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Solution.Presentation.Controllers
{
    public class PostController : Controller
    {
        // GET: Post

        private readonly IPostService _postService = new PostService();
        private readonly IForumService _forumService = new ForumService();
        //private readonly IPostReplyService _postReplyService = new PostReplyService();
        private readonly IUserService us = new UserService();
        private readonly object EntryState;
        private  MyContext _context=new MyContext();
        private string  userId;
        private readonly ApplicationUserManager _userManager;
        public PostController()
        {
            _userManager = new ApplicationUserManager(new UserStore<User>(_context));


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
        public ActionResult Index(int id)
        {
            var post = _postService.GetById(id);
            string CurrentUserId = null;
            bool isCurrentUserEmailConfirmed = false;
            var user = new User();
            if (User.Identity.IsAuthenticated)
            {
                user = us.GetById(User.Identity.GetUserId());
                 CurrentUserId = user.Id;
                 isCurrentUserEmailConfirmed = user.EmailConfirmed;
            }
                var replies = BuildPostReplies(post.Replies);
                var model = new PostIndexModel
                {
                    Id = post.Id,
                    Title = post.Title,
                    AuthorId = post.UserId,
                    AuthorName = post.User.FirstName,
                    AuthorImageUrl = post.User.image,
                    Created = post.Created,
                    PostContennt = post.Content,
                    Replies = replies,
                    ImageUrl = post.ImageUrl,
                    CurrentUserId = CurrentUserId,
                    isCurrentUserEmailConfirmed = isCurrentUserEmailConfirmed
                };
                theUserInfo();
            
            return View(model);
            
        }

        public  ActionResult Delete(int id)
        {
            //var post =  _postService.GetById(id);
            //var forum = _forumService.GetById(post.ForumId);

            Post p = new Post { Id = id };

            _context.Posts.Attach(p);
            _context.Posts.Remove(p);
            _context.SaveChanges();

            return RedirectToAction("Index", "Forum");
         }

        [HttpPost]
        public ActionResult DeleteComment(PostReply Reply)
        {
            //var post =  _postService.GetById(id);
            //var forum = _forumService.GetById(post.ForumId);


            _context.PostReplies.Attach(Reply);
            _context.PostReplies.Remove(Reply);
            _context.SaveChanges();
            return Json(new { Message = "Success", JsonRequestBehavior.AllowGet });

        }
        [HttpPost]
        public ActionResult Update(NewPostModel model)
        {


            Post secondPost = new Post
            {
                Id = model.PostId,
                Title = model.Title,
                Content = model.Content,
                ForumId = model.ForumId,
                Created = model.Created,
                UserId = User.Identity.GetUserId(),
                ImageUrl=model.CurrentImageUrl
            };

            if (model.ImageFile != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
                string extention = Path.GetExtension(model.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssff") + extention;
                model.ImagePath = "~/Image/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
                model.ImageFile.SaveAs(fileName);
                secondPost.ImageUrl = model.ImagePath;
            }

            _context.Posts.Attach(secondPost);
            _context.Entry(secondPost).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Index", "Post", new { id = secondPost.Id });
        }

        [HttpGet]
        public ActionResult Update(int id)
        { 
            var post = _postService.GetById(id);
            var model = new NewPostModel
            {
               Content=post.Content,
               Title=post.Title,
               ImagePath=post.ImageUrl,
               PostId=post.Id,
               ForumId=post.ForumId,
               Created=post.Created,
               CurrentImageUrl=post.ImageUrl
            };
            theUserInfo();
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
            string fileName = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
            string extention = Path.GetExtension(model.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssff") + extention;
            model.ImagePath = "~/Image/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
            model.ImageFile.SaveAs(fileName);

            return new Post
            {
                Title = model.Title,
                Content = model.Content,
                Created = DateTime.Now,
                UserId=userid,
                ForumId=model.ForumId,
                ImageUrl=model.ImagePath
            };
        }

        [HttpGet]
        public ActionResult Create(int id)
        {
            var forum = _forumService.GetById(id);
            var userId = User.Identity.GetUserId();
            var user = us.GetById(userId);
            
            if (userId == null) return RedirectToAction("Login", "Account");
            if (!user.EmailConfirmed) return RedirectToAction("Topic","Forum",new { id = id });
            var model = new NewPostModel
            {
                ForumName = forum.Title,
                ForumId = forum.Id,
                ForumImageUrl = forum.ImageUrl,
                AuthorName = us.GetById(User.Identity.GetUserId()).FirstName

            };
            theUserInfo();
            return View(model);
        } 


        [HttpPost]
        public ActionResult AddReply(PostReplyModel Reply)
        {

            var newPostReply = new PostReply
            {
                Created = DateTime.Now,
                PostId = Convert.ToInt32(Reply.PostId),
                Content = Reply.ReplyContent,
                UserId = User.Identity.GetUserId()


            };
            _context.PostReplies.Add(newPostReply);
            _context.SaveChanges();
            //_postReplyService.Add(newPostReply);
            //_postReplyService.Commit();

            return Json(Reply, JsonRequestBehavior.AllowGet);


        }

        [HttpPost]
        public ActionResult getReplies(PostIndexModel Post)
        {

            try
            {
                //set ProxyCreation to false
                _context.Configuration.ProxyCreationEnabled = false;

                IEnumerable<PostReply> replies = _context.PostReplies.Where(s => s.PostId == Post.Id)
                .Include(reply => reply.User).ToList();
                


                IEnumerable<PostReplyModel> theReplies=  replies.Select(reply => new PostReplyModel
                    {
                        Id = reply.Id,
                        AuthorName = reply.User.UserName,
                        AuthorId = reply.UserId,
                        AuthorImageUrl = reply.User.image,
                        Created = reply.Created,
                        ReplyContent = reply.Content

                    });
                

                return Json(theReplies, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
            finally
            {
                //restore ProxyCreation to its original state
                _context.Configuration.ProxyCreationEnabled = true;
            }
        }

        [HttpPost]

        public ActionResult UpdateReply(PostReply Reply)
        {
            Reply.Created = DateTime.Now;
            _context.PostReplies.Attach(Reply);
            _context.Entry(Reply).State = EntityState.Modified;
            _context.SaveChanges();

            return Json(new { Message = "Success", JsonRequestBehavior.AllowGet });
        }
        private IEnumerable<PostReplyModel> BuildPostReplies(ICollection<PostReply> replies)
        {

            return replies.Select(reply => new PostReplyModel
            {
                Id = reply.Id,
                AuthorName = reply.User.FirstName,
                AuthorId = reply.UserId,
                AuthorImageUrl = reply.User.image,
                Created = reply.Created,
                ReplyContent = reply.Content

            });
    
        }





    }
}