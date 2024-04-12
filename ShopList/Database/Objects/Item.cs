using SQLite;

namespace ShopList.Database.Objects
{
    [Table("Items")]
    public class Item
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
