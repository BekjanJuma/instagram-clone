using Instagram.Resources;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Instagram.Models
{
   [Table("Likes")]
   public class Like
   {
      [Key]
      public int LikeId { get; set; }
      [Display(Name = @"User", ResourceType = typeof(Strings))]
      public virtual ApplicationUser User { get; set; }
      [Display(Name = @"Post", ResourceType = typeof(Strings))]
      public virtual Post Post { get; set; }
      [Display(Name = @"Date", ResourceType = typeof(Strings))]
      public DateTime Date { get; set; }
   }
}