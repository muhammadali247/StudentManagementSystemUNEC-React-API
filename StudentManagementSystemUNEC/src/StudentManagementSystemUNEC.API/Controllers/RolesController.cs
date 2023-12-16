using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystemUNEC.Business.Services.Interfaces;
using StudentManagementSystemUNEC.Core.Utils.Enums;
using System.Net;

namespace StudentManagementSystemUNEC.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RolesController : ControllerBase
{
    private readonly IRoleService _roleService;
    public RolesController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet]
    //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Moderator")]
    public IActionResult GetAllRoles()
    {
        var roles = _roleService.GetRoles();
        return StatusCode((int)HttpStatusCode.OK, roles);
    }
}