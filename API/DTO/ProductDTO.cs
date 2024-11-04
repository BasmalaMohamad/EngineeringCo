using Core.Entities;

namespace API.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public Models Model { get; set; }
        public string ImageURL { get; set; }
        public float FlowRateIPM { get; set; }
        public float FlowRateGPM { get; set; }
        public float AirInletSize { get; set; }
        public float InletSize { get; set; }
        public float OutletSize { get; set; }
        public string Construction { get; set; }
    }
}
