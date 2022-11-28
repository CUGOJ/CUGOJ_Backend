using CUGOJ.Backend.Share.Infra;
using CUGOJ.Backend.Tools.Log;
using CUGOJ.Backend.Tools;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CUGOJ.Backend.Tools.Infra.HttpProvider
{
    // Singleton Service
    public class HttpProvider : IHttpProvider
    {
        private readonly Logger? _logger;
        public HttpProvider(Logger? logger=null)
        {
            _logger = logger;
            if(Config.AllowUnsafeSSL)
            {
                var handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback = delegate { return true; };
                client = new HttpClient(handler);
            }
            else
            {
                client = new HttpClient();
            }
        }
        private readonly HttpClient client;

        /// <summary>
        /// 使用Get发起一个Http请求，响应Content将会被解析成传入的类型T
        /// </summary>
        /// <typeparam name="T">要解析的结果类型</typeparam>
        /// <param name="baseUrl">请求基地址，形如 https://xxx.cugoj.xxx:12345/problem/:id </param>
        /// <param name="param">请求要注入的参数，参数会优先注入到路径变量，其次才是请求参数</param>
        /// <param name="headers">请求要注入的headers</param>
        /// <returns>响应结果</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<HttpResult<T>> Get<T>(string baseUrl, Dictionary<string, string>? param = null, Dictionary<string, string>? headers = null)
        {
            try
            {
#if DEBUG
                var parsedUri = ParseGetUrl(baseUrl, param);
                _logger?.Info($"HttpProvider.Get: {parsedUri}");
                var uri = new Uri(parsedUri);
                DateTime startTime = DateTime.Now;
#else
            var uri = new Uri(ParseGetUrl(baseUrl, param));
#endif
                var request = new HttpRequestMessage(HttpMethod.Get, uri);
                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        request.Headers.Add(header.Key, header.Value);
                    }
                }
                var response = await client.SendAsync(request);
                var result = new HttpResult<T>();
                result.Code = response.StatusCode;
                if (response.IsSuccessStatusCode)
                {
                    result.IsSuccess = true;
                    if (typeof(T) == typeof(string) || !typeof(T).IsClass)
                    {
                        result.Data = ParseBaseType<T>(await response.Content.ReadAsStringAsync());
                    }
                    else
                    {
                        result.Data = await System.Text.Json.JsonSerializer.DeserializeAsync<T>(response.Content.ReadAsStream());
                    }
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = await response.Content.ReadAsStringAsync();
                }
#if DEBUG
                _logger?.Info($"HttpProvider.Get: {uri} 用时: {(DateTime.Now - startTime).TotalMilliseconds}ms");
                _logger?.Info($"HttpProvider.Get: resp = {System.Text.Json.JsonSerializer.Serialize(result)}");
#endif
                return result;
            }
            catch(Exception e)
            {
                _logger?.Error($"Get请求发生Exception,message = {e.Message};stack = {e.StackTrace}");
                return new HttpResult<T>
                {
                    Code = System.Net.HttpStatusCode.BadRequest,
                    IsSuccess = false,
                    Message = $"请求出错,err={e}"
                };
            }
        }

        /// <summary>
        /// 使用Post发起一个Http请求，响应Content将会被解析成传入的类型T
        /// </summary>
        /// <typeparam name="T">要解析的结果类型</typeparam>
        /// <param name="baseUrl">请求基地址，形如 https://xxx.cugoj.xxx:12345/problem/:id </param>
        /// <param name="param">请求要注入的参数，参数会优先注入到路径变量，剩余参数会以Json的形式放在请求Body中</param>
        /// <param name="headers">请求要注入的headers</param>
        /// <returns>响应结果</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<HttpResult<T>> Post<T>(string baseUrl, Dictionary<string, string>? param = null, Dictionary<string, string>? headers = null)
        {
            var uri = new Uri(ParsePostUrl(baseUrl, param));
#if DEBUG
            _logger?.Log(LogLevel.Information, "HttpProvider.Post: {uri}", uri.ToString());
            DateTime startTime = DateTime.Now;

#endif
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }
            if (param != null)
            {
                request.Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(param), Encoding.UTF8, "application/json");
            }
#if DEBUG
            _logger?.Info($"HttpProvider.Post: Content = {request.Content}");
#endif
            var response = await client.SendAsync(request);
            var result = new HttpResult<T>();
            result.Code = response.StatusCode;
            if (response.IsSuccessStatusCode)
            {
                result.IsSuccess = true;
                if (typeof(T) == typeof(string) || !typeof(T).IsClass)
                {
                    result.Data = ParseBaseType<T>(await response.Content.ReadAsStringAsync());
                }
                else
                {
                    result.Data = await System.Text.Json.JsonSerializer.DeserializeAsync<T>(response.Content.ReadAsStream());
                }
            }
            else
            {
                result.IsSuccess = false;
                result.Message = await response.Content.ReadAsStringAsync();
            }
#if DEBUG
            _logger?.Info($"HttpProvider.Post: {uri} 用时: {(DateTime.Now - startTime).TotalMilliseconds}ms");
            _logger?.Info($"HttpProvider.Post: resp = {System.Text.Json.JsonSerializer.Serialize(result)}");
#endif
            return result;
        }

        public async Task<HttpResult<T>> Post<T>(string baseUrl, string content)
        {
            var uri = new Uri(baseUrl);
#if DEBUG
            _logger?.Log(LogLevel.Information, "HttpProvider.Post: {uri}", uri.ToString());
            DateTime startTime = DateTime.Now;
#endif
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Content = new StringContent($"\"{content}\"", Encoding.UTF8, "application/json");
#if DEBUG
            _logger?.Info($"HttpProvider.Post: Content = {request.Content}");
#endif
            var response = await client.SendAsync(request);
            var result = new HttpResult<T>();
            result.Code = response.StatusCode;
            if (response.IsSuccessStatusCode)
            {
                result.IsSuccess = true;
                if (typeof(T) == typeof(string) || !typeof(T).IsClass)
                {
                    result.Data = ParseBaseType<T>(await response.Content.ReadAsStringAsync());
                }
                else
                {
                    result.Data = await System.Text.Json.JsonSerializer.DeserializeAsync<T>(response.Content.ReadAsStream());
                }
            }
            else
            {
                result.IsSuccess = false;
                result.Message = await response.Content.ReadAsStringAsync();
            }
#if DEBUG
            _logger?.Info($"HttpProvider.Post: {uri} 用时: {(DateTime.Now - startTime).TotalMilliseconds}ms");
            _logger?.Info($"HttpProvider.Post: resp = {System.Text.Json.JsonSerializer.Serialize(result)}");
#endif
            return result;
        }


        private static string ParseGetUrl(string baseUrl, Dictionary<string, string>? param)
        {
            if (param == null)
                return baseUrl;
            var url = baseUrl;
            var paramString = "";
            foreach (var (key, value) in param)
            {
                if (url.Contains($":{key}"))
                    url = url.Replace($":{key}", value);
                else
                    paramString += $"{key}={value}&";
            }
            if (paramString.EndsWith('&'))
                paramString = paramString.Substring(0, paramString.Length - 1);
            if (paramString.Any())
            {
                url += $"?{paramString}";
            }
            return url;
        }

        private static string ParsePostUrl(string baseUrl, Dictionary<string, string>? param)
        {
            if (param == null)
                return baseUrl;
            var url = baseUrl;
            List<string> keys = new List<string>();
            foreach (var (key, value) in param)
            {
                if (url.Contains($":{key}"))
                {
                    url = url.Replace($":{key}", value);
                    keys.Add(key);
                }
            }
            keys.ForEach(key => param.Remove(key));
            return url;
        }

        private static T ParseBaseType<T> (string src)
        {
           return (T)Convert.ChangeType(src, typeof(T)) ;
        }
    }
}

