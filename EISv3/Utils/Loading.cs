using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace EISv3.Utils
{
    public static class Loading
    {
        private static ProgressBar progressBar;
        private static event Action actionEvent;
        private static MainWindow window;

        public static Object Show(Func<Object> actionToPerform)
        {
            actionEvent += new Action(invoke);

            object data = null;
            Thread thread = new Thread(() => {
                data = actionToPerform();
                actionEvent();
            });
            thread.Start();

            window = Mediator.getVar("Window") as MainWindow;
            window.Opacity = 0.4;

            progressBar = new ProgressBar();
            progressBar.Owner = window;
            progressBar.ShowDialog();

            return data;
        }

        private static void invoke()
        {
            progressBar.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
            new Action(
            delegate ()
            {
                progressBar.Close();
                window.Opacity = 1;
            }
            ));
        }
    }
}
