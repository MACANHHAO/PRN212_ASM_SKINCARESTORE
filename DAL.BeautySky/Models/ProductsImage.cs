using System;
using System.Collections.Generic;

namespace DAL.BeautySky.Models;

public partial class ProductsImage
{
    public int ProductsImageId { get; set; }

    public string? ImageDescription { get; set; }

    public string ImageUrl { get; set; } = null!;

    public int? ProductId { get; set; }

    public virtual Product? Product { get; set; }
}
