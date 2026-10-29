using Semillitas.Api.Entities;
using Semillitas.Api.Repositories;
using Semillitas.Core.DTOs;
using Semillitas.Core.Validators;

namespace Semillitas.Api.Validators;

public class NewPeriodoValidator : PeriodoCreateValidator
{
    private readonly Repository<Periodo> _repository;
    public NewPeriodoValidator(Repository<Periodo> repository) => _repository = repository;

    public override ValidationResult IsValid(CreatePeriodoDTO dto)
    {
        var result = base.IsValid(dto);
        if (!string.IsNullOrWhiteSpace(dto.Nombre) && _repository.Query().Any(x => x.Nombre.ToLower() == dto.Nombre.Trim().ToLower()))
            result.Errors.Add("Ya existe un período con ese nombre.");
        return result;
    }
}
