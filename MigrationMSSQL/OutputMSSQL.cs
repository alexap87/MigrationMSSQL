using Microsoft.Data.SqlClient;

namespace MigrationMSSQL
{
    class OutputMSSQL
    {
        public string SqlQuery()
        {
            string dataresult = "0";
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder()
                {
                    DataSource = "192.168.9.180",
                    UserID = "sa",
                    Password = "15793",
                    TrustServerCertificate = false,
                    Encrypt = false
                };
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    string sql = "SELECT COUNT(1) FROM MSCFT1.dbo.Prediction;";


                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            ;
                            while (reader.Read())
                            {
                                dataresult = reader.GetValue(0).ToString();
                            }
                            connection.Close();
                        }
                    }
                }
            }
            catch (SqlException e)
            {

                new Logging().writeLog(e.ToString());

            }
            return dataresult;
        }
    }
}
