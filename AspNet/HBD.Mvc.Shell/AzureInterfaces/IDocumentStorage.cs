#region using

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace AzureInterfaces
{
    public interface IDocumentStorage<TEntity, in TId> : IDisposable where TEntity : new()
    {
        Task<IList<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(TId id);
        Task<TEntity> CreateNewAsync();
        Task<bool> SaveAsync(TEntity note);
        Task<bool> DeleteAsync(TEntity note);
        Task<bool> DeleteByIdAsync(TId id);
        Task DeleteAllAsync();
    }
}