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

namespace MailClient
{
    /// <summary>
    /// Interaction logic for ViewWindow.xaml
    /// </summary>
    public partial class ViewWindow : Window
    {
        MessInfo message;
        public ViewWindow()
        {
            InitializeComponent();
            message = new MessInfo();
            this.DataContext = message;
        }
        public ViewWindow(MessInfo info)
        {
            InitializeComponent();
            message = info;
            this.DataContext = message;
        }
    }
}
