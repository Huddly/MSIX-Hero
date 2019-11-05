﻿using System;

namespace otor.msixhero.lib.BusinessLayer.Models.Manifest
{
    [Serializable]
    public class TargetOperatingSystem
    {
        public string Name { get; set; }

        public string TechnicalVersion { get; set; }
        
        public string MarketingCodename { get; set; }
    }
}
