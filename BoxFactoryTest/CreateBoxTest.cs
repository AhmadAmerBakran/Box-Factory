using System.Net.Http.Json;
using FluentAssertions;
using FluentAssertions.Execution;
using Newtonsoft.Json;


namespace BoxFactoryTest
{
    public class CreateBoxTest
    {
        private HttpClient _httpClient;

        [SetUp]
        public void Setup()
        {
            _httpClient = new HttpClient();
        }

        [Test]
        public async Task CreateBoxSuccessfully()
        {
            // Trigger any setup tasks
            Helper.TriggerRebuild();

            var box = new Box()
            {
                Id = 1,
                BoxName = "Mock Box",
                Price = 20,
                BoxWidth = 100,
                BoxLength = 150,
                BoxHeight = 80,
                BoxThickness = 5,
                BoxColor = "Brown",
                BoxImgUrl = ""
            };
            
            var url = "http://localhost:5000/api/box";
            
            HttpResponseMessage response;
            try
            {
                response = await _httpClient.PostAsJsonAsync(url, box);
                TestContext.WriteLine("Body Response: " + await response.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                throw new Exception(Helper.NoResponseMessage, e);
            }

            Box responseObject;
            try
            {
                responseObject = JsonConvert.DeserializeObject<Box>(
                    await response.Content.ReadAsStringAsync()) ?? throw new InvalidOperationException();
            }
            catch (Exception e)
            {
                throw new Exception(Helper.BadResponseBody(await response.Content.ReadAsStringAsync()), e);
            }

            using (new AssertionScope())
            {
                (await Helper.IsCorsFullyEnabledAsync(url)).Should().BeTrue();
                response.IsSuccessStatusCode.Should().BeTrue();
                responseObject.Should().BeEquivalentTo(box, Helper.MyBecause(responseObject, box));
            }
        }

        [TestCase("Mock Box", 2, "Red", 15, 100, 40, 5)]
        [TestCase("", 5, "Red", 15, 100, 40, 1)]
        [TestCase("Very long box name that should not pass the validation", 25, "Blue", 15, 220, 100, 2)]
        public async Task ShouldFailDueToDataValidation(string boxName, double price, string boxColor, double boxWidth, double boxLength, double boxHight, double boxThickness)
        {
            var box = new Box()
            {
                BoxName = boxName,
                Price = price,
                BoxWidth = boxWidth,
                BoxLength = boxLength,
                BoxHeight = boxHight,
                BoxThickness = boxThickness,
                BoxColor = boxColor
            };
            
            HttpResponseMessage response;
            try
            {
                response = await _httpClient.PostAsJsonAsync("http://localhost:5000/api/box", box);
                TestContext.WriteLine("Body Response: " + await response.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                throw new Exception(Helper.NoResponseMessage, e);
            }

            response.IsSuccessStatusCode.Should().BeFalse();
        }
    }
}
