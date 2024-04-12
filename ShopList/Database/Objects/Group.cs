using SQLite;

namespace ShopList.Database.Objects
{
    [Table("Groups")]
    public class Group
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public int CreatorId { get; set; }
    }
}
