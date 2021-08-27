using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjectMVC.Models
{
    public class Animal
    {
        public int AnimalID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string PictureName { get; set; }
        public string Description { get; set; }
        public int CategortId { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Comment> CommentsCollection { get; set; }
    }
}
