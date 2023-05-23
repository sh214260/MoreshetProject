using System;
using System.Collections.Generic;

namespace Repositories.Models;

public partial class Cart
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int? UserId { get; set; }

    public double TotalPrice { get; set; }

    public virtual Product? User { get; set; }

    public virtual User? UserNavigation { get; set; }
}
