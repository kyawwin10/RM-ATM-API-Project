using Microsoft.EntityFrameworkCore.Storage;
using REPOSITORY.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSITORY.UnitOfWork
{
    public interface IUnitOfWork: IDisposable
    {
        IProductRepository Products { get; }
        ISaleRepository Sales { get; set; }
        IUserRepository user { get; set; }
        ITransactionRepository transaction { get; set; }
        IFileRepository files { get; set; }

        Task<int> SaveChangeAsync();

        Task<IDbContextTransaction> BeginTransactionAsync();

        Task RollbackAsync();
    }
}
