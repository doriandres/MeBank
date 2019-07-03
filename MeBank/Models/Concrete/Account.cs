using MeBank.Models.Abstract;
using SQLite;

namespace MeBank.Models.Concrete
{
    [Table("CUENTA")]
    public class Account : IEntity
    {
        [Column("CUE_CODIGO"), PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Column("USU_CODIGO"), NotNull]
        public int UserId { get; set; }

        [Column("CUE_DESCRIPCION"), MaxLength(100), NotNull]
        public string Description { get; set; }

        [Column("CUE_MONEDA"), MaxLength(3), NotNull]
        public string Currency { get; set; }

        [Column("CUE_SALDO"), NotNull]
        public decimal Balance { get; set; }

        [Column("CUE_ESTADO"), MaxLength(1), NotNull]
        public string Status { get; set; }

    }
}