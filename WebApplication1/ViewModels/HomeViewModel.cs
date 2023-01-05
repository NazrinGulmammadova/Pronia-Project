using Core.Entities;

namespace WebApplication1.ViewModels;

public class HomeViewModel
{
    public IEnumerable<SlideItem> SlideItems { get; set; } = null!;
    public IEnumerable<ShippingItem> ShippingItems { get; set; }=null!;
}
