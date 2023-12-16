using Microsoft.AspNetCore.Mvc;
using StudentManagementSystemUNEC.Business.DTOs.CommonDTOs;
using StudentManagementSystemUNEC.Business.DTOs.TeacherRoleDtos;
using StudentManagementSystemUNEC.Business.Services.Interfaces;
using System.Net;

namespace StudentManagementSystemUNEC.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TeacherRolesController : ControllerBase
{
    private readonly ITeacherRoleService _teacherRoleService;

    public TeacherRolesController(ITeacherRoleService teacherRoleService)
    {
        _teacherRoleService = teacherRoleService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTeacherRoles([FromQuery] string? search)
    {
        var teacherRoles = await _teacherRoleService.GetAllTeacherRolesAsync(search);
        return StatusCode((int)HttpStatusCode.OK, teacherRoles);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTeacherRoleById(Guid id)
    {
        var teacherRole = await _teacherRoleService.GetTeacherRoleByIdAsync(id);
        return StatusCode((int)HttpStatusCode.OK, teacherRole);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTeacherRole(TeacherRolePostDTO teacherRolePostDTO)
    {
        await _teacherRoleService.CreateTeacherRoleAsync(teacherRolePostDTO);
        return StatusCode((int)HttpStatusCode.Created, new ResponseDTO(HttpStatusCode.Created, "Teacher role successfully created"));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTeacherRole(Guid id, TeacherRolePutDTO teacherRolePutDTO)
    {
        await _teacherRoleService.UpdateTeacherRoleAsync(id, teacherRolePutDTO);
        return StatusCode((int)HttpStatusCode.OK, new ResponseDTO(HttpStatusCode.OK, "Teacher role has been successfully updated!"));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTeacherRole(Guid id)
    {
        await _teacherRoleService.DeleteTeacherRoleAsync(id);
        return StatusCode((int)HttpStatusCode.OK, new ResponseDTO(HttpStatusCode.OK, "Teacher role has been successfully deleted!"));
    }
}