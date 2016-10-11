using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appl.Models.BusinessLayer.Data
{
    internal class Data
    {
        internal Data()
        {
        }

        internal Result GetData(string givenData = "", string query = "")
        {
            givenData = !string.IsNullOrEmpty(givenData) ? givenData : "subjects";
            query = !string.IsNullOrEmpty(query) ? query : "";

            var client = new RestClient("https://baas.kinvey.com/appdata/kid_rJ-gHb40/" + givenData + query);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", "Basic a2lkX3JKLWdIYjQwOmMxNDBmN2UwMDEyZDQ3YjE5YTUzMjc4ZTExYWM1NjRk");
            IRestResponse response = client.Execute(request);

            Result data = new Result
            {
                Status = "success",
                Data = response.Content
            };

            return data;
        }

        internal IRestResponse Delete(string givenData, string givenID)
        {
            var client = new RestClient("https://baas.kinvey.com/appdata/kid_rJ-gHb40/" + givenData + "/" + givenID);
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", "Basic a2lkX3JKLWdIYjQwOmMxNDBmN2UwMDEyZDQ3YjE5YTUzMjc4ZTExYWM1NjRk");
            request.AddHeader("content-type", "multipart/form-data; boundary=---011000010111000001101001");
            // request.AddParameter("multipart/form-data; boundary=---011000010111000001101001", "-----011000010111000001101001\r\nContent-Disposition: form-data; name=\"author\"\r\n\r\nNan\r\n-----011000010111000001101001\r\nContent-Disposition: form-data; name=\"subject_title\"\r\n\r\nNan\r\n-----011000010111000001101001--", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            return response;
        }
    }
}