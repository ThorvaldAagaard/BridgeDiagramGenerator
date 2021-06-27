using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgeDiagramGenerator
{
    public class BridgeDiagram
    {
        public string FileName { get; set; }
        public string Name { get; set; }
        public string Height { get; set; }
        public string Width { get; set; }
        public string Resolution { get; set; }
        public string[] North { get; set; }
        public string[] South { get; set; }
        public string[] West { get; set; }
        public string[] East { get; set; }

    }
}
