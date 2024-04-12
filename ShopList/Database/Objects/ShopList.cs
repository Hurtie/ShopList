using SQLite;

namespace ShopList.Database.Objects
{
    public class ShopList
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public int GroupID { get; set; }
    }
}
