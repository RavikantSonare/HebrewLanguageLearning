using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using HebrewLanguageLearning_Users.Models.DataModels;
using Newtonsoft.Json;
using System.Windows.Forms;

namespace HebrewLanguageLearning_Users.GenericClasses
{
    public class FileSetter
    {
        internal List<DictionaryModel> BindClassData()
        {
            return getFile();
        }
        List<DictionaryModel> getFile()
        {
            List<DictionaryModel> _DictionaryModellist = new List<DictionaryModel>();
            try
            {
                //string name = Assembly.GetCallingAssembly().GetName().Name;// Assembly.GetEntryAssembly().GetName().Name;
                //string folderBase = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                string dir = string.Format(@"{0}\{1}\", EntityConfig.FolderBase, EntityConfig.Name.ToString());
                string Path = dir + "/Dictionary/";
                DirectoryInfo dic = new DirectoryInfo(Path);
                var AllfileList = dic.GetFiles("*.json");
                var json = File.ReadAllText(Convert.ToString(Path + AllfileList[0]));
                // var json = File.ReadAllText(dir + "/Dictionary/" + "Dictionary20180124144041078.json");
                _DictionaryModellist = JsonConvert.DeserializeObject<List<DictionaryModel>>(json);
                return _DictionaryModellist;
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
            finally
            {
                GC.SuppressFinalize(this);
            }
            return _DictionaryModellist;
        }
        public static void LogError(Exception ex)
        {
            string message = string.Format("Time: {0}", DateTime.UtcNow.ToString("dd/MM/yyyy hh:mm:ss tt"));
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;
            message += string.Format("Message: {0}", ex.Message);
            message += Environment.NewLine;
            message += string.Format("StackTrace: {0}", ex.StackTrace);
            message += Environment.NewLine;
            message += string.Format("Source: {0}", ex.Source);
            message += Environment.NewLine;
            message += string.Format("TargetSite: {0}", ex.TargetSite.ToString());
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;
            using (StreamWriter writer = new StreamWriter(Application.StartupPath + "\\Errorfile\\ErrorLog.txt", true))
            {
                writer.WriteLine(message);
                writer.Close();
            }
        }

        ~FileSetter() { }

    }
}
