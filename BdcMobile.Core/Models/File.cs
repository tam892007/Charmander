﻿using BdcMobile.Core.Commons;
using System;
using System.Collections.Generic;
using System.Text;

namespace BdcMobile.Core.Models
{
    public class File
    {
        public int Is_CustomerSee { get; set; }
        public bool CustomerSee
        {
            get
            {
                return Is_CustomerSee != 0;
            }
        }

        public string Path { get; set; }
        public int FileIndex { get; set; }
        public int CharIndex { get; set; }
        public string RealName { get; set; }

        public string DisguisedName { get; set; }

        public DateTime CreateTime { get; set; }

        public string PathToDisplay
        {
            get => App.Context.ServerAddress + Path.Replace('\\', '/').TrimEnd('/') + '/' + DisguisedName;
        }

        public double Downsample => 50d;
    }

    public class ListFileResponseModel
    {
        public List<File> data { get; set; }
    }
}
