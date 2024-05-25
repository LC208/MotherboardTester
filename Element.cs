using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MotherboardTester
{
    public class Element
    {
        public string id { get; set; }

        public System.Windows.Shapes.Rectangle checkBox { get; set; }

        public bool isBroken { get; set; }
    
        public Element(bool isBroken, string id, System.Windows.Shapes.Rectangle checkBox) 
        { 
            this.isBroken = isBroken;
            this.id = id;
            this.checkBox = checkBox;
        }
    }
}
