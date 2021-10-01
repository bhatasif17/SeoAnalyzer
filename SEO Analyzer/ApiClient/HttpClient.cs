/* ------------------------------------------------------------------
 * The MIT License (MIT)
 * Copyright (c) 2021 Asif Bhat
   ------------------------------------------------------------------ */

namespace SEO_Analyzer.ApiClient
{
    public class HttpClient
    {
        public System.Net.Http.HttpClient Client { get; }
        public HttpClient(System.Net.Http.HttpClient client)
        {
            Client = client;
        }
    }
}
