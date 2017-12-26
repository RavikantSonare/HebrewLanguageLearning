using Caliburn.Micro;
using HebrewLanguageLearning_Users.GenericClasses;
using HebrewLanguageLearning_Users.Models.DataControllers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HebrewLanguageLearning_Users.ViewModels.BibleLearning
{
    public class BibleLearningViewModel : Conductor<object>
    {
        BibleLearningController _ObjBC = new BibleLearningController();
        private string _BibleTxtLesson = @"בראשית ברא אלוהים את השמים ואת הארץ.
והארץ היתה ללא צורה, וריק; ואת החושך על פני המעמקים.ורוח אלוהים נעה על פני המים.
ויאמר אלהים יהי אור ויהי אור.
וירא ה 'את האור, כי טוב: ואלוהים חילק את האור מהחושך.
ואלוהים קרא את יום האור, ואת החושך הוא קרא לילה.והערב והבוקר היו היום הראשון.
בראשית ברא אלוהים את השמים ואת הארץ.
והארץ היתה ללא צורה, וריק; ואת החושך על פני המעמקים.ורוח אלוהים נעה על פני המים.
ויאמר אלהים יהי אור ויהי אור.
וירא ה 'את האור, כי טוב: ואלוהים חילק את האור מהחושך.
ואלוהים קרא את יום האור, ואת החושך הוא קרא לילה.והערב והבוקר היו היום הראשון.
ויאמר אלהים, יהי ברקע בתוך המים, ונתן לו לחלק את המים מן המיםבראשית ברא אלוהים את השמים ואת הארץ.
והארץ היתה ללא צורה, וריק; ואת החושך על פני המעמקים.ורוח אלוהים נעה על פני המים.
ויאמר אלהים יהי אור ויהי אור.
וירא ה 'את האור, כי טוב: ואלוהים חילק את האור מהחושך.
ואלוהים קרא את יום האור, ואת החושך הוא קרא לילה.והערב והבוקר היו היום הראשון.
ויאמר אלהים, יהי ברקע בתוך המים, ונתן לו לחלק את המים מן המיםבראשית ברא אלוהים את השמים ואת הארץ.
והארץ היתה ללא צורה, וריק; ואת החושך על פני המעמקים.ורוח אלוהים נעה על פני המים.
ויאמר אלהים יהי אור ויהי אור.
וירא ה 'את האור, כי טוב: ואלוהים חילק את האור מהחושך.
ואלוהים קרא את יום האור, ואת החושך הוא קרא לילה.והערב והבוקר היו היום הראשון.
ויאמר אלהים, יהי ברקע בתוך המים, ונתן לו לחלק את המים מן המיםבראשית ברא אלוהים את השמים ואת הארץ.
והארץ היתה ללא צורה, וריק; ואת החושך על פני המעמקים.ורוח אלוהים נעה על פני המים.
ויאמר אלהים יהי אור ויהי אור.
וירא ה 'את האור, כי טוב: ואלוהים חילק את האור מהחושך.
ואלוהים קרא את יום האור, ואת החושך הוא קרא לילה.והערב והבוקר היו היום הראשון.
ויאמר אלהים, יהי ברקע בתוך המים, ונתן לו לחלק את המים מן המיםויאמר אלהים, יהי ברקע בתוך המים, ונתן לו לחלק את המים מן המים.";

        public string BibleTxtLesson
        {
            get { return _BibleTxtLesson; }
            set
            {
                _BibleTxtLesson = value;
                NotifyOfPropertyChange(() => BibleTxtLesson);
            }
        }
        private string _bibleTxt = @"בראשית ברא אלוהים את השמים ואת הארץ.
והארץ היתה ללא צורה, וריק; ואת החושך על פני המעמקים.ורוח אלוהים נעה על פני המים.
ויאמר אלהים יהי אור ויהי אור.
וירא ה 'את האור, כי טוב: ואלוהים חילק את האור מהחושך.
ואלוהים קרא את יום האור, ואת החושך הוא קרא לילה.והערב והבוקר היו היום הראשון.
ויאמר אלהים, יהי ברקע בתוך המים, ונתן לו לחלק את המים מן המים.";
        private List<SelectListItem> _ItemBook = EntityConfig._booksTitleList;
        public List<SelectListItem> ItemBook
        {
            get { return _ItemBook; }
            set
            {
                _ItemBook = value;
                NotifyOfPropertyChange(() => ItemBook);
            }
        }


        public string BibleTxt
        {
            get { return _bibleTxt; }
            set
            {
                _bibleTxt = value;
                NotifyOfPropertyChange(() => BibleTxt);
            }
        }
        protected override void OnActivate()
        {

            base.OnActivate();

            //MessageBox.Show("Deshboard");//This is for testing - currently doesn't display
        }
        public void DDLBookSelect(SelectListItem _ObjList)
        {
            string _currentfileName = _ObjList.Value;
            if (!string.IsNullOrEmpty(_currentfileName))
            {
                _ObjBC.ShowBookData(_currentfileName);
            }
        }
        //BibleLearningViewModel()
        //{
        //  //  var ItemBook = EntityConfig._booksTitleList;
        //}

    }
}
