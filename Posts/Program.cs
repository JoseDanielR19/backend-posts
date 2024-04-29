WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddCors(c => c.AddPolicy("AllowOrigin", options => options.WithOrigins("http://localhost:4200", "http://localhost:4200/*").AllowAnyHeader().AllowAnyHeader()));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseCors(options => options.WithOrigins("http://localhost:4200", "http://localhost:4200/*").AllowAnyHeader().AllowAnyMethod().WithExposedHeaders());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();