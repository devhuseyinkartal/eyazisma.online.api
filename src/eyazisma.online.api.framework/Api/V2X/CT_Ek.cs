using System;
using System.Xml.Serialization;

namespace eyazisma.online.api.framework.Api.V2X
{
    [Serializable()]
    [XmlType(Namespace = "urn:dpt:eyazisma:schema:xsd:Tipler-2")]
    public sealed class CT_Ek
    {
        [XmlElement(Order = 0)]
        public CT_Id Id { get; set; }

        [XmlElement(Order = 1)]
        public string BelgeNo { get; set; }

        [XmlElement(Order = 2)]
        public ST_KodEkTuru Tur { get; set; }

        [XmlElement(Order = 3)]
        public string DosyaAdi { get; set; }

        [XmlElement(DataType = "normalizedString", Order = 4)]
        public string MimeTuru { get; set; }

        [XmlElement(Order = 5)]
        public TextType Ad { get; set; }

        [XmlElement(Order = 6)]
        public int SiraNo { get; set; }

        [XmlElement(Order = 7)]
        public TextType Aciklama { get; set; }

        [XmlElement(DataType = "anyURI", Order = 8)]
        public string Referans { get; set; }

        [XmlElement(Order = 9)]
        public IdentifierType OzId { get; set; }

        [XmlElement(Order = 10)]
        public bool ImzaliMi { get; set; }

        [XmlIgnore()]
        public bool ImzaliMiSpecified { get; set; }

        [XmlElement(Order = 11)]
        public CT_Ozet Ozet { get; set; }
    }
}
