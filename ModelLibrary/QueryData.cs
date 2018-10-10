using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLibrary
{
    public class QueryData
    {
        private string _operation;

        public string Operation
        {
            get { return _operation; }
            set { _operation = value; }
        }


        public QueryData()
        {
               
        }

        public QueryData(string operation)
        {
            _operation = operation;
        }

        public override string ToString()
        {
            return $"{nameof(Operation)}";
        }
    }
}
