using PryanikyWebApp.Dto.Abstract;

namespace PryanikyWebApp.Dto
{
    public class OrderDto : MainDto
    {
        public Guid ProductId { get; set; }
        public ProductDto Product { get; set; }
    }
}
