using ProjectSaturn.Models;
using System.Data.SqlClient;

namespace ProjectSaturn.Service
{
    /*
     * iusr_qresume_reader
     * Pass: eYp&1yR0yQnT
     * 
     * iusr_qresume_editor
     * Pass: q7GT*N@UXqUj
     * 
     */

    public class DAL
    {
        private readonly string QReader = ""; 
        private readonly string QEditor = "";

        public DAL(string ReaderConnectionStrings, string WriterConnectionStrings) 
        {
            QReader = ReaderConnectionStrings; 
            QEditor = WriterConnectionStrings;
        }

        // -------------------------------------------Reader Calls-------------------------------------------------

        public string GetEmail(Guid guid)
        {
            string retStr = "";
            using (SqlConnection con = new(QReader))
            {
                using SqlCommand cmd = new("GetEmail", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("UserID", guid);

                try
                {
                    cmd.Connection.Open();

                    retStr = (string)cmd.ExecuteScalar();

                    cmd.Connection.Close();
                }
                catch (Exception e)
                {
                    ErrorLog.Msglist.Add(e.Message);
                }
            }
            return retStr;
        }

        //TODO : Analytic calls that interface with the Models' SqlDataReader method
            //cmd.Connection.Open();
            //
            //SqlDataReader dr = cmd.ExecuteReader();
            //while (dr.Read())
            //{
            //  var exp = new Model(dr);
            //}
            //
            //cmd.Connection.Close();



        //-------------------------------------------Writer Calls-------------------------------------------------

        public Guid AddUser(string email) // Adds a new user to the database
        {
            var guid = new Guid();
            using (SqlConnection con = new(QEditor))
            {
                using SqlCommand cmd = new("AddUser", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Email", email);

                try
                {
                    cmd.Connection.Open();

                    guid = (Guid)cmd.ExecuteScalar();

                    cmd.Connection.Close();
                }
                catch (Exception e)
                {
                    ErrorLog.Msglist.Add(e.Message);
                }
            }
            return guid;

        }

        public int AddPersonal(Personal personal, Guid userID) // Adds an entry to DataPersonal
        {
            int retID = 0;
            using (SqlConnection con = new(QEditor))
            {
                using SqlCommand cmd = new("DataAddPersonal", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                // Name
                cmd.Parameters.AddWithValue("FirstName", personal.FirstName);
                cmd.Parameters.AddWithValue("MiddleInit", personal.MiddleInit);
                cmd.Parameters.AddWithValue("LastName", personal.LastName);
                // Address
                cmd.Parameters.AddWithValue("Address", personal.Address);
                cmd.Parameters.AddWithValue("City", personal.City);
                cmd.Parameters.AddWithValue("State", personal.State);
                cmd.Parameters.AddWithValue("Zip", personal.Zip);
                // Contact Info
                cmd.Parameters.AddWithValue("Email", personal.Email);
                cmd.Parameters.AddWithValue("DayPhone", personal.DayPhone);
                cmd.Parameters.AddWithValue("EveningPhone", personal.EveningPhone);
                cmd.Parameters.AddWithValue("MobilePhone", personal.MobilePhone);
                // Is US Citizen
                cmd.Parameters.AddWithValue("IsUSCitizen", personal.IsUSCitizen);
                // Misc
                cmd.Parameters.AddWithValue("UserID", userID);

                try
                {
                    cmd.Connection.Open();

                    retID = Convert.ToInt32(cmd.ExecuteScalar());

                    cmd.Connection.Close();
                }
                catch (Exception e)
                {
                    ErrorLog.Msglist.Add(e.Message);
                }
            }
            return retID;
        }

        public int AddGeneral(General general, Guid userID) // Adds an entry to DataGeneral
        {
            int retID = 0;
            using (SqlConnection con = new(QEditor))
            {
                using SqlCommand cmd = new("DataAddGeneral", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                // General
                cmd.Parameters.AddWithValue("Degree", general.Degree);
                cmd.Parameters.AddWithValue("DegreeStatus", general.DegreeStatus);
                cmd.Parameters.AddWithValue("AntiGradDate", general.AntiGradDate);
                // GPA
                cmd.Parameters.AddWithValue("OverallGPA", general.OverallGPA);
                cmd.Parameters.AddWithValue("MajorGPA", general.MajorGPA);
                // SAT
                cmd.Parameters.AddWithValue("SATV", general.SATV);
                cmd.Parameters.AddWithValue("SATM", general.SATM);
                // GRE
                cmd.Parameters.AddWithValue("GREV", general.GREV);
                cmd.Parameters.AddWithValue("GREQ", general.GREQ);
                cmd.Parameters.AddWithValue("GREA", general.GREA);
                // Misc
                cmd.Parameters.AddWithValue("UserID", userID);

                try
                {
                    cmd.Connection.Open();

                    retID = Convert.ToInt32(cmd.ExecuteScalar());

                    cmd.Connection.Close();
                }
                catch (Exception e)
                {
                    ErrorLog.Msglist.Add(e.Message);
                }
            }
            return retID;
        }

        public int AddEducation(Education education, Guid userID, string SkillsGained) // Adds an entry to DataEducational
        {
            int retID = 0;
            using (SqlConnection con = new(QEditor))
            {
                using SqlCommand cmd = new("DataAddEducational", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                // General
                cmd.Parameters.AddWithValue("Name", education.Name);
                cmd.Parameters.AddWithValue("Major", education.Major);
                cmd.Parameters.AddWithValue("DegreeEarned", education.DegreeEarned);
                cmd.Parameters.AddWithValue("DegreeDate", education.DegreeDate);
                cmd.Parameters.AddWithValue("SkillsGained", SkillsGained);
                // School Address
                cmd.Parameters.AddWithValue("SchoolCity", education.SchoolCity);
                cmd.Parameters.AddWithValue("SchoolState", education.SchoolState);
                cmd.Parameters.AddWithValue("SchoolZip", education.SchoolZip);
                // GPA
                cmd.Parameters.AddWithValue("OverallGPA", education.OverallGPA);
                cmd.Parameters.AddWithValue("MajorGPA", education.MajorGPA);
                // Misc
                cmd.Parameters.AddWithValue("UserID", userID);

                try
                {
                    cmd.Connection.Open();

                    retID = Convert.ToInt32(cmd.ExecuteScalar());

                    cmd.Connection.Close();
                }
                catch (Exception e)
                {
                    ErrorLog.Msglist.Add(e.Message);
                }
            }
            return retID;
        }

        public int AddProfessional(Professional profession, Guid userID, string SkillsGained) // Add an entry to DataProfessional
        {
            int retID = 0;
            using (SqlConnection con = new(QEditor))
            {
                using SqlCommand cmd = new("DataAddProfessional", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                // Profession
                cmd.Parameters.AddWithValue("Name", profession.Name);
                cmd.Parameters.AddWithValue("Position", profession.Position);
                cmd.Parameters.AddWithValue("Location", profession.Location);
                cmd.Parameters.AddWithValue("SkillsGained", SkillsGained);
                // Date
                cmd.Parameters.AddWithValue("DateStart", profession.StartDate);
                cmd.Parameters.AddWithValue("DateEnd", profession.EndDate);
                // Misc
                cmd.Parameters.AddWithValue("UserID", userID);

                try
                {
                    cmd.Connection.Open();

                    retID = Convert.ToInt32(cmd.ExecuteScalar());

                    cmd.Connection.Close();
                }
                catch (Exception e)
                {
                    ErrorLog.Msglist.Add(e.Message);
                }
            }
            return retID;
        }

        public int AddCertification(Certifications certification, Guid userID) // Adds an entry to DataTraining
        {
            int retID = 0;
            using (SqlConnection con = new(QEditor))
            {
                using SqlCommand cmd = new("DataAddCertification", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                // Certification
                cmd.Parameters.AddWithValue("Certification", certification.Certification);
                cmd.Parameters.AddWithValue("Date", certification.Date);
                cmd.Parameters.AddWithValue("Completed", certification.Completed);
                // Misc
                cmd.Parameters.AddWithValue("UserID", userID);

                try
                {
                    cmd.Connection.Open();

                    retID = Convert.ToInt32(cmd.ExecuteScalar());

                    cmd.Connection.Close();
                }
                catch (Exception e)
                {
                    ErrorLog.Msglist.Add(e.Message);
                }
            }
            return retID;
        }

        public int AddSkills(Skills skill, Guid userId) // Adds an entry to DataKnowledge
        {
            int retID = 0;
            using (SqlConnection con = new(QEditor))
            {
                using SqlCommand cmd = new("DataAddSkill", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                // Skill
                cmd.Parameters.AddWithValue("Skill", skill.Skill);
                // Misc
                cmd.Parameters.AddWithValue("UserID", userId);

                try
                {
                    cmd.Connection.Open();

                    retID = Convert.ToInt32(cmd.ExecuteScalar());

                    cmd.Connection.Close();
                }
                catch (Exception e)
                {
                    ErrorLog.Msglist.Add(e.Message);
                }
            }
            return retID;
        }

        public int AddAwards(Awards award, Guid userID) // Adds an entry to DataAwards
        {
            int retID = 0;
            using (SqlConnection con = new(QEditor))
            {
                using SqlCommand cmd = new("DataAddAward", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                // Award
                cmd.Parameters.AddWithValue("Award", award.Award);
                cmd.Parameters.AddWithValue("EarnDate", award.EarnDate);
                // Misc
                cmd.Parameters.AddWithValue("UserID", userID);

                try
                {
                    cmd.Connection.Open();

                    retID = Convert.ToInt32(cmd.ExecuteScalar());

                    cmd.Connection.Close();
                }
                catch (Exception e)
                {
                    ErrorLog.Msglist.Add(e.Message);
                }
            }
            return retID;
        }
    }
}
