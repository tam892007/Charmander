using System;
using System.Collections.Generic;
using System.Text;

namespace BdcMobile.Core.Models
{
    public class ChatMessage
    {
        public string Content { get; set; }
        public bool IsFromMe { get; set; }
    }
}
