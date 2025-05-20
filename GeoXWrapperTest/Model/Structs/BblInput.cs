using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoXWrapperTest.Model.Structs
{
    public readonly struct BblInput
    {
        public string Boro { get; }
        public string Block { get; }
        public string Lot { get; }

        public string Bbl => $"{int.Parse(Boro):D1}{int.Parse(Block):D5}{int.Parse(Lot):D4}";

        public BblInput(string boro, string block, string lot)
        {
            Boro = boro;
            Block = block;
            Lot = lot;
        }

        public override string ToString() => Bbl;
    }
}
