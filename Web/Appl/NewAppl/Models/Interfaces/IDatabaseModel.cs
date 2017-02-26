using NewAppl.Models.BusinessLayer.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewAppl.Models.Interfaces
{
    internal interface IDatabaseModel
    {
        Result GetData(string url);
        Result GetFiles(string data = "");
        Result PostComment(FormCollection model);
    }
}