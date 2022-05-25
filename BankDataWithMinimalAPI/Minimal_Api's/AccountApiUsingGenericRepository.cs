using BankDataWithMinimalAPI.Data;
using BankDataWithMinimalAPI.Generic_Repository;
using BankDataWithMinimalAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BankDataWithMinimalAPI.Minimal_Api_s
{
    public static class AccountApiUsingGenericRepository
    {
        public static void MapAccountUsingGenericRepositoryRoutes(this IEndpointRouteBuilder app)
        {
            // Get All Accounts
            app.MapGet("api/accountdata", async (IGenericRepository<BankAccount> repository) =>
            {
                var data = await repository.GetAll();
                if(data != null)
                {
                    return Results.Ok(data);
                }
                return Results.NotFound();
            });

            //Get Account by Account Number
            app.MapGet("/accountdata/{id}", async (int id, IGenericRepository<BankAccount> repository) =>
                await repository.GetById(id)
                is BankAccount bankAccount
                ? Results.Ok(bankAccount)
                : Results.NotFound());

            //Add New Account
            app.MapPost("api/accountdata", async ([FromBody]BankAccount bankAccount, IGenericRepository<BankAccount> repository) =>
            {
                await repository.Insert(bankAccount);
                return Results.Created($"/accountdetails/{bankAccount.AccountNo}", bankAccount);
            });

            //Remove Account by Id
            app.MapDelete("api/accountdata/{id}", async (int id, IGenericRepository<BankAccount> repository) =>
            {
                if (await repository.GetById(id) is BankAccount bankAccount)
                {
                    await repository.Delete(bankAccount.AccountNo);
                    return Results.Ok(bankAccount);
                }

                return Results.NotFound();
            });

            // Update the Account Data
            app.MapPut("api/accountdata/Update", async (BankAccount bankAccount, IGenericRepository<BankAccount> repository) =>
            {
                var account = await repository.GetById(bankAccount.AccountNo);

                if (account is null) return Results.NotFound();

                account.AccountName = bankAccount.AccountName;
                account.AccountType = bankAccount.AccountType;
                account.Balance = bankAccount.Balance;
                await repository.Update(account);

                //var accountList = repository.GetAll();
                //return Results.Ok(accountList);
                return Results.NoContent();
            });
        }
    }
}
