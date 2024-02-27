using Core.Features.Abums;
using Core;
using Infra;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<IAlbumRepository,AlbumRepository>();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddHandlers();
//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllAlbumsQueryHandler).Assembly));
//builder.Services.AddMediatR(typeof(GetAllAlbumsQuery));
//builder.Services.AddSingleton<IAlbumRepository>(cfg =>
//{

//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
