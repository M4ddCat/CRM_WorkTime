using System;
using System.Collections.Generic;

namespace WorkTime.Models;

public partial class Invoice
{
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string? ProjectId { get; set; }

    public int PaymentStateId { get; set; }

    public DateTime? Date { get; set; }

    public double HoursWorked { get; set; }

    public double HourlyWage { get; set; }

    public double SumByHours { get; set; }

    public double Bonus { get; set; }

    public double Total { get; set; }

    public double Issued { get; set; }

    public double Remainder { get; set; }

    public double Debt { get; set; }

    public double RemWdebtAndBonus { get; set; }

    public double SumWithTax { get; set; }

    public virtual PaymentState PaymentState { get; set; } = null!;

    public virtual Project? Project { get; set; }

    public virtual AspNetUser User { get; set; } = null!;

    public virtual ICollection<InvoiceFile> InvoiceFiles { get; } = new List<InvoiceFile>();

    public Invoice()
    {
        Id = (DateTime.Now.Ticks - new DateTime(2021, 4, 29).Ticks).ToString();
    }
}
