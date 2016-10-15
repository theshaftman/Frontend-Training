using Appl.Models.BusinessLayer.Data;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Appl.Models.Interfaces
{
    internal interface IData
    {
        Result GetData(string givenData = "", string query = "");

        IRestResponse Delete(string givenData, string givenID);

        IRestResponse ModifySubject(string updateID = null, FormCollection data = null);

        IRestResponse ModifyComment(string updateID = null, FormCollection data = null);

        IRestResponse SendQuestion(FormCollection data);
    }
}