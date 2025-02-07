using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PnfUpdateTool
{
    internal class Options
    {
        public Options() { }
        public bool ModelChanged { get; set; }      // 是否为改模文件
        public bool IsGlow { get; set; }            // 是否为发光文件
    }
}
