using Microsoft.AspNetCore.Mvc;
using Semillitas.Api.Services;
using Semillitas.Api.Validators;
using Semillitas.Core.DTOs;

namespace Semillitas.Api.Controllers;

[ApiController]
[Route("api/inscripciones")]
public class InscripcionesController : ControllerBase
{
    public InscripcionService Service { get; }
    public NewInscripcionValidator CreateValidator { get; }

    public InscripcionesController(InscripcionService service, NewInscripcionValidator createValidator)
    {
        Service = service;
        CreateValidator = createValidator;
    }

    [HttpGet("periodo/{periodoId:int}")]
    public IActionResult GetByPeriodo(int periodoId)
    {
        var data = Service.GetByPeriodo(periodoId);
        return Ok(data);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var data = Service.GetById(id);
        if (data == null) return NotFound();
        return Ok(data);
    }

    [HttpGet("vencidas")]
    public IActionResult GetVencidas()
    {
        var data = Service.GetVencidas();
        return Ok(data);
    }

    [HttpPost]
    public IActionResult Post(CreateInscripcionDTO dto)
    {
        var result = CreateValidator.IsValid(dto);
        if (result.IsValid)
        {
            var data = Service.Create(dto);
            if (data == null) return BadRequest("Alguno de los talleres indicados no existe.");
            return Ok(data);
        }
        return BadRequest(result.Errors);
    }

    [HttpPut("{id:int}/pago")]
    public IActionResult MarcarPago(int id, [FromQuery] bool pagada)
    {
        var data = Service.MarcarPago(id, pagada);
        if (data == null) return NotFound();
        return Ok(data);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var result = Service.Delete(id);
        if (result == false) return NotFound();
        return Ok();
    }
}
