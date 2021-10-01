/* ------------------------------------------------------------------
 * The MIT License (MIT)
 * Copyright (c) 2021 Asif Bhat
   ------------------------------------------------------------------ */
namespace SEO_Analyzer.Models
{
    public class WordCounterViewModel
    {
        public string Text { get; set; }

        public string URL { get; set; }

        public bool IncludeMetaTags { get; set; }

        public bool IncludeExternalSources { get; set; }
    }
}