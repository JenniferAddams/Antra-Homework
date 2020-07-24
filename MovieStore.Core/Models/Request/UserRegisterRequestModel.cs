using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MovieStore.Core.Models.Request
{
    public class UserRegisterRequestModel
    {
        //DataAnnotation are userful for validation in Asp.Net Core/framwork
        [Required]
        [EmailAddress]
        [StringLength(59)]
        public string Email { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        [StringLength(20,ErrorMessage ="make sure password length is between 8 and 20",MinimumLength =8)]
        //password should be string(at least one upper one lower one num and a special char
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,}$", ErrorMessage = "Password Should have minimum 8 with at least one upper, lower, number and special character")]
        public string Password { get; set; }
    }
}
