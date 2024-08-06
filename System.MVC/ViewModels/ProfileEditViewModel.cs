using System.ComponentModel.DataAnnotations;

namespace System.MVC.ViewModels
{
    public class ProfileEditViewModel
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }


        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }



    }
}
