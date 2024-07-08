using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MailKit.Security;
using MimeKit;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MailClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string username;// = "vitaliy.laben@gmail.com"; // change here
        string password;// = "kpjc jryl bizv hfmx"; // change here

        private ImapClient client = new();
        ViewModel model;
        public MainWindow()
        {
            InitializeComponent();
            LoginWindow login = new LoginWindow();
            login.ShowDialog();
            username = login.loginTb.Text;
            password = login.passTb.Password;
            model = new ViewModel(username);
            this.DataContext = model;

            client.Connect("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);

            client.Authenticate(username, password);

            foreach (var fl in client.GetFolders(client.PersonalNamespaces[0]))
            {
                folderList.Items.Add(fl.Name);
            }

            folderList.SelectionChanged += FolderList_SelectionChanged;
            messageList.SelectionChanged += MessageList_SelectionChanged;
        }

        private void MessageList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessInfo item;
            if(messageList.SelectedItem != null)
            {
                item = (messageList.SelectedItem as MessInfo)!;
                ViewWindow window = new ViewWindow(item);
                window.ShowDialog();
            }
            
        }

        private async void FolderList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            model.ClearList();

            try
            {
                if (folderList.SelectedIndex == 0)
                {
                    client.Inbox.Open(FolderAccess.ReadOnly);
                    var uids = client.Inbox.Search(SearchQuery.All);

                    foreach (var uid in uids)
                    {
                        var m = client.Inbox.GetMessage(uid);
                        model.AddMess(await GetMessAsync(m));
                    }
                }
                else if (folderList.SelectedIndex == 1)
                {
                    MessageBox.Show("Welcome to gmail");
                }
                else if (folderList.SelectedIndex == 2)
                {
                    var sentFolder = client.GetFolder(SpecialFolder.Important);
                    sentFolder.Open(FolderAccess.ReadOnly);
                    var uids = sentFolder.Search(SearchQuery.All);
                    foreach (var mes in uids)
                    {
                        var m = sentFolder.GetMessage(mes);
                        model.AddMess(await GetMessAsync(m));
                    }
                }
                else if (folderList.SelectedIndex == 3)
                {
                    var sentFolder = client.GetFolder(SpecialFolder.All);
                    sentFolder.Open(FolderAccess.ReadOnly);
                    var uids = sentFolder.Search(SearchQuery.All);
                    foreach (var mes in uids)
                    {
                        var m = sentFolder.GetMessage(mes);
                        model.AddMess(await GetMessAsync(m));
                    }
                }
                else if (folderList.SelectedIndex == 4)
                {
                    var sentFolder = client.GetFolder(SpecialFolder.Trash);
                    sentFolder.Open(FolderAccess.ReadOnly);
                    var uids = sentFolder.Search(SearchQuery.All);
                    foreach (var mes in uids)
                    {
                        var m = sentFolder.GetMessage(mes);
                        model.AddMess(await GetMessAsync(m));
                    }
                }
                else if (folderList.SelectedIndex == 5)
                {
                    var sentFolder = client.GetFolder(SpecialFolder.Sent);
                    sentFolder.Open(FolderAccess.ReadOnly);
                    var uids = sentFolder.Search(SearchQuery.All);
                    foreach (var mes in uids)
                    {
                        var m = sentFolder.GetMessage(mes);
                        model.AddMess(await GetMessAsync(m));
                    }
                }
                else if (folderList.SelectedIndex == 6)
                {
                    var sentFolder = client.GetFolder(SpecialFolder.Flagged);
                    sentFolder.Open(FolderAccess.ReadOnly);
                    var uids = sentFolder.Search(SearchQuery.All);
                    foreach (var mes in uids)
                    {
                        var m = sentFolder.GetMessage(mes);
                        model.AddMess(await GetMessAsync(m));
                    }
                }
                else if (folderList.SelectedIndex == 7)
                {
                    var sentFolder = client.GetFolder(SpecialFolder.Junk);
                    sentFolder.Open(FolderAccess.ReadOnly);
                    var uids = sentFolder.Search(SearchQuery.All);
                    foreach (var mes in uids)
                    {
                        var m = sentFolder.GetMessage(mes);
                        model.AddMess(await GetMessAsync(m));
                    }
                }
                else
                {
                    var sentFolder = client.GetFolder(SpecialFolder.Drafts);
                    sentFolder.Open(FolderAccess.ReadOnly);
                    var uids = sentFolder.Search(SearchQuery.All);
                    foreach (var mes in uids)
                    {
                        var m = sentFolder.GetMessage(mes);
                        model.AddMess(await GetMessAsync(m));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public Task<MessInfo> GetMessAsync(MimeMessage m)
        {
            return Task.Run(() =>
            {
                MessInfo message = new MessInfo();
                message.From = m.From;
                message.To = m.To;
                message.Theme = m.Subject;
                message.Body = m.TextBody;
                message.Attach = m.Attachments;
                message.Time = m.Date;
                Thread.Sleep(500);
                return message;
            });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageWindow window = new MessageWindow(model,username,password);
            window.Show();
        }
    }
}