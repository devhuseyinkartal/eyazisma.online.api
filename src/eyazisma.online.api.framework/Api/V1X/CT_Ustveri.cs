using System;
using System.Xml.Serialization;

namespace eyazisma.online.api.Api.V1X
{
    [Serializable]
    [XmlType(Namespace = "urn:dpt:eyazisma:schema:xsd:Ustveri-1")]
    [XmlRoot("Ustveri", Namespace = "urn:dpt:eyazisma:schema:xsd:Ustveri-1", IsNullable = false)]
    public sealed class CT_Ustveri
    {
        [XmlElement(Namespace = "urn:dpt:eyazisma:schema:xsd:Tipler-1", DataType = "normalizedString", Order = 0)]
        public string BelgeId { get; set; }

        [XmlElement(Namespace = "urn:dpt:eyazisma:schema:xsd:Tipler-1", Order = 1)]
        public TextType Konu { get; set; }

        [XmlElement(Namespace = "urn:dpt:eyazisma:schema:xsd:Tipler-1", Order = 2)]
        public DateTime Tarih { get; set; }

        [XmlElement(Namespace = "urn:dpt:eyazisma:schema:xsd:Tipler-1", Order = 3)]
        public string BelgeNo { get; set; }

        [XmlElement(Namespace = "urn:dpt:eyazisma:schema:xsd:Tipler-1", Order = 4)]
        public ST_KodGuvenlikKodu GuvenlikKodu { get; set; }

        [XmlElement(Namespace = "urn:dpt:eyazisma:schema:xsd:Tipler-1", IsNullable = true, Order = 5)]
        public DateTime? GuvenlikKoduGecerlilikTarihi { get; set; }

        [XmlIgnore] public bool GuvenlikKoduGecerlilikTarihiSpecified { get; set; }

        [XmlElement(Namespace = "urn:dpt:eyazisma:schema:xsd:Tipler-1", DataType = "normalizedString", Order = 6)]
        public string MimeTuru { get; set; }

        [XmlElement(Namespace = "urn:dpt:eyazisma:schema:xsd:Tipler-1", Order = 7)]
        public IdentifierType OzId { get; set; }

        [XmlArray(Namespace = "urn:dpt:eyazisma:schema:xsd:Tipler-1", Order = 8)]
        [XmlArrayItem("Dagitim", IsNullable = false)]
        public CT_Dagitim[] DagitimListesi { get; set; }

        [XmlArray(Namespace = "urn:dpt:eyazisma:schema:xsd:Tipler-1", Order = 9)]
        [XmlArrayItem("Ek", IsNullable = false)]
        public CT_Ek[] Ekler { get; set; }

        [XmlArray(Namespace = "urn:dpt:eyazisma:schema:xsd:Tipler-1", Order = 10)]
        [XmlArrayItem("Ilgi", IsNullable = false)]
        public CT_Ilgi[] Ilgiler { get; set; }

        [XmlElement(Namespace = "urn:dpt:eyazisma:schema:xsd:Tipler-1", Order = 11)]
        public string Dil { get; set; }

        [XmlElement(Namespace = "urn:dpt:eyazisma:schema:xsd:Tipler-1", Order = 12)]
        public CT_Olusturan Olusturan { get; set; }

        [XmlArray(Namespace = "urn:dpt:eyazisma:schema:xsd:Tipler-1", Order = 13)]
        [XmlArrayItem("Ilgili", IsNullable = false)]
        public CT_Ilgili[] IlgiliListesi { get; set; }

        [XmlElement(Namespace = "urn:dpt:eyazisma:schema:xsd:Tipler-1", Order = 14)]
        public string DosyaAdi { get; set; }
    }
}