using Microsoft.AspNetCore.Mvc;
using StudentManagementSystemUNEC.Business.DTOs.CommonDTOs;
using StudentManagementSystemUNEC.Business.DTOs.ExampTypeDTOs;
using StudentManagementSystemUNEC.Business.Services.Interfaces;
using System.Net;

namespace StudentManagementSystemUNEC.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExamTypesController : ControllerBase
{
    private readonly IExamTypeService _examTypeService;

    public ExamTypesController(IExamTypeService examTypeService)
    {
        _examTypeService = examTypeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetExamTypes([FromQuery] string? search)
    {
        var examTypes = await _examTypeService.GetAllExamTypesAsync(search);
        return StatusCode((int)HttpStatusCode.OK, examTypes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetExamTypeById(Guid id)
    {
        var examType = await _examTypeService.GetExamTypeByIdAsync(id);
        return StatusCode((int)HttpStatusCode.OK, examType);
    }

    [HttpPost]
    public async Task<IActionResult> CreateExamType(ExamTypePostDTO examTypePostDTO)
    {
        await _examTypeService.CreateExamTypeAsync(examTypePostDTO);
        return StatusCode((int)HttpStatusCode.Created, new ResponseDTO(HttpStatusCode.Created, "Exam type successfully created"));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateExamType(Guid id, ExamTypePutDTO examTypePutDTO)
    {
        await _examTypeService.UpdateExamTypeAsync(id, examTypePutDTO);
        return StatusCode((int)HttpStatusCode.OK, new ResponseDTO(HttpStatusCode.OK, "Exam type has been successfully updated!"));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExamType(Guid id)
    {
        await _examTypeService.DeleteExamTypeAsync(id);
        return StatusCode((int)HttpStatusCode.OK, new ResponseDTO(HttpStatusCode.OK, "Exam type role has been successfully deleted!"));
    }
}