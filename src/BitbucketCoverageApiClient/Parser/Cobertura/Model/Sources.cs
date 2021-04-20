﻿using System.Xml.Serialization;

// Generated from cobertura XML schema
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace BitbucketCoverageApiClient.Parser.Cobertura.Model
{
    [XmlRoot(ElementName = "sources")]
    public class Sources
    {
        [XmlElement(ElementName = "source")] 
        public string Source { get; set; }
    }
}