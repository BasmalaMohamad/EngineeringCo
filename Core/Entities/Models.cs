using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Models
    {
        [Key]
        public int ModelID { get; set; }
        public string Name { get; set; }
        public virtual List<Product> Products { get; set; } = new List<Product>();
    }
}
