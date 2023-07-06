using Microsoft.Extensions.PlatformAbstractions;
using MultiTracksWebApi.Services.Implementation;
using MultiTracksWebApi.Services.Interface;
using MultiTracksWebApi.Settings;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IArtistDetails, ArtistDetails>();
builder.Services.AddScoped<ISongDetails, SongDetails>();
builder.Services.AddSwaggerGen(options =>
{
    options.IncludeXmlComments(GetXmlCommentPath());
});
builder.Services.AddScoped<IFileUpload, FileUpload>();
builder.Services.Configure<CloudinaryConfig>(builder.Configuration.GetSection("CloudinaryConfig"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
static string GetXmlCommentPath()
{
    var basePath = PlatformServices.Default.Application.ApplicationBasePath;
    var fileName = typeof(Program).GetTypeInfo().Assembly.GetName().Name + ".xml";
    return Path.Combine(basePath, fileName);
}