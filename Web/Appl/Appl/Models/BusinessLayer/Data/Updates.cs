using Appl.Models.BusinessLayer.Account;
using Appl.Models.Interfaces;
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

        bool IUpdates.UpdateCurrentData(FormCollection collection, string username)
        {
            bool result = false;

            username = InputEncoding.DecodePassword(username);

            return result;
        }
    }
}