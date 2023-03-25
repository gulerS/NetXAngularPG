using FluentValidation.AspNetCore;
using NetXAngularPG.Domain.Entities;
using NetXAngularPG.Infrastructure.Filters;
using NetXAngularPG.Persistance;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
    policy.WithOrigins("http://localhost:4200", "https://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials()
));


// Add services to the container.
builder.Services.AddPersistanceServices();




builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>())
 .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<Product>())
  .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);



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
app.UseStaticFiles();

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();