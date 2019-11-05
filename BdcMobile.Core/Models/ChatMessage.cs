using BdcMobile.Core.Commons;
using System;
using System.Collections.Generic;
using System.Text;

namespace BdcMobile.Core.Models
{
    public class ChatMessage
    {
        public string TextContent { get; set; }
        public byte [] PictureContent { get; set; }
        public int ChatID { get; set; }
        public int SurID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Content { get; set; }
        public int BelongingTo { get; set; }
        public int FileIndex { get; set; }
        public int Type { get; set; }
        public ChatType CType { get; set; }
        public string Func { get; set; }
        public string target { get; set; }
        public string CreateTime { get; set; }
        public bool IsFromMe { get; set; }
        public string UserImg { get; set; }
        public string UserImageURL
        {
            get
            {
                if (string.IsNullOrWhiteSpace(UserImg))
                {
                    // TODO: Remove later
                    return "http://103.47.192.239:81/public/Uploads/HR/EMPLOYEE_IMAGE/6_MR_2019-04-03_07-54-10.jpg";
                }
                else
                {
                    return Constants.AppAPI.IPAPI + UserImg;
                }

            }
        }
    }

    public class ListChatMessageResponseModel
    {
        public List<ChatMessage> data { get; set; }
        public ChatType Type { get; set; }
    }

    public enum ChatType
    {
        Text,
        Picture,
    }
}
