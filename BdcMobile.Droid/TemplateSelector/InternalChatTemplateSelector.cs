using BdcMobile.Core.Models;
using MvvmCross.Droid.Support.V7.RecyclerView.ItemTemplates;

namespace BdcMobile.Droid.TemplateSelector
{
    public class InternalChatTemplateSelector : IMvxTemplateSelector
    {
        public int ItemTemplateId { get; set; } // fallback ItemTemplateId 

        public int GetItemViewType(object item)
        {
            var msg = item as ChatMessage;
            if (msg == null)
            {
                return -1;
            }

            if (msg.Files == null || msg.Files.Count < 2)
            {
                return msg.IsFromMe ? 1 : 2;
            }
            else
            {
                return msg.IsFromMe ? 3 : 4;
            }
        }

        public int GetItemLayoutId(int viewType)
        {
            switch(viewType)
            {
                case 1: return Resource.Layout.ItemMessageSent;
                case 2: return Resource.Layout.ItemMessageReceived;
                case 3: return Resource.Layout.PictureMessageSent;
                case 4: return Resource.Layout.PictureMessageReceived;

                default: return -1;
            }
        }
    }
}