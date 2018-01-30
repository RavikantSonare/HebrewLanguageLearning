﻿using HebrewLanguageLearning_Users.GenericClasses;
using HebrewLanguageLearning_Users.Models.DataModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
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
        static private string CurrentUrl = EntityConfig.HostingUri;

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
            string filelocalPath = "";
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
                    var listOfWord = response.Content.ReadAsStringAsync().Result;
                    string path = UserDataFolder("Dictionary", true);
                    string filename = AppendTimeStamp("Dictionary") + ".json";
                    System.IO.File.WriteAllText(path + filename, listOfWord);
                    filelocalPath = listOfWord;

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
            DownloadImages();


            return isDone;
        }

        private void DownloadImages()
        {
            //if (File.Exists(filelocalPath))
            //{
            //JObject results = JObject.Parse(filelocalPath);
            List<DictionaryModel> AllDataObject = new List<DictionaryModel>();
            NullDataReturn:
            AllDataObject = getDataFromLocalFile();
            if (AllDataObject == null && AllDataObject.Count() <= 0) { goto NullDataReturn; }
            List<string> ImagesNameList = new List<string>();

            ImagesNameList = AllDataObject.Select(z => z.ListOfPictures.LastOrDefault()).ToList();

            //Process each object
            try
            {
                foreach (dynamic result in ImagesNameList)
                {
                    lock (this)
                    {
                        if (!string.IsNullOrEmpty(result))
                        {
                            var responseWeb = GetMediaData(result).Result;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }

        }

        private async Task<bool> GetMediaData(string fileName)
        {
            bool isDone = false;
            string filePath = "";
            HttpClient client = new HttpClient();
            try
            {
                lock (this)
                {
                    // List data response.
                    HttpResponseMessage response = client.GetAsync(new Uri("http://biblingo.mobi96.org/Media/Pictures/" + fileName)).Result;

                    // Check that response was successful or throw exception
                    response.EnsureSuccessStatusCode();

                    // Read response asynchronously and save asynchronously to file
                    filePath = UserDataFolder("Media\\Pictures", false) + fileName;
                    using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        //copy the content from response to filestream
                        response.Content.CopyToAsync(fileStream).Wait();

                    }
                }
                
            }
            catch (Exception ex)
            {
                isDone = false;
            }

            return isDone;
        }

        private static string UserDataFolder(string isSub, bool isDelete)
        {
            try
            {
                string name = Assembly.GetCallingAssembly().GetName().Name;// Assembly.GetEntryAssembly().GetName().Name;
                string folderBase = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                string dir = string.Format(@"{0}\{1}\", folderBase, name.ToString());
                return CheckDir(dir, isSub, isDelete);
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        private static string CheckDir(string dir, string isSub, bool isDelete = true)
        {
            if (!isDelete)
            {
                dir = dir + isSub + @"\";
                if (Directory.Exists(dir))
                {
                    return dir;
                }
                else
                {
                    Directory.CreateDirectory(dir);
                }
                return dir;
            }

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
