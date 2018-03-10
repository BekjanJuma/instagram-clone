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
        public virtual ApplicationUser User { get; set; }
        public virtual Post Post { get; set; }
        public DateTime Date { get; set; }
    }
}