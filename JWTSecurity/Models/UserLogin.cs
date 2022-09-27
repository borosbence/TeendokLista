﻿using System.ComponentModel.DataAnnotations;

namespace JWTSecurity.Models
{
    public class UserLogin
    {
        [Required]
        public string Username { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}