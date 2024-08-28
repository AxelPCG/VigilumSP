using DAL.FluentAPI.ConfiguracaoTabelas;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DAL
{
    public class BancoAPIContext : DbContext
    {
        public BancoAPIContext(DbContextOptions<BancoAPIContext> options) : base(options) {}

        public DbSet<Cidade> TBL_Cidade { get; set; }
        public DbSet<Regiao> TBL_Regiao { get; set; }
        public DbSet<SubPrefeitura> TBL_SubPrefeitura { get; set; }
        public DbSet<Aviso> TBL_Aviso { get; set; }
        public DbSet<Previsao> TBL_Previsao {  get; set; }
        public DbSet<Umidade> TBL_Umidade { get; set; }
        public DbSet<Temperatura> TBL_Temperatura { get; set; }
        public DbSet<Nuvem> TBL_Nuvem { get; set; }
        public DbSet<Precipitacao> TBL_Precipitacao { get; set; }
        public DbSet<Ventania> TBL_Ventania { get; set; }

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