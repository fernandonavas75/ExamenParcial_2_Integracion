using Back_Examen_Fn.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/ingresos")]
[ApiController]
public class IngresosController : ControllerBase
{
    private readonly Parcial02Context _context;

    public IngresosController(Parcial02Context context)
    {
        _context = context;
    }

    // Listar ingresos por estado
    [HttpGet]
    [Route("ListarPorEstado/{estado}")]
    public ActionResult<List<Ingreso>> ListarPorEstado(bool estado)
    {
        var estadoInt = estado ? 1 : 0;
        try
        {
            var ingresos = _context.Ingresos
                                   .Where(i => i.Estado == estado)
                                   .ToList();
            return Ok(ingresos);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error al listar los ingresos por estado: {ex.Message}");
        }
    }

    // Sumar ingresos (valores) en un rango de fechas
    [HttpGet]
    [Route("SumarPorRangoFechas")]
    public ActionResult<decimal> SumarPorRangoFechas([FromQuery] DateTime fechaInicio, [FromQuery] DateTime fechaFin)
    {
        try
        {
            var fechaInicioDateOnly = DateOnly.FromDateTime(fechaInicio);
            var fechaFinDateOnly = DateOnly.FromDateTime(fechaFin);

            var total = _context.Ingresos
                                .Where(i => i.Fecha >= fechaInicioDateOnly && i.Fecha <= fechaFinDateOnly)
                                .Sum(i => i.Valor);

            return Ok($"El total de ingresos entre {fechaInicio:yyyy-MM-dd} y {fechaFin:yyyy-MM-dd} es: ${total}");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error al sumar los ingresos: {ex.Message}");
        }
    }

    // Promedio de ingresos (valores) según el tipo
    [HttpGet]
    [Route("PromedioPorTipo/{idTipo}")]
    public ActionResult<decimal> PromedioPorTipo(int idTipo)
    {
        try
        {
            var promedio = _context.Ingresos
                                   .Where(i => i.IdTipo == idTipo)
                                   .Average(i => i.Valor);

            return Ok($"El promedio de los ingresos del tipo con ID {idTipo} es: ${promedio}");
        }
        catch (InvalidOperationException)
        {
            return NotFound($"No hay ingresos del tipo con ID {idTipo}.");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error al calcular el promedio: {ex.Message}");
        }
    }

    // CRUD ADICIONAL

    // Crear un nuevo ingreso
    [HttpPost]
    [Route("Crear")]
    public ActionResult Crear([FromBody] Ingreso nuevoIngreso)
    {
        try
        {
            if (ModelState.IsValid)
            {
                _context.Ingresos.Add(nuevoIngreso);
                _context.SaveChanges();
                return Ok("Ingreso creado con éxito.");
            }
            return BadRequest("Datos inválidos.");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error al crear el ingreso: {ex.Message}");
        }
    }

    // Leer todos los ingresos
    [HttpGet]
    [Route("Listar")]
    public ActionResult<List<Ingreso>> Listar()
    {
        try
        {
            var ingresos = _context.Ingresos.Include(i => i.IdTipoNavigation).ToList();
            return Ok(ingresos);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error al listar los ingresos: {ex.Message}");
        }
    }

    // Leer un ingreso por ID
    [HttpGet]
    [Route("Detalle/{id}")]
    public ActionResult<Ingreso> Detalle(int id)
    {
        try
        {
            var ingreso = _context.Ingresos.Include(i => i.IdTipoNavigation).FirstOrDefault(i => i.Id == id);

            if (ingreso == null)
            {
                return NotFound($"El ingreso con ID {id} no existe.");
            }

            return Ok(ingreso);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error al obtener el ingreso: {ex.Message}");
        }
    }

    // Actualizar un ingreso existente
    [HttpPut]
    [Route("Actualizar/{id}")]
    public ActionResult Actualizar(int id, [FromBody] Ingreso ingresoActualizado)
    {
        try
        {
            var ingreso = _context.Ingresos.Find(id);
            if (ingreso == null)
            {
                return NotFound($"El ingreso con ID {id} no existe.");
            }

            // Actualizar los datos del ingreso
            ingreso.Nombre = ingresoActualizado.Nombre;
            ingreso.Fecha = ingresoActualizado.Fecha;
            ingreso.Valor = ingresoActualizado.Valor;
            ingreso.Estado = ingresoActualizado.Estado;
            ingreso.IdTipo = ingresoActualizado.IdTipo;

            _context.SaveChanges();
            return Ok("Ingreso actualizado con éxito.");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error al actualizar el ingreso: {ex.Message}");
        }
    }

    // Eliminar un ingreso
    [HttpDelete]
    [Route("Eliminar/{id}")]
    public ActionResult Eliminar(int id)
    {
        try
        {
            var ingreso = _context.Ingresos.Find(id);
            if (ingreso == null)
            {
                return NotFound($"El ingreso con ID {id} no existe.");
            }

            _context.Ingresos.Remove(ingreso);
            _context.SaveChanges();
            return Ok("Ingreso eliminado con éxito.");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error al eliminar el ingreso: {ex.Message}");
        }
    }
}
