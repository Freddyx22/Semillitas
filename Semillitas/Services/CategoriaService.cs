using Microsoft.EntityFrameworkCore;
using Semillitas.Api.Entities;
using Semillitas.Api.Mappers;
using Semillitas.Api.Repositories;
using Semillitas.Core.DTOs;

namespace Semillitas.Api.Services;

public class CategoriaService
{
    private readonly Repository<Categoria> _categorias;

    public CategoriaService(Repository<Categoria> categorias)
    {
        _categorias = categorias;
    }

    public List<CategoriaDTO> GetAll() => _categorias.Query()
        .OrderBy(x => x.Nombre).Select(x => x.ToDTO()).ToList();

    public CategoriaDTO? GetById(int id) => _categorias.Query()
        .Where(x => x.Id == id).Select(x => x.ToDTO()).FirstOrDefault();

    public CategoriaDTO? Create(CreateCategoriaDTO dto)
    {
        var data = _categorias.Add(dto.ToEntity());
        return data.ToDTO();
    }

    public CategoriaDTO? Update(EditCategoriaDTO dto)
    {
        var data = _categorias.GetById(dto.Id);
        if (data is null) return null;
        dto.ApplyTo(data);
        _categorias.Update(data);
        return data.ToDTO();
    }

    public bool Delete(int id)
    {
        var data = _categorias.GetById(id);
        if (data is null) return false;
        _categorias.Delete(data);
        return true;
    }
}
