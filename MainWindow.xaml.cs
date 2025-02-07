using System.Diagnostics;
using System.IO;
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
using Microsoft.Win32;

namespace PnfUpdateTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _folderPath = "";
        private string _gamePath = "";
        private string _sdkPath = "";
        private Options _options = new Options();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFolderDialog openFolderDialog = new OpenFolderDialog();
            openFolderDialog.InitialDirectory = "C:\\";
            openFolderDialog.Title = "选择Pnf文件夹所在目录";

            bool? result = openFolderDialog.ShowDialog();
            if (result == true)
            {
                this._folderPath = openFolderDialog.FolderName;
                ImportAddressBox.Text = _folderPath;
                MessageBox.Show(_folderPath);
            }
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            OpenFolderDialog openFolderDialog = new OpenFolderDialog();
            openFolderDialog.InitialDirectory = "C:\\";
            openFolderDialog.Title = "选择游戏根目录";

            bool? result = openFolderDialog.ShowDialog();
            if (result == true)
            {
                this._gamePath = openFolderDialog.FolderName;
                string game = _gamePath + "\\WorldOfWarships.exe";
                if (!File.Exists(game))
                {
                    MessageBox.Show("请选择正确的游戏根目录，目录需包含游戏可执行文件！");
                    this._gamePath = "";
                    return;
                }
                GameAddressBox.Text = _gamePath;
            }
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            OpenFolderDialog openFolderDialog = new OpenFolderDialog();
            openFolderDialog.InitialDirectory = "C:\\";
            openFolderDialog.Title = "选择SDK目录";

            bool? result = openFolderDialog.ShowDialog();
            if (result == true)
            {
                this._sdkPath = openFolderDialog.FolderName;
                if (_sdkPath.StartsWith("ModsSDK"))
                {
                    MessageBox.Show("请选择正确的SDK目录，格式：ModsSDK_版本号");
                    this._sdkPath = "";
                    return;
                }
                GameAddressBox.Text = _sdkPath;
            }
        }

        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            // 检查三个路径是否为空
            if (_folderPath == "" || _gamePath == "" || _sdkPath == "")
            {
                MessageBox.Show("三个路径均为必填项！");
                return;
            }
            PnfConvert pnfConvert = new PnfConvert();
            pnfConvert.Convert(_folderPath, _gamePath, _sdkPath, _options);
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            _options.ModelChanged = true;
        }

        private void CheckBox_UnChecked(object sender, RoutedEventArgs e)
        {
            _options.ModelChanged = false;
        }

        private void CheckBox2_Checked(object sender, RoutedEventArgs e)
        {
            _options.IsGlow = true;
        }

        private void CheckBox2_UnChecked(object sender, RoutedEventArgs e)
        {
            _options.IsGlow = false;
        }
    }
}