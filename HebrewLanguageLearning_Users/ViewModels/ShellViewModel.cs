using Caliburn.Micro;
using HebrewLanguageLearning_Users.Models.DataModels;
using HebrewLanguageLearning_Users.Models.DataProviders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Markup;

namespace HebrewLanguageLearning_Users.ViewModels
{

    public class ShellViewModel : Conductor<object>
    {

        private readonly SimpleContainer container;
        private INavigationService navigationService;

        public ShellViewModel() { }

        public ShellViewModel(SimpleContainer container)
        {
            this.container = container;
        }

        public void RegisterFrame(Frame frame)
        {
            navigationService = new FrameAdapter(frame);

            container.Instance(navigationService);

            // navigationService.NavigateToViewModel(typeof(Account.LoginViewModel));
             navigationService.NavigateToViewModel(typeof(Dashboard.DashboardViewModel));
            //  navigationService.NavigateToViewModel(typeof(BibleLearning.BibleLearningFromMediaWordChoiceViewModel));

            // navigationService.NavigateToViewModel(typeof(BibleLearning.BibleLearningFromMediaViewModel));
            //navigationService.NavigateToViewModel(typeof(BibleLearning.BibleLearningFromMediaViewModel));
            // navigationService.NavigateToViewModel(typeof(BibleLearning.BibleLearningViewModel));
            //  navigationService.NavigateToViewModel(typeof(Game.JerichoGameViewModel));
            //navigationService.NavigateToViewModel(typeof(BibleLearning.BibleLearningFromMediaWordChoiceViewModel));

            //  navigationService.NavigateToViewModel(typeof(Game.JerichoGameViewModel));


        }

        public void MouseDown_Settings(object sender, MouseEventArgs e)
        {
            navigationService.NavigateToViewModel(typeof(Account.SettingViewModel));
            // StateMenu.Toggle(0);
        }

        public void MouseDown_LogOut(object sender, MouseEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        public void MouseDown_Profile(object sender, MouseEventArgs e)
        {

        }

        public void MenuIconOpen()
        {
            if (StateValue == "Hidden") { StateValue = "Visible"; }
            else
            {
                StateValue = "Hidden";
            }
        }

        private string _state = "Hidden";
        public string StateValue
        {
            get { return _state; }
            set
            {
                _state = value;
                NotifyOfPropertyChange(() => StateValue);
            }
        }

        public object StateMenu { get; private set; }

        public StackPanel _VocabDecksPanel;
        public StackPanel VocabDecksPanel
        {
            get { return _VocabDecksPanel; }
            set
            {
                _VocabDecksPanel = value;
                NotifyOfPropertyChange(() => VocabularyLesson);
            }
        }

        public List<VocabDecksGroupModel> _VocabularyLesson;
        public List<VocabDecksGroupModel> VocabularyLesson
        {
            get { return _VocabularyLesson; }
            set
            {
                _VocabularyLesson = value;
                NotifyOfPropertyChange(() => VocabularyLesson);
            }
        }        
        public void VocabDecksMenu()
        {
            try
            {
                DBModel obj = new DBModel();
                DataTable dt = new DataTable();
                dt = (DataTable)obj.SelectData("HLL_VocabDecks");
                List<VocabularyModel> tmplist = new List<VocabularyModel>();
                foreach (DataRow row in dt.Rows)
                {
                    tmplist.Add(new VocabularyModel { LessonId = row.ItemArray[1].ToString(), LessonDecks = row.ItemArray[5].ToString() });

                }
                List<VocabDecksGroupModel> tmpVg = new List<VocabDecksGroupModel>();
                var tmp = tmplist.GroupBy(x => x.LessonId).
                    Select(y => new
                    {
                        LessonId = y.Key,
                        LessonDecks = y.Select(x => x.LessonDecks).ToList()
                    }).ToList();
                object objTemp;
                foreach (var item in tmp)
                {

                    List<VocabularyModel> vocabularyModeltmp = new List<VocabularyModel>();
                    objTemp = item.LessonDecks;

                    foreach (var itemData in item.LessonDecks)
                    {
                        vocabularyModeltmp.Add(new VocabularyModel { LessonDecks = itemData });
                    }

                    tmpVg.Add(new VocabDecksGroupModel { LessonId = "Lesson " + item.LessonId, vocabularyModel = vocabularyModeltmp });
                }
                // vg = tmp;

                VocabularyLesson = tmpVg;

                //   List<VocabularyModel> lst = dt.AsEnumerable().ToList<VocabularyModel>();
                //   List<VocabularyModel> list = dt.AsEnumerable().ToList();
                //   var _DictionaryModellist = JsonConvert.DeserializeObject<List<VocabularyModel>>(Data.ToString());
            }
            catch { }
        }
        
    }
   //public class VocabDecksGroup
   // {
   //     public string LessonId { get; set; }
   //     public List<VocabularyModel> vocabularyModel { get; set; }
   // }


}
