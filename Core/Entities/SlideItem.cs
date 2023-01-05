using System.ComponentModel.DataAnnotations;
using Core.Interfaces;

namespace Core.Entities;

public class SlideItem:IEntity
{
    public int Id { get; set; }
    public string? Photo { get; set; }
    [MaxLength(50)]
    public string? Offer { get; set; }
    [Required, MaxLength(100)]
    public string? Title { get; set; }
    [Required, MaxLength(200)]
    public string? Description { get; set; }
}
