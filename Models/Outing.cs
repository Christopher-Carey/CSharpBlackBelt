using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeltExam.Models
{
    public class Outing
    {
        [Key]
        public int OutingId {get;set;}
        [Required(ErrorMessage = "Required")]
        public string Title {get;set;}

        public TimeSpan Time {get;set;}

        [DataType(DataType.Date)]
        public DateTime Date {get;set;}
        
        public int Duration {get;set;}
        public string DurationType {get;set;}
        
        [Required(ErrorMessage = "Required")]
        public string Description {get;set;}
        public DateTime CreatedAt {get;set;}=DateTime.Now;
        public DateTime UpdatedAt {get;set;}=DateTime.Now;
        public int UserId {get;set;}

        public User Creator {get;set;}

        public List<Association> OutingUsers {get;set;}

        
    }
}