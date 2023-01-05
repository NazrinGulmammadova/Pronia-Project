using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Areas.Admin.ViewModels.Slider;

public class SlideCreateVM
{
    [Required]
    public IFormFile Photo { get; set; }
    [MaxLength(50)]
    public string? Offer { get; set; }
    [Required, MaxLength(100)]
    public string? Title { get; set; }
    [Required, MaxLength(200)]
    public string? Description { get; set; }
}
