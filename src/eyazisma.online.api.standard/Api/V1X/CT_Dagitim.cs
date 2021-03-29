using System;
using System.Xml.Serialization;

namespace eyazisma.online.api.Api.V1X
{
    [Serializable]
    [XmlType(Namespace = "urn:dpt:eyazisma:schema:xsd:Tipler-1")]
    public sealed class CT_Dagitim
    {
        [XmlElement("GercekSahis", typeof(CT_GercekSahis), Order = 0)]
        [XmlElement("KurumKurulus", typeof(CT_KurumKurulus), Order = 0)]
        [XmlElement("TuzelSahis", typeof(CT_TuzelSahis), Order = 0)]
        public object Item { get; set; }

        [XmlElement(Order = 1)] public ST_KodIvedilik Ivedilik { get; set; }

        [XmlElement(Order = 2)] public ST_KodDagitimTuru DagitimTuru { get; set; }

        [XmlElement(DataType = "duration", Order = 3)]
        public string Miat { get; set; }

        [XmlArray(Order = 4)]
        [XmlArrayItem("KonulmamisEk", IsNullable = false)]
        public CT_KonulmamisEk[] KonulmamisEkListesi { get; set; }
    }
}