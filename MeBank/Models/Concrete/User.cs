using System;
using MeBank.Models.Abstract;
using SQLite;

namespace MeBank.Models.Concrete
{
    [Table("USUARIO")]
    public class User : IEntity
    {
        [Column("USU_CODIGO"), PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Column("USU_IDENTIFICACION"), Unique, NotNull]
        public string CitizenId { get; set; }

        [Column("USU_NOMBRE"), NotNull]
        public string Name { get; set; }

        [Column("USU_USERNAME"), Unique, NotNull]
        public string Username { get; set; }

        [Column("USU_PASSWORD"), NotNull]
        public string Password { get; set; }

        [Column("USU_EMAIL"), Unique, NotNull]
        public string Email { get; set; }

        [Column("USU_FEC_NAC"), NotNull]
        public DateTime Birthday { get; set; }

        [Column("USU_ESTADO"), MaxLength(1), NotNull]
        public string Status { get; set; }
    }
}