using Core.Entities;

namespace API.Helper
{
    public class AccessFilterOptions
    {
        public List<string> Name { get; set; } = null!;
        public List<string> Construction { get; set; } = null!;
        public List<string> Model { get; set; } = null!;
        public List<string> PumpName { get; set; } = null!;
        public List<string> Category { get; set; } = null!;
    }
}
