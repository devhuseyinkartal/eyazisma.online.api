﻿using System;
using System.Xml.Serialization;

namespace eyazisma.online.api.framework.Api.V1X
{
    [Serializable()]
    [XmlType(Namespace = "urn:dpt:eyazisma:schema:xsd:Tipler-1")]
    public sealed class CT_Id
    {
        [XmlAttribute(DataType = "normalizedString")]
        public string Value { get; set; }

        [XmlAttribute()]
        public bool EYazismaIdMi { get; set; }

        [XmlIgnore()]
        public bool EYazismaIdMiSpecified { get; set; }
    }
}
