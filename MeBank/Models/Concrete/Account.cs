using MeBank.Models.Abstract;
using Newtonsoft.Json;
using SQLite;

namespace MeBank.Models.Concrete
{
    [Table("CUENTA")]
    public class Account : IEntity
    {
        [Column("CUE_CODIGO"), PrimaryKey, AutoIncrement]
        [JsonProperty(PropertyName = "CUE_CODIGO")]
        public int Id { get; set; }

        [Column("USU_CODIGO"), NotNull]
        [JsonProperty(PropertyName = "USU_CODIGO")]

        public int UserId { get; set; }

        [Column("CUE_DESCRIPCION"), MaxLength(100), NotNull]
        [JsonProperty(PropertyName = "CUE_DESCRIPCION")]

        public string Description { get; set; }

        [Column("CUE_MONEDA"), MaxLength(3), NotNull]
        [JsonProperty(PropertyName = "CUE_MONEDA")]

        public string Currency { get; set; }

        [Column("CUE_SALDO"), NotNull]
        [JsonProperty(PropertyName = "CUE_SALDO")]
        public decimal Balance { get; set; }

        [Column("CUE_ESTADO"), MaxLength(1), NotNull]
        [JsonProperty(PropertyName = "CUE_ESTADO")]
        public string Status { get; set; }

    }
}