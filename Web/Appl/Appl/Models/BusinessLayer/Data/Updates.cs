using Appl.Models.BusinessLayer.Account;
using Appl.Models.BusinessLayer.Structure;
using Appl.Models.Interfaces;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Appl.Models.BusinessLayer.Data
{
    internal class Updates : IUpdates
    {
        private IData _data;

        internal Updates()
        {
            this._data = new Data();
        }

        IList<Update> IUpdates.GetCurrentUpdates(string username)
        {
            username = InputEncoding.DecodePassword(username);
            Result currentData = this._data.GetData("userLastModificationID", "?query={\"userID\": \"" + username + "\"}");

            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            dynamic routesList = jsonSerializer.DeserializeObject(currentData.Data);
            string modificationID = routesList[0]["modificationID"].ToString();

            Result updatesData = this._data.GetData("userModification", "?query={\"id\":{\"$gte\": \"" + modificationID + "\"}}");

            JavaScriptSerializer jsonResultSerializer = new JavaScriptSerializer();
            dynamic routesResultList = jsonResultSerializer.DeserializeObject(updatesData.Data);
            IList<Update> list = new List<Update>();

            for (int i = 0; i < routesResultList.Length; i++)
            {
                Update item = new Update()
                {
                    Username = routesResultList[i]["username"],
                    Modification = routesResultList[i]["modification"],
                    Link = routesResultList[i]["link"]
                };

                list.Add(item);
            }
            
            return list;
        }

        bool IUpdates.UpdateCurrentData(string username)
        {
            bool result = true;
            username = InputEncoding.DecodePassword(username);

            try
            {
                // Get user last modification id.
                Result currentData = this._data.GetData("userLastModificationID", "?query={\"userID\": \"" + username + "\"}");

                JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
                dynamic routesList = jsonSerializer.DeserializeObject(currentData.Data);
                string modificationID = routesList[0]["_id"].ToString();

                Result updatesData = this._data.GetData("userModification", "?query={}&sort={\"id\": -1}&limit=1");

                JavaScriptSerializer jsonResultSerializer = new JavaScriptSerializer();
                dynamic routesResultList = jsonResultSerializer.DeserializeObject(updatesData.Data);
                string updateNumber = routesResultList[0]["id"].ToString();

                int number = int.Parse(updateNumber);
                number += 1;

                IRestResponse data = this._data.ModifyUpdate(username, modificationID, number.ToString());
            }
            catch (Exception)
            {
                result = false;
            }
            
            return result;
        }

        bool IUpdates.UpdateInsertData(string username, string modification, string link)
        {
            bool result = true;

            try
            {
                // Get the latest id number.
                Result updatesData = this._data.GetData("userModification", "?query={}&sort={\"id\": -1}&limit=1");

                JavaScriptSerializer jsonResultSerializer = new JavaScriptSerializer();
                dynamic routesResultList = jsonResultSerializer.DeserializeObject(updatesData.Data);
                string updateNumber = routesResultList[0]["id"].ToString();

                int number = int.Parse(updateNumber);
                number += 1;
                
                // Get the date in the moment.
                DateTime dateNow = DateTime.Now;

                // Create the dynamic obect.
                dynamic currentObject = new 
                {
                    datatable = "userModification",
                    method = "post",
                    id = number.ToString(),
                    username = username,
                    modification = modification,
                    link = link,
                    modificationDate = dateNow
                };

                IRestResponse modificationRequest = this._data.InsertModification(currentObject);
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }
    }
}