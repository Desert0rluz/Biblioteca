using Data;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Biblioteca.API.Endpoints;

public static class BookExtensions
{
    public static void AddEndpointsBook(this WebApplication app)
    {
        app.MapGet("/Book", () =>
        {
            var dal = new DAL<Book>(new BibliotecaContext());
            return Results.Ok(dal.List());
        });

        app.MapGet("/Book/{title}", (string title) =>
        {
            var dal = new DAL<Book>(new BibliotecaContext());
            var livro = dal.GetBy(a => a.Title.ToUpper().Equals(title.ToUpper()));
            if (livro is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(livro);
        });

        app.MapDelete("/Book/{id}", ([FromServices] DAL<Book> dal, int id) =>
        {
            var book = dal.GetBy(a => a.Id == id);
            if (book is null)
            {
                return Results.NotFound();
            }
            dal.Delete(book);
            return Results.NoContent();
        });

        app.MapPost("/Book", ([FromBody] Book newBook) =>
        {
            var dal = new DAL<Book>(new BibliotecaContext());
            dal.Add(newBook);
            return Results.Ok();
        });

        app.MapPut("/Book/{id}", ([FromServices] DAL<Book> dal, int id, [FromBody] Book updatedBook) =>
        {
            var existingBook = dal.GetBy(a => a.Id == id);
            if (existingBook is null)
            {
                return Results.NotFound();
            }

            existingBook.Title = updatedBook.Title ?? existingBook.Title;
            existingBook.Author = updatedBook.Author ?? existingBook.Author;
            existingBook.ISBN = updatedBook.ISBN ?? existingBook.ISBN;
            existingBook.PublishedYear = updatedBook.PublishedYear != 0 ? updatedBook.PublishedYear : existingBook.PublishedYear;
            existingBook.Genre = updatedBook.Genre ?? existingBook.Genre;

            dal.Update(existingBook);
            return Results.Ok(existingBook);
        });

    }
}
