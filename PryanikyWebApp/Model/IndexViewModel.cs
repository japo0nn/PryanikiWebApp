using PryanikyWebApp.Dto;

namespace PryanikyWebApp.Model
{
    public class IndexViewModel<T> where T : class
    {
        public IEnumerable<T> Items { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
