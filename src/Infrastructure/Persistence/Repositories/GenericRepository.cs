
using Application.Presistence.Contracts;
using Microsoft.EntityFrameworkCore;
using Persistence.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly ApplicationDbContext context;

    public GenericRepository(ApplicationDbContext context)
    {
        this.context = context;
    }
    public async Task AddAsync(T entity)
    {
        await context.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
       var old=await context .Set<T>().FindAsync(id);
        if (old != null)
        context.Set<T>().Remove(old);
        await context.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    => await context.Set<T>().AsNoTracking().ToListAsync();

    public async Task<T?> GetAsync(int id)
    => await context.Set<T>().FindAsync(id);

    public async Task UpdateAsync(T entity)
    {
       context.Entry(entity).State = EntityState.Modified;
       await context.SaveChangesAsync();
    }
}
