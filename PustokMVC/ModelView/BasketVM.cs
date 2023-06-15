namespace PustokMVC.ModelView
{
    public class BasketVM
    {
        public List<BasketItemVM> BasketItemVMs { get; set; } =new List<BasketItemVM>();

        public decimal TotalAmount { get; set; }
    }
}
