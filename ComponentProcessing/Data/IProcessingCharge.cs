using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComponentProcessing.Data
{
    interface IProcessingCharge
    {
        public int ProcessingCharge { get;  }
        public int ProcessingDuration { get; }
        
    }
}
