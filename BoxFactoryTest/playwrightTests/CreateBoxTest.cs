using Microsoft.Playwright.NUnit;

namespace BoxFactoryTest.playwrightTests
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class CreateBoxTest : PageTest
    {
        [Test]
        public async Task CheckInputFields()
        {
            await Page.GotoAsync("http://localhost:4200/box");
            
            Dictionary<string, string> expectedPlaceholders = new Dictionary<string, string>
            {
                {"boxName", "boxName"},
                {"price", "price"},
                {"boxWidth", "boxWidth"},
                {"boxLength", "boxLength"},
                {"boxHeight", "boxHeight"},
                {"boxThickness", "boxThickness"},
                {"boxColor", "boxColor"},
                {"boxImgUrl", "boxImgUrl"}
            };

            foreach (var kvp in expectedPlaceholders)
            {
                var formControlName = kvp.Key;
                var expectedPlaceholder = kvp.Value;

                var element = await Page.QuerySelectorAsync($"ion-input[formControlName='{formControlName}']");
                var actualPlaceholder = await element.GetAttributeAsync("placeholder");

                Assert.AreEqual(expectedPlaceholder, actualPlaceholder, $"Mismatch for {formControlName}");
            }
        }

    }
}