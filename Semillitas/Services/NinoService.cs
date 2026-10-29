using Microsoft.EntityFrameworkCore;
using Semillitas.Api.Entities;
using Semillitas.Api.Mappers;
using Semillitas.Api.Repositories;
using Semillitas.Core.DTOs;

namespace Semillitas.Api.Services;

public class NinoService
{
    private readonly Repository<Nino> _ninos;
    private readonly Repository<Tutor> _tutores;

    public NinoService(Repository<Nino> ninos, Repository<Tutor> tutores)
    {
        _ninos = ninos;
        _tutores = tutores;
    }

    public List<NinoDTO> GetByTutor(int tutorId) => _ninos.Query().Include(x => x.Tutor)
        .Where(x => x.TutorId == tutorId).OrderBy(x => x.Nombre)
        .ToList().Select(x => x.ToDTO()).ToList();

    public NinoDTO? GetById(int id) => _ninos.Query().Include(x => x.Tutor)
        .Where(x => x.Id == id).ToList().Select(x => x.ToDTO()).FirstOrDefault();

    public NinoDTO? Create(CreateNinoDTO dto)
    {
        if (_tutores.GetById(dto.TutorId) is null) return null;
        var nino = _ninos.Add(dto.ToEntity());
        return GetById(nino.Id);
    }

    public bool Delete(int id)
    {
        var nino = _ninos.GetById(id);
        if (nino is null) return false;
        _ninos.Delete(nino);
        return true;
    }
}
