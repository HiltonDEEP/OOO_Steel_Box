using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOO_Steel_Box.Classes
{
    internal class BuildInOrder
    {
        public Classes.PCBuildExtended PCBuildExtended { get; set; }
        public int countBuildInOrder {  get; set; }
        public BuildInOrder(Classes.PCBuildExtended pcBuildExtended)
        {
            PCBuildExtended = pcBuildExtended;
            countBuildInOrder = 1;
        }
    }
}
