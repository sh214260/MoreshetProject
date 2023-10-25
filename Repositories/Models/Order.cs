using System;
using System.Collections.Generic;

namespace Repositories.Models;

public partial class Order
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int DeliveryPrice { get; set; }

    public DateTime DateOrder { get; set; }

    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public bool PaidUp { get; set; }

    public bool Receipt { get; set; }

    public int TotalPrice { get; set; }

    public virtual ICollection<ItemsForOrder> ItemsForOrders { get; set; } = new List<ItemsForOrder>();

    public virtual User User { get; set; } = null!;
}
