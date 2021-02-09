using System;
using System.Xml.Serialization;

namespace eyazisma.online.api.framework.Api.V1X
{
    [Serializable()]
    [XmlType(Namespace = "urn:dpt:eyazisma:schema:xsd:Tipler-1")]
    public sealed class CT_Imza
    {
        [XmlElement(Order = 0)]
        public CT_GercekSahis Imzalayan { get; set; }

        [XmlElement(Order = 1)]
        public CT_GercekSahis YetkiDevreden { get; set; }

        [XmlElement(Order = 2)]
        public CT_GercekSahis VekaletVeren { get; set; }

        [XmlElement(Order = 3)]
        public NameType Makam { get; set; }

        [XmlElement(Order = 4)]
        public TextType Amac { get; set; }

        [XmlElement(Order = 5)]
        public TextType Aciklama { get; set; }

        [XmlElement(Order = 6)]
        public DateTime Tarih { get; set; }

        [XmlElement(Order = 7)]
        public string TCYK { get; set; }

        [XmlIgnore()]
        public bool TarihSpecified { get; set; }
    }
}
