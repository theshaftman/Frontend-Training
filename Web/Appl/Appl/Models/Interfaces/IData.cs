using Appl.Models.BusinessLayer.Data;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appl.Models.Interfaces
{
    internal interface IData
    {
        Result GetData(string givenData = "", string query = "");

        IRestResponse Delete(string givenData, string givenID);
    }
}