/* ------------------------------------------------------------------
 * The MIT License (MIT)
 * Copyright (c) 2021 Asif Bhat
   ------------------------------------------------------------------ */
using HtmlAgilityPack;
using Microsoft.Extensions.Hosting;
using SEO_Analyzer.Helpers.Contracts;
using SEO_Analyzer.Models;
using SEO_Analyzer.Services.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
namespace SEO_Analyzer.Services
{
    public class WordCountService : IWordCountService
    {
        //private string pathToJsonStopwordsName = "stopwordsPath";
        private char[] separators = new char[] { ' ', ',', '.', ':', ';', '&', '\t', '?', '!', '"' };
        private string validWordPattern = @"(^[a-z]{3,}$)";//english, at least 3 symbol long
        private IConfigHelper configHelper;
        private IFileHelper fileHelper;
        private IHtmlDocumentHelper htmlHelper;
        private IHostEnvironment hostEnvironment;
        private ApiClient.HttpClient apiClient;

        public WordCountService(IConfigHelper configHelper,
                                IFileHelper fileHelper,
                                IHtmlDocumentHelper htmlHelper,
                                IHostEnvironment hostEnvironment,
                                ApiClient.HttpClient apiClient)
        {
            this.configHelper = configHelper;
            this.fileHelper = fileHelper;
            this.htmlHelper = htmlHelper;
            this.hostEnvironment = hostEnvironment;
            this.apiClient = apiClient;
        }

        public async Task<ExternalSourceInfoModel> GetRemoteData(string url)
        {
            var result = new ExternalSourceInfoModel();
            result.Status = Status.NotResolved;

            try
            {
                var uri = new Uri(url);
                var resp = await apiClient.Client.GetAsync(uri);
                if (resp.IsSuccessStatusCode)
                {
                    var content = resp.Content.ReadAsStreamAsync().Result;
                    var stream = new StreamReader(content, Encoding.UTF8);
                    var htmlText = stream.ReadToEnd();
                    var document = new HtmlDocument();
                    document.LoadHtml(htmlText);

                    if (htmlHelper.IsValid(document))
                    {
                        htmlHelper.SanitizeContent(document);
                        result.Status = Status.Success;
                        result.Document = document;
                    }
                }
                else
                {
                    result.Status = Status.NotResolved;
                    result.StatusMsg = "Status Message:" + resp.StatusCode.ToString();
                }
            }
            catch (Exception ex)
            {
                result.StatusMsg = ex.Message;
                result.Status = Status.Failed;
            }

            return result;
        }

        public IDictionary<string, int> GetWordsFrequenciesFromText(string text)
        {
            var dictionary = new Dictionary<string, int>();
            HashSet<string> stopWords = GetStopWords();
            if (!string.IsNullOrEmpty(text) && !string.IsNullOrWhiteSpace(text))
            {
                text = text.Trim();
                var words = text.Split(separators).ToList();
                foreach (var w in words)
                {
                    if (!string.IsNullOrEmpty(w) && !string.IsNullOrWhiteSpace(w)
                          && !stopWords.Contains(w.ToLower())
                          && Regex.IsMatch(w.ToLower(), validWordPattern))
                    {
                        if (dictionary.ContainsKey(w.ToLower()))
                        {
                            dictionary[w.ToLower()] += 1;
                        }
                        else
                        {
                            dictionary.Add(w.ToLower(), 1);
                        }
                    }

                }

            }

            return dictionary;
        }

        public IDictionary<string, int> GetWordsFrequenciesFromExternalSource(HtmlDocument document)
        {
            var body = document.DocumentNode.SelectSingleNode("//body");
            var text = body.InnerText;
            var dictionary = GetWordsFrequenciesFromText(text);

            return dictionary;
        }

        public IDictionary<string, int> GetMetaKeywordsFrequencies(HtmlDocument document, IDictionary<string, int> wordsCountDictionary)
        {
            var metaKeywordsFrequencesDictionary = new Dictionary<string, int>();
            HashSet<string> stopWords = GetStopWords();
            var keywordsContent = string.Empty;
            HtmlNode metaNode = document.DocumentNode.SelectSingleNode("//meta[contains(@name, 'keyword')]");

            //var metaNode = document.DocumentNode.SelectNodes("//meta/@content");
            if (metaNode != null)
            {
                keywordsContent = metaNode.GetAttributeValue("content", string.Empty);
            }

            if (!string.IsNullOrEmpty(keywordsContent) && wordsCountDictionary != null)
            {
                var keywords = keywordsContent.Split(separators);
                if (keywords.Any())
                {
                    foreach (var word in keywords)
                    {
                        if (!string.IsNullOrEmpty(word) && !stopWords.Contains(word)
                            && wordsCountDictionary.ContainsKey(word)
                            && !metaKeywordsFrequencesDictionary.ContainsKey(word))
                        {
                            var frequency = wordsCountDictionary[word];
                            metaKeywordsFrequencesDictionary.Add(word, frequency);
                        }
                        else
                        {
                            if (!string.IsNullOrWhiteSpace(word) && metaKeywordsFrequencesDictionary.ContainsKey(word))
                            {
                                metaKeywordsFrequencesDictionary[word] += 1;
                            }
                            else if (!string.IsNullOrWhiteSpace(word))
                            {
                                metaKeywordsFrequencesDictionary.Add(word, 1);
                            }
                        }

                    }
                }

            }

            return metaKeywordsFrequencesDictionary;
        }

        public IList<string> GetExternalLinks(string url, HtmlDocument document)
        {
            return htmlHelper.GetExternalLinks(url, document);
        }

        private HashSet<string> GetStopWords()
        {
            var path = $"{this.hostEnvironment.ContentRootPath}/wwwroot/stopwords.json";
            HashSet<string> stopWords = fileHelper.GetUniqueResultsFromJson(path);
            return stopWords;
        }
    }

}
