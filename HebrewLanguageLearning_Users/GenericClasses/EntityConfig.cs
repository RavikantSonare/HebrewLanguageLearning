﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebrewLanguageLearning_Users.GenericClasses
{
    public class EntityConfig
    {
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
    }

    public class SelectListItem
    {   public string Text { get; internal set; }
        public string Value { get; internal set; }
    }
}