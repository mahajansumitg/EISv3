using log4net;
using System;
using System.Threading;

namespace EISv3.Utils
{
    public static class Loading
    {
        static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static ProgressBar progressBar;
        private static event Action ActionEvent;
        private static MainWindow window;

        public static Object Show(Func<Object> actionToPerform)
        {
            ActionEvent += new Action(Invoke);

            //Thread to Perform action
            object data = null;
            Thread thread = new Thread(() => {
                data = actionToPerform();  //action like getdata, setdata, etc
                ActionEvent();
            });
            thread.Start();

            window = Mediator.getVar("Window") as MainWindow;
            window.Opacity = 0.4;

            progressBar = new ProgressBar();
            progressBar.Owner = window;
            progressBar.ShowDialog();

            return data;
        }

        //Inform ProgressBar that the Task is completed & Close the ProgressBar
        private static void Invoke()
        {
            log4net.Config.XmlConfigurator.Configure();

            progressBar.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
            new Action(
            delegate ()
            {
                progressBar.Close();
                window.Opacity = 1;
                log.Info("-----ProgressBar Running------");
            }
            ));
        }
    }
}
