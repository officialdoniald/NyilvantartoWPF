using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NyilvántartóWPF
{
     public class Megrendelések
     {
          public string ügyfélnév { get; set; }

          public string ügyfélcím { get; set; }

          public string anyagnév { get; set; }

          public string mennyiség { get; set; }

          public string egységár { get; set; }

          public string áfatartalom { get; set; }

          public string megrendelés_id { get; set; }

          public string eredeti_mennyiség { get; set; }

          public string dátum { get; set; }
     }
}
