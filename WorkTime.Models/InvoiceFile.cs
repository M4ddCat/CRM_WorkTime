using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTime.Models;
public partial class InvoiceFile
{
    public string Id { get; set; } = null!;
    public string InvoiceId { get; set; } = null!;
    public string Name { get; set; } = null!;
    public byte[]? File { get; set; } = null!;
    public virtual Invoice Invoice { get; set; } = null!;
}

