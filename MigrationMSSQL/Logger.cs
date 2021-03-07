using System;
using System.Threading;

namespace MigrationMSSQL
{
    class Logger
    {
        int count = 0;
        
        bool enabled = true;
        public void Start()
        {
            while(enabled)
            {
                if(count != Convert.ToInt32(new OutputMSSQL().SqlQuery()))
                {
                    _ = new MysqlMigrationPrediction(new PredictionUses()).Migration();

                    _ = new MySqlMigration(new UserContext()).Migration();
                    
                    count = Convert.ToInt32(new OutputMSSQL().SqlQuery());
                }
                Thread.Sleep(5000);
            }
        }
        public void Stop()
        {
            enabled = false;
        }
    }
}
