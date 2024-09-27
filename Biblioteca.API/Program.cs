using Biblioteca.API.Endpoints;
using BibliotecaShared.Models.Models;
using Data;
using Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BibliotecaContext>();
builder.Services.AddTransient<DAL<Book>>();
builder.Services.AddTransient<DAL<Owner>>();
builder.Services.AddTransient<DAL<Friend>>();
builder.Services.AddTransient<DAL<Loan>>();
builder.Services.AddTransient<DAL<Gender>>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.AddEndpointsBook();
app.AddEndpointsOwner();
app.AddEndpointsFriend();
app.AddEndpointsLoan();
app.AddEndpointsGender();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
