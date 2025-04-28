using UNITEC.NV.DAL.Acceso;
using UNITEC.NV.DAL.Acceso.Interfaz;
using UNITEC.NV.BL.Acceso;
using UNITEC.NV.BL.Acceso.Interfaz;
using UNITEC.NV.BL.Generos.Interfaz;
using UNITEC.NV.DAL.Generos.Interfaz;
using UNITEC.NV.DAL.Generos;
using UNITEC.NV.BL.Generos;
using UNITEC.NV.BL.Plataformas.Interfaz;
using UNITEC.NV.DAL.Plataformas.Interfaz;
using UNITEC.NV.BL.Plataformas;
using UNITEC.NV.DAL.Plataformas;
using UNITEC.NV.DAL.Reportes.Interfaz;
using UNITEC.NV.BL.Reportes;
using UNITEC.NV.DAL.Reportes;
using UNITEC.NV.BL.Usuarios.Interfaz;
using UNITEC.NV.DAL.Usuarios.Interfaz;
using UNITEC.NV.DAL.Usuarios;
using UNITEC.NV.BL.Usuarios;
using UNITEC.NV.BL.Videojuegos.Interfaz;
using UNITEC.NV.DAL.Videojuegos.Interfaz;
using UNITEC.NV.DAL.Videojuegos;
using UNITEC.NV.BL.Videojuegos;
using UNITEC.NV.BL.Reportes.Interfaz;

namespace UNITEC.NV.API.Configuration
{
    public static class DependenciasConfiguration
    {
        public static void ConfigureDependencias(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddTransient<IAccesoManager, AccesoManager>();
            services.AddTransient<IAccesoRepository, AccesoRepository>();

            services.AddTransient<IGenerosManager, GeneroManager>();
            services.AddTransient<IGeneroRepository, GeneroRepository>();

            services.AddTransient<IPlataformasManager, PlataformaManager>();
            services.AddTransient<IPlataformasRepository, PlataformaRepository>();

            services.AddTransient<IReportesManager, ReportesManager>();
            services.AddTransient<IReportesRepository, ReportesRepository>();

            services.AddTransient<IUsuariosManager, UsuarioManager>();
            services.AddTransient<IUsuariosRepository, UsuarioRepository>();

            services.AddTransient<IVideojuegosManager, VideojuegoManager>();
            services.AddTransient<IVideojuegosRepository, VideojuegoRepository>();
        }
    }
}
