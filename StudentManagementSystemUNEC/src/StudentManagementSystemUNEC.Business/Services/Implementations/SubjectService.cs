using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystemUNEC.Business.DTOs.SubjectDTOs;
using StudentManagementSystemUNEC.Business.Exceptions.SubjectExceptions;
using StudentManagementSystemUNEC.Business.Services.Interfaces;
using StudentManagementSystemUNEC.Core.Entities;
using StudentManagementSystemUNEC.DataAccess.Contexts;
using StudentManagementSystemUNEC.DataAccess.Repositories.Interfaces;

namespace StudentManagementSystemUNEC.Business.Services.Implementations;

public class SubjectService : ISubjectService
{
    private readonly AppDbContext _context;
    private readonly ISubjectRepository _subjectRepository;
    private readonly IMapper _mapper;

    public SubjectService(ISubjectRepository subjectRepository, IMapper mapper, AppDbContext context = null)
    {
        _subjectRepository = subjectRepository;
        _mapper = mapper;
        _context = context;
    }

    public async Task<List<SubjectGetDTO>> GetAllSubjectsAsync(string? search)
    {
        try
        {
            //var subjects = await _subjectRepository.GetAll().ToListAsync();
            var subjects = await _subjectRepository.GetFiltered(s => search != null ? s.Name.Contains(search) : true).ToListAsync();
            var subjectGetDTOs = _mapper.Map<List<SubjectGetDTO>>(subjects);
            return subjectGetDTOs;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<SubjectGetDTO> GetSubjectByIdAsync(Guid Id)
    {
        var subject = await _subjectRepository.GetSingleAsync(s => s.Id == Id);

        if (subject is null)
            throw new SubjectNotFoundByIdException($"Subject not found by id: {Id}");

        var subjectGTO = _mapper.Map<SubjectGetDTO>(subject);
        return subjectGTO;
    }
    public async Task CreateSubjectAsync(SubjectPostDTO subjectPostDTO)
    {
        var existingSubject = await _context.Subjects.SingleOrDefaultAsync(s =>
            s.Name == subjectPostDTO.Name || s.subjectCode == subjectPostDTO.subjectCode);

        if (existingSubject is not null)
            throw new SubjectIsAlreadyExistException($"Subject by name {subjectPostDTO.Name} already exsists with {subjectPostDTO.subjectCode} subjest code");

        var subject = _mapper.Map<Subject>(subjectPostDTO);
        await _subjectRepository.CreateAsync(subject);
        await _subjectRepository.SaveAsync();
    }

    public async Task UpdateSubjectAsync(Guid Id, SubjectPutDTO subjectPutDTO)
    {
        var subject = await _subjectRepository.GetSingleAsync(s => s.Id == Id);

        if (subject is null)
            throw new SubjectNotFoundByIdException($"Subject not found by id: {Id}");

        subject = _mapper.Map(subjectPutDTO, subject);
        _subjectRepository.Update(subject);
        await _subjectRepository.SaveAsync();
    }

    public async Task DeleteSubjectAsync(Guid Id)
    {
        var subject = await _subjectRepository.GetSingleAsync(s => s.Id == Id);

        if (subject is null)
            throw new SubjectNotFoundByIdException($"Subject not found by id: {Id}");

        _subjectRepository.Delete(subject);
        await _subjectRepository.SaveAsync();
    }

    public async Task<bool> CheckSubjectExistsByIdAsync(Guid Id)
    {
        return await _subjectRepository.isExsistAsync(s => s.Id == Id);
    }
}
