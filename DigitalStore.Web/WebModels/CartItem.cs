using DigitalStore.Web.WebModels;

namespace DigitalStore.Web.WebModels
{
    public class CartItem
    {
        public CartProductDto Product { get; set; } = null!;
        public int Quantity { get; set; } = 1;
    }
}