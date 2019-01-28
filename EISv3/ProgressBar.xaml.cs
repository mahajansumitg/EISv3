using EISv3.Utils;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EISv3
{
    /// <summary>
    /// Interaction logic for ProgressBar.xaml
    /// </summary>
    public partial class ProgressBar : Window
    {
        readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ProgressBar()
        {
            InitializeComponent();
            log4net.Config.XmlConfigurator.Configure();
            log.Info("-----ProgressBar Running------");
        }
    }
}
