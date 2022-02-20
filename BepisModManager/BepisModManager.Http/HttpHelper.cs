﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BepisModManager.Http
{
    public static class HttpHelper
    {
        static readonly HttpClient _httpClient = new HttpClient();

        public static async Task<string> GetStringFromURLAsync(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("User-Agent", $"BepisModManager.Http/${Assembly.GetExecutingAssembly().GetName().Version.ToString()}");
            var response = await _httpClient.SendAsync(request);
            var str = await response.Content.ReadAsStringAsync();
            return str;
        }

        public static async void DownloadFileAsync(string url, string outputPath)
        {
            Uri uri;

            if (!Uri.TryCreate(url, UriKind.Absolute, out uri))
                throw new InvalidOperationException("URI is invalid.");

            if (!File.Exists(outputPath))
                File.Create(outputPath);

            byte[] fileBytes = await _httpClient.GetByteArrayAsync(url);
            File.WriteAllBytes(outputPath, fileBytes);
        }
    }
}
