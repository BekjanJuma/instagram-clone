using Instagram.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Instagram.Models
{
   [Table("Follows")]
   public class Follow
   {
      [Key]
      public int FollowId { get; set; }

      [Display(Name = @"Following", ResourceType = typeof(Strings))]
      public virtual ApplicationUser Following { get; set; }

      [Display(Name = @"Followed", ResourceType = typeof(Strings))]
      public virtual ApplicationUser Followed { get; set; }

      [Display(Name = @"Date", ResourceType = typeof(Strings))]
      public DateTime Date { get; set; }
   }
}