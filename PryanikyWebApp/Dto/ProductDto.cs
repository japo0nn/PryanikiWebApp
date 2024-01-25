using PryanikyWebApp.Dto.Abstract;

namespace PryanikyWebApp.Dto
{
    public class ProductDto : MainDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
    }
}
