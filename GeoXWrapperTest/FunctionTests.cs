using GeoXWrapperLib;
using GeoXWrapperTest.Helper;
using GeoXWrapperTest.Model;
using GeoXWrapperTest.Model.Display;
using GeoXWrapperTest.Model.Enum;
using GeoXWrapperTest.Model.Response;
using GeoXWrapperTest.Model.Structs;
using GeoXWrapperTest.Service;
using GeoXWrapperTest.Service.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace GeoXWrapperTest
{
    [TestClass]
    public sealed class FunctionTests
    {
        private IServiceProvider ServiceProvider;

        IGeoService GeoService;

        [TestInitialize]
        public void TestInit()
        {
            // Setup the DI container   // Set up the Dependency Injection container
            ServiceProvider = new ServiceCollection()
                .AddSingleton<IGeoService, GeoService>()
                .AddSingleton<Geo, Geo>()
                .BuildServiceProvider();

            GeoService = ServiceProvider.GetService<IGeoService>();
        }

        public static IEnumerable<object[]> F1A_AddrInputs => TestResponseHelper.AddrInputs_Generator(FunctionCode.F1A);

        [TestMethod]
        [DynamicData(nameof(F1A_AddrInputs), DynamicDataSourceType.Property)]
        public void Function1A(AddrInput input, string output)
        {
            var result = JsonSerializer.Serialize(
                                            GeoService.Function1A(new FunctionInput{
                                                Borough = input.Boro,
                                                AddressNo = input.AddrNo,
                                                StreetName = input.StName,
                                                ZipCode = input.Zip,
                                                Unit = input.Unit
                                            }));

            

            var actual = Regex.Replace(result, @"[\r\t\n\s]+", "");
            var expected = Regex.Replace(output, @"[\r\t\n\s]+", "");

            // Uncomment for Debugging Unit Tests for Errors. Read Logs in Debug Folder
            using (StreamWriter writer = new StreamWriter(Directory.GetCurrentDirectory() + "\\Results_1A", append: true))
            {
                writer.WriteLine(actual);
                writer.WriteLine(expected);
            }
         

            Assert.AreEqual(actual, expected);
        }

        public static IEnumerable<object[]> F1B_AddrInputs => TestResponseHelper.AddrInputs_Generator(FunctionCode.F1B);

        [TestMethod]
        [DynamicData(nameof(F1B_AddrInputs), DynamicDataSourceType.Property)]
        public void Function1B(AddrInput input, string output)
        {
            var result = JsonSerializer.Serialize(
                                            GeoService.Function1B(new FunctionInput
                                            {
                                                Borough = input.Boro,
                                                AddressNo = input.AddrNo,
                                                StreetName = input.StName,
                                            }));



            var actual = Regex.Replace(result, @"[\r\t\n\s]+", "");
            var expected = Regex.Replace(output, @"[\r\t\n\s]+", "");

            
            // Uncomment for Debugging Unit Tests for Errors. Read Logs in Debug Folder
            using (StreamWriter writer = new StreamWriter(Directory.GetCurrentDirectory()+"\\Results_1B", append: true))
            {
                writer.WriteLine(actual);
                writer.WriteLine(expected);
            }
            

            Assert.AreEqual(actual, expected);
        }

        public static IEnumerable<object[]> F1E_AddrInputs => TestResponseHelper.AddrInputs_Generator(FunctionCode.F1E);

        [TestMethod]
        [DynamicData(nameof(F1E_AddrInputs), DynamicDataSourceType.Property)]
        public void Function1E(AddrInput input, string output)
        {
            var result = JsonSerializer.Serialize(
                                            GeoService.Function1E(new FunctionInput
                                            {
                                                Borough = input.Boro,
                                                AddressNo = input.AddrNo,
                                                StreetName = input.StName,
                                                ZipCode = input.Zip,
                                                Unit = input.Unit
                                            }));


            var actual = Regex.Replace(result, @"[\r\t\n\s]+", "");
            var expected = Regex.Replace(output, @"[\r\t\n\s]+", "");

           
            // Uncomment for Debugging Unit Tests for Errors. Read Logs in Debug Folder
            using (StreamWriter writer = new StreamWriter(Directory.GetCurrentDirectory() + "\\Results_1E", append: true))
            {
                writer.WriteLine(actual);
                writer.WriteLine(expected);
            }
        

            Assert.AreEqual(actual, expected);
        }

        public static IEnumerable<object[]> F1L_AddrInputs => TestResponseHelper.AddrInputs_Generator(FunctionCode.F1L);

        [TestMethod]
        [DynamicData(nameof(F1L_AddrInputs), DynamicDataSourceType.Property)]
        public void Function1L(AddrInput input, string output)
        {
            var result = JsonSerializer.Serialize(
                                            GeoService.Function1L(new FunctionInput
                                            {
                                                Borough = input.Boro,
                                                AddressNo = input.AddrNo,
                                                StreetName = input.StName,
                                                ZipCode = input.Zip,
                                                Unit = input.Unit
                                            }));


            var actual = Regex.Replace(result, @"[\r\t\n\s]+", "");
            var expected = Regex.Replace(output, @"[\r\t\n\s]+", "");


            // Uncomment for Debugging Unit Tests for Errors. Read Logs in Debug Folder
            using (StreamWriter writer = new StreamWriter(Directory.GetCurrentDirectory() + "\\Results_1L", append: true))
            {
                writer.WriteLine(actual);
                writer.WriteLine(expected);
            }


            Assert.AreEqual(actual, expected);
        }

        public static IEnumerable<object[]> F1N_AddrInputs => TestResponseHelper.AddrInputs_Generator(FunctionCode.F1N);

        [TestMethod]
        [DynamicData(nameof(F1N_AddrInputs), DynamicDataSourceType.Property)]
        public void Function1N(AddrInput input, string output)
        {
            var result = JsonSerializer.Serialize(
                                            GeoService.Function1N(new FunctionInput
                                            {
                                                Borough = input.Boro,
                                                AddressNo = input.AddrNo,
                                                StreetName = input.StName,
                                                StreetNameLength = input.StNameLength,
                                                ZipCode = input.Zip,
                                                Unit = input.Unit
                                            }));


            var actual = Regex.Replace(result, @"[\r\t\n\s]+", "");
            var expected = Regex.Replace(output, @"[\r\t\n\s]+", "");


            // Uncomment for Debugging Unit Tests for Errors. Read Logs in Debug Folder
            using (StreamWriter writer = new StreamWriter(Directory.GetCurrentDirectory() + "\\Results_1N", append: true))
            {
                writer.WriteLine(actual);
                writer.WriteLine(expected);
            }


            Assert.AreEqual(actual, expected);
        }

        public static IEnumerable<object[]> F1R_AddrInputs => TestResponseHelper.AddrInputs_Generator(FunctionCode.F1R);

        [TestMethod]
        [DynamicData(nameof(F1R_AddrInputs), DynamicDataSourceType.Property)]
        public void Function1R(AddrInput input, string output)
        {
            var result = JsonSerializer.Serialize(
                                            GeoService.Function1R(new FunctionInput
                                            {
                                                Borough = input.Boro,
                                                AddressNo = input.AddrNo,
                                                StreetName = input.StName,
                                                StreetNameLength = input.StNameLength,
                                                ZipCode = input.Zip,
                                                Unit = input.Unit
                                            }));


            var actual = Regex.Replace(result, @"[\r\t\n\s]+", "");
            var expected = Regex.Replace(output, @"[\r\t\n\s]+", "");


            // Uncomment for Debugging Unit Tests for Errors. Read Logs in Debug Folder
            using (StreamWriter writer = new StreamWriter(Directory.GetCurrentDirectory() + "\\Results_1R", append: true))
            {
                writer.WriteLine(actual);
                writer.WriteLine(expected);
            }


            Assert.AreEqual(actual, expected);
        }

        public static IEnumerable<object[]> F2Node_AddrInputs => TestResponseHelper.AddrInputs_Generator(FunctionCode.F2Node); 
 
        [TestMethod]
        [DynamicData(nameof(F2Node_AddrInputs), DynamicDataSourceType.Property)]
        public void Function2Node(AddrInput input, string output)
        {
            var result = JsonSerializer.Serialize(GeoService.Function2Node(new FunctionInput{ NodeId = input.NodeId }));

            var actual = Regex.Replace(result, @"[\r\t\n\s]+", "");
            var expected = Regex.Replace(output, @"[\r\t\n\s]+", "");


            // Uncomment for Debugging Unit Tests for Errors. Read Logs in Debug Folder
            using (StreamWriter writer = new StreamWriter(Directory.GetCurrentDirectory() + "\\Results_2Node", append: true))
            {
                writer.WriteLine(actual);
                writer.WriteLine(expected);
            }

            Assert.AreEqual(actual, expected);
        }


    }
}
