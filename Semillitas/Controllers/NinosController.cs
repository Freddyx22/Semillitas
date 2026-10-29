using Microsoft.AspNetCore.Mvc;
using Semillitas.Api.Services;
using Semillitas.Api.Validators;
using Semillitas.Core.DTOs;
using Semillitas.Core.Validators;

namespace Semillitas.Api.Controllers;

[ApiController]
[Route("api/ninos")]
public class NinosController : ControllerBase
{
    public NinoService Service { get; }
    public NinoCreateValidator CreateValidator { get; }
    public DeleteNinoValidator DeleteValidator { get; }

    public NinosController(NinoService service, NinoCreateValidator createValidator, DeleteNinoValidator deleteValidator)
    {
        Service = service;
        CreateValidator = createValidator;
        DeleteValidator = deleteValidator;
    }

    [HttpGet("tutor/{tutorId:int}")]
    public IActionResult GetByTutor(int tutorId)
    {
        var data = Service.GetByTutor(tutorId);
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
    public IActionResult Post(CreateNinoDTO dto)
    {
        var result = CreateValidator.IsValid(dto);
        if (result.IsValid)
        {
            var data = Service.Create(dto);
            if (data == null) return BadRequest("El tutor indicado no existe.");
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
