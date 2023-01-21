﻿namespace SharedKernel.Resources.JwtAuthenticationManager.Models
{
    public class AuthenticationResponse
    {
        public string UserName { get; set; }
        public string Token { get; set; }
        public int ExpiresIn { get; set; }
    }
}
