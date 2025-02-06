using Microsoft.Extensions.Options;
using MODEL.ApplicationConfig;
using MODEL;
using REPOSITORY.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using REPOSITORY.Repositories.Repositories;
using REPOSITORY.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore.Storage;
using System.Transactions;

namespace REPOSITORY.UnitOfWork
{
    public class UnitOfWork: IUnitOfWork
    {
        private DataContext _dataContext;
        private IDbContextTransaction _transaction;

        public UnitOfWork(DataContext dataContext, IOptions<AppSettings> appsettings)
        {
            _dataContext = dataContext;
            AppSettings = appsettings.Value;
            Products = new ProductRepository(dataContext);
            Sales = new SaleRepository(dataContext);
            user = new UserRepository(dataContext);
            transaction = new TransactionRepository(dataContext);
            files = new FileRepository(dataContext);
        }
        public IProductRepository Products { get; set; }
        public ISaleRepository Sales {  get; set; }
        public IUserRepository user {  get; set; }
        public ITransactionRepository transaction { get; set; }
        public IFileRepository files { get; set; }
        public AppSettings AppSettings { get; set; }

        public void Dispose()
        {
            _dataContext.Dispose();
        }

        public async Task<int> SaveChangeAsync()
        {
            return await _dataContext.SaveChangesAsync();
        }
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            _transaction = await _dataContext.Database.BeginTransactionAsync();
            return _transaction;
        }

        public async Task RollbackAsync()
        {
            await _transaction.RollbackAsync();
        }
    }
}