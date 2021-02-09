using System;
using System.Xml.Serialization;

namespace eyazisma.online.api.framework.Api.V2X
{
    [Serializable()]
    [XmlType(Namespace = "urn:dpt:eyazisma:schema:xsd:Tipler-2")]
    public sealed class CT_Ilgi
    {
        [XmlElement(Order = 0)]
        public CT_Id Id { get; set; }

        [XmlElement(Order = 1)]
        public string BelgeNo { get; set; }

        [XmlElement(Order = 2)]
        public DateTime Tarih { get; set; }

        [XmlIgnore()]
        public bool TarihSpecified { get; set; }

        [XmlElement(Order = 3)]
        public string Etiket { get; set; }

        [XmlElement(DataType = "normalizedString", Order = 4)]
        public string EkId { get; set; }

        [XmlElement(Order = 5)]
        public TextType Ad { get; set; }

        [XmlElement(Order = 6)]
        public TextType Aciklama { get; set; }

        [XmlElement(Order = 7)]
        public IdentifierType OzId { get; set; }
    }
}
