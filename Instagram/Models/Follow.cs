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

        public virtual ApplicationUser Following { get; set; }

        public virtual ApplicationUser Followed { get; set; }

        public DateTime Date { get; set; }
    }
}