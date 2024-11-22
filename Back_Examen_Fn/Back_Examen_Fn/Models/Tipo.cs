using System;
using System.Collections.Generic;

namespace Back_Examen_Fn.Models;

public partial class Tipo
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Ingreso> Ingresos { get; set; } = new List<Ingreso>();
}
