using SQLite;

namespace ShopList.Database.Objects
{
    public class UserGroup
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string GroupID { get; set; }
        public int UserId { get; set; }
    }
}
