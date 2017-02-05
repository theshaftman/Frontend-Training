using NewAppl.Models.BusinessLayer.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewAppl.Models.Interfaces
{
    internal interface IDatabaseModel
    {
        Result GetData(string url);
    }
}