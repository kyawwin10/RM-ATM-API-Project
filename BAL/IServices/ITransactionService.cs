using MODEL.ApplicationConfig;
using MODEL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.IServices
{
    public interface ITransactionService
    {
        Task<ResponseModel> WithDraw(WithDrawDTO inputModel);

        Task<bool> Deposit(DepositDTO inputModel);
    }
}
