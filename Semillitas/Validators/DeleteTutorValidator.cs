using Semillitas.Api.Entities;
using Semillitas.Api.Repositories;
using Semillitas.Core.Validators;

namespace Semillitas.Api.Validators;

public class DeleteTutorValidator
{
    private readonly Repository<Nino> _repository;
    public DeleteTutorValidator(Repository<Nino> repository) => _repository = repository;

    public ValidationResult IsValid(int idTutor)
    {
        ValidationResult result = new();
        if (_repository.Query().Any(x => x.TutorId == idTutor))
            result.Errors.Add("No se puede eliminar el tutor porque tiene niños registrados.");
        return result;
    }
}
