using System;
using System.Collections.Generic;

namespace PaneGEST_Blazor_MySQL.Models;

public partial class Loja
{
    public int IdLoja { get; set; }

    public string NomeLoja { get; set; } = null!;

    public string? Observacoes { get; set; }

    public virtual ICollection<Movimento> Movimentos { get; set; } = new List<Movimento>();
}
