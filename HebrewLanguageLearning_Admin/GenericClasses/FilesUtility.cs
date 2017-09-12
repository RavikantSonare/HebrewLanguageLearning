using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;

namespace HebrewLanguageLearning_Admin.GenericClasses
{
    public static class FilesUtility
    {
        public static async Task<string> base64ToImage(string base64Img, string Id, string type, string TableRef)
        {
            string image = string.Empty;
            try
            {
                string filePath = string.Empty;

                var path = DateTime.Now;
                var data = String.Format("{0:d/M/yyyy HH:mm:ss}", path);
                data = data.Replace(@"/", "").Trim(); data = data.Replace(@":", "").Trim(); data = data.Replace(" ", String.Empty);
                if (!string.IsNullOrEmpty(base64Img))
                {
                    filePath = HostingEnvironment.MapPath("~/Media/Pictures/");
                    image = Id + type + "_Pic" + data + ".png";
                    //if (File.Exists(filePath + oldImage))
                    //{
                    //    System.IO.File.Delete((filePath + oldImage));
                    //}
                    byte[] bytes = System.Convert.FromBase64String(base64Img);
                    FileStream fs = new FileStream(filePath + image, FileMode.CreateNew, FileAccess.Write, FileShare.None);
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();
                }
                return image;
            }
            catch (Exception ex) { }
            return image;
        }

        public static string FileTobase64Convertion(HttpPostedFileBase file)
        {
            if (file.ContentLength > 0)
            {
                Stream fs = file.InputStream;
                BinaryReader br = new System.IO.BinaryReader(fs);
                byte[] bytes = br.ReadBytes((Int32)fs.Length);
                string obj = Convert.ToBase64String(bytes, 0, bytes.Length);
                return obj;
            }
            return null;
        }

        public static async Task<string> base64ToFile(string base64File, string Id, string type, string TableRef)
        {
            string filePath = string.Empty;
            string audio = string.Empty;
            try
            {

                var path = DateTime.Now;
                var data = String.Format("{0:d/M/yyyy HH:mm:ss}", path);
                data = data.Replace(@"/", "").Trim(); data = data.Replace(@":", "").Trim(); data = data.Replace(" ", String.Empty);
                if (!string.IsNullOrEmpty(base64File))
                {
                    filePath = HostingEnvironment.MapPath("~/Media/Sounds/");
                    audio = Id + type + TableRef + data + ".mp3";
                    //if (File.Exists(filePath + oldImage))
                    //{
                    //    System.IO.File.Delete((filePath + oldImage));
                    //}
                    byte[] bytes = System.Convert.FromBase64String(base64File);
                    FileStream fs = new FileStream(filePath + audio, FileMode.CreateNew, FileAccess.Write, FileShare.None);
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();
                }
                return audio;
            }
            catch (Exception ex) { }
            return audio;
        }

        public static async Task<string> directTofile(HttpPostedFileBase file, string Id, string type, string ext)
        {
            string filePath = string.Empty;
            string video = string.Empty;
            try
            {

                var path = DateTime.Now;
                var data = String.Format("{0:d/M/yyyy HH:mm:ss}", path);
                data = data.Replace(@"/", "").Trim(); data = data.Replace(@":", "").Trim(); data = data.Replace(" ", String.Empty);
                if (!string.IsNullOrEmpty(file.ContentLength.ToString()))
                {
                    filePath = HostingEnvironment.MapPath("~/Media/Videos/");
                    video = Id + type + "_Video" + data + ext;
                    //if (File.Exists(filePath + oldImage))
                    //{
                    //    System.IO.File.Delete((filePath + oldImage));
                    //}

                    byte[] bytes = System.Convert.FromBase64String(FileTobase64Convertion(file));
                    FileStream fs = new FileStream(filePath + video, FileMode.CreateNew, FileAccess.Write, FileShare.None);
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();
                }
                return video;
            }
            catch (Exception ex) { }
            return video;
        }
    }
}