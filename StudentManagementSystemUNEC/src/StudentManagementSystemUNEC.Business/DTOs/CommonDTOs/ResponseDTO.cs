using System.Net;

namespace StudentManagementSystemUNEC.Business.DTOs.CommonDTOs;

public record ResponseDTO(HttpStatusCode StatusCode, string Message);