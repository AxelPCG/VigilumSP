using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.FluentAPI.ConfiguracaoTabelas
{
    public class MunicipioConfig : ConfiguracaoContextoBase
    {
        public override void Configurar(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Municipio>(municipio =>
            {
                municipio.Property(x => x.Geometry).HasColumnType("SDO_GEOMETRY");
                municipio.Property(x => x.Cord_Central).HasColumnType("SDO_GEOMETRY");
            });
        }
    }
}