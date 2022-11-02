using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Backend.Share.Infra
{
    public class HttpResult<T>
    {
        public T? Data { get; set; }
        public bool IsSuccess { get; set; }
        public HttpStatusCode Code { get; set; }
        public string? Message { get; set; }
    }
    
    public interface IHttpProvider
    {
        public Task<HttpResult<T>> Get<T>(string baseUrl, Dictionary<string, string>? param = null, Dictionary<string, string>? headers = null);
        public Task<HttpResult<T>> Post<T>(string baseUrl, Dictionary<string, string>? param = null, Dictionary<string, string>? headers = null);
        public Task<HttpResult<T>> Post<T>(string baseUrl, string content);
    }
}
