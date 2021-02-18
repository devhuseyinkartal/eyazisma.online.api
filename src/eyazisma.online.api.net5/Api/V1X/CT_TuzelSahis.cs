using System;
using System.Xml.Serialization;

namespace eyazisma.online.api.Api.V1X
{
    [Serializable()]
    [XmlType(Namespace = "urn:dpt:eyazisma:schema:xsd:Tipler-1")]
    public sealed class CT_TuzelSahis
    {
        [XmlElement(Order = 0)]
        public IdentifierType Id { get; set; }

        [XmlElement(Order = 1)]
        public NameType Adi { get; set; }

        [XmlElement(Order = 2)]
        public CT_IletisimBilgisi IletisimBilgisi { get; set; }
    }
}
