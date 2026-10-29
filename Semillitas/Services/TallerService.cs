using Microsoft.EntityFrameworkCore;
using Semillitas.Api.Entities;
using Semillitas.Api.Mappers;
using Semillitas.Api.Repositories;
using Semillitas.Core.DTOs;

namespace Semillitas.Api.Services;

public class TallerService
{
    private readonly Repository<Taller> _talleres;
    private readonly Repository<Categoria> _categorias;

    public TallerService(Repository<Taller> talleres, Repository<Categoria> categorias)
    {
        _talleres = talleres;
        _categorias = categorias;
    }

    public List<TallerDTO> GetAll() => _talleres.Query().Include(x => x.Categoria)
        .OrderBy(x => x.Nombre).ToList().Select(x => x.ToDTO()).ToList();

    public List<TallerDTO> GetByCategoria(int categoriaId) => _talleres.Query().Include(x => x.Categoria)
        .Where(x => x.CategoriaId == categoriaId).OrderBy(x => x.Nombre)
        .ToList().Select(x => x.ToDTO()).ToList();

    public TallerDTO? GetById(int id) => _talleres.Query().Include(x => x.Categoria)
        .Where(x => x.Id == id).ToList().Select(x => x.ToDTO()).FirstOrDefault();

    public TallerDTO? Create(CreateTallerDTO dto)
    {
        if (_categorias.GetById(dto.CategoriaId) is null) return null;
        var taller = _talleres.Add(dto.ToEntity());
        return GetById(taller.Id);
    }

    public TallerDTO? Update(EditTallerDTO dto)
    {
        var taller = _talleres.GetById(dto.Id);
        if (taller is null || _categorias.GetById(dto.CategoriaId) is null) return null;
        dto.ApplyTo(taller);
        _talleres.Update(taller);
        return GetById(taller.Id);
    }

    public bool Delete(int id)
    {
        var taller = _talleres.GetById(id);
        if (taller is null) return false;
        _talleres.Delete(taller);
        return true;
    }
}
