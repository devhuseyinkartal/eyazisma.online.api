using System;
using System.Xml.Serialization;

namespace eyazisma.online.api.framework.Api.V2X
{
    [Serializable()]
    [XmlType(Namespace = "urn:dpt:eyazisma:schema:xsd:NihaiUstveri-2")]
    [XmlRoot("NihaiUstVeri", Namespace = "urn:dpt:eyazisma:schema:xsd:NihaiUstveri-2", IsNullable = false)]
    public sealed class CT_NihaiUstveri
    {
        [XmlElement(Namespace = "urn:dpt:eyazisma:schema:xsd:Tipler-2", Order = 0)]
        public DateTime Tarih { get; set; }

        [XmlElement(Namespace = "urn:dpt:eyazisma:schema:xsd:Tipler-2", Order = 1)]
        public string BelgeNo { get; set; }

        [XmlArray(Namespace = "urn:dpt:eyazisma:schema:xsd:Tipler-2", Order = 2)]
        [XmlArrayItem("Imza", IsNullable = false)]
        public CT_Imza[] BelgeImzalar { get; set; }
    }
}
