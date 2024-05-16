using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage ="New Password is Required")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "New Confirm Password is Required")]
        [DataType(DataType.Password)]
        [Compare("NewPassword",ErrorMessage ="Two Passwords Does't match")]
        public string ConfirmNewPassword { get; set;}

    }
}
