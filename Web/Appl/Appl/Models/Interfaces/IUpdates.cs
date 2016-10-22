using Appl.Models.BusinessLayer.Data;
using Appl.Models.BusinessLayer.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Appl.Models.Interfaces
{
    internal interface IUpdates
    {
        IList<Update> GetCurrentUpdates(string username);

        bool UpdateCurrentData(string username);

        bool UpdateInsertData(string username, string modification, string link);
    }
}
