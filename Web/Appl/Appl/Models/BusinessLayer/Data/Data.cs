﻿using Appl.Models.Interfaces;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Appl.Models.BusinessLayer.Data
{
    internal class Data : IData
    {
        internal Data()
        {
        }

        Result IData.GetData(string givenData, string query)
        {
            givenData = !string.IsNullOrEmpty(givenData) ? givenData : "subjects";
            query = !string.IsNullOrEmpty(query) ? query : "";

            RestClient client = new RestClient("https://baas.kinvey.com/appdata/kid_rJ-gHb40/" + givenData + query);
            RestRequest request = new RestRequest(Method.GET);
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

        IRestResponse IData.Delete(string givenData, string givenID)
        {
            RestClient client = new RestClient("https://baas.kinvey.com/appdata/kid_rJ-gHb40/" + givenData + "/" + givenID);
            RestRequest request = new RestRequest(Method.DELETE);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", "Basic a2lkX3JKLWdIYjQwOmMxNDBmN2UwMDEyZDQ3YjE5YTUzMjc4ZTExYWM1NjRk");
            request.AddHeader("content-type", "multipart/form-data; boundary=---011000010111000001101001");
            // request.AddParameter("multipart/form-data; boundary=---011000010111000001101001", "-----011000010111000001101001\r\nContent-Disposition: form-data; name=\"author\"\r\n\r\nNan\r\n-----011000010111000001101001\r\nContent-Disposition: form-data; name=\"subject_title\"\r\n\r\nNan\r\n-----011000010111000001101001--", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            return response;
        }

        IRestResponse IData.SendQuestion(FormCollection data)
        {
            string id = data["id"];
            string author = data["author"];
            string question = data["question"];

            if (string.IsNullOrEmpty(id) ||
                string.IsNullOrEmpty(author) ||
                string.IsNullOrEmpty(question))
            {
                return null;
            }

            RestClient client = new RestClient("https://baas.kinvey.com/appdata/kid_rJ-gHb40/questions");
            RestRequest request = new RestRequest(Method.POST);
            request.AddHeader("postman-token", "0b5fdfae-d6f3-b28e-be6c-d22de0916c21");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", "Basic a2lkX3JKLWdIYjQwOmMxNDBmN2UwMDEyZDQ3YjE5YTUzMjc4ZTExYWM1NjRk");
            request.AddHeader("content-type", "multipart/form-data; boundary=---011000010111000001101001");

            string[] inputNames = new string[] { "id", "author", "question" };
            string[] inputParameteres = new string[] { id, author, question };

            request.AddParameter("multipart/form-data; boundary=---011000010111000001101001", GetPostMethod(inputNames, inputParameteres),
                ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            return response;
        }

        private string GetPostMethod(string[] inputNames, string[] inputParameters)
        {
            if (inputNames.Length != inputParameters.Length)
            {
                return null;
            }

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < inputNames.Length; i++)
            {
                sb.Append(string.Format(
                    "-----011000010111000001101001\r\nContent-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}\r\n",
                    inputNames[i],
                    inputParameters[i]));
            }

            sb.Append("-----011000010111000001101001--");

            return sb.ToString();
        }
    }
}