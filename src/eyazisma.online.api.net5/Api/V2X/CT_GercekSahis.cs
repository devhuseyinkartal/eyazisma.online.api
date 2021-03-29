using System;
using System.Xml.Serialization;

namespace eyazisma.online.api.Api.V2X
{
    [Serializable]
    [XmlType(Namespace = "urn:dpt:eyazisma:schema:xsd:Tipler-2")]
    public sealed class CT_GercekSahis
    {
        [XmlElement(Order = 0)] public CT_Kisi Kisi { get; set; }

        [XmlElement(DataType = "normalizedString", Order = 1)]
        public string TCKN { get; set; }

        [XmlElement(Order = 2)] public TextType Gorev { get; set; }

        [XmlElement(Order = 3)] public CT_IletisimBilgisi IletisimBilgisi { get; set; }
    }
}