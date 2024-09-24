using Data;
using Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/listarLivros", () =>
{ 
    var dal = new DAL<Book>(new BibliotecaContext());
    return dal.List();
});

app.


app.Run();
