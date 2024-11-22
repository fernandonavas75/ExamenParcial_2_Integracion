using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using Front_Examen_FN.Models;

namespace Front_Examen_FN.Controllers
{
    public class IngresosController : Controller
    {
        private readonly string apiUrl = "https://localhost:7037/api/ingresos"; // Cambia según la URL de tu API.

        // Listar ingresos por estado
        public async Task<ActionResult> ListarPorEstado(bool estado)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync($"{apiUrl}/ListarPorEstado/{estado}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var ingresos = JsonConvert.DeserializeObject<List<Ingreso>>(json);
                    return View(ingresos);
                }
                else
                {
                    ViewBag.Error = "No se pudieron cargar los ingresos.";
                    return View(new List<Ingreso>());
                }
            }
        }

        // Sumar ingresos por rango de fechas
        public async Task<ActionResult> SumarPorRangoFechas(string fechaInicio, string fechaFin)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync($"{apiUrl}/SumarPorRangoFechas?fechaInicio={fechaInicio}&fechaFin={fechaFin}");
                if (response.IsSuccessStatusCode)
                {
                    var total = await response.Content.ReadAsStringAsync();
                    ViewBag.Total = total;
                    return View();
                }
                else
                {
                    ViewBag.Error = "No se pudo calcular la suma.";
                    return View();
                }
            }
        }

        // Promedio de ingresos por tipo
        public async Task<ActionResult> PromedioPorTipo(int idTipo)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync($"{apiUrl}/PromedioPorTipo/{idTipo}");
                if (response.IsSuccessStatusCode)
                {
                    var promedio = await response.Content.ReadAsStringAsync();
                    ViewBag.Promedio = promedio;
                    return View();
                }
                else
                {
                    ViewBag.Error = "No se pudo calcular el promedio.";
                    return View();
                }
            }
        }
    }
}
