using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Xml.Linq;
using Microsoft.AspNet.Identity;
using ShopList.Database.Objects;

namespace ShopList.Database
{
    public class Queries
    {
        static HttpClient client = new HttpClient();
        public static User userData = null;

        static async Task<bool> Init()
        {
            if (client.BaseAddress is null)
            {
                client.BaseAddress = new Uri("http://localhost:3000");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = TimeSpan.FromSeconds(5);
            }
            try
            {
                HttpResponseMessage response = await client.GetAsync($"{client.BaseAddress}items");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", "Не удалось соединиться с API, попробуйте зайти позже", "OK");
                return false;
            }
            return true;
        }

        public static async Task LoadUser(string id)
        {
            if(await Init())
            {
                HttpResponseMessage response = await client.GetAsync($"{client.BaseAddress}user/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<User[]>();
                    if (result is not null && result.Length != 0)
                    {
                        userData = result[0];
                    }
                }
            }
        }

        public static async Task<string> CheckUserAsync(string login)
        {
            if(await Init())
            {
                HttpResponseMessage response = await client.GetAsync($"{client.BaseAddress}users/{login}");
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<User[]>();
                    if (result is not null && result.Length != 0)
                    {
                        userData = result[0];
                    }
                }

                return userData.Password;
            }
            return "";
        }

        public static async Task<List<Group>> GetGroups(int userId)
        {

            List<Group> groups = null;
            if (await Init())
            {
                HttpResponseMessage response = await client.GetAsync($"{client.BaseAddress}groups/{userId}");
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<List<Group>>();
                    if (result is not null && result.Count != 0)
                    {
                        groups = result;
                    }
                }
            }
            return groups;
        }

        public static async Task<List<Objects.ShopList>> GetLists(int groupId)
        {
            List<Objects.ShopList> lists = null;

            if(await Init())
            {
                HttpResponseMessage response = await client.GetAsync($"{client.BaseAddress}lists/{groupId}");
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<List<Objects.ShopList>>();
                    if (result is not null && result.Count != 0)
                    {
                        lists = result;
                    }
                }
            }

            return lists;
        }

        public static async Task<List<Objects.ItemList>> GetItems(int listId)
        {
            List<Objects.ItemList> items = null;

            if (await Init())
            {
                HttpResponseMessage response = await client.GetAsync($"{client.BaseAddress}items/{listId}");
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<List<Objects.ItemList>>();
                    if (result is not null && result.Count != 0)
                    {
                        items = result;
                    }
                }
            }

            return items;
        }

        public static async Task<List<Objects.User>> GetGroupUsers(int groupId)
        {
            List<Objects.User> users = null;
            if(await Init())
            {


                HttpResponseMessage response = await client.GetAsync($"{client.BaseAddress}groupUsers/{groupId}");
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<List<Objects.User>>();
                    if (result is not null && result.Count != 0)
                    {
                        users = result;
                    }
                }

            }

            return users;
        }

        public static async Task<List<Item>> LoadItems()
        {
            List<Item> items = null;
            
            if(await Init())
            {
                HttpResponseMessage response = await client.GetAsync($"{client.BaseAddress}items");
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<List<Item>>();
                    if (result is not null && result.Count != 0)
                    {
                        items = result;
                    }
                }
            }


            return items;
        }

        public static async Task AddItemToList(int listID, int itemID)
        {
            if (await Init())
            {

                ItemList newItem = new ItemList { ItemID = itemID, ListID = listID };

                HttpResponseMessage response = await client.PostAsJsonAsync($"{client.BaseAddress}item", newItem);
                response.EnsureSuccessStatusCode();
            }
        }

        public static async Task UpdateItem(int listID, int itemID, string info)
        {
            if(await Init())
            {
                ItemList newItem = new ItemList { ItemID = itemID, ListID = listID, Info = info };

                HttpResponseMessage response = await client.PostAsJsonAsync($"{client.BaseAddress}item/update", newItem);
                response.EnsureSuccessStatusCode();
            }
        }

        public static async Task RegisterUser(string login, string password, string name)
        {
            if (await Init())
            {
                PasswordHasher pw = new PasswordHasher();
                string hp = pw.HashPassword(password);

                User newUser = new User() { Login = login, Password = hp, Name = name };

                HttpResponseMessage response = await client.PostAsJsonAsync($"{client.BaseAddress}users", newUser);
                response.EnsureSuccessStatusCode();

                await CheckUserAsync(login);
            }
        }

        public static async Task CreateGroup(string name, int creatorID)
        {
            if( await Init())
            {
                Group newGroup = new Group() { Name = name, Creator_Id = creatorID };

                HttpResponseMessage response = await client.PostAsJsonAsync($"{client.BaseAddress}groups", newGroup);
                response.EnsureSuccessStatusCode();
            }
        }

        public static async Task CreateList(string name, int groupID)
        {
            if(await Init())
            {
                Objects.ShopList newList = new Objects.ShopList() { Name = name, GroupID = groupID };

                HttpResponseMessage response = await client.PostAsJsonAsync($"{client.BaseAddress}list", newList);
                response.EnsureSuccessStatusCode();
            }
        }

        public static async Task AddUserToGroup(int userID, int groupID)
        {
            if(await Init())
            {
                UserGroup newUserGroup = new UserGroup() { UserId = userID, GroupID = groupID };

                HttpResponseMessage response = await client.PostAsJsonAsync($"{client.BaseAddress}usergroups", newUserGroup);
                response.EnsureSuccessStatusCode();
            }
        }

        public static async Task DeleteUser()
        {
            if (await Init())
            {
                HttpResponseMessage response = await client.PostAsJsonAsync($"{client.BaseAddress}users/delete", userData);
                response.EnsureSuccessStatusCode();
            }
        }

        public static async Task DeleteGroup(int id)
        {
            if (await Init())
            {
                HttpResponseMessage response = await client.PostAsJsonAsync($"{client.BaseAddress}groups/delete", new Group() { Id = id });
                response.EnsureSuccessStatusCode();
            }
        }

        public static async Task DeleteFromGroup(int id)
        {
            if (await Init())
            {
                HttpResponseMessage response = await client.PostAsJsonAsync($"{client.BaseAddress}groupsUser/delete", new UserGroup() { UserId = userData.Id, GroupID = id });
                response.EnsureSuccessStatusCode();
            }
        }

        public static async Task DeleteList(int id)
        {
            if (await Init())
            {
                HttpResponseMessage response = await client.PostAsJsonAsync($"{client.BaseAddress}lists/delete", new Objects.ShopList() { Id = id });
                response.EnsureSuccessStatusCode();
            }
        }

        public static async Task DeleteFromList(int itemid, int listid)
        {
            if (await Init())
            {
                HttpResponseMessage response = await client.PostAsJsonAsync($"{client.BaseAddress}listitem/delete", new ItemList() { ItemID = itemid, ListID = listid });
                response.EnsureSuccessStatusCode();
            }
        }

        public static async Task<List<Results>> Results()
        {
            List<Results> results = null;

            if (await Init())
            {
                HttpResponseMessage response = await client.GetAsync($"{client.BaseAddress}results/{userData.Id}");
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<List<Results>>();
                    if (result is not null && result.Count != 0)
                    {
                        results = result;
                    }
                }
            }


            return results;
        }
    }
}
