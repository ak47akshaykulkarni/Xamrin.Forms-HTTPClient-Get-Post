using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xamform
{
    class Authenticate
    {
        public class AResult
        {
            public Nullable<int> Id { get; set; }
            public bool Autheticated { get; set; }
        }

        
            public int Code { get; set; }
            public string Description { get; set; }
            public AResult Result { get; set; }
            public int ResultCount { get; set; }
        
    }
}
