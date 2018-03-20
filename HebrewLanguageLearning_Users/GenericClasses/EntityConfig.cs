using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HebrewLanguageLearning_Users.GenericClasses
{
    public class EntityConfig
    {
        #region Software Configuration
        internal static string HostingUri = "http://biblingo.mobi96.org/Home/";//?UserId="1"//   
       // internal static string HostingUri = "http://localhost:57293/Home/";//?UserId="1"//     
        internal static string Name = Assembly.GetCallingAssembly().GetName().Name;// Assembly.GetEntryAssembly().GetName().Name;
        internal static string FolderBase = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        internal static string MediaUriPictures = FolderBase + @"\" + Name + @"\Media\Pictures\";
        internal static string MediaUriSounds = FolderBase + @"\" + Name + @"\Media\Sounds\";
        internal static string MediaUriVideos = FolderBase + @"\" + Name + @"\Media\Videos\";
        internal static string LocalfileRootPath = System.AppDomain.CurrentDomain.BaseDirectory;
        internal static Dictionary<int, string> APIList = new Dictionary<int, string>() { { 1, "SyncData" }, { 2, "SyncTable" } };
        #endregion

        #region _booksTitleList
        public static List<SelectListItem> _booksTitleList = new List<SelectListItem>()
        {
         new SelectListItem{ Value = "0",  Text ="Select Book"}
        ,new SelectListItem{ Value = "1Chr",  Text ="1 Chronicles" }
        ,new SelectListItem{ Value = "1Kgs",  Text ="1 Kings" }
        ,new SelectListItem{ Value = "1Sam",  Text ="1 Samuel" }
        ,new SelectListItem{ Value = "2Chr",  Text ="2 Chronicles" }
        ,new SelectListItem{ Value = "2Kgs",  Text ="2 Kings" }
        ,new SelectListItem{ Value = "2Sam",  Text ="2 Samuel" }
        ,new SelectListItem{ Value = "Amos",  Text ="Amos" }
        ,new SelectListItem{ Value = "Dan",  Text ="Daniel" }
        ,new SelectListItem{ Value = "Deut",  Text ="Deuteronomy" }
        ,new SelectListItem{ Value = "Eccl", Text ="Ecclesiastes" }
        ,new SelectListItem{ Value = "Est", Text ="Esther" }
        ,new SelectListItem{ Value = "Exod", Text ="Exodus" }
        ,new SelectListItem{ Value = "Ezek", Text ="Ezekiel" }
        ,new SelectListItem{ Value = "Ezra", Text ="Ezra" }
        ,new SelectListItem{ Value = "Gen", Text ="Genesis" }
        ,new SelectListItem{ Value = "Hab", Text ="Habakkuk" }
        ,new SelectListItem{ Value = "Hag", Text ="Haggai" }
        ,new SelectListItem{ Value = "Hos", Text ="Hosea" }
        ,new SelectListItem{ Value = "Isa", Text ="Isaiah" }
        ,new SelectListItem{ Value = "Jer", Text ="Jeremiah" }
        ,new SelectListItem{ Value = "Job", Text ="Job" }
        ,new SelectListItem{ Value = "Joel", Text ="Joel" }
        ,new SelectListItem{ Value = "Jonah", Text ="Jonah" }
        ,new SelectListItem{ Value = "Josh", Text ="Joshua" }
        ,new SelectListItem{ Value = "Judg", Text ="Judges" }
        ,new SelectListItem{ Value = "Lam", Text ="Lamentations" }
        ,new SelectListItem{ Value = "Lev", Text ="Leviticus" }
        ,new SelectListItem{ Value = "Mal", Text ="Malachi" }
        ,new SelectListItem{ Value = "Mic", Text ="Micah" }
        ,new SelectListItem{ Value = "Nah", Text ="Nahum" }
        ,new SelectListItem{ Value = "Neh", Text ="Nehemiah" }
        ,new SelectListItem{ Value = "Num", Text ="Numbers" }
        ,new SelectListItem{ Value = "Obad", Text ="Obadiah" }
        ,new SelectListItem{ Value = "Prov", Text ="Proverbs" }
        ,new SelectListItem{ Value = "Ps", Text ="Psalms" }
        ,new SelectListItem{ Value = "Ruth", Text ="Ruth" }
        ,new SelectListItem{ Value = "Song", Text ="Song of Songs" }
        ,new SelectListItem{ Value = "Zech", Text ="Zechariah" }
        ,new SelectListItem{ Value = "Zeph", Text ="Zephaniah" }
        };

        #endregion

        #region _ProgressBarAssociationList
        public static List<ProgressBarText> _ProgressBarAssociationList = new List<ProgressBarText>()
        {
         new ProgressBarText{ id = 0,  Value="VOCABULARY"}
        ,new ProgressBarText{ id = 1,  Value="ASSOCIATION"}
        ,new ProgressBarText{ id = 2,  Value="PASSIVE KNOWLEDGE" }
        ,new ProgressBarText{ id = 3,  Value="REVIEW GAME" }
        ,new ProgressBarText{ id = 4,  Value="ACTIVE KNOWLEDGE" }
        ,new ProgressBarText{ id = 5,  Value="REVIEW GAME" }
        ,new ProgressBarText{ id = 6,  Value="GRAMMAR" }
        ,new ProgressBarText{ id = 7,  Value="ASSOCIATION" }
        ,new ProgressBarText{ id = 8,  Value="ACTIVE KNOWLEDGE" }
        ,new ProgressBarText{ id = 9,  Value="THE FINAL ACT" }
        //,new ProgressBarText{ id = 10, Value="APPLICATION" }
        };
        #endregion

        #region _ProgressBarAssociationListSecond
        public static List<ProgressBarText> _ProgressBarAssociationListSecond = new List<ProgressBarText>()
        {
         new ProgressBarText{ id = 0,  Value="VOCABULARY"}
        ,new ProgressBarText{ id = 1,  Value="ASSOCIATION"}
        ,new ProgressBarText{ id = 2,  Value="PASSIVE KNOWLEDGE" }
        ,new ProgressBarText{ id = 3,  Value="REVIEW GAME" }
        ,new ProgressBarText{ id = 4,  Value="ACTIVE KNOWLEDGE" }
        ,new ProgressBarText{ id = 5,  Value="REVIEW GAME" }
        ,new ProgressBarText{ id = 6,  Value="GRAMMAR" }
        ,new ProgressBarText{ id = 7,  Value="ASSOCIATION" }
        ,new ProgressBarText{ id = 8,  Value="PASSIVE KNOWLEDGE" }
        ,new ProgressBarText{ id = 9,  Value="ACTIVE KNOWLEDGE" }

        };
        #endregion
    }

    public class SelectListItem
    {
        public string Text { get; internal set; }
        public string Value { get; internal set; }
    }
    public class ProgressBarText
    {
        public int id { get; internal set; }
        public string Value { get; internal set; }
        public bool ShowStatus { get; internal set; }
    }
}
