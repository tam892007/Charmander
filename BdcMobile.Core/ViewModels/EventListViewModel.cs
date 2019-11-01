using BdcMobile.Core.Models;
using MvvmCross.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BdcMobile.Core.ViewModels
{
    public class EventListViewModel : MvxViewModel
    {
        public List<Event> Events { get; set; }
        public override Task Initialize()
        {
            Events = new List<Event>
            {
                new Event(),
                new Event(),
                new Event(),
            };

            return base.Initialize();
        }
    }
}
