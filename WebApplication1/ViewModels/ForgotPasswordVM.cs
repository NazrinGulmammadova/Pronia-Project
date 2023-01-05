using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModels;

public class ForgotPasswordVM
{
    [Required, MaxLength(256), DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    [Required,DataType(DataType.Password)]
    public string NewPassword { get; set; }
    [Required, DataType(DataType.Password),Compare(nameof(NewPassword))]
    public string ConfirmNewPassword { get; set; }
}
