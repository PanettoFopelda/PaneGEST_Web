using System;
using System.Collections.Generic;

namespace PaneGEST_Blazor_MySQL.Models;

public partial class Movimento
{
    public int IdMovimento { get; set; }

    public int IdLoja { get; set; }

    public DateOnly DataMovimento { get; set; }

    public double ValorOn { get; set; }

    public double ValorOff { get; set; }

    public string? Observacoes { get; set; }

    public virtual Loja IdLojaNavigation { get; set; } = null!;
}
