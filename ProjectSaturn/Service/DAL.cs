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

        //TODO : Analytic calls will be made here

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



        //-------------------------------------------Writer Calls-------------------------------------------------

        public Guid AddUser(string email) // Adds a new user
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
                cmd.Parameters.AddWithValue("UserID", userID);
                cmd.Parameters.AddWithValue("FirstName", personal.FirstName);
                cmd.Parameters.AddWithValue("LastName", personal.LastName);
                cmd.Parameters.AddWithValue("Email", personal.Email);
                cmd.Parameters.AddWithValue("PhoneNumber", personal.PhoneNumber);
                cmd.Parameters.AddWithValue("Address", personal.Address);
                cmd.Parameters.AddWithValue("Goal", personal.Goal);

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

        public int AddEducation(Education education, Guid userID, string SkillsList) // Adds an entry to DataEducational
        {
            int retID = 0;
            using (SqlConnection con = new(QEditor))
            {
                using SqlCommand cmd = new("DataAddEducational", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("UserID", userID);
                cmd.Parameters.AddWithValue("Name", education.Name);
                cmd.Parameters.AddWithValue("Location", education.Location);
                cmd.Parameters.AddWithValue("DateStart", education.StartDate);
                cmd.Parameters.AddWithValue("DateEnd", education.EndDate);
                cmd.Parameters.AddWithValue("SkillList", SkillsList);
                cmd.Parameters.AddWithValue("GPA", education.GPA);

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

        public int AddTraining(Trainings training, Guid userID)
        {
            int retID = 0;
            using (SqlConnection con = new(QEditor))
            {
                using SqlCommand cmd = new("DataAddTraining", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("UserID", userID);
                cmd.Parameters.AddWithValue("Desc", training.Desc);
                cmd.Parameters.AddWithValue("Date", training.Date);
                cmd.Parameters.AddWithValue("Completed", training.Completed);

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

        public int AddProfessional(Professional profession, Guid userID, string SkillsList)
        {
            int retID = 0;
            using (SqlConnection con = new(QEditor))
            {
                using SqlCommand cmd = new("DataAddProfessional", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("UserID", userID);
                cmd.Parameters.AddWithValue("Name", profession.Name);
                cmd.Parameters.AddWithValue("Position", profession.PositionAtComp);
                cmd.Parameters.AddWithValue("Location", profession.Location);
                cmd.Parameters.AddWithValue("DateStart", profession.StartDate);
                cmd.Parameters.AddWithValue("DateEnd", profession.EndDate);
                cmd.Parameters.AddWithValue("SkillList", SkillsList);

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

        public int AddKnowledge(Knowledge knowledge, Guid userId)
        {
            int retID = 0;
            using (SqlConnection con = new(QEditor))
            {
                using SqlCommand cmd = new("DataAddKnowledge", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("UserID", userId);
                cmd.Parameters.AddWithValue("Desc", knowledge.Desc);

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

        public int AddAwards(Awards award, Guid userID)
        {
            int retID = 0;
            using (SqlConnection con = new(QEditor))
            {
                using SqlCommand cmd = new("DataAddAward", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("UserID", userID);
                cmd.Parameters.AddWithValue("Desc", award.Desc);
                cmd.Parameters.AddWithValue("Date", award.Date);

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
