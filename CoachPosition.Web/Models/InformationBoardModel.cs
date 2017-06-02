using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoachPosition.Web.Models
{
    public class InformationBoardModel
    {
        public IEnumerable<int> Cars { get; set; }
        public IEnumerable<string> Letters { get; set; }
        public string NumTrain { get; set; }
        public int Way { get; set; }
    }
}