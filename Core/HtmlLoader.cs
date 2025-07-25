﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;

namespace WinFormsApp1.Core
{
    internal class HtmlLoader
    {
        readonly HttpClient client;
        readonly string url;

        public HtmlLoader(IParserSettings parserSettings)
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64)");
            url = $"{parserSettings.BaseUrl}/{parserSettings.Prefix}";
        }


        public async Task<string> GetSourceByPageId(int id)
        {

            var currentUrl= url.Replace("{CurrentId}", id.ToString());
            var responce=await client.GetAsync(currentUrl);
            string source = null;

            if (responce != null && responce.StatusCode == HttpStatusCode.OK)
            {
                source = await responce.Content.ReadAsStringAsync();
            }
            return source;



        }
    }
}
