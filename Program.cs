using Microsoft.EntityFrameworkCore;
using ReactWebApi.Context;
using ReactWebApi.Exceptions;
using ReactWebApi.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//.AddNewtonsoftJson() is added to services.AddControllers() for patch request only.
builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(policies =>
	{
		policies.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
	});
	//options.AddPolicy("MyLocalHost", policies =>
	//{
	//	policies.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod();
	//});
});

builder.Services.AddDbContext<UserContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("UserDBConn"));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//for Global Exception Handler no need to put try catch in every method
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
//builder.Services.AddExceptionHandler<NotImplementedExceptionHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseExceptionHandler(_ => { });

app.UseHttpsRedirection();

app.UseRouting();

//app.UseCors("MyLocalHost");
app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
