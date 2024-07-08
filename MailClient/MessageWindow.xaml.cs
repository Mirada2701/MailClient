using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
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
    /// Interaction logic for MessageWindow.xaml
    /// </summary>
    public partial class MessageWindow : Window
    {
        ViewModel model;
        bool important;
        string user_name;
        string user_pass;
        public MessageWindow(ViewModel model, string user_name, string user_pass)
        {
            InitializeComponent();
            this.model = model;
            this.DataContext = model;
            important = false;
            this.user_name = user_name;
            this.user_pass = user_pass;
        }
        private void Attach_Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();
            model.AddAttach(dialog.FileName);
        }

        private void Send_Button_Click(object sender, RoutedEventArgs e)
        {
            MailMessage message = new MailMessage(model.From, model.To, model.Theme, model.Body);
            foreach (var item in model.Attachments)
            {
                message.Attachments.Add(new Attachment(item));
            }
            if (important)
                message.Priority = MailPriority.High;

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;

            client.Credentials = new NetworkCredential(user_name, user_pass);
            client.SendCompleted += Client_SendCompleted;

            client.SendAsync(message, message);
        }

        private void Client_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            var state = (MailMessage)e.UserState;
            MessageBox.Show($"Message was sent! Subject: {state.Subject}!");
        }

        private void Mark_Important_Button_Click(object sender, RoutedEventArgs e)
        {
            important = true;
        }
    }
}
