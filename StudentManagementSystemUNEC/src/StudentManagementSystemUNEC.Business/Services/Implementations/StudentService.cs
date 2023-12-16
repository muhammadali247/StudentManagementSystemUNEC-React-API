using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystemUNEC.Business.DTOs.StudentDTOs;
using StudentManagementSystemUNEC.Business.Exceptions.GroupExceptions;
using StudentManagementSystemUNEC.Business.Exceptions.StudentExceptions;
using StudentManagementSystemUNEC.Business.Exceptions.UserExceptions;
using StudentManagementSystemUNEC.Business.HelperServices.Interfaces;
using StudentManagementSystemUNEC.Business.Services.Interfaces;
using StudentManagementSystemUNEC.Core.Entities;
using StudentManagementSystemUNEC.Core.Entities.Identity;
using StudentManagementSystemUNEC.DataAccess.Contexts;
using StudentManagementSystemUNEC.DataAccess.Repositories.Interfaces;

namespace StudentManagementSystemUNEC.Business.Services.Implementations;

public class StudentService : IStudentService
{
    private readonly AppDbContext _context;
    private readonly IStudentRepository _studentRepository;
    private readonly IStudentGroupRepository _studentGroupRepository;
    private readonly IGroupRepository _groupRepository;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IFileService _fileService;
    private readonly UserManager<AppUser> _userManager;

    public StudentService(AppDbContext context, IStudentRepository studentRepository, IMapper mapper, IGroupRepository groupRepository, IStudentGroupRepository studentGroupRepository, IFileService fileService, IWebHostEnvironment webHostEnvironment, UserManager<AppUser> userManager)
    {
        _context = context;
        _studentRepository = studentRepository;
        _studentGroupRepository = studentGroupRepository;
        _groupRepository = groupRepository;
        _mapper = mapper;
        _fileService = fileService;
        _webHostEnvironment = webHostEnvironment;
        _userManager = userManager;
    }

    public async Task<List<StudentGetDTO>> GetAllStudentsAsync(string? search)
    {
        try
        {
            var students = await _studentRepository.GetFiltered(s => search != null ? (s.Name.Contains(search) || s.Surname.Contains(search)) : true, isTracking: true, "studentGroups.Group.Faculty", "Group.Faculty", "studentGroups.Group", "examResults.Exam.ExamType", "examResults.Exam.GroupSubject.Subject", "AppUser").ToListAsync();
            //var students = await _studentRepository.GetAll().ToListAsync();

            var studentDTOs = _mapper.Map<List<StudentGetDTO>>(students);

            return studentDTOs;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }

    public async Task<StudentGetDTO> GetStudentByIdAsync(Guid Id)
    {
        var student = await _studentRepository.GetSingleAsync(s => s.Id == Id, isTracking: true, "AppUser", "studentGroups.Group.Faculty", "examResults.Exam.ExamType", "examResults.Exam.GroupSubject.Subject", "Group.Faculty");

        if (student is null)
            throw new StudentNotFoundByIdException($"Student not found by id: {Id}");

        var studentDTO = _mapper.Map<StudentGetDTO>(student);
        return studentDTO;
    }

    public async Task CreateStudentAsync(StudentPostDTO studentPostDTO)
    {
        try
        {
            if (studentPostDTO is null)
            {
                throw new ArgumentNullException(nameof(studentPostDTO), "studentPostDTO is null");
            }

            if (studentPostDTO.AppUserId is not null)
            {
                var user = await _userManager.Users.Include(u => u.Student).FirstOrDefaultAsync(u => u.Id == studentPostDTO.AppUserId);
                if (user is null)
                {
                    throw new UserNotFoundByIdException("User not found");
                }
                if (user.Student is not null)
                {
                    throw new UserAlreadyHasStudentException("User is already taken");
                }
                if (user.Teacher is not null)
                {
                    throw new UserCannotBeStudentAndTeacherException("User  already belongs to the teacher");
                }
            }

            if (studentPostDTO.MainGroup is not null && !(await _groupRepository.isExsistAsync(g => g.Id == studentPostDTO.MainGroup)))
            {
                throw new GroupNotFoundByIdException($"Main group not found by Id: {studentPostDTO.MainGroup}");
            }

            var student = _mapper.Map<Student>(studentPostDTO);
            if(studentPostDTO is not null)
            {
                string image = await _fileService.FileUploadAsync(studentPostDTO.Image, Path.Combine("assets", "uploads", "images", "student-images"), "image/", 300);
                student.Image = image;
            }

            // Check for null references in the student object properties
            if (student is null)
            {
                throw new Exception("Mapped student object is null");
            }

            if (studentPostDTO.GroupIds is not null)
            {
                List<StudentGroup> newStudentGroups = new List<StudentGroup>();
                for (int i = 0; i < studentPostDTO.GroupIds.Count; i++)
                {
                    if (!await _groupRepository.isExsistAsync(g => g.Id == studentPostDTO.GroupIds[i]))
                        throw new GroupNotFoundByIdException($"Group with Id:{studentPostDTO.GroupIds[i]} not found");

                    var studentGroup = new StudentGroup()
                    {
                        StudentId = student.Id,
                        GroupId = studentPostDTO.GroupIds[i],
                        subGroup = studentPostDTO.subGroups[i]
                    };
                    newStudentGroups.Add(studentGroup);
                }
                student.studentGroups = newStudentGroups;
            }

            await _studentRepository.CreateAsync(student);
            await _studentRepository.SaveAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    //public async Task UpdateStudentAsync(Guid Id, StudentPutDTO studentPutDTO)
    //{
    //    try
    //    {
    //        var student = await _studentRepository.GetSingleAsync(s => s.Id == Id);

    //        if (student is null)
    //            throw new StudentNotFoundByIdException($"Student not found by id: {Id}");

    //        if (studentPutDTO.AppUserId is not null)
    //        {
    //            var user = await _userManager.Users.Include(u => u.Student).FirstOrDefaultAsync(u => u.Id == studentPutDTO.AppUserId);
    //            if (user is null)
    //            {
    //                throw new UserNotFoundByIdException("User not found");
    //            }
    //            if (user.Student is not null && user.Student.Id != student.Id)
    //            {
    //                throw new UserAlreadyHasStudentException("User is already taken");
    //            }

    //        }

    //        if (studentPutDTO.MainGroup is not null && !(await _groupRepository.isExsistAsync(g => g.Id == studentPutDTO.MainGroup)))
    //        {
    //            throw new GroupNotFoundByIdException($"Main group not found by Id: {studentPutDTO.MainGroup}");
    //        }

    //        string fileName = student.Image;
    //        if (studentPutDTO.Image is not null)
    //        {
    //            string deleteImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "uploads", "images", "student-images");
    //            _fileService.DeleteFile(Path.Combine(deleteImagePath, student.Image));

    //            fileName = await _fileService.FileUploadAsync(studentPutDTO.Image, Path.Combine("assets", "uploads", "images", "student-images"), "image/", 300);
    //        }

    //        student = _mapper.Map(studentPutDTO, student);
    //        student.Image = fileName;

    //        if (studentPutDTO.GroupIds is not null)
    //        {
    //            List<StudentGroup>? groupsToRemove = student.studentGroups?.Where(sg => !studentPutDTO.GroupIds.Any(g => g == sg.GroupId)).ToList();
    //            if (groupsToRemove is not null && groupsToRemove.Count() != 0)
    //            {
    //                _studentGroupRepository.DeleteList(groupsToRemove);
    //                await _studentGroupRepository.SaveAsync();

    //            }

    //            List<Guid>? groupsToAdd = studentPutDTO.GroupIds.Where(g => !student.studentGroups.Any(sg => sg.GroupId == g)).ToList();
    //            if (groupsToAdd is not null && groupsToAdd.Count() != 0)
    //            {
    //                List<StudentGroup> newStudentGroups = new List<StudentGroup>();
    //                for (int i = 0; i < studentPutDTO.GroupIds.Count; i++)
    //                {
    //                    if (!await _groupRepository.isExsistAsync(g => g.Id == studentPutDTO.GroupIds[i]))
    //                        throw new GroupNotFoundByIdException($"Group with Id:{studentPutDTO.GroupIds[i]} not found");

    //                    var studentGroup = new StudentGroup()
    //                    {
    //                        StudentId = student.Id,
    //                        GroupId = studentPutDTO.GroupIds[i],
    //                        subGroup = studentPutDTO.subGroups[i]
    //                    };
    //                    newStudentGroups.Add(studentGroup);
    //                }
    //                _studentGroupRepository.AddList(newStudentGroups);
    //                await _studentGroupRepository.SaveAsync();
    //            }
    //        }

    //        _studentRepository.Update(student);
    //        await _studentRepository.SaveAsync();
    //    }
    //    catch (Exception)
    //    {

    //        throw;
    //    }
    //}

    public async Task UpdateStudentAsync(Guid Id, StudentPutDTO studentPutDTO)
    {
        try
        {
            var student = await _studentRepository.GetSingleAsync(s => s.Id == Id, isTracking: false, "studentGroups");

            if (student is null)
                throw new StudentNotFoundByIdException($"Student not found by id: {Id}");

            //if (studentPutDTO.AppUserId is not null)
            //{
            //    var user = await _userManager.Users.Include(u => u.Student).FirstOrDefaultAsync(u => u.Id == studentPutDTO.AppUserId);
            //    if (user is null)
            //    {
            //        throw new UserNotFoundByIdException("User not found");
            //    }
            //    if (user.Student is not null && user.Student.Id != student.Id)
            //    {
            //        throw new UserAlreadyHasStudentException("User is already taken");
            //    }
            //}

            if (studentPutDTO.MainGroup is not null && !(await _groupRepository.isExsistAsync(g => g.Id == studentPutDTO.MainGroup)))
            {
                throw new GroupNotFoundByIdException($"Main group not found by Id: {studentPutDTO.MainGroup}");
            }

            string fileName = student.Image;
            if (studentPutDTO.Image is not null)
            {
                string deleteImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "uploads", "images", "student-images");
                _fileService.DeleteFile(Path.Combine(deleteImagePath, student.Image));

                fileName = await _fileService.FileUploadAsync(studentPutDTO.Image, Path.Combine("assets", "uploads", "images", "student-images"), "image/", 300);
            }

            student = _mapper.Map(studentPutDTO, student);
            student.Image = fileName;

            if (studentPutDTO.GroupIds is not null)
            {
                if (student.studentGroups != null)
                {
                    List<StudentGroup> groupsToRemove = student.studentGroups
                        .Where(sg => !studentPutDTO.GroupIds.Any(g => g == sg.GroupId))
                        .ToList();

                    if (groupsToRemove.Count() != 0)
                    {
                        _studentGroupRepository.DeleteList(groupsToRemove);
                        await _studentGroupRepository.SaveAsync();
                    }

                    List<Guid> groupsToAdd = studentPutDTO.GroupIds
                        .Where(g => !student.studentGroups.Any(sg => sg.GroupId == g))
                        .ToList();

                    if (groupsToAdd.Count() != 0)
                    {
                        List<StudentGroup> newStudentGroups = new List<StudentGroup>();
                        for (int i = 0; i < studentPutDTO.GroupIds.Count; i++)
                        {
                            if (!await _groupRepository.isExsistAsync(g => g.Id == studentPutDTO.GroupIds[i]))
                                throw new GroupNotFoundByIdException($"Group with Id:{studentPutDTO.GroupIds[i]} not found");

                            var studentGroup = new StudentGroup()
                            {
                                StudentId = student.Id,
                                GroupId = studentPutDTO.GroupIds[i],
                                subGroup = studentPutDTO.subGroups[i]
                            };
                            newStudentGroups.Add(studentGroup);
                        }
                        _studentGroupRepository.AddList(newStudentGroups);
                        await _studentGroupRepository.SaveAsync();
                    }
                }
                else
                {
                    List<StudentGroup> newStudentGroups = new List<StudentGroup>();
                    for (int i = 0; i < studentPutDTO.GroupIds.Count; i++)
                    {
                        if (!await _groupRepository.isExsistAsync(g => g.Id == studentPutDTO.GroupIds[i]))
                            throw new GroupNotFoundByIdException($"Group with Id:{studentPutDTO.GroupIds[i]} not found");

                        var studentGroup = new StudentGroup()
                        {
                            StudentId = student.Id,
                            GroupId = studentPutDTO.GroupIds[i],
                            subGroup = studentPutDTO.subGroups[i]
                        };
                        newStudentGroups.Add(studentGroup);
                    }
                    student.studentGroups = newStudentGroups;
                }
            }

            _studentRepository.Update(student);
            await _studentRepository.SaveAsync();
        }
        catch (Exception ex)
        {
            // Handle exceptions and log the error
            Console.WriteLine(ex.Message);
            throw;
        }
    }


    public async Task DeleteStudentAsync(Guid id)
    {
        var student = await _studentRepository.GetSingleAsync(s => s.Id == id);

        string deleteImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "uploads", "images", "student-images");
        _fileService.DeleteFile(Path.Combine(deleteImagePath, student.Image));

        _studentRepository.SoftDelete(student);
        _studentRepository.Update(student);
        await _studentRepository.SaveAsync();
    }

    public async Task<bool> CheckStudentExistsByIdAsync(Guid id)
    {
        return await _studentRepository.isExsistAsync(s => s.Id == id);
    }
}