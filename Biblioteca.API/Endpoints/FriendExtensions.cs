using Data;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Biblioteca.API.Endpoints;

public static class FriendExtensions
{
    public static void AddEndpointsFriend(this WebApplication app)
    {
        app.MapGet("/Friend", () =>
        {
            var dal = new DAL<Friend>(new BibliotecaContext());
            return Results.Ok(dal.List());
        }).WithTags("Friends");

        app.MapGet("/Friend/{name}", (string name) =>
        {
            var dal = new DAL<Friend>(new BibliotecaContext());
            var friend = dal.GetBy(a => a.Name.ToUpper().Equals(name.ToUpper()));
            if (friend is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(friend);
        }).WithTags("Friends");

        app.MapPost("/Friend", ([FromBody] Friend newFriend) =>
        {
            var dal = new DAL<Friend>(new BibliotecaContext());
            dal.Add(newFriend);
            return Results.Created($"/Friend/{newFriend.FriendId}", newFriend);
        }).WithTags("Friends");

        app.MapPut("/Friend/{id}", ([FromServices] DAL<Friend> dal, int id, [FromBody] Friend updatedFriend) =>
        {
            var existingFriend = dal.GetBy(a => a.FriendId == id);
            if (existingFriend is null)
            {
                return Results.NotFound();
            }

            existingFriend.Name = updatedFriend.Name ?? existingFriend.Name;
            existingFriend.Email = updatedFriend.Email ?? existingFriend.Email;
            existingFriend.PhoneNumber = updatedFriend.PhoneNumber ?? existingFriend.PhoneNumber;

            dal.Update(existingFriend);
            return Results.Ok(existingFriend);
        }).WithTags("Friends");

        app.MapDelete("/Friend/{id}", ([FromServices] DAL<Friend> dal, int id) =>
        {
            var friend = dal.GetBy(a => a.FriendId == id);
            if (friend is null)
            {
                return Results.NotFound();
            }
            dal.Delete(friend);
            return Results.NoContent();
        }).WithTags("Friends");
    }
}
