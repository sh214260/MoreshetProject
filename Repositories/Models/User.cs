using System;
using System.Collections.Generic;

namespace Repositories.Models;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Email { get; set; }

    public string Password { get; set; } = null!;

    public string? Address { get; set; }

    public string? PhoneNumber1 { get; set; }

    public string PhoneNumber2 { get; set; } = null!;

    public int Type { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
