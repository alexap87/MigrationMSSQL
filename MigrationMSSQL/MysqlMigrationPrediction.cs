using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MigrationMSSQL
{
    class MysqlMigrationPrediction
    {
        List<string[]> basevalue;
        public MysqlMigrationPrediction(PredictionUses data)
        {
            basevalue = data.SqlQuery(new OutputMysqlPred());
        }
        public async Task Migration()
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

                await connection.OpenAsync();

                foreach (string[] s in basevalue)
                {
                    try
                    {
                        using (var command = connection.CreateCommand())
                        {
                            command.CommandText = @"insert into `millkoscan`.`prediction` (`SampRef`, `RepNoRef`, `CompRef`, `Value`, `Flags`) " +
                                "values (@SampRef, @RepNoRef, @CompRef, @Value, @Flags);";
                            command.Parameters.AddWithValue("@SampRef", Convert.ToInt32(s[0]));
                            command.Parameters.AddWithValue("@RepNoRef", Convert.ToInt32(s[1]));
                            command.Parameters.AddWithValue("@CompRef", Convert.ToInt32(s[2]));
                            command.Parameters.AddWithValue("@Value", float.Parse(s[3]));
                            command.Parameters.AddWithValue("@Flags", Convert.ToInt32(s[4]));

                            await command.ExecuteNonQueryAsync();
                        }
                    }
                    catch (MySqlException ex)
                    {
                        new Logging().writeLog(ex.ToString());
                    }
                }
                connection.Close();

            }
        }
    }
}
