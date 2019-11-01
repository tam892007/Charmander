namespace BdcMobile.Core.Models
{
    public class User
    {
        public string LoginName { get; set; }
        public string DisplayName { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Token { get; set; }
    }
}
