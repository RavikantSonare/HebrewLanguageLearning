using HebrewLanguageLearning_Users.GenericClasses;
using HebrewLanguageLearning_Users.Models.DataModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HebrewLanguageLearning_Users.Models.DataControllers
{
    class SettingController
    {
        //UserId=
        private string urlParameters = "1";
        private string CurrentUrl = EntityConfig.HostingUri;
        internal bool SyncData(string UsersID)
        {
            //FileSetter _OBJ = new GenericClasses.FileSetter();
            //_OBJ.BindClassData();
            // return true;
            return GetData(UsersID);
        }
        public List<DictionaryModel> getDataFromLocalFile()
        {
            FileSetter _OBJ = new GenericClasses.FileSetter();
            return _OBJ.BindClassData();
        }
        private bool GetData(string UserId)
        {
            bool isDone = false;

            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(CurrentUrl);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = client.GetAsync("?UserId=" + UserId).Result;  // Blocking call!
                if (response.IsSuccessStatusCode)
                {
                    var products = response.Content.ReadAsStringAsync().Result;
                    string path = UserDataFolder("Dictionary");
                    string filename = AppendTimeStamp("Dictionary") + ".json";
                    System.IO.File.WriteAllText(path + filename, products);
                }
                else
                {
                    Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                }


                isDone = true;

            }
            catch (Exception ex)
            {
                isDone = false;
            }

            return isDone;
        }


        private static string UserDataFolder(string isSub)
        {
            try
            {
                string name = Assembly.GetCallingAssembly().GetName().Name;// Assembly.GetEntryAssembly().GetName().Name;
                string folderBase = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                string dir = string.Format(@"{0}\{1}\", folderBase, name.ToString());
                return CheckDir(dir, isSub);
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        private static string CheckDir(string dir, string isSub)
        {


            if (String.IsNullOrEmpty(isSub))
            {
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                return dir;
            }
            else
            {
                dir = dir + isSub + @"\";
                if (Directory.Exists(dir))
                {
                    Directory.Delete(dir, true); Directory.CreateDirectory(dir);
                }
                else
                {
                    Directory.CreateDirectory(dir);
                }
                return dir;
            }
            return dir;
        }
        public static string AppendTimeStamp(string fileName)
        {
            return string.Concat(
                Path.GetFileNameWithoutExtension(fileName),
                DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                Path.GetExtension(fileName)
                );
        }
    }
}
