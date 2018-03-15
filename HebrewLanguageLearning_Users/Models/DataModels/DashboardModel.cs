using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebrewLanguageLearning_Users.Models.DataModels
{
   public class DashboardModel
    {
        public int completedLesson { get; set; }
        public int completedPhases { get; set; }
        public int completedParagraph { get; set; }
        public string currentBookAndchapterId { get; set; }
        public int currentScreenStatus { get; set; }
        public string LeftDate { get; set; }
      
    }
}
