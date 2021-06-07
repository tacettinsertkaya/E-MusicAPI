﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMusicAPI.Models.Configurations
{
    public class EmailModel
    {
        public EmailModel(string to, string subject, string message, bool isBodyHtml)
        {
            To = to;
            Subject = subject;
            Message = message;
            IsBodyHtml = isBodyHtml;
        }
        public string To
        {
            get;
        }
        public string Subject
        {
            get;
        }
        public string Message
        {
            get;
        }
        public bool IsBodyHtml
        {
            get;
        }
    }
}
