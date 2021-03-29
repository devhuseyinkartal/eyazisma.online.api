using System;
using System.Xml.Serialization;

namespace eyazisma.online.api.Api.V1X
{
    [Serializable]
    [XmlType(Namespace = "urn:dpt:eyazisma:schema:xsd:Tipler-1")]
    [XmlRoot("Olusturan", Namespace = "urn:dpt:eyazisma:schema:xsd:Tipler-1", IsNullable = false)]
    public sealed class CT_Olusturan
    {
        [XmlElement("GercekSahis", typeof(CT_GercekSahis), Order = 0)]
        [XmlElement("KurumKurulus", typeof(CT_KurumKurulus), Order = 0)]
        [XmlElement("TuzelSahis", typeof(CT_TuzelSahis), Order = 0)]
        public object Item { get; set; }
    }
}