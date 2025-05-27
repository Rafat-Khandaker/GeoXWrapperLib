using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoXWrapperTest.Model
{
    public class FunctionInput
    {
        public string Borough { get; set; } = "";
        public string ZipCode { get; set; } = "";
        public string AddressNo { get; set; } = "";
        public string StreetName { get; set; } = "";
        public string StreetNameLength { get; set; } = "32";
        public string StreetNameFormat { get; set; } = "S";
        public string TPad { get; set; } = "";
        public string BrowseFlag{ get; set; } = "";
        public string Unit { get; set; } = "";
        public string Hns { get; set; } = "n";
        public string NodeId { get; set; }
        public string RoadBed { get; set; } = "";
    }
}
