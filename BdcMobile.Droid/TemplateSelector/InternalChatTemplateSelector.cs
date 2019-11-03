using BdcMobile.Core.Models;
using MvvmCross.Droid.Support.V7.RecyclerView.ItemTemplates;

namespace BdcMobile.Droid.TemplateSelector
{
    public class InternalChatTemplateSelector : IMvxTemplateSelector
    {
        public int ItemTemplateId { get; set; } // fallback ItemTemplateId 

        public int GetItemViewType(object item)
        {
            if (item is ChatMessage) 
            {
                return (item as ChatMessage).IsFromMe ? 1 : 2;
            }

            return -1;
        }

        public int GetItemLayoutId(int viewType)
        {
            if (viewType == 1)
                return Resource.Layout.ItemMessageSent;
            if (viewType == 2)
                return Resource.Layout.ItemMessageReceived;

            return ItemTemplateId;
        }
    }
}