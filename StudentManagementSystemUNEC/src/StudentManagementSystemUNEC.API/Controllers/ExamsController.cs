using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystemUNEC.Business.DTOs.CommonDTOs;
using StudentManagementSystemUNEC.Business.DTOs.ExamDTOs;
using StudentManagementSystemUNEC.Business.Services.Interfaces;
using System.Net;

namespace StudentManagementSystemUNEC.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExamsController : ControllerBase
{
    private readonly IExamService _examService;

    public ExamsController(IExamService examService)
    {
        _examService = examService;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> GetExams([FromQuery] string? search)
    {
        var exams = await _examService.GetAllExamsAsync(search);
        return StatusCode((int)HttpStatusCode.OK, exams);
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> GetExamById(Guid Id)
    {
        var exam = await _examService.GetExamByIdAsync(Id);
        return StatusCode((int)HttpStatusCode.OK, exam);
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> CreateExam(ExamPostDTO examPostDTO)
    {
        await _examService.CreateExamAsync(examPostDTO);
        return StatusCode((int)HttpStatusCode.Created, new ResponseDTO(HttpStatusCode.Created, "Exam has been successfully created!"));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateExam(Guid Id, ExamPutDTO examPutDTO)
    {
        await _examService.UpdateExamAsync(Id, examPutDTO);
        return StatusCode((int)HttpStatusCode.OK, new ResponseDTO(HttpStatusCode.OK, "Exam has been successfully updated!"));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteExam(Guid Id)
    {
        await _examService.DeleteExamAsync(Id);
        return StatusCode((int)HttpStatusCode.OK, new ResponseDTO(HttpStatusCode.OK, "Exam has been successfully deleted!"));
    }
}