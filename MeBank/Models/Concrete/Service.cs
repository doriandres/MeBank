using MeBank.Models.Abstract;
using SQLite;

namespace MeBank.Models.Concrete
{
    [Table("SERVICIO")]
    public class Service : IEntity
    {
        [Column("SER_CODIGO"), PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Column("SER_DESCRIPCION"), MaxLength(100), NotNull]
        public string Description { get; set; }

        [Column("SER_ESTADO"), MaxLength(1), NotNull]
        public string Status { get; set; }
    }
}