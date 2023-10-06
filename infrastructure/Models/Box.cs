using System.ComponentModel.DataAnnotations;

namespace infrastructure.Models;

public class Box
{
    public int Id { get; set; }
    
    [Required, MinLength(4), MaxLength(12)]
    public string BoxName { get; set; }
    
    [Required, Range(5,27)]
    public double Price { get; set; }
    
    [Required, Range(15, 125)]
    public double BoxWidth { get; set; }
    
    [Required, Range(15, 220)]
    public double BoxLength { get; set; }
    
    [Required, Range(15, 100)]
    public double BoxHeight { get; set; }
    
    [Required, Range(1, 5)]
    public double BoxThickness { get; set; }
    
    public string BoxColor { get; set; }

    public string BoxImgUrl { get; set; }
    
}