namespace Prods.Models.ViewModel
{
    public class DashBoardVM
    {
        public int ProductCount { get; set; }
        public int JournalCount { get; set; }
        public int TotalCount { get; set; }
        public int OrderCount { get; set; }
        public IEnumerable<Order> orders { get; set; }
        public IEnumerable<Product> products { get; set; }
    }
}
