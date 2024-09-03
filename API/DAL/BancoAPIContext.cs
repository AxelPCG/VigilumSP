using DAL.FluentAPI.ConfiguracaoTabelas;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DAL
{
    public class BancoAPIContext : DbContext
    {
        public BancoAPIContext(DbContextOptions<BancoAPIContext> options) : base(options) {}

        public DbSet<Distrito> Distritos { get; set; }
        public DbSet<Municipio> Municipios { get; set; }
        public DbSet<Previsao> Previsoes { get; set; }  
        public DbSet<PrevisaoFutura> PrevisoesFuturas { get; set; }
        public DbSet<Zona> Zonas { get; set; }
        public DbSet<Ocorrencia> Ocorrencias { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseOracle("Your Oracle Connection String");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BancoAPIContext).Assembly);
            
            ConfigurarEntidadesAtravesDasClassesConfig(modelBuilder);
        }

        private void ConfigurarEntidadesAtravesDasClassesConfig(ModelBuilder modelBuilder)
        {
            var configTypes = RecuperarTodosOsTiposQueHerdamDaClasse(typeof(ConfiguracaoContextoBase));

            foreach (var configType in configTypes)
            {
                var obj = (ConfiguracaoContextoBase)Activator.CreateInstance(configType);
                obj.Configurar(modelBuilder);
            }
        }

        IEnumerable<Type> RecuperarTodosOsTiposQueHerdamDaClasse(Type MyType)
        {
            return Assembly.GetAssembly(MyType)
                .GetTypes()
                .Where(TheType =>
                TheType.IsClass
                && !TheType.IsAbstract
                && TheType.IsSubclassOf(MyType));
        }
    }
}