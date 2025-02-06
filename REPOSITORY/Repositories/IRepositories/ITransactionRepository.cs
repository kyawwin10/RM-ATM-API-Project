using MODEL.Entitied;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Transaction = MODEL.Entitied.Transaction;

namespace REPOSITORY.Repositories.IRepositories
{
    public interface ITransactionRepository: IGenericRepository<Transaction>
    {
    }
}
