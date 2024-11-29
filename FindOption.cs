using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFindFeature
{
    public class FindOption
    {
        public string StringToFind { get; set; } = string.Empty;
        public bool FindDontContain { get; set; } = false;//v

        public bool IsCaseSensitive { get; set; } = false;//i
        public bool Count { get; set; }= false;//c
        public bool ShowLineNumber { get; set; } = false;//n
        public bool SkipFilesOffline{ get; set; } = true;//off[line]
        public string Path {  get; set; } = string.Empty ;
        public bool Help {  get; set; } = false;    

    }
}
