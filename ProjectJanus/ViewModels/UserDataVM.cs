using ProjectJanus.Models;

/*
 * TODO: Possibly remove both this page and ViewModels as it appears they are not being used.
 */

namespace ProjectJanus.ViewModels
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
        public Skills Knowledge { get; set; }

        // This is the Awards section
        public Awards Awards { get; set; }
    }
}
