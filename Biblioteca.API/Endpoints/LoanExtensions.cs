using BibliotecaShared.Models.Models;
using Data;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.API.Endpoints;

public static class LoanExtensions
{
    public static void AddEndpointsLoan(this WebApplication app)
    {
        app.MapGet("/Loan", () =>
        {
            var dal = new DAL<Loan>(new BibliotecaContext());
            return Results.Ok(dal.List());
        }).WithTags("Loans");

        app.MapGet("/Loan/{id}", (int id) =>
        {
            var dal = new DAL<Loan>(new BibliotecaContext());
            var loan = dal.GetBy(l => l.LoanId == id);
            if (loan is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(loan);
        }).WithTags("Loans");

        app.MapDelete("/Loan/{id}", ([FromServices] DAL<Loan> dal, int id) =>
        {
            var loan = dal.GetBy(l => l.LoanId == id);
            if (loan is null)
            {
                return Results.NotFound();
            }
            dal.Delete(loan);
            return Results.NoContent();
        }).WithTags("Loans");

        app.MapPost("/Loan", ([FromBody] Loan newLoan) =>
        {
            var dal = new DAL<Loan>(new BibliotecaContext());
            dal.Add(newLoan);
            return Results.Created($"/Loan/{newLoan.LoanId}", newLoan); 
        }).WithTags("Loans");

        app.MapPut("/Loan/{id}", ([FromServices] DAL<Loan> dal, int id, [FromBody] Loan updatedLoan) =>
        {
            var existingLoan = dal.GetBy(l => l.LoanId == id);
            if (existingLoan is null)
            {
                return Results.NotFound();
            }

            existingLoan.FriendId = updatedLoan.FriendId != 0 ? updatedLoan.FriendId : existingLoan.FriendId;
            existingLoan.Books = updatedLoan.Books ?? existingLoan.Books;
            existingLoan.LoanDate = updatedLoan.LoanDate != default ? updatedLoan.LoanDate : existingLoan.LoanDate;

            dal.Update(existingLoan);
            return Results.Ok(existingLoan);
        }).WithTags("Loans");
    }
}
