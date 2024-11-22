using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Front_Examen_FN.Models
{
    public class Ingreso
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Fecha { get; set; }
        public decimal Valor { get; set; }
        public bool Estado { get; set; }
        public int IdTipo { get; set; }
    }
}