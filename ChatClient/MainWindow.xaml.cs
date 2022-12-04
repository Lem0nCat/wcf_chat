using ChatClient.ServiceChat;
using System.Windows;
using System.Windows.Input;

namespace ChatClient {
    public partial class MainWindow : Window, IServiceChatCallback {
        bool isConnected = false;
        ServiceChatClient client;
        User user = null;
        public MainWindow() {
            InitializeComponent();
        }

        void ConnectUser() {
            if (!isConnected) {
                client = new ServiceChatClient(new System.ServiceModel.InstanceContext(this));
                user = client.Connect(tbUserName.Text, "1234");
                tbUserName.IsEnabled = false;
                bConnDisconn.Content = "Disconnect";
                isConnected = true;
            }
        }

        void DisconnectUser() {
            if (isConnected) {
                client.Disconnect(user.ID);
                tbUserName.IsEnabled = true;
                bConnDisconn.Content = "Connect";
                isConnected = false;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            DisconnectUser();
        }

        private void bConnDisconn_Click(object sender, RoutedEventArgs e) {
            if (isConnected) {
                DisconnectUser();
            } else {
                ConnectUser();
            }
        }

        private void tbMessage_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter && client != null) {
                client.SendMessage(tbMessage.Text, user.ID);
                tbMessage.Text = string.Empty;
            }
        }

        private void tbMessage_PreviewMouseDown(object sender, MouseButtonEventArgs e) {

        }

        public void MessageCallback(string message) {
            lbChat.Items.Add(message);
            lbChat.ScrollIntoView(lbChat.Items[lbChat.Items.Count - 1]);
        }
    }
}
