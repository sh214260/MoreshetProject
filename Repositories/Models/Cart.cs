using System;
using System.Collections.Generic;

namespace Repositories.Models;

public partial class Cart
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public double TotalPrice { get; set; }

    public bool IsOpen { get; set; }

    public virtual ICollection<CartProduct> CartProducts { get; set; } = new List<CartProduct>();

    public virtual User User { get; set; } = null!;
}
