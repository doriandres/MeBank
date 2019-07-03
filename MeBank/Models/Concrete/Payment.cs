using System;
using MeBank.Models.Abstract;
using SQLite;

namespace MeBank.Models.Concrete
{
    [Table("PAGO")]
    public class Payment : IEntity
    {
        [Column("PAG_CODIGO"), PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Column("SER_CODIGO"), NotNull]
        public int ServiceId { get; set; }

        [Column("CUE_CODIGO"), NotNull]
        public int AccountId { get; set; }

        [Column("PAG_FECHA"), NotNull]
        public DateTime Date { get; set; }

        [Column("PAG_MONTO"), NotNull]
        public decimal Amount { get; set; }
    }
}