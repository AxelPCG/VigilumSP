using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.FluentAPI.ConfiguracaoTabelas
{
    public abstract class ConfiguracaoContextoBase
    {
        private protected void FillKeyInEnums<TEntity>(EntityTypeBuilder<TEntity> obj) where TEntity : class
        {
            obj.HasKey("Id");
            obj.HasIndex("Id").IsUnique();
            obj.Property("Id").ValueGeneratedNever();
        }

        public abstract void Configurar(ModelBuilder modelBuilder);
    }
}
