using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComponentProcessing.Data
{
    public class Replace : IProcessingCharge
    {
        public int ProcessingCharge { get { return 300; } }
        public int ProcessingDuration { get { return 2; } }

        
    }
}
