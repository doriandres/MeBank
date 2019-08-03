using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MeBank.Models.Abstract;

namespace MeBank.Services.Abstract
{
    public interface IEntityService<T> where T : IEntity
    {
        Task<List<T>> FindAllAsync();

        Task<List<T>> FindAllWhereAsync(Expression<Func<T, bool>> filter);

        Task<T> FindByIdAsync(int id);

        Task<int> SaveAsync(T item);

        Task<int> DeleteAsync(T item);
    }
}