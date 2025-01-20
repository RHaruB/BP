using BP_API.Contracts;
using BP_API.Models;
using BP_API.Service;
using crypto;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Infrastructure;
using QuestPDF.Fluent;


namespace BP_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<IClientes, ClientesService>();
            builder.Services.AddScoped<ICuenta, CuentaService>();
            builder.Services.AddScoped<IParametro, ParametroService>();
            builder.Services.AddScoped<IMovimientos, MovimientosService>();
            builder.Services.AddScoped<IReporte, ReporteService>();


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            #region Conexion Base de Datos

            builder.Services.AddDbContext<BPContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetSection("AppSettings").GetSection("DefaultConnection").Value);

            });

            #endregion

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "API",
                                  builder =>
                                  {
                                      builder.WithHeaders("*");
                                      builder.WithOrigins("*");
                                      builder.WithMethods("*");
                                      builder.AllowAnyOrigin();

                                  });
            });
            QuestPDF.Settings.License = LicenseType.Community;

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseCors("API");


            app.MapControllers();

            app.Run();
        }
    }
}
