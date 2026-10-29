using Semillitas.Api.Entities;
using Semillitas.Core.DTOs;

namespace Semillitas.Api.Mappers;

public static class CategoriaMapper
{
    public static CategoriaDTO ToDTO(this Categoria categoria)
    {
        var dto = new CategoriaDTO
        {
            Id = categoria.Id,
            Nombre = categoria.Nombre
        };
        return dto;
    }

    public static Categoria ToEntity(this CreateCategoriaDTO dto)
    {
        var categoria = new Categoria
        {
            Nombre = dto.Nombre.Trim()
        };
        return categoria;
    }

    public static void ApplyTo(this EditCategoriaDTO dto, Categoria categoria)
    {
        categoria.Nombre = dto.Nombre.Trim();
    }
}
