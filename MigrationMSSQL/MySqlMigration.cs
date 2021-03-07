using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace MigrationMSSQL
{
    class MySqlMigration
    {
        List<string[]> basevalue;
        public MySqlMigration(UserContext data)
        {
            basevalue = data.SqlQuery(new OutputMySQL());
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
                try
                {
                    await connection.OpenAsync();
                    foreach (string[] s in basevalue)
                    {
                        using (var command = connection.CreateCommand())
                        {
                            command.CommandText = @"INSERT INTO `millkoscan`.`sample` (`SampNo`, `SampleId`, `ProdRef`, `ZeroRef`, `NoOfReps`, `Kind`, " +
                                "`DateTime`, `DilutionFactor`, `SampleWeight`, `Excluded`, `Flags`, `Remark`, `UDF1`, `UDF2`, `UDF3`, `UDF4`) " +
                                "VALUES (@SampleNo, @SampleId, @ProdRef, @ZeroRef, @NoOfReps, @Kind, @DateTime, @DilutionFactor, @SampleWeight, " +
                                "@Excluded, @Flags, @Remark, @UDF1, @UDF2, @UDF3, @UDF4);";
                            command.Parameters.AddWithValue("@SampleNo", Convert.ToInt32(s[0]));
                            command.Parameters.AddWithValue("@SampleId", s[1]);
                            command.Parameters.AddWithValue("@ProdRef", Convert.ToInt32(s[2]));
                            command.Parameters.AddWithValue("@ZeroRef", Convert.ToInt32(s[3]));
                            command.Parameters.AddWithValue("@NoOfReps", Convert.ToInt32(s[4]));
                            command.Parameters.AddWithValue("@Kind", Convert.ToInt32(s[5]));
                            command.Parameters.AddWithValue("@DateTime", Convert.ToDateTime(s[6]));
                            command.Parameters.AddWithValue("@DilutionFactor", float.Parse(s[7]));
                            command.Parameters.AddWithValue("@SampleWeight", float.Parse(s[8]));
                            command.Parameters.AddWithValue("@Excluded", Convert.ToBoolean(s[9]) ? 1 : 0);
                            command.Parameters.AddWithValue("@Flags", Convert.ToInt32(s[10]));
                            command.Parameters.AddWithValue("@Remark", s[11]);
                            command.Parameters.AddWithValue("@UDF1", s[12]);
                            command.Parameters.AddWithValue("@UDF2", s[13]);
                            command.Parameters.AddWithValue("@UDF3", s[14]);
                            command.Parameters.AddWithValue("@UDF4", s[15]);
                            
                            await command.ExecuteNonQueryAsync();
                        }
                    }
                    connection.Close();
                }
                catch (MySqlException e)
                {
                    new Logging().writeLog(e.ToString());
                }
            }
        }
    }
}
