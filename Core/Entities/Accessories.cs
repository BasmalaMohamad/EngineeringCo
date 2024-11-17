using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Accessories
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PumpName { get; set; }
        public string ImageURL { get; set; }
        public string Model { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public string Construction { get; set; }
        public float Size { get; set; } 
        
        public bool IsDeleted { get; set; }
    }
}
