using System.ComponentModel.DataAnnotations;

namespace DAL.Models.Base
{
    public class Entity : Entity<int>
    {
    }

    public class Entity<T>
    {
        [Key]
        public T Id { get; set; }
    }
}
