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

            switch (msg.CType)
            {
                case ChatType.Text: return msg.IsFromMe ? 1 : 2;
                case ChatType.Picture: return msg.IsFromMe ? 3 : 4;

                default: return -1;
            }
        }

        public int GetItemLayoutId(int viewType)
        {
            switch(viewType)
            {
                case 1: return Resource.Layout.ItemMessageSent;
                case 2: return Resource.Layout.ItemMessageReceived;
                case 3: return Resource.Layout.PictureMessageSent;

                default: return -1;
            }
        }
    }
}