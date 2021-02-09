using System;
using System.Xml.Serialization;

namespace eyazisma.online.api.Api.V2X
{
    [Serializable()]
    [XmlType(Namespace = "urn:dpt:eyazisma:schema:xsd:Ustveri-2")]
    [XmlRoot("UstVeri", Namespace = "urn:dpt:eyazisma:schema:xsd:Ustveri-2", IsNullable = false)]
    public sealed class CT_Ustveri
    {
        [XmlElement(Namespace = "urn:dpt:eyazisma:schema:xsd:Tipler-2", DataType = "normalizedString", Order = 0)]
        public string BelgeId { get; set; }

        [XmlElement(Namespace = "urn:dpt:eyazisma:schema:xsd:Tipler-2", Order = 1)]
        public TextType Konu { get; set; }

        [XmlElement(Namespace = "urn:dpt:eyazisma:schema:xsd:Tipler-2", Order = 2)]
        public ST_KodGuvenlikKodu GuvenlikKodu { get; set; }

        [XmlElement(Namespace = "urn:dpt:eyazisma:schema:xsd:Tipler-2", IsNullable = true, Order = 3)]
        public DateTime? GuvenlikKoduGecerlilikTarihi { get; set; }

        [XmlIgnore()]
        public bool GuvenlikKoduGecerlilikTarihiSpecified { get; set; }

        [XmlElement(Namespace = "urn:dpt:eyazisma:schema:xsd:Tipler-2", DataType = "normalizedString", Order = 4)]
        public string MimeTuru { get; set; }

        [XmlElement(Namespace = "urn:dpt:eyazisma:schema:xsd:Tipler-2", Order = 5)]
        public IdentifierType OzId { get; set; }

        [XmlArray(Namespace = "urn:dpt:eyazisma:schema:xsd:Tipler-2", Order = 6)]
        [XmlArrayItem("Dagitim", IsNullable = false)]
        public CT_Dagitim[] DagitimListesi { get; set; }

        [XmlArray(Namespace = "urn:dpt:eyazisma:schema:xsd:Tipler-2", Order = 7)]
        [XmlArrayItem("Ek", IsNullable = false)]
        public CT_Ek[] Ekler { get; set; }

        [XmlArray(Namespace = "urn:dpt:eyazisma:schema:xsd:Tipler-2", Order = 8)]
        [XmlArrayItem("Ilgi", IsNullable = false)]
        public CT_Ilgi[] Ilgiler { get; set; }

        [XmlElement(Namespace = "urn:dpt:eyazisma:schema:xsd:Tipler-2", Order = 9)]
        public string Dil { get; set; }

        [XmlElement(Namespace = "urn:dpt:eyazisma:schema:xsd:Tipler-2", Order = 10)]
        public CT_Olusturan Olusturan { get; set; }

        [XmlArray(Namespace = "urn:dpt:eyazisma:schema:xsd:Tipler-2", Order = 11)]
        [XmlArrayItem("Ilgili", IsNullable = false)]
        public CT_Ilgili[] IlgiliListesi { get; set; }

        [XmlElement(Namespace = "urn:dpt:eyazisma:schema:xsd:Tipler-2", Order = 12)]
        public string DosyaAdi { get; set; }

        [XmlElement(Namespace = "urn:dpt:eyazisma:schema:xsd:Tipler-2", Order = 13)]
        public CT_SDPBilgisi SdpBilgisi { get; set; }

        [XmlArray(Namespace = "urn:dpt:eyazisma:schema:xsd:Tipler-2", Order = 14)]
        [XmlArrayItem("Heysk", IsNullable = false)]
        public CT_HEYSK[] HeyskListesi { get; set; }

        [XmlElement(Namespace = "urn:dpt:eyazisma:schema:xsd:Tipler-2", Order = 15)]
        public CT_DogrulamaBilgisi DogrulamaBilgisi { get; set; }
    }
}
