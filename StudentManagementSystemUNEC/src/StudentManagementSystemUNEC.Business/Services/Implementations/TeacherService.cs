using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystemUNEC.Business.DTOs.TeacherDTOs;
using StudentManagementSystemUNEC.Business.Exceptions.TeacherExceptions;
using StudentManagementSystemUNEC.Business.Exceptions.UserExceptions;
using StudentManagementSystemUNEC.Business.HelperServices.Interfaces;
using StudentManagementSystemUNEC.Business.Services.Interfaces;
using StudentManagementSystemUNEC.Core.Entities;
using StudentManagementSystemUNEC.DataAccess.Contexts;
using StudentManagementSystemUNEC.DataAccess.Repositories.Interfaces;

namespace StudentManagementSystemUNEC.Business.Services.Implementations;

public class TeacherService : ITeacherService
{
    private readonly AppDbContext _context;
    private readonly ITeacherRepository _teacherRepository;
    private readonly IMapper _mapper;
    private readonly IFileService _fileService;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public TeacherService(ITeacherRepository teacherRepository, IMapper mapper, AppDbContext context, IWebHostEnvironment webHostEnvironment, IFileService fileService)
    {
        _teacherRepository = teacherRepository;
        _mapper = mapper;
        _context = context;
        _webHostEnvironment = webHostEnvironment;
        _fileService = fileService;
    }

    public async Task<List<TeacherGetDTO>> GetAllTeachersAsync(string? search)
    {
        var teachers = await _teacherRepository.GetFiltered(t => search != null ? t.Name.Contains(search) : true, isTracking: true, "AppUser", "teacherSubjects.GroupSubject.Group.Faculty", "teacherSubjects.GroupSubject.Subject", "teacherSubjects.TeacherRole").ToListAsync();
        var teacherGetDTOs = _mapper.Map<List<TeacherGetDTO>>(teachers);
        return teacherGetDTOs;
    }

    public async Task<TeacherGetDTO> GetTeacherByIdAsync(Guid Id)
    {
        var teacher = await _teacherRepository.GetSingleAsync(t => t.Id == Id, isTracking: true, "AppUser", "teacherSubjects.GroupSubject.Group.Faculty", "teacherSubjects.GroupSubject.Subject", "teacherSubjects.TeacherRole");

        if (teacher is null)
            throw new TeacherNotFoundByIdException($"Teacher not found by id: {Id}");

        var teacherGetDTO = _mapper.Map<TeacherGetDTO>(teacher);
        return teacherGetDTO;
    }

    public async Task CreateTeacherAsync(TeacherPostDTO teacherPostDTO)
    {
        try
        {
            if (teacherPostDTO?.AppUserId is not null)
            {
                var user = await _context.Users.Include(u => u.Teacher).Include(u => u.Student).FirstOrDefaultAsync(u => u.Id == teacherPostDTO.AppUserId);
                if (user is null)
                {
                    throw new UserNotFoundByIdException("User not found");
                }
                if (user.Teacher is not null)
                {
                    throw new UserAlreadyHasTeacherException("User is already taken");
                }
                if (user.Student is not null)
                {
                    throw new UserCannotBeStudentAndTeacherException("User  already belongs to the Student ");
                }

            }

            var teacher = _mapper.Map<Teacher>(teacherPostDTO);

            if (teacherPostDTO is not null && teacherPostDTO?.Image is not null)
            {
                string image = await _fileService.FileUploadAsync(teacherPostDTO.Image, Path.Combine("assets", "uploads", "images", "teacher-images"), "image/", 300);
                teacher.Image = image;
            }

            await _teacherRepository.CreateAsync(teacher);
            await _teacherRepository.SaveAsync();
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task UpdateTeacherAsync(Guid Id, TeacherPutDTO teacherPutDTO)
    {
        var teacher = await _teacherRepository.GetSingleAsync(t => t.Id == Id);

        if (teacher is null)
            throw new TeacherNotFoundByIdException($"Teacher not found by id: {Id}");


        string fileName = teacher.Image;
        if (teacherPutDTO.Image is not null)
        {
            string deleteImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "uploads", "images", "teacher-images");
            _fileService.DeleteFile(Path.Combine(deleteImagePath, teacher.Image));

            fileName = await _fileService.FileUploadAsync(teacherPutDTO.Image, Path.Combine("assets", "uploads", "images", "teacher-images"), "image/", 300);
        }

        //if (teacherPutDTO.AppUserId is not null)
        //{
        //    var user = await _context.Users.Include(u => u.Teacher).Include(u => u.Student).FirstOrDefaultAsync(u => u.Id == teacherPutDTO.AppUserId);

        //    if (user is null)
        //    {
        //        throw new UserNotFoundByIdException("User not found");
        //    }
        //    if (user.Teacher is not null && user.Teacher.Id != teacher.Id)
        //    {
        //        throw new UserAlreadyHasTeacherException("User is already taken");
        //    }
        //    if (user.Student is not null)
        //    {
        //        throw new UserCannotBeStudentAndTeacherException("User  already belongs to the Student ");

        //    }
        //}

        teacher = _mapper.Map(teacherPutDTO, teacher);
        teacher.Image = fileName;

        _teacherRepository.Update(teacher);
        await _teacherRepository.SaveAsync();
    }

    public async Task DeleteTeacherAsync(Guid Id)
    {
        var teacher = await _teacherRepository.GetSingleAsync(t => t.Id == Id);

        if (teacher is null)
            throw new TeacherNotFoundByIdException($"Teacher not found by id: {Id}");

        string deleteImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "uploads", "images", "teacher-images");
        _fileService.DeleteFile(Path.Combine(deleteImagePath, teacher.Image));

        _teacherRepository.Delete(teacher);
        _teacherRepository.Update(teacher);
        await _teacherRepository.SaveAsync();
    }

    public async Task<bool> CheckTeacherExistsByIdAsync(Guid Id)
    {
        return await _teacherRepository.isExsistAsync(t => t.Id == Id);
    }
}