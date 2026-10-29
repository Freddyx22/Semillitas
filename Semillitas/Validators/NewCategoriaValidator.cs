using Semillitas.Api.Entities;
using Semillitas.Api.Repositories;
using Semillitas.Core.DTOs;
using Semillitas.Core.Validators;

namespace Semillitas.Api.Validators;

public class NewCategoriaValidator : CategoriaCreateValidator
{
    private readonly Repository<Categoria> _repository;
    public NewCategoriaValidator(Repository<Categoria> repository) => _repository = repository;

    public override ValidationResult IsValid(CreateCategoriaDTO dto)
    {
        var result = base.IsValid(dto);
        if (!string.IsNullOrWhiteSpace(dto.Nombre) && _repository.Query().Any(x => x.Nombre.ToLower() == dto.Nombre.Trim().ToLower()))
            result.Errors.Add("Ya existe una categoría con ese nombre.");
        return result;
    }
}
