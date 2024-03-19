using System;
using System.Collections.Generic;

namespace Repositories.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int CategoryId { get; set; }

    public int Price { get; set; }

    public string? Description { get; set; }

    public string? Type { get; set; }

    public bool? Weight { get; set; }

    public string? Image { get; set; }

    public double? Length { get; set; }

    public double? Width { get; set; }

    public string? Comment { get; set; }
    public virtual ICollection<CartProduct> CartProducts { get; set; } = new List<CartProduct>();

    public virtual ICollection<ItemsForOrder> ItemsForOrders { get; set; } = new List<ItemsForOrder>();
}
