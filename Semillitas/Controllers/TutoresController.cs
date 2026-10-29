using Microsoft.AspNetCore.Mvc;
using Semillitas.Api.Services;
using Semillitas.Api.Validators;
using Semillitas.Core.DTOs;
using Semillitas.Core.Validators;

namespace Semillitas.Api.Controllers;

[ApiController]
[Route("api/tutores")]
public class TutoresController : ControllerBase
{
    public TutorService Service { get; }
    public TutorCreateValidator CreateValidator { get; }
    public DeleteTutorValidator DeleteValidator { get; }

    public TutoresController(TutorService service, TutorCreateValidator createValidator, DeleteTutorValidator deleteValidator)
    {
        Service = service;
        CreateValidator = createValidator;
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
    public IActionResult Post(CreateTutorDTO dto)
    {
        var result = CreateValidator.IsValid(dto);
        if (result.IsValid)
        {
            var data = Service.Create(dto);
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
