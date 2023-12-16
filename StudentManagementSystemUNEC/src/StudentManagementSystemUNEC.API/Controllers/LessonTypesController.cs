using Microsoft.AspNetCore.Mvc;
using StudentManagementSystemUNEC.Business.DTOs.CommonDTOs;
using StudentManagementSystemUNEC.Business.DTOs.LessonTypeDTOs;
using StudentManagementSystemUNEC.Business.Services.Interfaces;
using System.Net;

namespace StudentManagementSystemUNEC.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LessonTypesController : ControllerBase
{
    private readonly ILessonTypeService _lessonTypeService;

    public LessonTypesController(ILessonTypeService lessonTypeService)
    {
        _lessonTypeService = lessonTypeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetLessonTypes([FromQuery] string? search)
    {
        var lessonTypes = await _lessonTypeService.GetAllLessonTypesAsync(search);
        return StatusCode((int)HttpStatusCode.OK, lessonTypes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetLessonTypeById(Guid id)
    {
        var lessonType = await _lessonTypeService.GetLessonTypeByIdAsync(id);
        return StatusCode((int)HttpStatusCode.OK, lessonType);
    }

    [HttpPost]
    public async Task<IActionResult> CreateLessonType(LessonTypePostDTO lessonTypePostDTO)
    {
        await _lessonTypeService.CreateLessonTypeAsync(lessonTypePostDTO);
        return StatusCode((int)HttpStatusCode.Created, new ResponseDTO(HttpStatusCode.Created, "Lesson type successfully created"));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLessonType(Guid id, LessonTypePutDTO lessonTypePutDTO)
    {
        await _lessonTypeService.UpdateLessonTypeAsync(id, lessonTypePutDTO);
        return StatusCode((int)HttpStatusCode.OK, new ResponseDTO(HttpStatusCode.OK, "Lesson type has been successfully updated!"));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteLessonType(Guid id)
    {
        await _lessonTypeService.DeleteLessonTypeAsync(id);
        return StatusCode((int)HttpStatusCode.OK, new ResponseDTO(HttpStatusCode.OK, "Lesson type has been successfully deleted!"));
    }
}