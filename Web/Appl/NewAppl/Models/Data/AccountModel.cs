using NewAppl.Models.BusinessLayer.Structure;
using NewAppl.Models.Interfaces;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewAppl.Models.Data
{
    internal class AccountModel : IAccountModel
    {
        internal AccountModel()
        {

        }

        Result IAccountModel.LogIn(FormCollection model)
        {
            string username = model["username"];
            string password = model["password"];

            var client = new RestClient("https://baas.kinvey.com/user/kid_S1lQExb_e/login");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", "Basic a2lkX1MxbFFFeGJfZTpiMDZmNWYyNzk2ZDc0ZDY1YjdkODA1NGJiODcxYjk4NQ==");
            request.AddHeader("content-type", "multipart/form-data; boundary=----WebKitFormBoundary7MA4YWxkTrZu0gW");
            request.AddParameter("multipart/form-data; boundary=----WebKitFormBoundary7MA4YWxkTrZu0gW", "------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data; name=\"username\"\r\n\r\n" + username +
                "\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data; name=\"password\"\r\n\r\n" + password + "\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW--", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            Result result = new Result()
            {
                Status = response.ResponseStatus.ToString(),
                Data = response.Content
            };
            return result;
        }

        Result IAccountModel.Register(FormCollection model)
        {
            string username = model["username"];
            string password = model["password"];

            var client = new RestClient("https://baas.kinvey.com/user/kid_S1lQExb_e");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("x-kinvey-skip-business-logic", "true");
            request.AddHeader("x-kinvey-master-create-user", "true");
            request.AddHeader("x-kinvey-api-version", "3");
            request.AddHeader("authorization", "Basic a2lkX1MxbFFFeGJfZTpiMDZmNWYyNzk2ZDc0ZDY1YjdkODA1NGJiODcxYjk4NQ==");
            request.AddHeader("content-type", "multipart/form-data; boundary=----WebKitFormBoundary7MA4YWxkTrZu0gW");
            request.AddParameter("multipart/form-data; boundary=----WebKitFormBoundary7MA4YWxkTrZu0gW", "------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data; name=\"email\"\r\n\r\n" +
                username + "\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data; name=\"username\"\r\n\r\n" + 
                username + "\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data; name=\"password\"\r\n\r\n" + 
                password + "\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW--", ParameterType.RequestBody);
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