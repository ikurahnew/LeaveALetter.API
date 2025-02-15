using LeaveALetter.API.Core.Letters.Commands;
using LeaveALetter.API.Core.Letters.Queries;
using LeaveALetter.API.Core.Users.Commands;
using LeaveALetter.API.Core.Users.Services;
using LeaveALetter.API.Data.Letters.Repositories;
using LeaveALetter.API.Data.Users.Repositories;
using MapsterMapper;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IMapper, Mapper>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<ILetterRepository, LetterRepository>();
builder.Services.AddMediatR(mediator => mediator.RegisterServicesFromAssembly(typeof(CreateLetterHandler).Assembly));
builder.Services.AddMediatR(mediator => mediator.RegisterServicesFromAssembly(typeof(CreateUserHandler).Assembly));
builder.Services.AddMediatR(mediator => mediator.RegisterServicesFromAssembly(typeof(GetLettersByAuthor).Assembly));
builder.Services.AddMediatR(mediator => mediator.RegisterServicesFromAssembly(typeof(GetLettersByReciever).Assembly));

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
