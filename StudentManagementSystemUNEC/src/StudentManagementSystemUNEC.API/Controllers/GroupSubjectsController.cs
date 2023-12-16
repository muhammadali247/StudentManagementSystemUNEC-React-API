using Microsoft.AspNetCore.Mvc;
using StudentManagementSystemUNEC.Business.DTOs.CommonDTOs;
using StudentManagementSystemUNEC.Business.DTOs.GroupSubjectDTOs;
using StudentManagementSystemUNEC.Business.Services.Interfaces;
using System.Net;

namespace StudentManagementSystemUNEC.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GroupSubjectsController : ControllerBase
{
    private readonly IGroupSubjectService _groupSubjectService;

    public GroupSubjectsController(IGroupSubjectService groupSubjectService)
    {
        _groupSubjectService = groupSubjectService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllGroupSubjects()
    {
        var groupSubjects = await _groupSubjectService.GetAllGroupSubjectsAsync();
        return Ok(groupSubjects);
    }

    [HttpPost]
    public async Task<IActionResult> CreateGroupSubject(GroupSubjectPostDTO groupSubjectPostDTO)
    {
        await _groupSubjectService.CreateGroupSubjectAsync(groupSubjectPostDTO);
        return StatusCode((int)HttpStatusCode.Created, new ResponseDTO(HttpStatusCode.Created, "Subject for group successfully created"));

    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> GetGroupSubjectById(Guid Id)
    {
        var groupSubject = await _groupSubjectService.GetGroupSubjectByIdAsync(Id);
        return Ok(groupSubject);
    }

    [HttpPut("{Id}")]
    public async Task<IActionResult> UpdateGroupSubject(Guid Id, GroupSubjectPutDTO groupSubjectPutDTO)
    {
        await _groupSubjectService.UpdateGroupSubjectAsync(Id, groupSubjectPutDTO);
        return StatusCode((int)HttpStatusCode.OK, new ResponseDTO(HttpStatusCode.OK, "Groups subject updated successfully"));
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> DeleteGroupSubject(Guid Id)
    {
        await _groupSubjectService.DeleteGroupSubjectAsync(Id);
        return StatusCode((int)HttpStatusCode.OK, new ResponseDTO(HttpStatusCode.OK, "Group subject deleted successefully"));
    }
}
