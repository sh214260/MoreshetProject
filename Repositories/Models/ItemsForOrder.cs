using System;
using System.Collections.Generic;

namespace Repositories.Models;

public partial class ItemsForOrder
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int OrderId { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
