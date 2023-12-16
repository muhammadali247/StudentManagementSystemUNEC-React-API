using Microsoft.AspNetCore.Mvc;
using StudentManagementSystemUNEC.Business.DTOs.CommonDTOs;
using StudentManagementSystemUNEC.Business.DTOs.StudentDTOs;
using StudentManagementSystemUNEC.Business.DTOs.SubjectDTOs;
using StudentManagementSystemUNEC.Business.Services.Interfaces;
using System.Net;

namespace StudentManagementSystemUNEC.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SubjectsController : ControllerBase
{
    private readonly ISubjectService _subjectService;

    public SubjectsController(ISubjectService subjectService)
    {
        _subjectService = subjectService;
    }

    [HttpGet]
    public async Task<IActionResult> GetSubjects([FromQuery] string? search)
    {
        var subjects = await _subjectService.GetAllSubjectsAsync(search);
        return StatusCode((int)HttpStatusCode.OK, subjects);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSubjectById(Guid id)
    {
        var subject = await _subjectService.GetSubjectByIdAsync(id);
        return StatusCode((int)HttpStatusCode.OK, subject);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSubject(SubjectPostDTO subjectPostDTO)
    {
        await _subjectService.CreateSubjectAsync(subjectPostDTO);
        return StatusCode((int)HttpStatusCode.Created, new ResponseDTO(HttpStatusCode.Created, "Subject successfully created"));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSubject(Guid id, SubjectPutDTO subjectPutDTO)
    {
        await _subjectService.UpdateSubjectAsync(id, subjectPutDTO);
        return StatusCode((int)HttpStatusCode.OK, new ResponseDTO(HttpStatusCode.OK, "Subject has been successfully updated!"));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteSubject(Guid id)
    {
        await _subjectService.DeleteSubjectAsync(id);
        return StatusCode((int)HttpStatusCode.OK, new ResponseDTO(HttpStatusCode.OK, "Subject has been successfully deleted!"));
    }
}
