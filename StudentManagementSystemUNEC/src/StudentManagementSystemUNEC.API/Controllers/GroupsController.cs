using Microsoft.AspNetCore.Mvc;
using StudentManagementSystemUNEC.Business.DTOs.CommonDTOs;
using StudentManagementSystemUNEC.Business.DTOs.GroupDTOs;
using StudentManagementSystemUNEC.Business.Services.Interfaces;
using System.Net;

namespace StudentManagementSystemUNEC.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GroupsController : ControllerBase
{
    private readonly IGroupService _groupService;

    public GroupsController(IGroupService groupService)
    {
        _groupService = groupService;
    }

    [HttpGet]
    public async Task<IActionResult> GetGroups([FromQuery] string? search)
    {
        var groups = await _groupService.GetAllGroupsAsync(search);
        return StatusCode((int)HttpStatusCode.OK, groups);
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> GetGroupById(Guid Id)
    {
        var group = await _groupService.GetGroupByIdAsync(Id);
        return StatusCode((int)HttpStatusCode.OK, group);
    }

    [HttpPost]
    public async Task<IActionResult> CreateGroup(GroupPostDTO groupPostDTO)
    {
        await _groupService.CreateGroupAsync(groupPostDTO);
        return StatusCode((int)HttpStatusCode.Created, new ResponseDTO(HttpStatusCode.Created, "Group has been successfully created!"));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateGroup(Guid Id, GroupPutDTO groupPutDTO)
    {
        await _groupService.UpdateStudentAsync(Id, groupPutDTO);
        return StatusCode((int)HttpStatusCode.OK, new ResponseDTO(HttpStatusCode.OK, "Group has been successfully updated!"));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteGroup(Guid Id)
    {
        await _groupService.DeleteStudentAsync(Id);
        return StatusCode((int)HttpStatusCode.OK, new ResponseDTO(HttpStatusCode.OK, "Group has been successfully deleted!"));
    }
}
