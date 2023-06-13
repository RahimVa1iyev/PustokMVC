namespace PustokMVC.ModelView
{
    public class BasketVM
    {
        public List<BasketItemVM> BasketItemVMs { get; set; }

        public decimal TotalAmount { get; set; }
    }
}
