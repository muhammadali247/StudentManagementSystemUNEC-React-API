using Microsoft.AspNetCore.Mvc;
using StudentManagementSystemUNEC.Business.DTOs.CommonDTOs;
using StudentManagementSystemUNEC.Business.DTOs.StudentDTOs;
using StudentManagementSystemUNEC.Business.Services.Interfaces;
using System.Net;

namespace StudentManagementSystemUNEC.API.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentsController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetStudents([FromQuery] string? search)
    {
        var students = await _studentService.GetAllStudentsAsync(search);
        return StatusCode((int)HttpStatusCode.OK, students);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetStudentById(Guid id)
    {
        var student = await _studentService.GetStudentByIdAsync(id);
        return StatusCode((int)HttpStatusCode.OK, student);
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> CreateStudent([FromForm] StudentPostDTO studentPostDTO)
    {
        await _studentService.CreateStudentAsync(studentPostDTO);
        return StatusCode((int)HttpStatusCode.Created, new ResponseDTO(HttpStatusCode.Created, "Student successfully created"));
    }

    [HttpPut("{id}")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UpdateStudent(Guid id, [FromForm] StudentPutDTO studentPutDTO)
    {
        await _studentService.UpdateStudentAsync(id, studentPutDTO);
        return StatusCode((int)HttpStatusCode.OK, new ResponseDTO(HttpStatusCode.OK, "Student has been successfully updated!"));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteStudent(Guid id)
    {
        await _studentService.DeleteStudentAsync(id);
        return StatusCode((int)HttpStatusCode.OK, new ResponseDTO(HttpStatusCode.OK, "Student has been successfully deleted!"));
    }
}
