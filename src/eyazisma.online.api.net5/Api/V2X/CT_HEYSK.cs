using System;
using System.Xml.Serialization;

namespace eyazisma.online.api.Api.V2X
{
    [Serializable]
    [XmlType(Namespace = "urn:dpt:eyazisma:schema:xsd:Tipler-2")]
    public sealed class CT_HEYSK
    {
        [XmlElement(Order = 0)] public int Kod { get; set; }

        [XmlElement(Order = 1)] public string Ad { get; set; }

        [XmlElement(Order = 2)] public string Tanim { get; set; }
    }
}