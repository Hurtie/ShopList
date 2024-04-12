using SQLite;

namespace ShopList.Database.Objects
{
    [Table("Items_in_list")]
    public class ItemList
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int ListId { get; set; }
        public int ItemID { get; set; }
        public int Amount { get; set; }
        public string Info { get; set; }
    }
}
