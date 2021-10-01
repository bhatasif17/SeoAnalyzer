/* ------------------------------------------------------------------
 * The MIT License (MIT)
 * Copyright (c) 2021 Asif Bhat
   ------------------------------------------------------------------ */
using Microsoft.AspNetCore.Mvc;
using SEO_Analyzer.Helpers.Contracts;
using SEO_Analyzer.Models;
using SEO_Analyzer.Services.Contracts;
using System.Threading.Tasks;

namespace SEO_Analyzer.Controllers
{
    public class AnalyzerController : Controller
    {
        private IWordCountService wordService;
        private IJsonResponseFormatHelper responseFormatHelper;
        public AnalyzerController(IWordCountService wordService, IJsonResponseFormatHelper responseFormatHelper)
        {
            this.wordService = wordService;
            this.responseFormatHelper = responseFormatHelper;
        }

        [HttpPost]
        public async Task<ActionResult> CountWords(WordCounterViewModel model)
        {
            var result = new WordCounterResultViewModel();

            if (!string.IsNullOrEmpty(model.Text))
            {
                result.WordsFrequencies = wordService.GetWordsFrequenciesFromText(model.Text);
            }
            else if (!string.IsNullOrEmpty(model.URL))
            {
                if (!model.URL.StartsWith("http"))
                    model.URL = $"https://{model.URL}";

                result.ExternalSourceUrl = model.URL;

                var source = await wordService.GetRemoteData(model.URL);
                result.ResponseStatus = source.Status;
                result.ResponseStatusMsg = source.StatusMsg;

                if (result.ResponseStatus == Status.Success)
                {
                    result.WordsFrequencies = wordService.GetWordsFrequenciesFromExternalSource(source.Document);

                    if (model.IncludeMetaTags)
                    {
                        result.MetaKeywordsFrequencies = wordService.GetMetaKeywordsFrequencies(source.Document, result.WordsFrequencies);
                    }

                    if (model.IncludeExternalSources)
                    {
                        result.ExternalLinks = wordService.GetExternalLinks(model.URL, source.Document);
                    }
                }
            }

            var jsonResult = this.responseFormatHelper.GetJsonParsedModel(result);
            return this.Json(jsonResult);
        }

    }
}
