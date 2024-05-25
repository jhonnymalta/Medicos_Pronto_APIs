using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MP.Cliente.API.Data;
using MP.Cliente.API.Interfaces;
using MP.Cliente.API.MessageConsumer;
using MP.Cliente.API.Repositories;


var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ClienteDbContext>(options =>
    options.UseSqlServer(connectionString));

var conexao = new DbContextOptionsBuilder<ClienteDbContext>();
conexao.UseSqlServer(connectionString);

// Add services to the container.
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddSingleton(new CriarClienteFromRabbitMQRepository(conexao.Options));

//builder.Services.AddSingleton<ICriarClienteFromRabbitMQRepository, CriarClienteFromRabbitMQRepository>();
builder.Services.AddHostedService<RabbitMQMessageConsumer>();

builder.Services.AddControllers();

//cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("Total",
        builder =>
        builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc(name: "v1", new OpenApiInfo
    {
        Title = "Médicos Pronto Agenda Api",
        Description = "Esta API faz parte da aplicação enterprise Médicos Pronto",
        Contact = new OpenApiContact() { Name = "Jhonatan Landes", Email = "jhonatanlandes@gmail.com" },
        License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Insira o token JWT desta maneira: Bearer {seu token}",
        Name = "Authorization",
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[] { }
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("Total");
app.MapControllers();

app.Run();
