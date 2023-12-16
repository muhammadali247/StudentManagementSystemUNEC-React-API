using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystemUNEC.Business.DTOs.FacultyDTOs;
using StudentManagementSystemUNEC.Business.Exceptions.FacultyExceptions;
using StudentManagementSystemUNEC.Business.Services.Interfaces;
using StudentManagementSystemUNEC.Core.Entities;
using StudentManagementSystemUNEC.DataAccess.Repositories.Interfaces;

namespace StudentManagementSystemUNEC.Business.Services.Implementations;

public class FacultyService : IFacultyService
{
    private readonly IFacultyRepository _facultyRepository;
    private readonly IMapper _mapper;

    public FacultyService(IFacultyRepository facultyRepository, IMapper mapper)
    {
        _facultyRepository = facultyRepository;
        _mapper = mapper;
    }

    public async Task<List<FacultyGetDTO>> GetAllFacultiesAsync(string? search)
    {
        var faculties = await _facultyRepository.GetFiltered(f => search != null ? f.Name.Contains(search) : true, isTracking: true, "Groups").ToListAsync();

        var facultyGetDTOs = _mapper.Map<List<FacultyGetDTO>>(faculties);

        return facultyGetDTOs;
    }

    public async Task<FacultyGetDTO> GetFacultyByIdAsync(Guid Id)
    {
        var faculty = await _facultyRepository.GetSingleAsync(f => f.Id == Id, isTracking: true, "Groups");

        if (faculty is null)
            throw new FacultyNotFoundByIdException($"Faculty not found by Id:{Id}");

        var facultyGetDTO = _mapper.Map<FacultyGetDTO>(faculty);

        return facultyGetDTO;
    }

    public async Task CreateFacultyAsync(FacultyPostDTO facultyPostDTO)
    {
        if (facultyPostDTO is null)
        {
            throw new ArgumentNullException(nameof(facultyPostDTO), "facultyPostDTO is null");
        }

        var faculty = _mapper.Map<Faculty>(facultyPostDTO);

        // Check for null references in the student object properties
        if (faculty is null)
        {
            throw new Exception("Mapped group object is null");
        }

        await _facultyRepository.CreateAsync(faculty);
        await _facultyRepository.SaveAsync();
    }

    public async Task UpdateFacultyAsync(Guid Id, FacultyPutDTO facultyPutDTO)
    {
        var faculty = await _facultyRepository.GetSingleAsync(f => f.Id == Id);

        if (faculty is null)
            throw new FacultyNotFoundByIdException($"Faculty not found by Id:{Id}");

        faculty = _mapper.Map(facultyPutDTO, faculty);

        _facultyRepository.Update(faculty);
        await _facultyRepository.SaveAsync();
    }

    public async Task DeleteFacultyAsync(Guid Id)
    {
        var faculty = await _facultyRepository.GetSingleAsync(f => f.Id == Id);

        if (faculty is null)
            throw new FacultyNotFoundByIdException($"Faculty not found by Id:{Id}");

        _facultyRepository.Delete(faculty);
        await _facultyRepository.SaveAsync();
    }

    public async Task<bool> CheckFacultyExistsByIdAsync(Guid Id)
    {
        return await _facultyRepository.isExsistAsync(f => f.Id == Id);
    }
}
