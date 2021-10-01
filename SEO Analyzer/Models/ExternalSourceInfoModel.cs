/* ------------------------------------------------------------------
 * The MIT License (MIT)
 * Copyright (c) 2021 Asif Bhat
   ------------------------------------------------------------------ */
using HtmlAgilityPack;

namespace SEO_Analyzer.Models
{
    public class ExternalSourceInfoModel
    {
        public HtmlDocument Document { get; set; }

        public Status Status { get; set; }

        public string StatusMsg { get; set; }
    }
}
