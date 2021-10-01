/* ------------------------------------------------------------------
 * The MIT License (MIT)
 * Copyright (c) 2021 Asif Bhat
   ------------------------------------------------------------------ */
using System.Collections.Generic;

namespace SEO_Analyzer.Models
{
    public class FriendlyJsonFormatModel
    {
        public IEnumerable<DictEntry> WordsFrequencies { get; set; }

        public IEnumerable<DictEntry> MetaKeywordsFrequencies { get; set; }

        public string ExternalSourceUrl { get; set; }

        public string ResponseStatus { get; set; }

        public string ResponseStatusMsg { get; set; }

        public IEnumerable<DictEntry> ExternalLinks { get; set; }
    }
}
