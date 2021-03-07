using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace MigrationMSSQL
{
    class PredictionUses
    {
        public List<string[]> SqlQuery(OutputMysqlPred val)
        {
            List<string[]> dataresourse = new List<string[]>();
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
                    string sql = "SELECT * FROM MSCFT1.dbo.Prediction WHERE SampRef >= " +val.OutputData()+ ";";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dataresourse.Add(new string[]
                                {
                                       reader.GetValue(0).ToString(), reader.GetValue(1).ToString(),
                                       reader.GetValue(2).ToString(), reader.GetValue(3).ToString(),reader.GetValue(4).ToString()
                                });
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
            return dataresourse;
        }
    }
}
