using System;
using MeBank.Models.Abstract;
using Newtonsoft.Json;
using SQLite;

namespace MeBank.Models.Concrete
{
    [Table("TRANSFERENCIA")]
    public class Transfer : IEntity
    {
        [Column("TRA_CODIGO"), PrimaryKey, AutoIncrement]
        [JsonProperty("TRA_CODIGO")]
        public int Id { get; set; }

        [Column("TRA_CUENTA_ORIGEN"), NotNull]
        [JsonProperty("TRA_CUENTA_ORIGEN")]
        public int OriginAccountId { get; set; }

        [Column("TRA_CUENTA_DESTINO"), NotNull]
        [JsonProperty("TRA_CUENTA_DESTINO")]
        public int DestinyAccountId { get; set; }

        [Column("TRA_DESCRIPCION"), NotNull]
        [JsonProperty("TRA_DESCRIPCION")]
        public string Description { get; set; }

        [Column("TRA_ESTADO"), MaxLength(1), NotNull]
        [JsonProperty("TRA_ESTADO")]
        public string Status { get; set; }

        [Column("TRA_FECHA"), NotNull]
        [JsonProperty("TRA_FECHA")]
        public DateTime Date { get; set; }

        [Column("TRA_MONTO"), NotNull]
        [JsonProperty("TRA_MONTO")]
        public decimal Amount { get; set; }
    }
}