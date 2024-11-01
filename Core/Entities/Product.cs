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
        public string Model { get; set; }
        public string ImageURL { get; set; }
        public float FlowRateIPM { get; set; }
        public float FlowRateGPM { get; set; }
        public float InletSizeMM { get; set; }
        public float InletSizeInch { get; set; }
        public float OutletSizeMM { get; set; }
        public float OutletSizeInch { get; set; }
        public string Construction { get; set; }
        public string Type { get; set; }
        public Documentation Documentation { get; set; }
    }
}
