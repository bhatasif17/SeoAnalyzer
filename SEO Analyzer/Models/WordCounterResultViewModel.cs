/* ------------------------------------------------------------------
 * The MIT License (MIT)
 * Copyright (c) 2021 Asif Bhat
   ------------------------------------------------------------------ */
using System.Collections.Generic;

namespace SEO_Analyzer.Models
{
    public class WordCounterResultViewModel
    {
        public IDictionary<string, int> WordsFrequencies { get; set; }

        public IDictionary<string, int> MetaKeywordsFrequencies { get; set; }

        public string ExternalSourceUrl { get; set; }

        public Status ResponseStatus { get; set; } = Status.NotSpecified;

        public string ResponseStatusMsg { get; set; } = string.Empty;

        public IEnumerable<string> ExternalLinks { get; set; }
    }


}