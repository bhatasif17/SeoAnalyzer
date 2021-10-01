/* ------------------------------------------------------------------
 * The MIT License (MIT)
 * Copyright (c) 2021 Asif Bhat
   ------------------------------------------------------------------ */
using SEO_Analyzer.Helpers.Contracts;
using SEO_Analyzer.Models;
using System.Linq;
using System.Text.Json;

namespace SEO_Analyzer.Helpers
{
    public class JsonResponseFormatHelper : IJsonResponseFormatHelper
    {
        public string GetJsonParsedModel(WordCounterResultViewModel inputModel)
        {
            var formatModel = new FriendlyJsonFormatModel();
            formatModel.ExternalSourceUrl = inputModel.ExternalSourceUrl;
            formatModel.ResponseStatus = inputModel.ResponseStatus.ToString();
            formatModel.ResponseStatusMsg = inputModel.ResponseStatusMsg;

            if (inputModel.WordsFrequencies != null)
            {
                formatModel.WordsFrequencies = inputModel.WordsFrequencies.Select(c => new DictEntry { Key = c.Key, Value = c.Value }).ToList();
            }

            if (inputModel.MetaKeywordsFrequencies != null)
            {
                formatModel.MetaKeywordsFrequencies = inputModel.MetaKeywordsFrequencies.Select(c => new DictEntry { Key = c.Key, Value = c.Value }).ToList();
            }

            if (inputModel.ExternalLinks != null)
            {
                formatModel.ExternalLinks = inputModel.ExternalLinks.Select(c => new DictEntry { Key = c }).ToList();
            }

            var jsonResult = JsonSerializer.Serialize(formatModel);

            return jsonResult;
        }
    }
}
