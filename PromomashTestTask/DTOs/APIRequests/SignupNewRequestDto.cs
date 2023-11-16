#pragma warning disable CS8618
namespace PromomashTestTask.DTOs.APIRequests;

public class SignupNewRequestDto
{
    public string Login { get; set; }
    public string Password { get; set; }
    public bool PolicyAccepted { get; set; }
    public Guid ProvinceId { get; set; }
}