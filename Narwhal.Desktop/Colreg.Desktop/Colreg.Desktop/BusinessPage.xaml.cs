using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
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
using System.Windows.Threading;

namespace Colreg.Desktop
{
    /// <summary>
    /// Interaction logic for BusinessPage.xaml
    /// </summary>
    /// 
    public class DataModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Bytecode { get; set; }

    }

    public partial class BusinessPage : MetroWindow
    {
        private List<DataModel> dataModelList = new List<DataModel>();
        public BusinessPage()
        {
            InitializeComponent();


            var url = "http://localhost:5000/Compiler/GetRepositories";

            var loginModel = new LoginModel
            {
                Email = Settings1.Default.userEmail,
                Password = Settings1.Default.userPassword
            };
            //send http post to url using loginmodel
            var json = System.Text.Json.JsonSerializer.Serialize(loginModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Settings1.Default.token);
            var response = client.GetAsync(url).Result;
            var responseJson = response.Content.ReadAsStringAsync().Result;
            var makiUserRepos = System.Text.Json.JsonSerializer.Deserialize<List<Repository>>(responseJson);

            if (makiUserRepos == null)
            {
                MessageBox.Show("No repos found");
            }

            dataModelList = makiUserRepos.Select(x => new DataModel
            {
                Id = x.id,
                Name = x.name,
                Description = x.description,
                Bytecode = x.byteCode,
            }).ToList();

            executablesDataGrid.ItemsSource = null;
            executablesDataGrid.ItemsSource = makiUserRepos.Select(x => new DataModel
            {
                Id = x.id,
                Name = x.name,
                Description = x.description,
                Bytecode = x.byteCode,
            }).ToList();

        }

        private void runButton_Click(object sender, RoutedEventArgs e)
        {
            //clear folder
            System.IO.DirectoryInfo di = new DirectoryInfo(@"C:\Users\Eminf\Area24");

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }

            Thread.Sleep(1000);

            var index = executablesDataGrid.SelectedIndex;
            var selectedModel = dataModelList[index];
            if (selectedModel.Bytecode != null)
            {
                selectedModel.Bytecode = "using System;using Constructs;using HashiCorp.Cdktf;using HashiCorp.Cdktf.Providers.Docker.Config;using HashiCorp.Cdktf.Providers.Docker.Container;using HashiCorp.Cdktf.Providers.Docker.DataDockerImage;using HashiCorp.Cdktf.Providers.Docker.Image;using HashiCorp.Cdktf.Providers.Docker.Network;using HashiCorp.Cdktf.Providers.Docker.Plugin;using HashiCorp.Cdktf.Providers.Docker.Provider;" + selectedModel.Bytecode;
                string directoryPath = @"C:\Users\Eminf\Area24";
                using Process cmd = new Process();

                cmd.StartInfo.WorkingDirectory = directoryPath;
                cmd.StartInfo.FileName = "cmd.exe";

                cmd.StartInfo.RedirectStandardInput = true;
                cmd.StartInfo.UseShellExecute = false;
                //cmd.StartInfo.CreateNoWindow = true;

                cmd.Start();

                using (StreamWriter sw = cmd.StandardInput)
                {
                    if (sw.BaseStream.CanWrite)
                    {
                        Thread.Sleep(2000);
                        sw.WriteLine("cdktf init --template=csharp --providers=kreuzwerker/docker --local");
                        Thread.Sleep(15000);
                        sw.WriteLine("area2");
                        Thread.Sleep(5000);
                        sw.WriteLine("desc");
                        Thread.Sleep(5000);
                        sw.WriteLine("Y");
                        Thread.Sleep(10000);
                        //replace işlemi
                        File.WriteAllText(@"C:\Users\Eminf\Area24\MainStack.cs", selectedModel.Bytecode);
                        Thread.Sleep(5000);
                        sw.WriteLine("cdktf deploy --auto-approve");
                        Thread.Sleep(90000);
                    }
                }

                cmd.WaitForExit();

            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var url = "http://localhost:5000/Compiler/GetRepositories";

            var loginModel = new LoginModel
            {
                Email = Settings1.Default.userEmail,
                Password = Settings1.Default.userPassword
            };
            //send http post to url using loginmodel
            var json = System.Text.Json.JsonSerializer.Serialize(loginModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Settings1.Default.token);
            var response = client.GetAsync(url).Result;
            var responseJson = response.Content.ReadAsStringAsync().Result;
            var makiUserRepos = System.Text.Json.JsonSerializer.Deserialize<List<Repository>>(responseJson);

            if (makiUserRepos == null)
            {
                MessageBox.Show("No repos found");
            }

            
            dataModelList = makiUserRepos.Select(x => new DataModel
            {
                Id = x.id,
                Name = x.name,
                Description = x.description,
                Bytecode = x.byteCode,
            }).ToList();

            executablesDataGrid.ItemsSource = null;
            executablesDataGrid.ItemsSource = makiUserRepos.Select(x => new DataModel
            {
                Id = x.id,
                Name = x.name,
                Description = x.description,
                Bytecode = x.byteCode,
            }).ToList();
        }
    }
}
