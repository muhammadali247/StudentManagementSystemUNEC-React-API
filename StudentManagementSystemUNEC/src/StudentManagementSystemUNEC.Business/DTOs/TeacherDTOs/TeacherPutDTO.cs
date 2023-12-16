﻿using Microsoft.AspNetCore.Http;
using StudentManagementSystemUNEC.Core.Utils.Enums;

namespace StudentManagementSystemUNEC.Business.DTOs.TeacherDTOs;

public class TeacherPutDTO
{
    public IFormFile? Image { get; set; }
    //public string? AppUserId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string middleName { get; set; }
    //public string Gender { get; set; }
    public Gender Gender { get; set; }
    //public string Country { get; set; }
    public Country Country { get; set; }
    public DateTime? BirthDate { get; set; }
}