using Data;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Biblioteca.API.Endpoints;

public static class OwnerExtensions
{
    public static void AddEndpointsOwner(this WebApplication app)
    {
        app.MapGet("/Owner", () =>
        {
            var dal = new DAL<Owner>(new BibliotecaContext());
            return Results.Ok(dal.List());
        }).WithTags("Owners");

        app.MapGet("/Owner/{name}", (string name) =>
        {
            var dal = new DAL<Owner>(new BibliotecaContext());
            var owner = dal.GetBy(a => a.Name.ToUpper().Equals(name.ToUpper()));
            if (owner is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(owner);
        }).WithTags("Owners");

        app.MapPost("/Owner", ([FromBody] Owner newOwner) =>
        {
            var dal = new DAL<Owner>(new BibliotecaContext());
            dal.Add(newOwner);
            return Results.Created($"/Owner/{newOwner.OwnerId}", newOwner);
        }).WithTags("Owners");

        app.MapPut("/Owner/{id}", ([FromServices] DAL<Owner> dal, int id, [FromBody] Owner updatedOwner) =>
        {
            var existingOwner = dal.GetBy(a => a.OwnerId == id);
            if (existingOwner is null)
            {
                return Results.NotFound();
            }

            existingOwner.Name = updatedOwner.Name ?? existingOwner.Name;
            existingOwner.Email = updatedOwner.Email ?? existingOwner.Email;
            existingOwner.PhoneNumber = updatedOwner.PhoneNumber ?? existingOwner.PhoneNumber;

            dal.Update(existingOwner);
            return Results.Ok(existingOwner);
        }).WithTags("Owners");

        app.MapDelete("/Owner/{id}", ([FromServices] DAL<Owner> dal, int id) =>
        {
            var owner = dal.GetBy(a => a.OwnerId == id);
            if (owner is null)
            {
                return Results.NotFound();
            }
            dal.Delete(owner);
            return Results.NoContent();
        }).WithTags("Owners");
    }
}
