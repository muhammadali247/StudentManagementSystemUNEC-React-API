﻿namespace StudentManagementSystemUNEC.Business.DTOs.UserDTOs;

public class UnassignedUserGetDTO
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public List<string> Roles { get; set; }
}