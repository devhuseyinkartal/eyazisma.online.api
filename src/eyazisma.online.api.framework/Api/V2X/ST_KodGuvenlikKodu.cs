using System;
using System.Xml.Serialization;

namespace eyazisma.online.api.Api.V2X
{
    [Serializable()]
    [XmlType(Namespace = "urn:dpt:eyazisma:schema:xsd:KodGuvenlik-2")]
    [XmlRoot("GuvenlikKodu", Namespace = "urn:dpt:eyazisma:schema:xsd:Tipler-2", IsNullable = false)]
    public enum ST_KodGuvenlikKodu
    {
        YOK = 1,
        HZO = 3,
        OZL = 4,
        GZL = 5,
        CGZ = 6
    }
}
