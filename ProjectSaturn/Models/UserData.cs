namespace ProjectSaturn.Models
{
    public class UserData //This will house all of the user's data in via lists of the data's models.
    {
        public static List<Personal> PersonalList { get; set; } //Personal

        public static List<Education> EducationalList { get; set; } //Education Experience
        
        public static List<TrainingAwards> TrainingsList { get; set; } //Completed Trainings
        
        public static List<Professional> ProfessionalList { get; set; } //Professional Experience
                
        public static List<Knowledge> KnowledgeList { get; set; } //Knowledge
        
        public static List<TrainingAwards> AwardsList { get; set; } //Awards
    }
}
