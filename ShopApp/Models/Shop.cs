using System.ComponentModel.DataAnnotations;

namespace ShopApp.Models;

public class Shop
{
    public int Id { get; set; }

    [Display(Name = "Name")]
    public string Name { get; set; } = "";

    [Display(Name = "Slug")]
    public string Slug { get; set; } = "";

    [Display(Name = "Description")]
    public string Description { get; set; } = "";

    [Display(Name = "Date Opened")]
    public DateTime? DateOpened { get; set; }
}
