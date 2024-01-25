using PryanikyWebApp.Data.Abstract;

namespace PryanikyWebApp.Data
{
    public class Order : Entity
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
