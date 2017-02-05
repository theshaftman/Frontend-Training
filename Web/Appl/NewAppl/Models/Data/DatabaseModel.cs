using NewAppl.Models.BusinessLayer.Structure;
using NewAppl.Models.Interfaces;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewAppl.Models.Data
{
    internal class DatabaseModel : IDatabaseModel
    {
        internal DatabaseModel()
        {

        }

        Result IDatabaseModel.GetData(string url)
        {
            var client = new RestClient("https://baas.kinvey.com/appdata/kid_S1lQExb_e/" + url);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", "Basic a2lkX1MxbFFFeGJfZTpiMDZmNWYyNzk2ZDc0ZDY1YjdkODA1NGJiODcxYjk4NQ==");
            request.AddHeader("content-type", "multipart/form-data; boundary=----WebKitFormBoundary7MA4YWxkTrZu0gW");
            request.AddParameter("multipart/form-data; boundary=----WebKitFormBoundary7MA4YWxkTrZu0gW", "------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data; name=\"id\"\r\n\r\n1\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data; name=\"author\"\r\n\r\nTest\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data; name=\"subject_title\"\r\n\r\nTitle\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data; name=\"subject_body\"\r\n\r\nBody\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW--", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            Result result = new Result()
            {
                Status = response.ResponseStatus.ToString(),
                Data = response.Content
            };
            return result;
        }
    }
}