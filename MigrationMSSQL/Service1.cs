using System.ServiceProcess;
using System.Threading;

namespace MigrationMSSQL
{
    public partial class Service1 : ServiceBase
    {
        Logger logger;
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            logger = new Logger();
            Thread loggerThread = new Thread(new ThreadStart(logger.Start));
            new Logging().writeLog("Service started");
            loggerThread.Start();
        }

        protected override void OnStop()
        {
            logger.Stop();
            new Logging().writeLog("Service stoped");
            Thread.Sleep(1000);
        }
    }
}
