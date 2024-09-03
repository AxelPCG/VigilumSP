using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.FluentAPI.ConfiguracaoTabelas
{
    public class DistritoConfig : ConfiguracaoContextoBase
    {
        public override void Configurar(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Distrito>(distrito =>
            {
                distrito.Property(x => x.CORD_CENTRAL).HasColumnType("SDO_GEOMETRY");
            }); 
        }
    }
}