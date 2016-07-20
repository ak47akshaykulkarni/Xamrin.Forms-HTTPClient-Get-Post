using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xamform
{
    class results
    {
       
            public int Code { get; set; }
            public object Description { get; set; }
            public List<results.CResult> Result { get; set; }
            public int ResultCount { get; set; }
        
        public class CResult
        {
            public int Id { get; set; }
            public string Salutation { get; set; }
            public string FirstName { get; set; }
            public string MiddleName { get; set; }
            public string LastName { get; set; }
        }


    }
}
