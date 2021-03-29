using System;
using System.Xml;
using System.Xml.Serialization;

namespace eyazisma.online.api.Api.V2X
{
    [Serializable]
    [XmlType(Namespace = "urn:dpt:eyazisma:schema:xsd:Tipler-2")]
    public sealed class CT_OzetAlgoritmasi
    {
        [XmlText] [XmlAnyElement(Order = 0)] public XmlNode[] Any { get; set; }

        [XmlAttribute(DataType = "anyURI")] public string Algorithm { get; set; }
    }
}