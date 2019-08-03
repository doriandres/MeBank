using MeBank.Models.Abstract;
using Newtonsoft.Json;
using SQLite;

namespace MeBank.Models.Concrete
{
    [Table("SERVICIO")]
    public class Service : IEntity
    {
        [Column("SER_CODIGO"), PrimaryKey, AutoIncrement]
        [JsonProperty("SER_CODIGO")]
        public int Id { get; set; }

        [Column("SER_DESCRIPCION"), MaxLength(100), NotNull]
        [JsonProperty("SER_DESCRIPCION")]
        public string Description { get; set; }

        [Column("SER_ESTADO"), MaxLength(1), NotNull]
        [JsonProperty("SER_ESTADO")]
        public string Status { get; set; }
    }
}