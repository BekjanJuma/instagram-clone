using Instagram.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Instagram.Models
{
   [Table("Posts")]
   public class Post
   {
      [Key]
      public int PostId { get; set; }

      [Display(Name = @"User", ResourceType = typeof(Strings))]
      public virtual ApplicationUser User { get; set; }

      public byte[] Image { get; set; }

      [MaxLength(150)]
      [Display(Name = @"Description", ResourceType = typeof(Strings))]
      public string Description { get; set; }

      [Display(Name = @"Date", ResourceType = typeof(Strings))]
      public DateTime Date { get; set; }

      [Display(Name = @"Comments", ResourceType = typeof(Strings))]
      public virtual List<Comment> Comments { get; set; }

      [Display(Name = @"Likes", ResourceType = typeof(Strings))]
      public virtual List<Like> Likes { get; set; }
   }
}