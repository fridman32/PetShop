using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjectMVC.Models
{
    public class Comment
    {
        public int CommentID { get; set; }
        public int AnimalID { get; set; }
        public virtual Animal Animal { get; set; }
        public string CommentText { get; set; }
    }
}
