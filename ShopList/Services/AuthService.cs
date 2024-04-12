using ShopList.Database;

namespace ShopList.Services
{
    public class AuthService
    {
        private const string AuthState = "AuthState";
        public async Task<bool> IsAuthenticatedAsync()
        {
            await Task.Delay(2000);

            var authState = Preferences.Default.Get(AuthState, false);

            return authState;
        }
        public bool Login(string login, string pass)
        {
            if ()
            {
                Preferences.Default.Set(AuthState, true);
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
