using Microsoft.AspNetCore.Mvc;
using StudentManagementSystemUNEC.Business.DTOs.CommonDTOs;
using StudentManagementSystemUNEC.Business.DTOs.FacultyDTOs;
using StudentManagementSystemUNEC.Business.Services.Interfaces;
using System.Net;

namespace StudentManagementSystemUNEC.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FacultiesController : ControllerBase
{
    private readonly IFacultyService _facultyService;

    public FacultiesController(IFacultyService facultyService)
    {
        _facultyService = facultyService;
    }

    [HttpGet]
    public async Task<IActionResult> GetFaculties([FromQuery] string? search)
    {
        var faculties = await _facultyService.GetAllFacultiesAsync(search);
        return StatusCode((int)HttpStatusCode.OK, faculties);
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> GetFacultyById(Guid Id)
    {
        var faculty = await _facultyService.GetFacultyByIdAsync(Id);
        return StatusCode((int)HttpStatusCode.OK, faculty);
    }

    [HttpPost]
    public async Task<IActionResult> CreateFaculty(FacultyPostDTO facultyPostDTO)
    {
        await _facultyService.CreateFacultyAsync(facultyPostDTO);
        return StatusCode((int)HttpStatusCode.Created, new ResponseDTO(HttpStatusCode.Created, "Faculty has been successfully created!"));
    }

    [HttpPut("{Id}")]
    public async Task<IActionResult> UpdateFaculty(Guid Id, FacultyPutDTO facultyPutDTO)
    {
        await _facultyService.UpdateFacultyAsync(Id, facultyPutDTO);
        return StatusCode((int)HttpStatusCode.OK, new ResponseDTO(HttpStatusCode.OK, "Faculty has been successfully updated!"));
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> DeleteFaculty(Guid Id)
    {
        await _facultyService.DeleteFacultyAsync(Id);
        return StatusCode((int)HttpStatusCode.OK, new ResponseDTO(HttpStatusCode.OK, "Faculty has been successfully deleted!"));
    }
}
