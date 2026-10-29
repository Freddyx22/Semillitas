using Semillitas.Api.Entities;
using Semillitas.Api.Repositories;
using Semillitas.Core.Validators;

namespace Semillitas.Api.Validators;

public class DeletePeriodoValidator
{
    private readonly Repository<Inscripcion> _repository;
    public DeletePeriodoValidator(Repository<Inscripcion> repository) => _repository = repository;

    public ValidationResult IsValid(int idPeriodo)
    {
        ValidationResult result = new();
        if (_repository.Query().Any(x => x.PeriodoId == idPeriodo))
            result.Errors.Add("No se puede eliminar el período porque tiene inscripciones.");
        return result;
    }
}
