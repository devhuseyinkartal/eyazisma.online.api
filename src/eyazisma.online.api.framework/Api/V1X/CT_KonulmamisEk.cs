﻿using System;
using System.Xml.Serialization;

namespace eyazisma.online.api.framework.Api.V1X
{
    [Serializable()]
    [XmlType(Namespace = "urn:dpt:eyazisma:schema:xsd:Tipler-1")]
    public sealed class CT_KonulmamisEk
    {
        [XmlElement(DataType = "normalizedString", Order = 0)]
        public string EkId { get; set; }
    }
}
