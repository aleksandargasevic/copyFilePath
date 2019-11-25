using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CopyPathTool.Properties;
using Microsoft.Win32;

namespace CopyPathTool
{
    class Program
    {

        #region Properties

        private static string _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CopyPathTool.exe");

        #endregion


        static void Main(string[] args) {

            ExportApp();

            WriteRegistryKeys();

            Environment.Exit(0);
        }

        #region Methods

        private static void ExportApp() {
            File.WriteAllBytes(_filePath, Resources.RightClickFilePath);
        }

        private static void WriteRegistryKeys() {

            try {
                //Write Registry Key for all files
                var rootKeyFiles = Registry.ClassesRoot.CreateSubKey(@"*\shell\Copy File Path");
                var fileCommandKey = rootKeyFiles.CreateSubKey("command");
                fileCommandKey.SetValue("", "\"" + _filePath + "\"" + "\"%0\"");
            }
            catch { }


            try {

                //Write Registry Key for folders
                var rootKeyDirectory = Registry.ClassesRoot.CreateSubKey(@"Directory\shell\Copy Folder Path");
                var directoryCommandKey = rootKeyDirectory.CreateSubKey("command");
                directoryCommandKey.SetValue("", "\"" + _filePath + "\"" + "\"%0\"");
            }
            catch  {}

        }

        #endregion
    }
}
