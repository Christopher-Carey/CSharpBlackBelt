using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeltExam.Models
{
    public class User
    {
        [Key]
        public int UserId {get;set;}
        [Required]
        [MinLength(2, ErrorMessage = "Must Be Longer then 2 Charecters")]
        [Display(Name="First Name")]
        public string FirstName {get;set;}
        [Required]
        [MinLength(2, ErrorMessage = "Must Be Longer then 2 Charecters")]
        [Display(Name="Last Name")]
        public string LastName {get;set;}
        [Required]
        [EmailAddress]
        public string Email {get;set;}

        [Required]
        [MinLength(8, ErrorMessage = "Must Be Longer then 8 Charecters")]
        [DataType(DataType.Password)]
        public string Password {get;set;}

        [NotMapped]
        [Compare("Password", ErrorMessage = "Passwords Must Match")]
        [DataType(DataType.Password)]
        public string Confirm {get;set;}

        public List<Association> MyOutings {get;set;}


    }
}