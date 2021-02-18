using System;
using System.Xml.Serialization;

namespace eyazisma.online.api.Api.V2X
{
    [Serializable()]
    [XmlType(Namespace = "urn:dpt:eyazisma:schema:xsd:Tipler-2")]
    public sealed class CT_Ozet
    {
        [XmlElement(Order = 0)]
        public CT_OzetAlgoritmasi OzetAlgoritmasi { get; set; }

        [XmlElement(DataType = "base64Binary", Order = 1)]
        public byte[] OzetDegeri { get; set; }
    }
}
