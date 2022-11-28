using System;
using System.Collections.Generic;

namespace WorkTime.Models;

public partial class PaymentState
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Invoice> Invoices { get; } = new List<Invoice>();
}
