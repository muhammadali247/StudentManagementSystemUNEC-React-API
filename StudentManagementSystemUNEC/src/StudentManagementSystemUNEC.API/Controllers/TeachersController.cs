using Microsoft.AspNetCore.Mvc;
using StudentManagementSystemUNEC.Business.DTOs.CommonDTOs;
using StudentManagementSystemUNEC.Business.DTOs.TeacherDTOs;
using StudentManagementSystemUNEC.Business.Services.Interfaces;
using System.Net;

namespace StudentManagementSystemUNEC.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TeachersController : ControllerBase
{
    private readonly ITeacherService _teacherService;

    public TeachersController(ITeacherService teacherService)
    {
        _teacherService = teacherService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTeachers([FromQuery] string? search)
    {
        var teachers = await _teacherService.GetAllTeachersAsync(search);
        return StatusCode((int)HttpStatusCode.OK, teachers);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTeacherById(Guid id)
    {
        var teacher = await _teacherService.GetTeacherByIdAsync(id);
        return StatusCode((int)HttpStatusCode.OK, teacher);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTeacher([FromForm] TeacherPostDTO teacherPostDTO)
    {
        await _teacherService.CreateTeacherAsync(teacherPostDTO);
        return StatusCode((int)HttpStatusCode.Created, new ResponseDTO(HttpStatusCode.Created, "Teacher successfully created"));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTeacher([FromForm] Guid id, TeacherPutDTO teacherPutDTO)
    {
        await _teacherService.UpdateTeacherAsync(id, teacherPutDTO);
        return StatusCode((int)HttpStatusCode.OK, new ResponseDTO(HttpStatusCode.OK, "Teacher has been successfully updated!"));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteTeacher(Guid id)
    {
        await _teacherService.DeleteTeacherAsync(id);
        return StatusCode((int)HttpStatusCode.OK, new ResponseDTO(HttpStatusCode.OK, "Teacher has been successfully deleted!"));
    }
}
