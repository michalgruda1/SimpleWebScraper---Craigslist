using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using SimpleWebScraper.Data;
using System.Text.RegularExpressions;
using SimpleWebScraper.Workers;
using System.Collections.Generic;

namespace SimpleWebScraper.Unit.Test
{
    [TestClass]
    public class ScraperTest
    {
        private readonly Scraper scraper = new Scraper();

        [TestMethod]
        public void FindCollectionWithNoParts()
        {
            string testHtml = "Some dummy text <a href=\"https://boston.craigslist.org/gbs/ctd/d/2014-gmc-sierra-1500-regular/6765227753.html\" data-id=\"6765227753\" class=\"result-title hdrlnk\">2014 GMC Sierra 1500 Regular Cab Pickup 2D 6 1/2 ft pickup RED -</a> more dummy text";

            ScrapeCriteria scrapeCriteriaWithNoPart = new ScrapeCriteriaBuilder()
                .WithData(testHtml)
                .WithRegex(@"<a href=\""(.*?)\"" data-id=\""(.*?)\"" class=\""result-title hdrlnk\"">(.*?)</a>")
                .WithRegexOption(RegexOptions.ExplicitCapture)
                .Build();

            List<string> scrapeResults = scraper.Scrape(scrapeCriteriaWithNoPart);

            Assert.IsTrue(scrapeResults.Count.Equals(1));
            Assert.IsTrue(scrapeResults[0] == "<a href=\"https://boston.craigslist.org/gbs/ctd/d/2014-gmc-sierra-1500-regular/6765227753.html\" data-id=\"6765227753\" class=\"result-title hdrlnk\">2014 GMC Sierra 1500 Regular Cab Pickup 2D 6 1/2 ft pickup RED -</a>");
        }

        [TestMethod]
        public void FindCollectionWithTwoParts()
        {
            string testHtml = "Some dummy text <a href=\"https://boston.craigslist.org/gbs/ctd/d/2014-gmc-sierra-1500-regular/6765227753.html\" data-id=\"6765227753\" class=\"result-title hdrlnk\">2014 GMC Sierra 1500 Regular Cab Pickup 2D 6 1/2 ft pickup RED -</a> more dummy text";

            ScrapeCriteria scrapeCriteriaWithTwoParts = new ScrapeCriteriaBuilder()
                .WithData(testHtml)
                .WithRegex(@"<a href=\""(.*?)\"" data-id=\""(.*?)\"" class=\""result-title hdrlnk\"">(.*?)</a>")
                .WithRegexOption(RegexOptions.ExplicitCapture)
                .WithPart(new ScrapeCriteriaPartBuilder()
                    .WithRegex(@">(.*?)</a>")
                    .WithRegexOption(RegexOptions.Singleline)
                    .Build())
                .WithPart(new ScrapeCriteriaPartBuilder()
                    .WithRegex(@"href=\""(.*?)\""")
                    .WithRegexOption(RegexOptions.Singleline)
                    .Build())
                .Build();

            List<string> scrapeResults = scraper.Scrape(scrapeCriteriaWithTwoParts);

            Assert.IsTrue(scrapeResults.Count.Equals(2));
            Assert.IsTrue(scrapeResults[0] == "2014 GMC Sierra 1500 Regular Cab Pickup 2D 6 1/2 ft pickup RED -");
            Assert.IsTrue(scrapeResults[1] == "https://boston.craigslist.org/gbs/ctd/d/2014-gmc-sierra-1500-regular/6765227753.html");
        }

    }
}
