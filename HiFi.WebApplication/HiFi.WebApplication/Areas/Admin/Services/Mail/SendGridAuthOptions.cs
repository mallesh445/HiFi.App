﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiFi.WebApplication.Areas.Admin.Services.Mail
{
    public class SendGridAuthOptions
    {
        public string KeyName { get; set; }
        public string ApiKey { get; set; }
    }
}