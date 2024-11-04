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
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public Models Model { get; set; } = null!;
        public string ImageURL { get; set; }
        public float FlowRateIPM { get; set; }
        public float FlowRateGPM { get; set; }
       
        public float AirInletSize { get; set; }
        public float InletSize { get; set; }

        public float OutletSize { get; set; }
        public string Construction { get; set; }
        public Documentation Documentation { get; set; } = null!;
        public virtual List<Accessories> Accessories { get; set; } = new List<Accessories>();
        [ForeignKey("Model")]
        public int ModelId { get; set; }

    }
}
