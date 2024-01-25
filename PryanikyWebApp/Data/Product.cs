using PryanikyWebApp.Data.Abstract;

namespace PryanikyWebApp.Data
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
    }
}
