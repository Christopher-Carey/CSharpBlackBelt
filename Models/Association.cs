namespace BeltExam.Models
{
    public class Association
    {
        public int AssociationId {get;set;}
        public int UserId {get;set;}
        public int OutingId {get;set;}

        public User User {get;set;}
        public Outing Outing {get;set;}
        
    }
}