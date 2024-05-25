using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotherboardTester
{
    public class Motherboard
    {
        public string name;
        public List<Element> elements;
        public Motherboard(string name, params Element[] elements) {
            this.name = name;
            this.elements = new List<Element>(elements);
        }

    }
}
