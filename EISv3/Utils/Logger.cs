using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EISv3.Utils
{
    public static class Logger
    {
        public static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void logging(string message)
        {
            log4net.Config.XmlConfigurator.Configure();
            log.Info(message);
        }
    }
}
