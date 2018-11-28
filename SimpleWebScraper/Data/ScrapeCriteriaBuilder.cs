using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SimpleWebScraper.Data
{
    class ScrapeCriteriaBuilder
    {
        private string regex;
        private string data;
        private RegexOptions regexOption;
        private List<ScrapeCriteriaPart> parts;

        public ScrapeCriteriaBuilder()
        {
            SetDefaults();
        }

        private void SetDefaults()
        {
            regex = string.Empty;
            data = string.Empty;
            regexOption = RegexOptions.None;
            parts = new List<ScrapeCriteriaPart>();
        }

        public ScrapeCriteriaBuilder WithRegex(string regex)
        {
            this.regex = regex;
            return this;
        }

        public ScrapeCriteriaBuilder WithData(string data)
        {
            this.data = data;
            return this;
        }

        public ScrapeCriteriaBuilder WithRegexOption(RegexOptions regexOption)
        {
            this.regexOption = regexOption;
            return this;
        }

        public ScrapeCriteriaBuilder WithPart(ScrapeCriteriaPart scrapeCriteriaPart)
        {
            this.parts.Add(scrapeCriteriaPart);
            return this;
        }

        public ScrapeCriteria Build()
        {
            ScrapeCriteria scrapeCriteria = new ScrapeCriteria();
            scrapeCriteria.Regex = this.regex;
            scrapeCriteria.Data = this.data;
            scrapeCriteria.RegexOption = regexOption;
            scrapeCriteria.Parts = parts;
            return scrapeCriteria;
        }
    }
}
