using Microsoft.AspNet.Identity;
using ShopList.Database;

namespace ShopList.Services
{
    public class AuthService
    {
        private const string AuthState = "AuthState";
        private const string id = "userID";
        public async Task<bool> IsAuthenticatedAsync()
        {
            await Task.Delay(2000);

            var authState = Preferences.Default.Get(AuthState, false);

            return authState;
        }

        public async Task<bool> Login(string login, string pass)
        {
            string check = await Queries.CheckUserAsync(login);
            PasswordHasher pw = new PasswordHasher();


            if (pw.VerifyHashedPassword(check, pass) == PasswordVerificationResult.Success)
            {
                Preferences.Default.Set(AuthState, true);
                Preferences.Default.Set(id, Queries.userData.Id);
                return true;
            }

            return false;
        }
        public void Logout()
        {
            Preferences.Default.Remove(AuthState);
        }
    }
}
