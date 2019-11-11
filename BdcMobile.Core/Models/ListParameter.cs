using System;
using System.Collections.Generic;
using System.Text;

namespace BdcMobile.Core.Models
{
    public class ListParameter<T>
    {
        public int SelectedIndex { get; set; }
        public ICollection<T> Data { get; set; }

        public ListParameter(ICollection<T> data, int index)
        {
            Data = data;
            SelectedIndex = index;
        }
    }
}
