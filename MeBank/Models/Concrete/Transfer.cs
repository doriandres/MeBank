using System;
using MeBank.Models.Abstract;
using SQLite;

namespace MeBank.Models.Concrete
{
    [Table("TRANSFERENCIA")]
    public class Transfer : IEntity
    {
        [Column("TRA_CODIGO"), PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Column("TRA_CUENTA_ORIGEN"), NotNull]
        public int OriginAccountId { get; set; }

        [Column("TRA_CUENTA_DESTINO"), NotNull]
        public int DestinyAccountId { get; set; }

        [Column("TRA_DESCRIPCION"), NotNull]
        public string Description { get; set; }

        [Column("TRA_ESTADO"), MaxLength(1), NotNull]
        public string Status { get; set; }

        [Column("TRA_FECHA"), NotNull]
        public DateTime Date { get; set; }

        [Column("TRA_MONTO"), NotNull]
        public decimal Amount { get; set; }
    }
}