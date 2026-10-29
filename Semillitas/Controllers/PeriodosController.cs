using Microsoft.AspNetCore.Mvc;
using Semillitas.Api.Services;
using Semillitas.Api.Validators;
using Semillitas.Core.DTOs;

namespace Semillitas.Api.Controllers;

[ApiController]
[Route("api/periodos")]
public class PeriodosController : ControllerBase
{
    public PeriodoService Service { get; }
    public NewPeriodoValidator CreateValidator { get; }
    public EditPeriodoValidator EditValidator { get; }
    public DeletePeriodoValidator DeleteValidator { get; }

    public PeriodosController(PeriodoService service, NewPeriodoValidator createValidator, EditPeriodoValidator editValidator, DeletePeriodoValidator deleteValidator)
    {
        Service = service;
        CreateValidator = createValidator;
        EditValidator = editValidator;
        DeleteValidator = deleteValidator;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var data = Service.GetAll();
        return Ok(data);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var data = Service.GetById(id);
        if (data == null) return NotFound();
        return Ok(data);
    }

    [HttpPost]
    public IActionResult Post(CreatePeriodoDTO dto)
    {
        var result = CreateValidator.IsValid(dto);
        if (result.IsValid)
        {
            var data = Service.Create(dto);
            return Ok(data);
        }
        return BadRequest(result.Errors);
    }

    [HttpPut("{id:int}")]
    public IActionResult Put(int id, EditPeriodoDTO dto)
    {
        var result = EditValidator.IsValid(dto);
        if (result.IsValid)
        {
            var data = Service.Update(dto);
            if (data == null) return NotFound();
            return Ok(data);
        }
        return BadRequest(result.Errors);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var validation = DeleteValidator.IsValid(id);
        if (!validation.IsValid) return Conflict(validation.Errors);
        var result = Service.Delete(id);
        if (result == false) return NotFound();
        return Ok();
    }
}
