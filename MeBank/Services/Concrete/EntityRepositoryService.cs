using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MeBank.Models;
using MeBank.Models.Abstract;
using MeBank.Services.Abstract;
using SQLite;
using Xamarin.Forms;

namespace MeBank.Services.Concrete
{
    /// <summary>
    /// EntityRepositoryService is an abstract class that contains a base implementation for
    /// the methods needed for most of the entities repositories
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    public abstract class EntityRepositoryService<T> : IEntityService<T> where T : IEntity, new()
    {
        /// <summary>
        /// DataBase object connection to use
        /// </summary>
        private SQLiteAsyncConnection database;


        protected EntityRepositoryService()
        {
            // Assign the Application connection
            database = DependencyService.Get<DataBaseConnectionManager>().Connection;
            // Creates Data Base Table for Entity
            database.CreateTableAsync<T>().Wait();
        }

        /// <summary>
        /// Queries for all results
        /// </summary>
        /// <returns>Results list</returns>
        public Task<List<T>> FindAllAsync() => database.Table<T>().ToListAsync();

        /// <summary>
        /// Queries for all results according to the given filter function
        /// </summary>
        /// <param name="filter">Function to filter the expected results</param>
        /// <returns>Filtered results list</returns>
        public Task<List<T>> FindAllWhereAsync(Expression<Func<T, bool>> filter) => database.Table<T>().Where(filter).ToListAsync();

        /// <summary>
        /// Looks for an entity by its "Id" member
        /// </summary>
        /// <param name="id">Id member</param>
        /// <returns>If the entity was found it will return the entity itself otherwise null</returns>
        public Task<T> FindByIdAsync(int id) => database.Table<T>().Where(entity => entity.Id == id).FirstOrDefaultAsync();

        /// <summary>
        /// Saves an entity
        /// If an entity was already saved it will update it
        /// otherwise a new entity will be created in the DataBase
        /// </summary>
        /// <param name="entity">Entity instance to save or update</param>
        /// <returns>Amount of changes in DataBase made by the procedure</returns>
        public Task<int> SaveAsync(T entity)
        {
            if (entity.Id == 0)
            {
                return database.InsertAsync(entity);
            }
            return database.UpdateAsync(entity);
        }

        /// <summary>
        /// Deletes an entity from DataBase
        /// </summary>
        /// <param name="entity">Entity to remove</param>
        /// <returns>Amount of changes in DataBase made by the procedure</returns>
        public Task<int> DeleteAsync(T entity) => database.DeleteAsync(entity);
    }
}