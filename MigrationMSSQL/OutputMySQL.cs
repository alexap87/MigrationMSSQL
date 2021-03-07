using MySql.Data.MySqlClient;

namespace MigrationMSSQL
{
    class OutputMySQL
    {
        public OutputMySQL()
        {
            
        }
        public string OutputData()
        {
            string result = "";
            try
            {
                var builder = new MySqlConnectionStringBuilder
                {
                    Server = "192.168.98.9",
                    UserID = "ASUTP",
                    Password = "15793",
                    Port = 3306
                };
                using (var connection = new MySqlConnection(builder.ConnectionString))
                {
                    connection.Open();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT * FROM `millkoscan`.`sample` ORDER BY SampNo DESC LIMIT 1;";

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result = reader.GetValue(0).ToString();
                            }
                        }

                    }
                    connection.Close();
                }
            }
            catch (MySqlException e)
            {
                new Logging().writeLog(e.ToString());
            }
            return result;
        }
    }
}
