using System;
using System.Xml.Serialization;

namespace eyazisma.online.api.framework.Api.V1X
{
    [Serializable()]
    [XmlType(Namespace = "urn:dpt:eyazisma:schema:xsd:Tipler-1")]
    [XmlRoot("OzetAlgoritmasi", Namespace = "urn:dpt:eyazisma:schema:xsd:Tipler-1", IsNullable = false)]
    public sealed class CT_OzetAlgoritmasi
    {
        [XmlText()]
        [XmlAnyElement(Order = 0)]
        public System.Xml.XmlNode[] Any { get; set; }

        [XmlAttribute(DataType = "anyURI")]
        public string Algorithm { get; set; }
    }
}
