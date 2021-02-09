using System;
using System.Xml.Serialization;

namespace eyazisma.online.api.framework.Api.V1X
{
    [Serializable()]
    [XmlType(Namespace = "urn:dpt:eyazisma:schema:xsd:SifreliIcerikBilgisi-1")]
    [XmlRoot("SifreliIcerikBilgisi", Namespace = "urn:dpt:eyazisma:schema:xsd:SifreliIcerikBilgisi-1", IsNullable = false)]
    public sealed class CT_SifreliIcerikBilgisi
    {
        [XmlElement("URI", DataType = "anyURI", Order = 0)]
        public string[] URI { get; set; }

        [XmlAttribute(DataType = "ID")]
        public string Id { get; set; }

        [XmlAttribute()]
        public string Yontem { get; set; }

        [XmlAttribute()]
        public string Version { get; set; }
    }
}
