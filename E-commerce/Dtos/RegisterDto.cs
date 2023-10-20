using System.ComponentModel.DataAnnotations;

namespace E_commerce.Dtos
{
    public class RegisterDto
    {
        [Required]
        public string DisplayName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        //[RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^a-zA-Z0-9]).{6,}$\r\n",
            //ErrorMessage = "Error: The password must meet the following requirements:\r\n- It should contain at least one uppercase letter.\r\n- It should contain at least one lowercase letter.\r\n- It should contain at least one numeric digit.\r\n- It should contain at least one non-alphanumeric character.\r\n- It should have a minimum length of 6 characters.")]
        public string Password { get; set; }
    }
}
