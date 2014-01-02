using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PortableClassLibrary
{
    public class LineUp
    {
        public int ID { get; set; }
        public string Start { get; set; }
        public string Finish { get; set; }
        public int BandID { get; set; }
        public int StageID { get; set; }
        public int FestivaldagID { get; set; }
        public virtual Band Band { get; set; }
        public virtual Festivaldag Festivaldag { get; set; }
        public virtual Stage Stage { get; set; }
    }
}