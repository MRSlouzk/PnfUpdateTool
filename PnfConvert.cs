using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PnfUpdateTool
{
    internal class PnfConvert
    {
        public string NewestVersionPath = "";
        public string PnfImportPath = "";
        public string PnfExportPath = "";
        public void Convert(string folderPath, string gamePath, string sdkPath, Options options)
        {
            if (!PreCheck(gamePath))
            {
                return;         // TODO 返回指定参数进行弹窗
            }
        }

        public bool PreCheck(string gamePath)
        {
            gamePath = gamePath + "\\bin";
            DirectoryInfo di = new DirectoryInfo(@gamePath);
            DirectoryInfo[] directories = di.GetDirectories("*", SearchOption.AllDirectories);

            foreach (DirectoryInfo directory in directories)
            {
                if (int.TryParse(directory.Name, out int result))
                {
                    this.NewestVersionPath = directory.FullName + "\\res_mods";
                }
            }

            if (NewestVersionPath == "")
            {
                return false;
            }

            // Pnf文件检查
            string pyFilePath = NewestVersionPath + "\\PnFModsLoader.py";
            string pnfFolderPath = NewestVersionPath + "\\PnFMods";

            if (!File.Exists(pyFilePath))
            {
                FileStream fs = new FileStream(pyFilePath, FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine("");
                sw.Close();
            }
            if (!Directory.Exists(pnfFolderPath))
            {
                Directory.CreateDirectory(pnfFolderPath);
            }

            PnfImportPath = pnfFolderPath + "\\ModsSDK.zip";
            PnfExportPath = pnfFolderPath + "\\ModsSDKExport";

            // 临时目录
            Directory.CreateDirectory(PnfExportPath);
            Directory.CreateDirectory(PnfImportPath);

            FileStream fs1 = new FileStream(pnfFolderPath + "\\ModsSDKExport\\Main.py", FileMode.Create, FileAccess.Write);
            StreamWriter sw1 = new StreamWriter(fs1);
            sw1.WriteLine("API_VERSION = 'API_v1.0'\ncontentSdk.extractSources('ModName','ShipSdkName')\n");
            sw1.Close();

            return true;
        }

    }
}
