﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KFZ_Konfigurator
{
    public static class Constants
    {
        public const double KW_to_PS = 1.35962f;

        public static class Routes
        {
            public const string ModelOverview = nameof(ModelOverview);
            public const string EngineSettings = nameof(EngineSettings);
            public const string Exterior = nameof(Exterior);
            public const string Accessories = nameof(Accessories);
            public const string ConfigurationOverview = nameof(ConfigurationOverview);
        }
        public static class SubSeries
        {
            public const string AllroadQuattro = "allroad quattro";
        }
    }
}