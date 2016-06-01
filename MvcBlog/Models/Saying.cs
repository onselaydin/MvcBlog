using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcBlog.Models
{
    [Table("Saying")]
    public partial class Saying
    {


        [Key]
        public int SNo { get; set; }

        [Column(TypeName = "text")]
        public string Say_tr { get; set; }
        [Column(TypeName = "text")]
        public string Say_eng { get; set; }

    }
}