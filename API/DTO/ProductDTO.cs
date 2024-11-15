using Core.Entities;

namespace API.DTO
{
    public class ProductDTO
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Model { get; set; }
        public string ImageURL { get; set; }
        public float InletSize { get; set; }
        public float OutletSize { get; set; }
        public string Construction { get; set; }
        public int DocumentId { get; set; }
        public DocumentDTO Documentation { get; set; }


    }
}
