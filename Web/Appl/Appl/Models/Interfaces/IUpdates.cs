using Appl.Models.BusinessLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appl.Models.Interfaces
{
    internal interface IUpdates
    {
        IList<Update> GetCurrentUpdates(string username);
    }
}
