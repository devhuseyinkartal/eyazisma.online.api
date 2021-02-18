using System;
using System.Xml.Serialization;

namespace eyazisma.online.api.Api.V2X
{
    [Serializable()]
    [XmlType(Namespace = "urn:dpt:eyazisma:schema:xsd:Tipler-2")]
    [XmlRoot("DogrulamaBilgisi", Namespace = "urn:dpt:eyazisma:schema:xsd:Tipler-2", IsNullable = false)]
    public sealed class CT_DogrulamaBilgisi
    {
        [XmlElement(Order = 0)]
        public string DogrulamaAdresi { get; set; }
    }
}
