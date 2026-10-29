using Microsoft.EntityFrameworkCore;
using Semillitas.Api.Data;

namespace Semillitas.Api.Repositories;

public class Repository<T> where T : class
{
    private  SemillitasContext _context;
    public SemillitasContext Context { get { return _context; } }

    //inyectar el context

    public Repository(SemillitasContext context)
    {
        _context = context;
    }

    public IQueryable<T> Query()
    {
        return _context.Set<T>().AsQueryable();
    }

    public List<T> GetAll()
    {
        return _context.Set<T>().ToList();
    }

    public T? GetById(object id)
    {
        return _context.Find<T>(id);
    }

    public T Add(T entity)
    {
        _context.Add(entity);
        _context.SaveChanges();
        return entity;
    }

    public void Update(T entity)
    {
        _context.Update(entity);
        _context.SaveChanges();
    }

    public void Delete(T entity)
    {
        _context.Remove(entity);
        _context.SaveChanges();
    }
       
}
