    using Microsoft.AspNetCore.Http;
using StudentManagementSystemUNEC.Core.Utils.Enums;

namespace StudentManagementSystemUNEC.Business.DTOs.StudentDTOs;

public class StudentPostDTO
{
    public IFormFile? Image { get; set; }
    public string? AppUserId { get; set; }
    //public string Email { get; set; }
    //public string Username { get; set; }
    //public string Password { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string middleName { get; set; }
    public DateTime admissionYear { get; set; }
    //public string educationStatus { get; set; }
    public EducationStatus educationStatus { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? corporativeEmail { get; set; }
    public string? corporativePassword { get; set; }
    //public string Gender { get; set; }
    public Gender Gender { get; set; }
    //public string Country { get; set; }
    public Country Country { get; set; }
    public Guid? MainGroup { get; set; }
    public List<Guid>? GroupIds { get; set; }
    public List<SubGroup> subGroups { get; set; }
}




/* 
 {
  "userName": "testuser",
  "email": "muhammadma@code.edu.az",
  "password": "Salam123!",
  "confirmPassword": "Salam123!",
  "studentId": "2cd9a338-fdf5-444f-fdd7-08dbd53dee28",
  "teacherId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "roleId": [
    "57045e2e-2300-460d-b6ff-2cf5b42709e7"
  ]
}

{
  "userName": "testuser",
  "email": "muhammadma@code.edu.az",
  "password": "Salam123!",
  "confirmPassword": "Salam123!",
  "studentId": "a0132447-cf6a-4b52-f40e-08dbd3d8a30f",
  "teacherId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "roleId": [
    "57045e2e-2300-460d-b6ff-2cf5b42709e7"
  ]
}

"image": "random.jpg",
  "email": "randomEmail@gmail.com",
  "username": "random",
  "password": "Random123",
  "name": "Random",
  "surname": "Randomov",
  "middleName": "Randomovic",
  "admissionYear": "2023-10-10T06:40:24.495Z",
  "educationStatus": "graduated",
  "birthDate": "2023-10-10T06:40:24.495Z",
  "corporativeEmail": "randomcorporative@unec.com",
  "corporativePassword": "Random123",
  "gender": "male",
  "country": "Pakistan"

  "image": "oke.jpg",
  "email": "zuli@mail.com",
  "username": "Zulik",
  "password": "Zulik123",
  "name": "Zulfiqar",
  "surname": "Ali",
  "middleName": "Ahmad",
  "admissionYear": "2023-10-10T12:15:22.467",
  "educationStatus": "studies",
  "birthDate": "2023-10-10T12:15:22.467",
  "corporativeEmail": "zuli@corp.com",
  "corporativePassword": "saqol123",
  "gender": "male",
  "country": "Pakistan"

  "image": "okey.jpg",
  "email": "vovik@mail.com",
  "username": "vovi_super1",
  "password": "Farhad123",
  "name": "Farhad",
  "surname": "Ali",
  "middleName": "Ahmad",
  "admissionYear": "2023-10-10T12:15:22.467",
  "educationStatus": "studies",
  "birthDate": "2023-10-10T12:15:22.467",
  "corporativeEmail": "vovik@corp.com",
  "corporativePassword": "saqol123",
  "gender": "male",
  "country": "Pakistan"


  "name": "Information Technologies",
  "subjectCode": "00402",
  "semester": 5
  
{
  "name": "1087",
  "subGroup": 1,
  "creationYear": "2021-10-14T13:41:15.954Z",
  "facultyId": "fa6ad940-c4f8-462b-2cce-08dbccbb26a2",
  "studentIds": [
    "17959c4e-4189-4fd5-4035-08dbccb23ad7",
    "9abb8c9a-92a7-4ddd-4036-08dbccb23ad7"
  ]
}

{
  "image": "test.jpeg",
  "email": "abdulaziz.karimli@gmail.com",
  "username": "Ab.Karimli",
  "password": "Salam123!",
  "name": "Amdulaziz",
  "surname": "Karimli",
  "middleName": "Abdulaziz's father",
  "gender": "male",
  "country": "Azerbaijan",
  "birthDate": "2020-10-15T05:48:56.255Z"
}

{
  "groupId": "2c0e18bc-b0ab-4960-b7b3-08dbccbb7ec1",
  "subjectId": "e20f952c-f75e-456d-e18e-08dbcfd93b19",
  "teacherRole": [
    {
      "teacherId": "09e5499c-b8b4-403a-e987-08dbcd42b1b0",
      "roleId": "ac2cee6e-f60a-436a-be3c-08dbce645e02"
    }
  ],
  "credits": 10,
  "totalHours": 150
}

{
  "name": "Business Essential exam",
  "maxScore": 30,
  "date": "2023-10-19T16:45:56.615Z",
  "examTypeId": "527f6933-fd56-46c0-9c94-08dbd0cccbc4",
  "groupSubjectId": "fd878510-c79a-4ed2-a127-08dbcfe1a90b"
}

{
    "userName": "Magoo",
    "email": "magoo@example.com",
    "password": "Salam123!",
    "confirmPassword": "Salam123!",
    "studentId": "20fa62ce-ab48-479d-7f01-08dbcfe2d542",
    "teacherId": "09e5499c-b8b4-403a-e987-08dbcd42b1b0"
}

{
  "userName": "Ab.Karimli",
  "email": "abdulaziz@example.com",
  "password": "Salam123@",
  "confirmPassword": "Salam123@",
  "studentId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "teacherId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
 */