using Core.Entities;

namespace API.DTO
{
    public class AccessoriesDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PumpName { get; set; }
        public string ImageURL { get; set; } 
        public string Model { get; set; }
        public string Construction { get; set; }
        public Category Category { get; set; }
        public float Size { get; set; }
    }
}
