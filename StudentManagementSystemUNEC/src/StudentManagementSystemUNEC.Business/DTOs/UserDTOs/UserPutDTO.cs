using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystemUNEC.Business.DTOs.UserDTOs;

public class UserPutDTO
{
    public string UserName { get; set; }

    //public Guid? StudentId { get; set; }
    //public Guid? TeacherId { get; set; }

    //[DataType(DataType.EmailAddress)]
    //public string Email { get; set; }

    public List<string>? RoleIds { get; set; }
}