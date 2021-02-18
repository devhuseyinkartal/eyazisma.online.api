using System;
using System.Xml.Serialization;

namespace eyazisma.online.api.Api.V1X
{
    [Serializable()]
    [XmlType(Namespace = "urn:dpt:eyazisma:schema:xsd:BelgeImza-1")]
    [XmlRoot("BelgeImza", Namespace = "urn:dpt:eyazisma:schema:xsd:BelgeImza-1", IsNullable = false)]
    public sealed class CT_BelgeImza
    {
        [XmlArray(Namespace = "urn:dpt:eyazisma:schema:xsd:Tipler-1", Order = 0)]
        [XmlArrayItem("Imza", IsNullable = false)]
        public CT_Imza[] ImzaListesi { get; set; }
    }
}
