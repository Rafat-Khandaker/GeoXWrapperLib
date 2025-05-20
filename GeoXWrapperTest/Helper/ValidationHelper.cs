using DCP.Geosupport.DotNet.fld_def_lib;

using System.Text.RegularExpressions;
using GeoXWrapperTest.Model;
using GeoXWrapperLib.Model;
using GeoXWrapperTest.Model.Enum;
using GeoXWrapperLib;

namespace GeoXWrapperTest.Helper
{
    public static class ValidationHelper 
    {
        private static readonly Regex _houseNumRgx = new Regex(@"^[0-9a-zA-Z\s'?!;,:\-()\.\&\/]+$");
        private static readonly Regex _stNameRgx = new Regex(@"^[0-9a-zA-Z\s'?!;,:\-()\.\&\/]+$");

        private static readonly BoroAlias _manhattan = new BoroAlias("Manhattan", "MN", "1");
        private static readonly BoroAlias _newYork = new BoroAlias("New York", "NY", "1");
        private static readonly BoroAlias _bronx = new BoroAlias("Bronx", "BX", "2");
        private static readonly BoroAlias _brooklyn = new BoroAlias("Brooklyn", "BK", "3");
        private static readonly BoroAlias _queens = new BoroAlias("Queens", "QN", "4");
        private static readonly BoroAlias _statenIsland = new BoroAlias("Staten Island", "SI", "5");

        private static readonly fld_dict _fld = new fld_dict();

        public static string ValidateBoroInput(string boro)
        {
            if (string.IsNullOrWhiteSpace(boro))
                return string.Empty;
            else if (_manhattan.Match(boro) || _newYork.Match(boro))
                return "1";
            else if (_bronx.Match(boro))
                return "2";
            else if (_brooklyn.Match(boro))
                return "3";
            else if (_queens.Match(boro))
                return "4";
            else if (_statenIsland.Match(boro))
                return "5";
            else
                return boro;
        }


        public static List<AddressRange> CreateAddressRangeList(IEnumerable<AddrRangeX> addrXList, string tpad)
        {
            if (!addrXList.Any())
                return Enumerable.Empty<AddressRange>().ToList();

            List<AddressRange> arl = new List<AddressRange>();
            foreach (AddrRangeX arx in addrXList)
            {
                if (string.IsNullOrWhiteSpace(arx.b7sc.sc5))
                    break;

                arl.Add(new AddressRange
                {
                    type = arx.addr_type,
                    type_meaning = AddressRange.NormalizedType(arx.addr_type),
                    low_address_number = arx.lhnd,
                    high_address_number = arx.hhnd,
                    street_name = arx.stname,
                    bin = arx.bin.BINToString(),
                    b7sc = arx.b7sc.B7scToString(),
                    tpad_bin_status = "N/A"
                });
            }

            return arl;
        }

        /// <summary>
        /// Assemble the address range keys dictionary
        /// </summary>
        /// <param name="addrXList">The addr_x_list coming from wa2</param>
        /// <returns>Dictionary of k: address type flag as string, v: address type string</returns>
        public static Dictionary<string, string> CreateAddressRangeKeys(IEnumerable<AddrRangeX> addrXList)
        {
            Dictionary<string, string> arkDict = new Dictionary<string, string>
            {
                [string.Empty] = "Ordinary Address Range" //always present
            };

            if (!addrXList.Any())
                return arkDict;

            List<AddrRangeX> arl = addrXList.ToList();
            for (int i = 0; i < arl.Count; i++)
            {
                string addrType = arl[i].addr_type.Trim();

                if (string.IsNullOrWhiteSpace(addrType) || arkDict.ContainsKey(addrType))
                    continue;

                arkDict[addrType] = DescribeAddrKey(addrType);
            }

            return arkDict;
        }

        /// <summary>
        /// Assemble similar names list
        /// </summary>
        /// <param name="outB7scList">WA1's out_b7sc_list</param>
        /// <param name="outStNameList">WA1's out_stname_list</param>
        /// <returns>List of SimilarName objects for with geocall error/warning data</returns>
        public static List<SimilarName> CreateSimilarNamesList(IEnumerable<B7sc> outB7scList, IEnumerable<string> outStNameList)
        {
            if (!outB7scList.Any() || !outStNameList.Any())
                return Enumerable.Empty<SimilarName>().ToList();

            List<B7sc> b7scs = outB7scList.ToList();
            List<string> stNames = outStNameList.ToList();

            if (b7scs.Count != stNames.Count)
                return Enumerable.Empty<SimilarName>().ToList();

            List<SimilarName> snl = new List<SimilarName>();
            for (int i = 0; i < b7scs.Count; i++)
            {
                if (string.IsNullOrWhiteSpace(b7scs[i].B7scToString()))
                    break;

                snl.Add(new SimilarName
                {
                    b7sc = b7scs[i].B7scToString(),
                    streetName = stNames[i]
                });
            }

            return snl;
        }

        public static List<CompleteBIN> CreateCompleteBINList(Wa1 wa1, Wa2F1ax wa2f1ax, string tpad, FunctionCode funcCode, Geo geoCaller)
        {
            string acceptedFunc;
            switch (funcCode)
            {
                case FunctionCode.F1A:
                case FunctionCode.F1B:
                    acceptedFunc = "1A";
                    break;
                case FunctionCode.FBL:
                    acceptedFunc = "BL";
                    break;
                default:
                    acceptedFunc = null;
                    break;
            }

            if (string.IsNullOrEmpty(acceptedFunc))
            {
                return Enumerable.Empty<CompleteBIN>().ToList();
                //throw new ArgumentException("Complete BINs may only be returned from F1A/F1B and FBL");
            }

            bool usingTpad = InsEq(tpad, "y") || InsEq(tpad, "true");

            //if overflow flag is not "E" then we can simply derive the bins list from the existing addr_x_list. No need for secondary calls
            if (InsEq(wa2f1ax.addr_overflow_flag, " "))
            {
                return string.IsNullOrWhiteSpace(wa2f1ax.addr_x_list.FirstOrDefault()?.bin.BINToString())
                    ? Enumerable.Empty<CompleteBIN>().ToList() //if first is empty, they are all empty
                    : wa2f1ax.addr_x_list
                        .Where(arx => !string.IsNullOrWhiteSpace(arx.bin.BINToString())) //filter blanks
                        .GroupBy(arx => arx.bin.BINToString()) //we do not care for differences aside from bin string, so we group on bin string...
                        .Select(arxGroup => arxGroup.First()) //..and only grab the first of each group -> will be unique
                        .Select(arx => new CompleteBIN
                        {
                            bin = arx.bin.BINToString(),
                            tpad = usingTpad
                                ? _fld.get_get_short_def("TPAD_bin_status", arx.TPAD_bin_status)
                                : null //no input TPAD flag => no TPAD bin status
                        })
                        .ToList();
            }

            //If there is a char in the overflow flag, we will need to make secondary calls to the long wa2 to get additional bins
            List<CompleteBIN> bins = new List<CompleteBIN>();

            Wa1 wa1Copy = new Wa1();
            wa1Copy.Wa1FromString(wa1.Wa1ToString()); //hacky deep clone of wa1 so we don't inadverdently alter the original instance

            //alter the copy to prep it for the long WA2 flagged geocall -> we are looking for extra bins that may exist for special cases
            wa1Copy.in_func_code = acceptedFunc;
            wa1Copy.in_long_wa2_flag = "L";
            wa1Copy.in_mode_switch = string.Empty;
            wa1Copy.in_roadbed_request_switch = string.Empty;

            //Execute F1AL with or without the TPAD option input
            if (usingTpad)
            {
                Wa2F1al_TPAD wa2f1al_tpad = new Wa2F1al_TPAD();
                geoCaller.GeoCall(ref wa1Copy, ref wa2f1al_tpad);

                foreach (TPADLongWa2Info binTpad in wa2f1al_tpad.TPAD_list)
                {
                    if (string.IsNullOrWhiteSpace(binTpad.bin.BINToString()))
                        break; //blank means end of list

                    bins.Add(new CompleteBIN
                    {
                        bin = binTpad.bin.BINToString(),
                        tpad = _fld.get_get_short_def("TPAD_bin_status", binTpad.TPAD_bin_status)
                    });
                }
            }
            else
            {
                Wa2F1al wa2f1al = new Wa2F1al();
                geoCaller.GeoCall(ref wa1Copy, ref wa2f1al);

                foreach (BIN bin in wa2f1al.bin_list)
                {
                    if (string.IsNullOrWhiteSpace(bin.BINToString()))
                        break; //blank means end of list

                    bins.Add(new CompleteBIN
                    {
                        bin = bin.BINToString(),
                        tpad = null //no input TPAD flag => no TPAD bin status
                    });
                }
            }

            return bins;
        }

        public static string ValidateStName(string input) => _stNameRgx.Match(input).Success ? input : string.Empty;

        private static bool InsEq(string a, string b) => string.Equals(a, b, StringComparison.OrdinalIgnoreCase);

        readonly struct BoroAlias
        {
            public string Name { get; }
            public string Abbreviation { get; }
            public string Code { get; }

            public BoroAlias(string name, string abbreviation, string code)
            {
                Name = name;
                Abbreviation = abbreviation;
                Code = code;
            }

            /// <summary>
            /// Check wheter an input matches this BoroAlias
            /// </summary>
            /// <param name="input">Input borough string</param>
            /// <returns>True if input matches an accepted name, abbreviation, or code. False otherwise</returns>
            public bool Match(string input) => string.Equals(input, Name, StringComparison.OrdinalIgnoreCase) || string.Equals(input, Abbreviation, StringComparison.OrdinalIgnoreCase) || string.Equals(input, Code, StringComparison.OrdinalIgnoreCase);
        }

        private static string DescribeAddrKey(string key)
        {
            switch (key)
            {
                case "N":
                    return "Non-Addressable Place Name";
                case "A":
                    return "Addressable Place Name";
                case "B":
                    return "Non-Addressable Unnamed Building";
                case "F":
                    return "Vacant Street Frontage";
                case "G":
                    return "Name of NAP Complex";
                case "H":
                    return "Hyphenated Address Range";
                case "M":
                    return "Mixed Hyphenation Address Range";
                case "O":
                    return "Out of Sequence Address";
                case "Q":
                    return "Pseudo Address";
                case "R":
                    return "Real Address for Vanity Address";
                case "U":
                case "V":
                    return "Vanity Address";
                case "W":
                    return "Non-Addressable Building Frontage";
                case "X":
                    return "Constituent NAP of Complex";
                default:
                    return string.Empty;
            }
        }
        public static List<LowHighB7SC> CreateB7ScList(IEnumerable<B7sc> xB7scList, IEnumerable<string> xStNameList)
        {
            if (!xB7scList.Any() || !xStNameList.Any())
                return Enumerable.Empty<LowHighB7SC>().ToList();

            List<B7sc> b7scs = xB7scList.ToList();
            List<string> stNames = xStNameList.ToList();

            if (b7scs.Count != stNames.Count)
                return Enumerable.Empty<LowHighB7SC>().ToList();

            List<LowHighB7SC> b7scList = new List<LowHighB7SC>();
            for (int i = 0; i < b7scs.Count; i++)
            {
                if (string.IsNullOrWhiteSpace(b7scs[i].B7scToString()))
                    break;

                b7scList.Add(new LowHighB7SC
                {
                    b7sc = b7scs[i].B7scToString(),
                    streetName = stNames[i]
                });
            }

            return b7scList;
        }


        public static List<LowHighB7SC> CreateB7ScList(IEnumerable<B7sc> xB7scList, IEnumerable<string> xStNameList, string length, int offset, Geo geoCaller)
        {
            if (!int.TryParse(length, out int len) || len == 0)
                return Enumerable.Empty<LowHighB7SC>().ToList();

            List<LowHighB7SC> b7scList = new List<LowHighB7SC>();
            List<B7sc> b7scs = xB7scList.ToList();
            List<string> stNames = xStNameList.ToList();

            for (int i = 0; i < len; i++)
            {
                b7scList.Add(new LowHighB7SC
                {
                    b7sc = ReadStCode1N(b7scs[i], stNames[i + offset], geoCaller),
                    streetName = stNames[i + offset]
                });
            }

            return b7scList;
        }

        public static string ValidateHouseNum(string input, Geo geoCaller)
        {
            Wa1 wa1Fd = new Wa1
            {
                in_func_code = "D",
                in_platform_ind = "C",
                in_hns = input
            };
            geoCaller.GeoCall(ref wa1Fd);

            return _houseNumRgx.Match(wa1Fd.out_hnd).Success
                ? wa1Fd.out_hnd
                : string.Empty;
        }

        public static string ReadStNameFdg(string inBoro, string inSc5, string inLgc, Geo geoCaller)
        {
            Wa1 wa1 = new Wa1
            {
                in_func_code = "DG",
                in_platform_ind = "C",
                in_b10sc1 = new B10sc
                {
                    boro = inBoro,
                    sc5 = inSc5,
                    lgc = inLgc
                }
            };
            geoCaller.GeoCall(ref wa1);

            return wa1.out_stname1;
        }

        public static string ReadStNameFd(string inBoro, string inStCode, Geo geoCaller)
        {
            Wa1 wa1 = new Wa1
            {
                in_func_code = "D",
                in_platform_ind = "C",
                in_b10sc1 = new B10sc
                {
                    boro = inBoro,
                    sc5 = inStCode
                }
            };
            geoCaller.GeoCall(ref wa1);

            return wa1.out_stname1;
        }

        private static string ReadStCode1N(B7sc b7sc, string st, Geo geoCaller)
        {
            Wa1 wa1F1n = new Wa1
            {
                in_func_code = "1N",
                in_platform_ind = "C",
                in_stname1 = st,
                in_b10sc1 = new B10sc
                {
                    boro = b7sc.boro
                }
            };
            geoCaller.GeoCall(ref wa1F1n);

            string boro = b7sc.boro.Trim().TrimStart('0');
            string sc5 = b7sc.sc5.Trim();
            string lgc = wa1F1n.out_b10sc1.lgc;

            return boro + sc5 + lgc;
        }

        public static string ReadOrientation(string sos)
        {
            switch (sos)
            {
                case "R":
                    return "right";
                case "L":
                    return "left";
                default:
                    return string.Empty;
            }
        }
    }
}
