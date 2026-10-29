using Microsoft.AspNetCore.Mvc;
using Semillitas.Api.Services;
using Semillitas.Core.DTOs;

namespace Semillitas.Api.Controllers;

[ApiController]
[Route("api/reportes")]
public class ReportesController : ControllerBase
{
    public ReporteService Service { get; }

    public ReportesController(ReporteService service)
    {
        Service = service;
    }

    [HttpGet("inscritos-por-taller/{periodoId:int}")]
    public IActionResult GetInscritosPorTaller(int periodoId)
    {
        var data = Service.GetInscritosPorTaller(periodoId);
        return Ok(data);
    }

    [HttpGet("ingresos/{periodoId:int}")]
    public IActionResult GetIngresoMensual(int periodoId)
    {
        var data = Service.GetIngresoMensual(periodoId);
        if (data == null) return NotFound();
        return Ok(data);
    }

    [HttpGet("ranking-talleres")]
    public IActionResult GetRankingTalleres([FromQuery] int? periodoId)
    {
        var data = Service.GetRankingTalleres(periodoId);
        return Ok(data);
    }
}
