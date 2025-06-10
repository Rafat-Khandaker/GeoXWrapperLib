using GeoXWrapperLib;
using GeoXWrapperLib.Model;
using GeoXWrapperTest.Helper;
using GeoXWrapperTest.Model;
using GeoXWrapperTest.Model.Display;
using GeoXWrapperTest.Model.Enum;
using GeoXWrapperTest.Model.Response;
using GeoXWrapperTest.Service.Contracts;


namespace GeoXWrapperTest.Service
{
    public class GeoService : IGeoService
    {
        Geo GeoCaller;
        
        public GeoService(Geo _geoCaller) {
            GeoCaller = _geoCaller;
        }

        public GeocallResponse<F1aDisplay, F1aResponse> Function1A(FunctionInput input)
        {
            Wa1 wa1 = new Wa1
            {
                in_func_code = "1A",
                in_platform_ind = "C",
                in_mode_switch = "X",
                in_b10sc1 = new B10sc { boro = ValidationHelper.ValidateBoroInput(input.Borough) },
                in_stname1 = input.StreetName?.Replace(" and ", " & ") ?? string.Empty,
                in_zip_code = input.ZipCode ?? string.Empty,
                in_unit = input.Unit?.Trim() ?? string.Empty,
                in_browse_flag = input.BrowseFlag ?? string.Empty,
                in_tpad_switch = input.TPad ?? string.Empty,
                in_hnd = string.Equals(input.Hns, "true", StringComparison.OrdinalIgnoreCase) || string.Equals(input.Hns, "y", StringComparison.OrdinalIgnoreCase) ? string.Empty : input.AddressNo ?? string.Empty,
                in_hns = string.Equals(input.Hns, "true", StringComparison.OrdinalIgnoreCase) || string.Equals(input.Hns, "y", StringComparison.OrdinalIgnoreCase) ? input.AddressNo ?? string.Empty :  string.Empty,
            };

            Wa2F1ax wa2f1ax = new Wa2F1ax();

            GeoCaller.GeoCall(ref wa1, ref wa2f1ax);

            var _funcReadStNameFd = (string inBoro, string inStCode) =>
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

                GeoCaller.GeoCall(ref wa1);

                return wa1.out_stname1;
            };

            _funcReadStNameFd(wa2f1ax.bid_id.boro, wa2f1ax.bid_id.B5scToString().Remove(0, 1));

    
            return new GeocallResponse<F1aDisplay, F1aResponse>
            {
                display = null,
                root = new F1aResponse(wa1, wa2f1ax)
                {
                    AddressRangeList = ValidationHelper.CreateAddressRangeList(wa2f1ax.addr_x_list, wa1.in_tpad_switch),
                    CompleteBINList = ValidationHelper.CreateCompleteBINList(wa1, wa2f1ax, wa1.in_tpad_switch, FunctionCode.F1A, GeoCaller) //FIXME write test cases
                }
            };
        }

      

        public GeocallResponse<F1bDisplay, F1bResponse> Function1B(FunctionInput input)
        {
            Wa1 wa1 = new Wa1()
            {
                in_func_code = "1B",
                in_platform_ind = "C",

                in_b10sc1 = new B10sc { boro = ValidationHelper.ValidateBoroInput(input.Borough) },
                in_zip_code = input.ZipCode ?? string.Empty,
                in_stname1 = input.StreetName?.Replace(" and ", " & ") ?? string.Empty,
                in_roadbed_request_switch = input.RoadBed ?? string.Empty,
                in_browse_flag = input.BrowseFlag ?? string.Empty,
                in_tpad_switch = input.TPad ?? string.Empty,
                in_unit = input.Unit?.Trim() ?? string.Empty,
                in_hnd = input.AddressNo ?? string.Empty
            };

            Wa2F1b wa2f1b = new Wa2F1b();

            GeoCaller.GeoCall(ref wa1, ref wa2f1b);

            return new GeocallResponse<F1bDisplay, F1bResponse>
            {
                display = null,
                root = new F1bResponse(wa1, wa2f1b)
            };
        }

        public GeocallResponse<F1eDisplay, F1eResponse> Function1E(FunctionInput input)
        {
            Wa1 wa1 = new Wa1()
            {
                in_func_code = "1E",
                in_platform_ind = "C",
                in_mode_switch = "X",

                in_b10sc1 = new B10sc { boro = ValidationHelper.ValidateBoroInput(input.Borough) },
                in_zip_code = input.ZipCode ?? string.Empty,
                in_stname1 = input.StreetName?.Replace(" and ", " & ") ?? string.Empty,
                in_unit = input.Unit?.Trim() ?? string.Empty,
                in_browse_flag = input.BrowseFlag ?? string.Empty,
                in_roadbed_request_switch = input.RoadBed ?? string.Empty,
                in_hnd = input.AddressNo ?? string.Empty

            };
            Wa2F1ex wa2f1ex = new Wa2F1ex();

            GeoCaller.GeoCall(ref wa1, ref wa2f1ex);

            return new GeocallResponse<F1eDisplay, F1eResponse>
            {
                display = null,
                root = new F1eResponse(wa1, wa2f1ex)
            };
        }

        public GeocallResponse<F1lDisplay, F1lResponse> Function1L(FunctionInput input)
        {
            Wa1 wa1 = new Wa1
            {
                in_func_code = "1L",
                in_hnd = input.AddressNo ?? string.Empty
            };

            //geocall and finalize responses
            GeoCaller.GeoCall(ref wa1);

            return new GeocallResponse<F1lDisplay, F1lResponse>
            {
                display = null,
                root = new F1lResponse(wa1)
            };
        }

        public GeocallResponse<F1nDisplay, F1nResponse> Function1N(FunctionInput input)
        {
            Wa1 wa1 = new Wa1
            {
                in_func_code = "1N",

                in_boro1 = ValidationHelper.ValidateBoroInput(input.Borough),
                in_stname1 = input.StreetName?.Replace(" and ", " & ") ?? string.Empty,
                in_snl = input.StreetNameLength ?? "32",
                in_stname_normalization = input.StreetNameFormat ?? "S"
            };

            GeoCaller.GeoCall(ref wa1);

            return new GeocallResponse<F1nDisplay, F1nResponse>
            {
                display = null,
                root = new F1nResponse(wa1)
            };
        }

        public GeocallResponse<F1rDisplay, F1rResponse> Function1R(FunctionInput input)
        {
            Wa1 wa1 = new Wa1
            {
                in_func_code = "1R",
                in_hnd = input.AddressNo ?? string.Empty
            };

            GeoCaller.GeoCall(ref wa1);

            return new GeocallResponse<F1rDisplay, F1rResponse>
            {
                display = null,
                root = new F1rResponse(wa1)
            };

        }

        public GeocallResponse<F2Display, F2Response> Function2(FunctionInput input)
        {
            Wa1 wa1 = new Wa1
            {
                in_func_code = "2W",
                in_platform_ind = "C",
                in_xstreet_names_flag = "E",

                in_boro1 = ValidationHelper.ValidateBoroInput(input.Borough1),
                in_stname1 = input.Street1?.Replace(" and ", " & ") ?? string.Empty,
                in_boro2 = ValidationHelper.ValidateBoroInput(input.Borough2),
                in_stname2 = input.Street2?.Replace(" and ", " & ") ?? string.Empty,
                in_compass_dir = input.CompassFlag ?? string.Empty,
                in_browse_flag = input.BrowseFlag ?? string.Empty
            };
            Wa2F2w wa2f2w = new Wa2F2w();

            GeoCaller.GeoCall(ref wa1, ref wa2f2w);

            var goatlike = new GeocallResponse<F2Display, F2Response>
            {
                display = new F2Display(wa1, wa2f2w, GeoCaller),
                root = null
            };

            if (string.Equals(wa1.out_grc, "03", StringComparison.CurrentCulture))
            {
                //Note: Node list & similar names list are readonly properties
                F2Display f2Display = goatlike.display;

                //B7Sc list and starting points
                Dictionary<int, List<string>> b7scs = ValidationHelper.CreateNodeB7ScDictionary(wa2f2w.node_list, wa2f2w.node_b7scs);

                f2Display.B7SCStartingPoints = b7scs.Keys.ToList();
                f2Display.B7SCList = b7scs.Values
                    .SelectMany(b7sc => b7sc)
                    .ToList();

                //Cross streets list and starting points
                Dictionary<int, List<string>> crxSts = ValidationHelper.CreateCrossStreetDict(wa1.in_boro1, wa2f2w, GeoCaller);

                f2Display.StartingXStreets = crxSts.Keys.ToList();
                f2Display.CrossStreets = crxSts.Values
                    .SelectMany(crx => crx)
                    .ToList();
            }


            return goatlike;
        }

        public GeocallResponse<F3cDisplay, F3cResponse> Function3C(FunctionInput input)
        {
            Wa1 wa1 = new Wa1
            {
                in_func_code = "3C",
                in_platform_ind = "C",
                in_auxseg_switch = "Y",
                in_mode_switch = "E",

                in_boro1 = ValidationHelper.ValidateBoroInput(input.Borough1),
                in_stname1 = input.OnStreet?.Replace(" and ", " & ") ?? string.Empty,
                in_compass_dir = input.CompassFlag ?? string.Empty,
                in_boro2 = ValidationHelper.ValidateBoroInput(input.Borough2),
                in_stname2 = input.FirstCrossStreet?.Replace(" and ", " & ") ?? string.Empty,
                in_boro3 = ValidationHelper.ValidateBoroInput(input.Borough3), 
                in_stname3 = input.SecondCrossStreet?.Replace(" and ", " & ") ?? string.Empty,
                in_browse_flag = input.BrowseFlag ?? string.Empty
            };
            Wa2F3ceas wa2f3ceas = new Wa2F3ceas();

            GeoCaller.GeoCall(ref wa1, ref wa2f3ceas);

            return new GeocallResponse<F3cDisplay, F3cResponse>
            {
                display = new F3cDisplay(wa1, wa2f3ceas, GeoCaller) {
                    LowB7SCList = ValidationHelper.CreateB7ScList(wa2f3ceas.wa2f3ce.lo_x_sts, wa1.out_stname_list, wa2f3ceas.wa2f3ce.lo_x_sts_cnt, 0, GeoCaller),
                    HighB7SCList = ValidationHelper.CreateB7ScList(wa2f3ceas.wa2f3ce.hi_x_sts, wa1.out_stname_list, wa2f3ceas.wa2f3ce.hi_x_sts_cnt, 5, GeoCaller)
                },
                root = null
            };
        }
    }
}
