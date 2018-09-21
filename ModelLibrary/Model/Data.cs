using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLibrary.Model
{
    public class Data
    {
        private int _a;
        private int _b;

        public int A
        {
            get { return _a; }
            set { _a = value; }
        }

        public int B
        {
            get { return _b; }
            set { _b = value; }
        }

        public Data()
        {
                
        }

        public Data(int a, int b)
        {
            _a = a;
            _b = b;
        }
    }
}
