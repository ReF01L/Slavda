using System;
using System.Collections.Generic;
using System.Text;

namespace LastSlavda
{
    public class Employ
    {
        public string name { get; set; }
        public int count { get; set; }

        public Employ(string name, int count)
        {
            this.name = name;
            this.count = count;
        }
    }
}
