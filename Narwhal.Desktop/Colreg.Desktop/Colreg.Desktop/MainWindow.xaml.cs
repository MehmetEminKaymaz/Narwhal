using MahApps.Metro.Controls;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
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
using System.Net.Http;

namespace Colreg.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public class Repository 
    {
        public Guid id { get; set; }
        public Guid userId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public bool isPublic { get; set; }
        public int starCount { get; set; }
        public int forkCount { get; set; }
        public string byteCode { get; set; }
    }

    public class MakiUserRepo
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public bool isPublic { get; set; }
        public int stars { get; set; }
        public int forks { get; set; }
        public string userId { get; set; }
        public DateTime createdDateTime { get; set; }
        public string bytecode { get; set; }
    }

    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            emailBox.Text = "mkaymaz@gmail.com";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Settings1.Default.userEmail = emailBox.Text;
            Settings1.Default.userPassword = passwordBox.Password;  
            
            var url = "http://localhost:5000/Compiler/Login";
            
            var loginModel = new LoginModel
            {
                Email = emailBox.Text,
                Password = passwordBox.Password
            };
            //send http post to url using loginmodel
            var json = System.Text.Json.JsonSerializer.Serialize(loginModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var client = new HttpClient();
            var response = client.PostAsync(url, content).Result;
            var responseJson = response.Content.ReadAsStringAsync().Result;
            //var makiUserRepos = System.Text.Json.JsonSerializer.Deserialize<string>(responseJson);

            if (responseJson == null)
            {
                MessageBox.Show("No repos found");
            }

            Settings1.Default.token = responseJson;

            this.Hide();
            BusinessPage businessPage = new BusinessPage();
            businessPage.Show();




        }
    }
}