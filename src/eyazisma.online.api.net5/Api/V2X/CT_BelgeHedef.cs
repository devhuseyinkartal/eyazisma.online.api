using System;
using System.Xml.Serialization;

namespace eyazisma.online.api.Api.V2X
{
    [Serializable]
    [XmlType(Namespace = "urn:dpt:eyazisma:schema:xsd:BelgeHedef-2")]
    [XmlRoot("BelgeHedef", Namespace = "urn:dpt:eyazisma:schema:xsd:BelgeHedef-2", IsNullable = false)]
    public sealed class CT_BelgeHedef
    {
        [XmlArray(Namespace = "urn:dpt:eyazisma:schema:xsd:Tipler-2", Order = 0)]
        [XmlArrayItem("Hedef", IsNullable = false)]
        public CT_Hedef[] HedefListesi { get; set; }
    }
}