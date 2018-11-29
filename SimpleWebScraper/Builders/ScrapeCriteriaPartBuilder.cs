using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SimpleWebScraper.Data
{
    class ScrapeCriteriaPartBuilder
    {
        private string regex;
        private RegexOptions regexOption;

        public ScrapeCriteriaPartBuilder()
        {
            SetDefaults();
        }

        private void SetDefaults()
        {
            regex = string.Empty;
            regexOption = new RegexOptions();
        }

        public ScrapeCriteriaPartBuilder WithRegex(string regex)
        {
            this.regex = regex;
            return this;
        }

        public ScrapeCriteriaPartBuilder WithRegexOption(RegexOptions regexOption)
        {
            this.regexOption = regexOption;
            return this;
        }

        public ScrapeCriteriaPart Build()
        {
            ScrapeCriteriaPart scrapeCriteriaPart = new ScrapeCriteriaPart();
            scrapeCriteriaPart.Regex = regex;
            scrapeCriteriaPart.RegexOption = regexOption;
            return scrapeCriteriaPart;
        }
    }
}
