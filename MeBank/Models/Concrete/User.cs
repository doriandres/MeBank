using System;
using MeBank.Models.Abstract;
using Newtonsoft.Json;
using SQLite;

namespace MeBank.Models.Concrete
{
    [Table("USUARIO")]
    public class User : IEntity
    {
        [Column("USU_CODIGO"), PrimaryKey, AutoIncrement]
        [JsonProperty(PropertyName = "USU_CODIGO")]
        public int Id { get; set; }

        [Column("USU_IDENTIFICACION"), Unique, NotNull]
        [JsonProperty(PropertyName = "USU_IDENTIFICACION")]
        public string CitizenId { get; set; }

        [Column("USU_NOMBRE"), NotNull]
        [JsonProperty(PropertyName = "USU_NOMBRE")]
        public string Name { get; set; }

        [Column("USU_USERNAME"), Unique, NotNull]
        [JsonProperty(PropertyName = "USU_USERNAME")]
        public string Username { get; set; }

        [Column("USU_PASSWORD"), NotNull]
        [JsonProperty(PropertyName = "USU_PASSWORD")]
        public string Password { get; set; }

        [Column("USU_EMAIL"), Unique, NotNull]
        [JsonProperty(PropertyName = "USU_EMAIL")]
        public string Email { get; set; }

        [Column("USU_FEC_NAC"), NotNull]
        [JsonProperty(PropertyName = "USU_FEC_NAC")]
        public DateTime Birthday { get; set; }

        [Column("USU_ESTADO"), MaxLength(1), NotNull]
        [JsonProperty(PropertyName = "USU_ESTADO")]
        public string Status { get; set; }

        [Ignore]
        [JsonProperty(PropertyName = "TOKEN")]
        public string Token { get; set; }
    }
}