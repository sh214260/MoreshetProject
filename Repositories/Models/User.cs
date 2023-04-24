using System;
using System.Collections.Generic;

namespace Repositories.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Mail { get; set; }

    public string? Password { get; set; }

    public string? Adress { get; set; }

    public string? PhoneNumber { get; set; }
    

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
