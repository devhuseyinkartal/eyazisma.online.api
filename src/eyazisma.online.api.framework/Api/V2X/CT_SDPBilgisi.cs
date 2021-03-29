using System;
using System.Xml.Serialization;

namespace eyazisma.online.api.Api.V2X
{
    [Serializable]
    [XmlType(Namespace = "urn:dpt:eyazisma:schema:xsd:Tipler-2")]
    [XmlRootAttribute("SdpBilgisi", Namespace = "urn:dpt:eyazisma:schema:xsd:Tipler-2", IsNullable = false)]
    public sealed class CT_SDPBilgisi
    {
        [XmlElement(Order = 0)] public CT_SDP AnaSdp { get; set; }

        [XmlArray(Order = 1)]
        [XmlArrayItem("SdpListesi", IsNullable = false)]
        public CT_SDP[] DigerSdpler { get; set; }
    }
}