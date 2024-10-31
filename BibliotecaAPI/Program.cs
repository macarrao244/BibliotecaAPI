using BibliotecaAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//Realiza a leitura da conexão com o banco
builder.Services.AddSingleton<LivrosRepository>(provider => new LivrosRepository(ConnectionString));
builder.Services.AddSingleton<UsuarioRepository>(provider => new UsuarioRepository(ConnectionString));
builder.Services.AddSingleton<EmprestimoRepository>(provider => new EmprestimoRepository(ConnectionString));

//Swagger Parte 1
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
});

var app = builder.Build();

//Swagger Parte 2
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Crud Biblioteca V1");
        c.RoutePrefix = string.Empty;
    });
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
