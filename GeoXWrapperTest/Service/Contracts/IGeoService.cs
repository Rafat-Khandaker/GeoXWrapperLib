using GeoXWrapperLib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoXWrapperTest.Model;
using GeoXWrapperTest.Model.Display;
using GeoXWrapperTest.Model.Response;

namespace GeoXWrapperTest.Service.Contracts
{
    public interface IGeoService
    {
        public GeocallResponse<F1aDisplay, F1aResponse> Function1A(FunctionInput input);

        public GeocallResponse<F1bDisplay, F1bResponse> Function1B(FunctionInput input);

        public GeocallResponse<F1eDisplay, F1eResponse> Function1E(FunctionInput input);
        public GeocallResponse<F1lDisplay, F1lResponse> Function1L(FunctionInput input);

        public GeocallResponse<F1nDisplay, F1nResponse> Function1N(FunctionInput input);

        public GeocallResponse<F1rDisplay, F1rResponse> Function1R(FunctionInput input);


        public GeocallResponse<F2Display, F2Response> Function2(FunctionInput input);

        public GeocallResponse<F3cDisplay, F3cResponse> Function3C(FunctionInput input);

    }
}
