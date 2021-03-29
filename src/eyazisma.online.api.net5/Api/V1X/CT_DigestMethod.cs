using System;
using System.Xml;
using System.Xml.Serialization;

namespace eyazisma.online.api.Api.V1X
{
    [Serializable]
    [XmlType(Namespace = "urn:dpt:eyazisma:schema:xsd:PaketOzeti-1")]
    [XmlRoot("DigestMethod", Namespace = "urn:dpt:eyazisma:schema:xsd:PaketOzeti-1", IsNullable = false)]
    public sealed class CT_DigestMethod
    {
        [XmlText] [XmlAnyElement(Order = 0)] public XmlNode[] Any { get; set; }

        [XmlAttribute(DataType = "anyURI")] public string Algorithm { get; set; }
    }
}