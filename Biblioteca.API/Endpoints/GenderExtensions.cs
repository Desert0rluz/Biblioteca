using BibliotecaShared.Models.Models;
using Data;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.API.Endpoints;

public static class GenderExtensions
{
    public static void AddEndpointsGender(this WebApplication app)
    {
        app.MapGet("/Gender", () =>
        {
            var dal = new DAL<Gender>(new BibliotecaContext());
            return Results.Ok(dal.List());
        }).WithTags("Genders");

        app.MapGet("/Gender/{id}", (int id) =>
        {
            var dal = new DAL<Gender>(new BibliotecaContext());
            var gender = dal.GetBy(g => g.GenderId == id);
            if (gender is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(gender);
        }).WithTags("Genders");

        app.MapDelete("/Gender/{id}", ([FromServices] DAL<Gender> dal, int id) =>
        {
            var gender = dal.GetBy(g => g.GenderId == id);
            if (gender is null)
            {
                return Results.NotFound();
            }
            dal.Delete(gender);
            return Results.NoContent();
        }).WithTags("Genders");

        app.MapPost("/Gender", ([FromBody] Gender newGender) =>
        {
            var dal = new DAL<Gender>(new BibliotecaContext());
            dal.Add(newGender);
            return Results.Created($"/Gender/{newGender.GenderId}", newGender); 
        }).WithTags("Genders");

        app.MapPut("/Gender/{id}", ([FromServices] DAL<Gender> dal, int id, [FromBody] Gender updatedGender) =>
        {
            var existingGender = dal.GetBy(g => g.GenderId == id);
            if (existingGender is null)
            {
                return Results.NotFound();
            }

            existingGender.Name = updatedGender.Name ?? existingGender.Name;
            existingGender.Description = updatedGender.Description ?? existingGender.Description;
            existingGender.Books = updatedGender.Books ?? existingGender.Books; 

            dal.Update(existingGender);
            return Results.Ok(existingGender);
        }).WithTags("Genders");
    }
}
