﻿using NewAppl.Models.BusinessLayer.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NewAppl.Models.Interfaces
{
    internal interface IEmailModel
    {
        Result SendMail(FormCollection model);
    }
}
