using BankDataWithMinimalAPI.Data;
using BankDataWithMinimalAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BankDataWithMinimalAPI.Minimal_Api_s
{
    public static class AccountApi
    {
        public static void MapAccountRoutes(this IEndpointRouteBuilder app)
        {
            // Get All Accounts
            app.MapGet("api/accountdetails", async (AppDbContext db) =>
            {
                var data = await db.BankAccount.AsNoTracking().ToListAsync();
                return Results.Ok(data);
            });

            // Get Account list whose balance is greater than 5000
            app.MapGet("api/accountdetails/balance", async (AppDbContext db) =>
            {
                var data = await db.BankAccount.Where(t => t.Balance > 5000).AsNoTracking().ToListAsync();
                return Results.Ok(data);
            });

            //Get Account list whose Account type is Saving
            app.MapGet("api/accountdetails/accounttype", async (AppDbContext db) =>
            {
                var data = await db.BankAccount.Where(t => t.AccountType.ToLower() == "saving").AsNoTracking().ToListAsync();
                return Results.Ok(data);
            });

            //Get Account by Account Number
            app.MapGet("/accountdetails/{id}", async (int id, AppDbContext db) =>
                await db.BankAccount.FindAsync(id)
                is BankAccount bankAccount
                ? Results.Ok(bankAccount)
                : Results.NotFound());

            //Add New Account
            app.MapPost("api/accountdetails", async (BankAccount bankAccount, AppDbContext db) =>
            {
                await db.BankAccount.AddAsync(bankAccount);
                await db.SaveChangesAsync();
                return Results.Created($"/accountdetails/{bankAccount.AccountNo}", bankAccount);
            });

            //Remove Account by Id
            app.MapDelete("api/accountdetails/{id}", async (int id, AppDbContext db) =>
            {
                if (await db.BankAccount.FindAsync(id) is BankAccount bankAccount)
                {
                    db.BankAccount.Remove(bankAccount);
                    await db.SaveChangesAsync();
                    return Results.Ok(bankAccount);
                }

                return Results.NotFound();
            });

            // Update the Account Data
            app.MapPut("api/accountdetails/{id}", async (int id, BankAccount bankAccount, AppDbContext db) =>
            {
                var account = await db.BankAccount.FindAsync(id);

                if (account is null) return Results.NotFound();

                account.AccountName = bankAccount.AccountName;
                account.AccountType = bankAccount.AccountType;
                account.Balance = bankAccount.Balance;

                await db.SaveChangesAsync();

                return Results.NoContent();
            });
        }

    }
}
