using Microsoft.EntityFrameworkCore;
using Semillitas.Api.Data;
using Semillitas.Api.Repositories;
using Semillitas.Api.Services;
using Semillitas.Api.Validators;
using Semillitas.Core.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

//registrar un contexto para inyeccion
var connectionString = builder.Configuration.GetConnectionString("SemillitasConnectionString");
if (string.IsNullOrWhiteSpace(connectionString))
    throw new InvalidOperationException("No se encontró ConnectionStrings:SemillitasConnectionString en appsettings.json.");

builder.Services.AddDbContext<SemillitasContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddScoped(typeof(Repository<>));

builder.Services.AddTransient<CategoriaService>();
builder.Services.AddTransient<TallerService>();
builder.Services.AddTransient<PeriodoService>();
builder.Services.AddTransient<TutorService>();
builder.Services.AddTransient<NinoService>();
builder.Services.AddTransient<InscripcionService>();
builder.Services.AddTransient<ReporteService>();

builder.Services.AddScoped<NewCategoriaValidator>();
builder.Services.AddScoped<EditCategoriaValidator>();
builder.Services.AddScoped<DeleteCategoriaValidator>();
builder.Services.AddSingleton<TallerCreateValidator>();
builder.Services.AddSingleton<TallerEditValidator>();
builder.Services.AddScoped<DeleteTallerValidator>();
builder.Services.AddScoped<NewPeriodoValidator>();
builder.Services.AddScoped<EditPeriodoValidator>();
builder.Services.AddScoped<DeletePeriodoValidator>();
builder.Services.AddSingleton<TutorCreateValidator>();
builder.Services.AddScoped<DeleteTutorValidator>();
builder.Services.AddSingleton<NinoCreateValidator>();
builder.Services.AddScoped<DeleteNinoValidator>();
builder.Services.AddScoped<NewInscripcionValidator>();

var app = builder.Build();

app.MapControllers();
app.Run();
