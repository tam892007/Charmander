namespace BdcMobile.Core.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string AccountName { get; set; }
        public string Password { get; set; }
        public string Type { get; set; }
        public string Image { get; set; }
        public string api_token { get; set; }
        public string CreateTime { get; set; }
        public bool IsAuthenticated { get; set; }
        public string ErrorMessage { get; set; }

       

        

    }

    public class LoginResponseModel
    {
        public string data { get; set; }
        public User user { get; set; }
    }

}
