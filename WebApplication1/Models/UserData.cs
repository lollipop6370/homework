﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Models
{
    public class UserData
    {
            public string account { get; set; }
            public string password1 { get; set; }
            public string password2 { get; set; }
            public string city { get; set; }
            public string village { get; set; }
            public string address { get; set; }
    }
}