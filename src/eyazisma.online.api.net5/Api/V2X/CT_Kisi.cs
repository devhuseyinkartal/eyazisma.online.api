using System;
using System.Xml.Serialization;

namespace eyazisma.online.api.Api.V2X
{
    [Serializable]
    [XmlType(Namespace = "urn:dpt:eyazisma:schema:xsd:Tipler-2")]
    public sealed class CT_Kisi
    {
        [XmlElement(Order = 0)] public NameType IlkAdi { get; set; }

        [XmlElement(Order = 1)] public NameType Soyadi { get; set; }

        [XmlElement(Order = 2)] public NameType IkinciAdi { get; set; }

        [XmlElement(Order = 3)] public NameType Unvan { get; set; }

        [XmlElement(Order = 4)] public TextType OnEk { get; set; }
    }
}