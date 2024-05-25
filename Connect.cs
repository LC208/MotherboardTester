using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotherboardTester
{
    public class Connect
    {
        public Pair<Element, Element>[] conn;
        public string[] values;
        public int connType;

        public Connect(string[] valuesForConnect,int connType ,params Pair<Element, Element>[] connection) 
        {
            conn = connection;
            values = valuesForConnect;
            this.connType = connType;
        }
    }


}
