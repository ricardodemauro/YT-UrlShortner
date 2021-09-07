using Carter;
using LiteDB;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();
builder.Services.AddSingleton<ILiteDatabase, LiteDatabase>(x => new LiteDatabase("short.db"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();
app.MapCarter();

app.Run();
