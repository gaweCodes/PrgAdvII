using System;
using System.Collections;
using System.Collections.Generic;

namespace Perf
{
    public class Root
    {
        public string Name;
        public List<Sub1> Sub1List;
    }

    public class Sub1
    {
        public DateTime CreatedAt;
        public int Id;
        public Hashtable Tbl;
        public List<Sub2> Sub2List;
    }

    public class Sub2
    {
        public DateTime CreatedAt;
        public bool IsOk;
        public string Name;
    }
}
