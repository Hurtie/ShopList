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
            if (login == "User-1" && pass == "1111")
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
