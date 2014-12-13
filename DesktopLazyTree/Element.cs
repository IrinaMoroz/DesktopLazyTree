using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopLazyTree
{
    public class Element
    {
        public Element(string path, bool isFolder)
        {
            Path = path;
            IsFolder = isFolder;
        }
        public bool IsFolder
        {
            get;
            set;
        }
        public string Path
        {
            set;
            get;
        }

        public override string ToString()
        {
            return GetNameFromPath(Path); ;
        }

        private string GetNameFromPath(string path)
        {
            StringBuilder sb = new StringBuilder();
            int lastIdx = path.LastIndexOf("\\");
            int firstIdx = path.IndexOf("\\");
            string name = path;
            if(path.IndexOf("\\") != path.Length-1)
                name = path.Substring(lastIdx + 1);
            return name;
        }
    }
}
