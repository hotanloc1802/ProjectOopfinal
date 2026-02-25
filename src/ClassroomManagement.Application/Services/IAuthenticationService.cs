namespace ClassroomManagement.Application.Services;

public interface IAuthenticationService
{
    /// <summary>
    /// Validates username/password and returns (StudentId, Role) if matched.
    /// </summary>
    Task<(string StudentId, string Role)?> AuthenticateAsync(string username, string password);
}

