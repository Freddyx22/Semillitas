using Semillitas.Api.Entities;
using Semillitas.Api.Repositories;
using Semillitas.Core.Validators;

namespace Semillitas.Api.Validators;

public class DeleteNinoValidator
{
    private readonly Repository<Inscripcion> _repository;
    public DeleteNinoValidator(Repository<Inscripcion> repository) => _repository = repository;

    public ValidationResult IsValid(int idNino)
    {
        ValidationResult result = new();
        if (_repository.Query().Any(x => x.NinoId == idNino))
            result.Errors.Add("No se puede eliminar al niño porque tiene inscripciones.");
        return result;
    }
}
