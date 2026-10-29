using Semillitas.Api.Entities;
using Semillitas.Api.Repositories;
using Semillitas.Core.DTOs;
using Semillitas.Core.Validators;

namespace Semillitas.Api.Validators;

public class EditCategoriaValidator : CategoriaEditValidator
{
    private readonly Repository<Categoria> _repository;
    public EditCategoriaValidator(Repository<Categoria> repository) => _repository = repository;

    public override ValidationResult IsValid(EditCategoriaDTO dto)
    {
        var result = base.IsValid(dto);
        if (dto.Id > 0 && _repository.GetById(dto.Id) is null)
            result.Errors.Add("La categoría que intenta editar no existe.");
        if (!string.IsNullOrWhiteSpace(dto.Nombre) && _repository.Query().Any(x => x.Id != dto.Id && x.Nombre.ToLower() == dto.Nombre.Trim().ToLower()))
            result.Errors.Add("Ya existe otra categoría con ese nombre.");
        return result;
    }
}
