using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework {
    public class ParallelSearchThreadParams {
        public List<string> tmpList { get; set; }
        public string searchWord { get; set; }
        public int levMaxValue { get; set; }
        public int threadNum { get; set; }
    }
}
