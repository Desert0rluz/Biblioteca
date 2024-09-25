using Biblioteca.API.Endpoints;
using Data;
using Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BibliotecaContext>();
builder.Services.AddTransient<DAL<Book>>();
builder.Services.AddTransient<DAL<Owner>>();
builder.Services.AddTransient<DAL<Friend>>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.AddEndpointsBook();
app.AddEndpointsOwner();
app.AddEndpointsFriend();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
