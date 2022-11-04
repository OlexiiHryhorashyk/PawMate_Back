using Amazon.S3;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PawMate.Image.API.Infastructure.Repository;
using PawMate.Image.API.Infastructure.Storage;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IStorage, Storage>(_ => new Storage(new AmazonS3Client(
    awsAccessKeyId: builder.Configuration["AWSAccessKeyId"],
    awsSecretAccessKey: builder.Configuration["AWSSecretKey"],
    region: Amazon.RegionEndpoint.GetBySystemName(builder.Configuration["AWSRegion"])),
    builder.Configuration["BucketName"]
));
builder.Services.AddScoped<IImageRepository, ImageRepository>();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
