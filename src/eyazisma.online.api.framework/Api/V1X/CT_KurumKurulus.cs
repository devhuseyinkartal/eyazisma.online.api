using System;
using System.Xml.Serialization;

namespace eyazisma.online.api.framework.Api.V1X
{
    [Serializable()]
    [XmlType(Namespace = "urn:dpt:eyazisma:schema:xsd:Tipler-1")]
    public sealed class CT_KurumKurulus
    {
        [XmlElement(DataType = "normalizedString", Order = 0)]
        public string KKK { get; set; }

        [XmlElement(DataType = "normalizedString", Order = 1)]
        public string BYK { get; set; }

        [XmlElement(Order = 2)]
        public NameType Adi { get; set; }

        [XmlElement(Order = 3)]
        public CT_IletisimBilgisi IletisimBilgisi { get; set; }
    }
}
