﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KFZ_Konfigurator.ViewModels
{
    public class CarModelPageViewModel : ViewModelBase
    {
        public IEnumerable<CarModelViewModel> CarModels { get; set; }
        public IEnumerable<string> CarSeriesList
        {
            get => CarModels?.Select(cur => cur.Series).Distinct().ToList() ?? Enumerable.Empty<string>();
        }
    }
}