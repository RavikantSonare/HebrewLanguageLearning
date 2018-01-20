using HebrewLanguageLearning_Users.GenericClasses;
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
        private string urlParameters="1";
        private string CurrentUrl = EntityConfig.HostingUri;
        internal bool SyncData(string UsersID)
        {

            return GetData(UsersID);
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
                HttpResponseMessage response = client.GetAsync("?UserId="+UserId).Result;  // Blocking call!
                if (response.IsSuccessStatusCode)
                {
                    var products = response.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                }

                //if (response.IsSuccessStatusCode)
                //{
                //    // Parse the response body. Blocking!
                //    var dataObjects = response.Content.ReadAsAsync<IEnumerable<DataObject>>().Result;
                //    foreach (var d in dataObjects)
                //    {
                //        Console.WriteLine("{0}", d.Name);
                //    }



                //    // Create Your Own Folder
                //}
                //else
                //{
                //    Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                //}
                isDone = true;

            }
            catch (Exception ex)
            {
                isDone = false;
            }
           
            return isDone;
        }

        //internal bool filesSaved(List<DataModelDictionary>)
        //{
        //    string strserialize = JsonConvert.SerializeObject(DataModelDictionary);
        //    string path = UserDataFolder();
        //    string filename = AppendTimeStamp("Dictionary") + ".json";
        //    System.IO.File.WriteAllText(path + filename, strserialize);
        //    return true;
        //}
        private static string UserDataFolder()
        {
            try
            {
                string name = Assembly.GetCallingAssembly().GetName().Name;// Assembly.GetEntryAssembly().GetName().Name;
                string folderBase = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                string dir = string.Format(@"{0}\{1}\", folderBase, name.ToString());
                return CheckDir(dir);
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        private static string CheckDir(string dir)
        {
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
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
