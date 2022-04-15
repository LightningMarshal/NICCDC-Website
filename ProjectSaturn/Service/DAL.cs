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

        public Personal GetDataPersonal(int itemID)
        {
            Personal personal = null;
            using (SqlConnection con = new(QReader))
            {
                using SqlCommand cmd = new("GetDataPersonal", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("ItemID", itemID);
                try
                {
                    cmd.Connection.Open();

                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                       personal = new Personal(dr);
                    }

                    cmd.Connection.Close();
                }
                catch (Exception e)
                {
                    ErrorLog.Msglist.Add(e.Message);
                }
            }
            return personal;
        }

        //-------------------------------------------Writer Calls-------------------------------------------------

        public Guid AddTempUser()
        {
            var guid = new Guid();
            using (SqlConnection con = new(QEditor))
            {
                using SqlCommand cmd = new("AddTempUser", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
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

        public int AddPersonal(Personal personal, Guid userID) //  Adds an entry to DataPersonal
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
    }
}
