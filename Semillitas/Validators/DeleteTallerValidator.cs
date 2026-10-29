using Semillitas.Api.Entities;
using Semillitas.Api.Repositories;
using Semillitas.Core.Validators;

namespace Semillitas.Api.Validators;

public class DeleteTallerValidator
{
    private readonly Repository<InscripcionDetalle> _repository;
    public DeleteTallerValidator(Repository<InscripcionDetalle> repository) => _repository = repository;

    public ValidationResult IsValid(int idTaller)
    {
        ValidationResult result = new();
        if (_repository.Query().Any(x => x.TallerId == idTaller))
            result.Errors.Add("No se puede eliminar el taller porque tiene inscripciones. Puede desactivarlo editándolo.");
        return result;
    }
}
