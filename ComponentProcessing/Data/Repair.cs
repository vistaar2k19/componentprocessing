using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComponentProcessing.Data
{
    public class Repair : IProcessingCharge
    {
        public  int  ProcessingCharge { get { return 500; }   }
        public int ProcessingDuration { get { return 5; } }

        
    }
}
