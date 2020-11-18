using System;

namespace BulkyBook.DataAccess.Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Category { get; }
        IStoredProcedure_Call StoredProcedure_Call { get; }

    }
}
