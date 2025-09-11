public class UpdateUserModel
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? PasswordHash { get; set; } // opsiyonel
    public string Role { get; set; } = "Student";
}
