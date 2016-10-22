using Appl.Models.BusinessLayer.Structure;
using Appl.Models.Interfaces;
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

        #region Training

        IRestResponse IData.ModifySubject(string updateID, FormCollection data)
        {
            string datatable = updateID != null ?  "subjects/" + updateID : "subjects";
            string id = data["id"];
            string author = data["author"];
            string subjectTitle = data["subject_title"];
            string subjectBody = data["subject_body"];
            bool isEscaping = data["isEscaping"] != null && 
                (data["isEscaping"].ToString().ToUpper() == "TRUE" || data["isEscaping"].ToString() == "1") ?
                true : false;

            if (string.IsNullOrEmpty(id) ||
                string.IsNullOrEmpty(author) ||
                string.IsNullOrEmpty(subjectTitle) ||
                string.IsNullOrEmpty(subjectBody))
            {
                return null;
            }

            if (isEscaping)
            {
                subjectBody = GetEscapingString(subjectBody);
            }

            RestClient client = new RestClient("https://baas.kinvey.com/appdata/kid_rJ-gHb40/" + datatable);
            Method action = updateID != null ? Method.PUT : Method.POST;

            RestRequest request = new RestRequest(action);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", "Basic a2lkX3JKLWdIYjQwOmMxNDBmN2UwMDEyZDQ3YjE5YTUzMjc4ZTExYWM1NjRk");
            request.AddHeader("content-type", "multipart/form-data; boundary=---011000010111000001101001");

            string[] inputNames = new string[] { "id", "author", "subject_title", "subject_body" };
            string[] inputParameteres = new string[] { id, author, subjectTitle, subjectBody };

            string parameter = GetPostMethod(inputNames, inputParameteres);

            if (parameter == null)
            {
                return null;
            }

            request.AddParameter("multipart/form-data; boundary=---011000010111000001101001", 
                parameter, ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            return response;
        }

        IRestResponse IData.ModifyComment(string updateID, FormCollection data)
        {
            string datatable = updateID != null ? "subjectComments/" + updateID : "subjectComments";
            string id = data["id"];
            string subjectID = data["subject_id"];
            string author = data["author"];
            string comment = data["comment"];

            if (string.IsNullOrEmpty(id) ||
                string.IsNullOrEmpty(subjectID) ||
                string.IsNullOrEmpty(author) ||
                string.IsNullOrEmpty(comment))
            {
                return null;
            }

            RestClient client = new RestClient("https://baas.kinvey.com/appdata/kid_rJ-gHb40/" + datatable);
            Method action = updateID != null ? Method.PUT : Method.POST;

            var request = new RestRequest(action);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", "Basic a2lkX3JKLWdIYjQwOmMxNDBmN2UwMDEyZDQ3YjE5YTUzMjc4ZTExYWM1NjRk");
            request.AddHeader("content-type", "multipart/form-data; boundary=---011000010111000001101001");

            string[] inputNames = new string[] { "id", "subject_id", "author", "comment" };
            string[] inputParameteres = new string[] { id, subjectID, author, comment };

            string parameter = GetPostMethod(inputNames, inputParameteres);

            if (parameter == null)
            {
                return null;
            }

            request.AddParameter("multipart/form-data; boundary=---011000010111000001101001", 
                parameter, ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            return response;
        }

        #endregion

        #region Messages

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

            string parameter = GetPostMethod(inputNames, inputParameteres);

            if (parameter == null)
            {
                return null;
            }

            request.AddParameter("multipart/form-data; boundary=---011000010111000001101001", 
                parameter, ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            return response;
        }

        #endregion

        #region Updates

        IRestResponse IData.ModifyUpdate(string username, string modificationID, string updateNumber)
        {
            if (string.IsNullOrEmpty(username) ||
                string.IsNullOrEmpty(modificationID) ||
                string.IsNullOrEmpty(updateNumber))
            {
                return null;
            }

            string datatable = "userLastModificationID/" + modificationID;

            RestClient client = new RestClient("https://baas.kinvey.com/appdata/kid_rJ-gHb40/" + datatable);
            Method action = Method.PUT;

            RestRequest request = new RestRequest(action);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", "Basic a2lkX3JKLWdIYjQwOmMxNDBmN2UwMDEyZDQ3YjE5YTUzMjc4ZTExYWM1NjRk");
            request.AddHeader("content-type", "multipart/form-data; boundary=---011000010111000001101001");

            string[] inputNames = new string[] { "userID", "modificationID" };
            string[] inputParameteres = new string[] { username, updateNumber };

            string parameter = GetPostMethod(inputNames, inputParameteres);

            if (parameter == null)
            {
                return null;
            }

            request.AddParameter("multipart/form-data; boundary=---011000010111000001101001",
                parameter, ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            return response;
        }

        IRestResponse IData.InsertModification(dynamic givenObject)
        {
            string datatable = givenObject.datatable;
            string method = givenObject.method;
            string id = givenObject.id;
            string username = givenObject.username;
            string modification = givenObject.modification;
            string link = givenObject.link;
            DateTime modificationDate = givenObject.modificationDate;

            if (string.IsNullOrEmpty(datatable) ||
                string.IsNullOrEmpty(method) ||
                string.IsNullOrEmpty(id) ||
                string.IsNullOrEmpty(username) ||
                string.IsNullOrEmpty(modification))
            {
                return null;
            }

            RestClient client = new RestClient("https://baas.kinvey.com/appdata/kid_rJ-gHb40/" + datatable);
            Method action = method == "post" ? Method.POST : Method.PUT;

            RestRequest request = new RestRequest(action);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", "Basic a2lkX3JKLWdIYjQwOmMxNDBmN2UwMDEyZDQ3YjE5YTUzMjc4ZTExYWM1NjRk");
            request.AddHeader("content-type", "multipart/form-data; boundary=---011000010111000001101001");

            string[] inputNames = new string[] { "id", "username", "modification", "link", "modificationDate" };
            dynamic[] inputParameteres = new dynamic[] { id, username, modification, link, modificationDate };

            string parameter = GetPostMethod(inputNames, inputParameteres);

            if (parameter == null)
            {
                return null;
            }

            request.AddParameter("multipart/form-data; boundary=---011000010111000001101001",
                parameter, ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            return response;
        }

        #endregion

        /// <summary>
        /// Create the post body.
        /// </summary>
        /// <param name="inputNames"></param>
        /// <param name="inputParameters"></param>
        /// <returns></returns>
        private string GetPostMethod(string[] inputNames, dynamic[] inputParameters)
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

        private string GetEscapingString(string input)
        {
            input = input.Replace("&amp;", "&");
            input = input.Replace("&lt;", "<");
            input = input.Replace("&gt;", ">");

            return input;
        }
    }
}