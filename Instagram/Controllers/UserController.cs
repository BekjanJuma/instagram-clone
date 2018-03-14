using Instagram.Filters;
using Instagram.Models;
using Instagram.Resources;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Instagram.Controllers
{
   [Authorize]
   [Localization]
   public class UserController : Controller
   {
      private readonly ApplicationContext db;

      public UserController(ApplicationContext context) {
         db = context;
      }


      public async Task<ActionResult> Index() {
         var me = await db.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
         List<string> follows = await db.Follows.Where(f => f.Following.UserName == User.Identity.Name)
            .Select(x => x.Followed.UserName).ToListAsync();

         // now for each following user get his posts
         // too many db calls - should be optimized later
         var posts = new List<Post>();
         foreach (var fol in follows) {
            var fposts = await db.Posts.Where(p => p.User.UserName == fol).ToListAsync();
            posts.AddRange(fposts);
         }
         posts.OrderByDescending(p => p.Date);
         return View(posts);
      }

      [HttpGet]
      public ActionResult NewPost() {
         return View();
      }

      [HttpPost]
      public async Task<ActionResult> NewPost(Post post) {

         byte[] imageData = null;
         if (Request.Files.Count > 0) {
            HttpPostedFileBase poImgFile = Request.Files["postImage"];
            if (poImgFile == null || poImgFile.ContentLength == 0) {
               ModelState.AddModelError("Image", Strings.Error_select_image);
               return View(post);
            }
            using (var binary = new BinaryReader(poImgFile.InputStream)) {
               imageData = binary.ReadBytes(poImgFile.ContentLength);
            }
         } else {
            ModelState.AddModelError("Image", Strings.Error_select_image);
            return View(post);
         }

         if (ModelState.IsValid) {
            post.User = await db.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
            post.Date = DateTime.Now;
            post.Image = imageData;
            db.Posts.Add(post);
            await db.SaveChangesAsync();
            return RedirectToAction("MyPosts");
         }
         return View(post);
      }

      [HttpGet]
      public async Task<ActionResult> MyPosts() {
         var me = await db.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
         return View(me.Posts.OrderByDescending(p => p.Date).ToList());
      }


      [HttpGet]
      public async Task<ActionResult> SearchUsers() {
         return View();
      }

      [HttpGet]
      public async Task<ActionResult> GetUsers() {
         // should be optimized later
         var me = await db.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
         var users = await db.Users
            .Select(u => new UserVM {
               UserName = u.UserName,
               Email = u.Email,
               FullName = u.FullName,
               AboutMe = u.AboutMe,
               Avatar = u.Avatar
            })
            .ToListAsync();
         return Json(new { success = true, data = users }, JsonRequestBehavior.AllowGet);
      }

      [HttpGet]
      public async Task<ActionResult> UserProfile(string id) {
         if (id == null) {
            var me = await db.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
            return View(me);
         }
         var user = await db.Users.FirstOrDefaultAsync(u => u.UserName == id);
         return View(user);
      }



      [HttpPost]
      public async Task<JsonResult> Follow(string username) {
         var user = await db.Users.FirstOrDefaultAsync(u => u.UserName == username);
         var me = await db.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
         if (user != null) {

            if (user.UserName == me.UserName) {
               return Json(new { success = false, msg = Strings.Error_cant_follow_myself });
            }

            var follow = new Follow() {
               Date = DateTime.Now,
               Followed = user,
               Following = me
            };

            db.Follows.Add(follow);
            user.numFollowers++;
            me.numFollowings++;
            db.Entry(user).State = EntityState.Modified;
            db.Entry(me).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return Json(new { success = true, msg = Strings.Added_to_follow });
         } else {
            return Json(new { success = false, msg = Strings.Error_cant_find_user });
         }
      }

      [HttpPost]
      public async Task<JsonResult> Toggle(int postId) {
         var post = await db.Posts.FirstOrDefaultAsync(p => p.PostId == postId);
         var me = await db.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
         var like = await db.Likes.FirstOrDefaultAsync(l => l.Post.PostId == postId
                                    && l.User.UserName == me.UserName);

         if (post != null) {
            // didn't like yet
            if (like == null) {
               var nlike = new Like {
                  Date = DateTime.Now,
                  User = me,
                  Post = post
               };
               db.Likes.Add(nlike);
            } else {
               // remove previous like
               db.Likes.Remove(like);
            }
            await db.SaveChangesAsync();
            return Json(new { success = true, msg = Strings.User_liked_post });
         } else {
            return Json(new { success = false, msg = Strings.Error_cant_find_post });
         }
      }

      [HttpPost]
      public async Task<JsonResult> Comment(int postId, string comment) {
         var me = await db.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
         var post = await db.Posts.FirstOrDefaultAsync(p => p.PostId == postId);

         if (post != null) {
            var ncomment = new Comment {
               Date = DateTime.Now,
               Content = comment,
               Post = post,
               User = me
            };

            db.Comments.Add(ncomment);
            await db.SaveChangesAsync();
            return Json(new { success = true, msg = Strings.Comment_added });
         } else {
            return Json(new { success = false, msg = Strings.Error_cant_find_post });
         }

      }

      class UserVM
      {
         public string UserName { get; set; }
         public string Email { get; set; }
         public string FullName { get; set; }
         public string AboutMe { get; set; }
         public byte[] Avatar { get; set; }
      }

   }
}