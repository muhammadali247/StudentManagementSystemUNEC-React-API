﻿namespace StudentManagementSystemUNEC.Business.DTOs.AuthDTOs;

public class LoginDTO
{
    public string UsernameOrEmail { get; set; }
    public string Password { get; set; }
    public bool RememberMe { get; set; }
}