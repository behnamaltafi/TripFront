// Interfaces
public class AuthResponse
{
    public bool Success { get; set; }
    public string Token { get; set; }
    public string UserId { get; set; }
    public string Email { get; set; }
    public IEnumerable<string> Errors { get; set; }
}