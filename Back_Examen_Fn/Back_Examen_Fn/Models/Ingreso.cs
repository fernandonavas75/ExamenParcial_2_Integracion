using System;
using System.Collections.Generic;

namespace Back_Examen_Fn.Models;

public partial class Ingreso
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public DateOnly Fecha { get; set; }

    public decimal Valor { get; set; }

    public bool Estado { get; set; }

    public int IdTipo { get; set; }

    public virtual Tipo IdTipoNavigation { get; set; } = null!;
}
