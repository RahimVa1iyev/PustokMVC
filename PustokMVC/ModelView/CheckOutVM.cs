namespace PustokMVC.ModelView
{
    public class CheckOutVM
    {
        public List<ItemCheckOutVM> Items { get; set; } = new List<ItemCheckOutVM>();   

        public decimal TotalAmount { get; set; }

        public OrderCreateVM Orders { get; set; }
    }
}
