using Microsoft.AspNetCore.Mvc;
using StudentManagementSystemUNEC.Business.DTOs.CommonDTOs;
using StudentManagementSystemUNEC.Business.DTOs.ExamResultDTOs;
using StudentManagementSystemUNEC.Business.Services.Interfaces;
using System.Net;

namespace StudentManagementSystemUNEC.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExamResultsController : ControllerBase
{
    private readonly IExamResultService _examResultService;

    public ExamResultsController(IExamResultService examResultService)
    {
        _examResultService = examResultService;
    }

    [HttpGet]
    public async Task<IActionResult> GetExamResults([FromQuery] string? search)
    {
        var examResults = await _examResultService.GetAllExamResultsAsync(search);
        return StatusCode((int)HttpStatusCode.OK, examResults);
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> GetExamResultById(Guid Id)
    {
        var examResult = await _examResultService.GetExamResultByIdAsync(Id);
        return StatusCode((int)HttpStatusCode.OK, examResult);
    }

    [HttpPost]
    public async Task<IActionResult> CreateExamResult(ExamResultPostDTO examResultPostDTO)
    {
        await _examResultService.CreateExamResultAsync(examResultPostDTO);
        return StatusCode((int)HttpStatusCode.Created, new ResponseDTO(HttpStatusCode.Created, "Exam result has been successfully created!"));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateExamResult(Guid Id, ExamResultPutDTO examResultPutDTO)
    {
        await _examResultService.UpdateExamResultAsync(Id, examResultPutDTO);
        return StatusCode((int)HttpStatusCode.OK, new ResponseDTO(HttpStatusCode.OK, "Exam result has been successfully updated!"));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteExamResult(Guid Id)
    {
        await _examResultService.DeleteExamResultAsync(Id);
        return StatusCode((int)HttpStatusCode.OK, new ResponseDTO(HttpStatusCode.OK, "Exam result has been successfully deleted!"));
    }
}