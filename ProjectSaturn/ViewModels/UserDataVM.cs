using ProjectSaturn.Models;

namespace ProjectSaturn.ViewModels
{
    public class UserDataVM
    {
        // This is the Personal Details section
        public Personal Personal { get; set; }

        // This is the Education section
        public Education Education { get; set; }

        // This is the Professional Experience section
        public Professional Professional { get; set; }

        // Trainings within the Education section
        public Certifications Training { get; set; } 

        // This is the Knowledge section
        public Knowledge Knowledge { get; set; }

        // This is the Awards section
        public Awards Awards { get; set; }
    }
}
