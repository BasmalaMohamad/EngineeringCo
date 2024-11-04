using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Documentation
    {
        [Key]
        public int DocumentID { get; set; }
        [ForeignKey("Product")]
        public int ProductID { get; set; }
        public string FileURL { get; set; }
        public Product Product { get; set; } = null!;
    }
}
