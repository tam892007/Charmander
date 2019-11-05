using System;
using System.Collections.Generic;
using System.Text;

namespace BdcMobile.Core.Models
{
    public class ChatMessage
    {
        public string TextContent { get; set; }
        public byte [] PictureContent { get; set; }
        public bool IsFromMe { get; set; }
        public ChatType Type { get; set; }
    }

    public enum ChatType
    {
        Text,
        Picture,
    }
}
