using MeBank.Models.Abstract;
using SQLite;

namespace MeBank.Models.Concrete
{
    [Table("CONFIGURATION")]
    public class Config : IEntity
    {
        [Column("ID"), PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Column("KEY"), Unique, NotNull]
        public string Key { get; set; }

        [Column("VALUE"), NotNull]
        public string Value { get; set; }
    }

}