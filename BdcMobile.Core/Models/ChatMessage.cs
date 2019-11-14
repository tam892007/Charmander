using BdcMobile.Core.Commons;
using FFImageLoading.Transformations;
using FFImageLoading.Work;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BdcMobile.Core.Models
{
    public class ChatMessage
    {
        public IList<ChatPicture> Files { get; set; }
        public int ChatID { get; set; }
        public int SurID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Content { get; set; }
        public int BelongingTo { get; set; }
        public string FileIndex { get; set; }
        public int Type { get; set; }
        public string Func { get; set; }
        public string target { get; set; }

        [JsonConverter(typeof(InvalidDataFormatJsonConverter))]
        public DateTime? CreateTime { get; set; }

        public string Time 
        {
            get
            {
                if (CreateTime != null)
                {
                    return string.Format("{0:HH mm}", CreateTime);
                }
                return string.Empty;
            }
        }

        public List<ITransformation> CircleTransformation => new List<ITransformation> { new CircleTransformation() };
        public bool IsFromMe { get; set; }
        public bool IsSent
        {
            get
            {
                return ChatID != 0;
            }
        }
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

        public bool IsSinglePicture => Files?.Count == 1;

        public bool IsMultiPicture => Files?.Count > 2;

        public string MultiPictureTextDisplay => $"+{Files?.Count - 2}";
    }

    public class ListChatMessageResponseModel
    {
        public List<ChatMessage> data { get; set; }
    }

    public class ChatSentResponse
    {

        public int lastID { get; set; }
        public string fileIndex { get; set; }
    }
}
