using System;
using System.Collections.Generic;

namespace Repositories.Models;

public partial class Product
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? CategoryId { get; set; }

    public int? Price { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual Category? Category { get; set; }

    public virtual ICollection<ItemsForOrder> ItemsForOrders { get; set; } = new List<ItemsForOrder>();
}
