using testproje.Models;

namespace testproje.Data
{
    public static class AplicationContext
    {
        public static List<Brand> Brands { get; set; }
        static AplicationContext()
        {
            Brands = new List<Brand>()
            {
                new Brand(){ Title = "Apple"},
                new Brand(){ Title = "Leica"},
                new Brand(){ Title = "Sennheiser"},
                new Brand(){ Title = "Lexmark"},
                new Brand(){ Title = "Xaomi"},
                new Brand(){ Title = "Total"},
                new Brand(){ Title = "RayBan"},
                new Brand(){ Title = "Kuda"},
                new Brand(){ Title = "Sterling"}
            };
        }
    }
}