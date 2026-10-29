using Semillitas.Api.Entities;
using Semillitas.Api.Repositories;
using Semillitas.Core.Validators;

namespace Semillitas.Api.Validators;

public class DeleteCategoriaValidator
{
    private readonly Repository<Taller> _repository;
    public DeleteCategoriaValidator(Repository<Taller> repository) => _repository = repository;

    public ValidationResult IsValid(int idCategoria)
    {
        ValidationResult result = new();
        if (_repository.Query().Any(x => x.CategoriaId == idCategoria))
            result.Errors.Add("No se puede eliminar la categoría porque contiene talleres.");
        return result;
    }
}
