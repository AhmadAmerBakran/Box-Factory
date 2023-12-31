﻿using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace BoxFactoryTest.playwrightTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class HomePageTest : PageTest
{
    [Test]
    public async Task HomepageHasDataBoxInTitle()
    {
        
        await Page.GotoAsync("http://localhost:5000/home");

        await Expect(Page).ToHaveTitleAsync(new Regex("Data Box"));

        var heading = Page.GetByRole(AriaRole.Heading);
        await Expect(heading).ToHaveTextAsync(new Regex("Welcome to Box Factory project"));

    }
    
    [Test]
    public async Task HomepageHasHeading()
    {
        
        await Page.GotoAsync("http://localhost:5000/");

        var heading = Page.GetByRole(AriaRole.Heading);
        await Expect(heading).ToHaveTextAsync(new Regex("Welcome to Box Factory project"));

    }
    
}