using System;
using System.Xml.Serialization;

namespace eyazisma.online.api.Api.V2X
{
    [Serializable]
    [XmlType(Namespace = "urn:dpt:eyazisma:schema:xsd:Tipler-2")]
    public sealed class CT_KurumKurulus
    {
        [XmlElement(DataType = "normalizedString", Order = 0)]
        public string KKK { get; set; }

        [XmlElement(Order = 1)] public NameType Adi { get; set; }

        [XmlElement(Order = 2)] public CT_IletisimBilgisi IletisimBilgisi { get; set; }

        [XmlElement(DataType = "normalizedString", Order = 3)]
        public string BirimKKK { get; set; }
    }
}