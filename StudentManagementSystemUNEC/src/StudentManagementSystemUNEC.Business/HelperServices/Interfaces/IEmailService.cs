namespace StudentManagementSystemUNEC.Business.HelperServices.Interfaces;

public interface IEmailService
{
    void Send(string to, string subject, string body);
}