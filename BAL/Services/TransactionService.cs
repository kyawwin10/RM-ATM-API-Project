using BAL.IServices;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MODEL.ApplicationConfig;
using MODEL.DTO;
using MODEL.Entitied;
using REPOSITORY.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services
{
    public class TransactionService: ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TransactionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseModel> WithDraw(WithDrawDTO inputModel)
        {
            if (inputModel == null || inputModel.Amount == null || inputModel.Amount <= 0)
            {
                return new ResponseModel { Message = "Invalid input data", Status = APIStatus.Error };
            }

            try
            {
                // Fetch user
                var user = (await _unitOfWork.user.GetByCondition(x => x.UserID == inputModel.UserID && x.ActiveFlag)).FirstOrDefault();
                if (user == null)
                {
                    return new ResponseModel { Message = "User not found", Status = APIStatus.Error };
                }

                // Check balance
                if (user.Balance < inputModel.Amount)
                {
                    return new ResponseModel { Message = "Insufficient balance", Status = APIStatus.Error };
                }

                // Reduce balance
                user.Balance -= inputModel.Amount;
                _unitOfWork.user.Update(user);

                // Record transaction
                var transaction = new Transaction
                {
                    UserID = inputModel.UserID,
                    TransactionType = inputModel.TransactionType ?? "Withdrawal",
                    Amount = inputModel.Amount.Value,
                    TransactionDate = DateTime.UtcNow
                };
                await _unitOfWork.transaction.Add(transaction);

                // Save changes
                await _unitOfWork.SaveChangeAsync();

                return new ResponseModel { Message = "Withdrawal processed successfully", Status = APIStatus.Success };
            }
            catch (Exception ex)
            {
                return new ResponseModel { Message = "An error occurred during withdrawal", Status = APIStatus.Error };
            }
        }



        public async Task<bool> Deposit(DepositDTO inputModel)
        {
            try
            {
                var user = (await _unitOfWork.user.GetByCondition(x => x.UserID == inputModel.UserID && x.ActiveFlag)).FirstOrDefault();
                if (user is null)
                {
                    return false; // User not found
                }

                user.Balance += inputModel.Amount;
                _unitOfWork.user.Update(user);

                // Create a transaction record
                var transaction = new Transaction
                {
                    UserID = inputModel.UserID,
                    TransactionType = inputModel.TransactionType,
                    Amount = inputModel.Amount,
                    TransactionDate = DateTime.UtcNow
                };
                await _unitOfWork.transaction.Add(transaction);

                await _unitOfWork.SaveChangeAsync();
                return true; // Operation successful
            }
            catch (Exception ex)
            {
                return false;
            }
        }


    }
}
