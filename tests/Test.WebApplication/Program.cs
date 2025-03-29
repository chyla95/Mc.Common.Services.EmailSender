using Mc.Common.Services.EmailSender.DependencyInjection.Extensions.ServiceCollection;
using Test.WebApplication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddEmailSender<IDefaultEmailSenderService, DefaultEmailSenderService>(configureEmailSender => configureEmailSender
    .WithEmailSenderSettingsResolvingStrategy<DefaultEmailSenderClientSettingsResolver>());    

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
