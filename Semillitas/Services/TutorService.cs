using Microsoft.EntityFrameworkCore;
using Semillitas.Api.Entities;
using Semillitas.Api.Mappers;
using Semillitas.Api.Repositories;
using Semillitas.Core.DTOs;

namespace Semillitas.Api.Services;

public class TutorService
{
    private readonly Repository<Tutor> _tutores;

    public TutorService(Repository<Tutor> tutores)
    {
        _tutores = tutores;
    }

    public List<TutorDTO> GetAll() => _tutores.Query()
        .OrderBy(x => x.Nombre).Select(x => x.ToDTO()).ToList();

    public TutorDTO? GetById(int id) => _tutores.Query()
        .Where(x => x.Id == id).Select(x => x.ToDTO()).FirstOrDefault();

    public TutorDTO? Create(CreateTutorDTO dto) => _tutores.Add(dto.ToEntity()).ToDTO();

    public bool Delete(int id)
    {
        var tutor = _tutores.GetById(id);
        if (tutor is null) return false;
        _tutores.Delete(tutor);
        return true;
    }
}
