using System;
using MeBank.Models.Abstract;
using Newtonsoft.Json;
using SQLite;

namespace MeBank.Models.Concrete
{
    [Table("PAGO")]
    public class Payment : IEntity
    {
        [Column("PAG_CODIGO"), PrimaryKey, AutoIncrement]
        [JsonProperty("PAG_CODIGO")]
        public int Id { get; set; }

        [Column("SER_CODIGO"), NotNull]
        [JsonProperty("SER_CODIGO")]
        public int ServiceId { get; set; }

        [Column("CUE_CODIGO"), NotNull]
        [JsonProperty("CUE_CODIGO")]
        public int AccountId { get; set; }

        [Column("PAG_FECHA"), NotNull]
        [JsonProperty("PAG_FECHA")]
        public DateTime Date { get; set; }

        [Column("PAG_MONTO"), NotNull]
        [JsonProperty("PAG_MONTO")]
        public decimal Amount { get; set; }
    }
}