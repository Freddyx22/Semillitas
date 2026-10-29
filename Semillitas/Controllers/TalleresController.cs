using Microsoft.AspNetCore.Mvc;
using Semillitas.Api.Services;
using Semillitas.Api.Validators;
using Semillitas.Core.DTOs;
using Semillitas.Core.Validators;

namespace Semillitas.Api.Controllers;

[ApiController]
[Route("api/talleres")]
public class TalleresController : ControllerBase
{
    public TallerService Service { get; }
    public TallerCreateValidator CreateValidator { get; }
    public TallerEditValidator EditValidator { get; }
    public DeleteTallerValidator DeleteValidator { get; }

    public TalleresController(TallerService service, TallerCreateValidator createValidator, TallerEditValidator editValidator, DeleteTallerValidator deleteValidator)
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

    [HttpGet("categoria/{categoriaId:int}")]
    public IActionResult GetByCategoria(int categoriaId)
    {
        var data = Service.GetByCategoria(categoriaId);
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
    public IActionResult Post(CreateTallerDTO dto)
    {
        var result = CreateValidator.IsValid(dto);
        if (result.IsValid)
        {
            var data = Service.Create(dto);
            if (data == null) return BadRequest("La categoría indicada no existe.");
            return Ok(data);
        }
        return BadRequest(result.Errors);
    }

    [HttpPut("{id:int}")]
    public IActionResult Put(int id, EditTallerDTO dto)
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
