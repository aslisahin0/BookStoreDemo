﻿namespace BookStoreDemo.Security
{
    public class JwtToken
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
        public DateTime Expiration { get; set; }
    }
}
