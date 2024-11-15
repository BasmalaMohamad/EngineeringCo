using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Accessories> Accessories { get; set; } = new List<Accessories>();
        public bool IsDeleted { get; set; }
    }
}
