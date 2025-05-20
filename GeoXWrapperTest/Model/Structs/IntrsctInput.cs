using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoXWrapperTest.Model.Structs
{
    public readonly struct IntrsctInput
    {
        public string Boro { get; }
        public string St1 { get; }
        public string St2 { get; }

        public string Compass { get; }
        public string NamedAlt { get; }

        public IntrsctInput(string boro, string st1, string st2, string compass = "", string namedAlt = "")
        {
            Boro = boro;
            St1 = st1;
            St2 = st2;

            //Optionals
            Compass = compass;
            NamedAlt = namedAlt;
        }

        public override string ToString() => string.IsNullOrWhiteSpace(Compass)
            ? $"{Boro} | {St1} & {St2}"
            : $"{Boro} | {St1} & {St2} | {Compass}";
    }
}
