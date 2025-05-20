using GeoXWrapperTest.Model.Enum;
using GeoXWrapperTest.Model.Structs;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace GeoXWrapperTest.Helper
{
    public static class TestResponseHelper
    {
        
        public static IEnumerable<object[]> AddrInputs_Generator(FunctionCode code)
        {
            switch (code) 
            {
                case FunctionCode.F1L:
                {
                        yield return new object[] { new AddrInput(addrNo: "a120"), File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Results", "Function" + ReturnFunctionCode(code), "Function" + ReturnFunctionCode(code) + "Response1.json")) };
                        yield return new object[] { new AddrInput(addrNo: "*120"), File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Results", "Function" + ReturnFunctionCode(code), "Function" + ReturnFunctionCode(code) + "Response2.json")) };
                        yield return new object[] { new AddrInput(addrNo: "\\120"), File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Results", "Function" + ReturnFunctionCode(code), "Function" + ReturnFunctionCode(code) + "Response3.json")) };
                        yield return new object[] { new AddrInput(addrNo: "120*-*120"), File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Results", "Function" + ReturnFunctionCode(code), "Function" + ReturnFunctionCode(code) + "Response4.json")) };
                        yield return new object[] { new AddrInput(addrNo: "120a"), File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Results", "Function" + ReturnFunctionCode(code), "Function" + ReturnFunctionCode(code) + "Response5.json")) };
                        break;
                };
                case FunctionCode.F1R:
                {
                        yield return new object[] { new AddrInput(addrNo: "a120"), File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Results", "Function" + ReturnFunctionCode(code), "Function" + ReturnFunctionCode(code) + "Response1.json")) };
                        yield return new object[] { new AddrInput(addrNo: "*120"), File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Results", "Function" + ReturnFunctionCode(code), "Function" + ReturnFunctionCode(code) + "Response2.json")) };
                        yield return new object[] { new AddrInput(addrNo: "\\120"), File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Results", "Function" + ReturnFunctionCode(code), "Function" + ReturnFunctionCode(code) + "Response3.json")) };
                        yield return new object[] { new AddrInput(addrNo: "120*-*120"), File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Results", "Function" + ReturnFunctionCode(code), "Function" + ReturnFunctionCode(code) + "Response4.json")) };
                        yield return new object[] { new AddrInput(addrNo: "120a"), File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Results", "Function" + ReturnFunctionCode(code), "Function" + ReturnFunctionCode(code) + "Response5.json")) };
                        break;
                };
                default:
                {
                        yield return new object[]
                        {
                            new AddrInput(
                                            boro: "1",
                                            addrNo: "120",
                                            sortFormatAddrNo: "000120000AA",
                                            stName: "Broadway",
                                            stNameLength: "4",
                                            zip: "10271",
                                            unit: "30A"
                                        ), File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Results", "Function"+ReturnFunctionCode(code), "Function"+ReturnFunctionCode(code)+"Response1.json"))

                        };

                        yield return new object[]
                        {
                            new AddrInput(
                                            boro: "2",
                                            addrNo: "500",
                                            sortFormatAddrNo: "000500000AA",
                                            stName: "Grand Concourse",
                                            stNameLength: "6",
                                            zip: "10451",
                                            unit: "123"
                                        ), File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Results", "Function"+ReturnFunctionCode(code), "Function"+ReturnFunctionCode(code)+"Response2.json"))

                        };

                        yield return new object[]
                        {
                            new AddrInput(
                                            boro: "3",
                                            addrNo: "620",
                                            sortFormatAddrNo: "000620000AA",
                                            stName: "Atlantic Ave",
                                            stNameLength: "32",
                                            zip: "11217",
                                            unit: "A"
                                        ), File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Results", "Function"+ReturnFunctionCode(code), "Function"+ReturnFunctionCode(code)+"Response3.json"))

                         };
                        yield return new object[]
                        {
                            new AddrInput(
                                            boro: "4",
                                            addrNo: "90-15",
                                            sortFormatAddrNo: "100090015AA",
                                            stName: "Queens Blvd",
                                            stNameLength: "33",
                                            zip: "11373",
                                            unit: "Z2"
                                        ), File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Results", "Function"+ReturnFunctionCode(code), "Function"+ReturnFunctionCode(code)+"Response4.json"))

                        };
                        yield return new object[]
                        {
                            new AddrInput(
                                            boro: "5",
                                            addrNo: "475",
                                            sortFormatAddrNo: "000475000AA",
                                            stName: "Seaview Ave",
                                            stNameLength: "99",
                                            zip: "10305",
                                            unit: "99"
                                        ), File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Results", "Function"+ReturnFunctionCode(code), "Function"+ReturnFunctionCode(code)+"Response5.json"))

                        };

                        break;

                }
                
            }
        }

        public static IEnumerable<object[]> AddrInput_Generator()
        {
            yield return new object[] { "a120", "" };
            yield return new object[] { "*120", "" };
            yield return new object[] { "\\120", "" };
            yield return new object[] { "120*-*120", "" };
            yield return new object[] { "120a", "" };

        }

        private static string ReturnFunctionCode(FunctionCode code)
        {
            switch (code)
            {
                case FunctionCode.F1A:
                    return "1A";
                case FunctionCode.F1B:
                    return "1B";
                case FunctionCode.F1E:
                    return "1E";
                case FunctionCode.FAP:
                    return "AP";
                case FunctionCode.F1L:
                    return "1L";
                case FunctionCode.F1R:
                    return "1R";
                case FunctionCode.F1N:
                    return "1N";
                case FunctionCode.F2:
                    return "2";
                case FunctionCode.F2W:
                    return "2W";
                case FunctionCode.F3:
                    return "3";
                case FunctionCode.F3C:
                    return "3C";
                case FunctionCode.F3S:
                    return "3S";
                case FunctionCode.FBL:
                    return "BL";
                case FunctionCode.FBN:
                    return "BN";
                case FunctionCode.FBF:
                    return "BF";
                case FunctionCode.FD:
                    return "D";
                case FunctionCode.FN:
                    return "N";
                case FunctionCode.FHR:
                    return "HR";
                default:
                    return string.Empty;
            }
        }

    }
}
