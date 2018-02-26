﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DesktopApp.Models
{
    public class Result
    {

        public Result(eResultTypes resultTypes, string message = null)
        {
            this.ResultTypes = resultTypes;
            this.Message = message ?? string.Empty;
        }

        public eResultTypes ResultTypes { get; }
        public string Message { get; }

    }
}