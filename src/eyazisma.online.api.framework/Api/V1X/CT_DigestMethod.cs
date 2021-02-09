using System;
using System.Xml.Serialization;

namespace eyazisma.online.api.framework.Api.V1X
{
    [Serializable()]
    [XmlType(Namespace = "urn:dpt:eyazisma:schema:xsd:PaketOzeti-1")]
    [XmlRoot("DigestMethod", Namespace = "urn:dpt:eyazisma:schema:xsd:PaketOzeti-1", IsNullable = false)]
    public sealed class CT_DigestMethod
    {
        [XmlText()]
        [XmlAnyElement(Order = 0)]
        public System.Xml.XmlNode[] Any { get; set; }

        [XmlAttribute(DataType = "anyURI")]
        public string Algorithm { get; set; }
    }
}
