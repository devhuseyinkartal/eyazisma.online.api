using System;
using System.Xml.Serialization;

namespace eyazisma.online.api.Api.V1X
{
    [Serializable()]
    [XmlType(Namespace = "urn:dpt:eyazisma:schema:xsd:KodGuvenlik-1")]
    [XmlRoot("GuvenlikKodu", Namespace = "urn:dpt:eyazisma:schema:xsd:Tipler-1", IsNullable = false)]
    public enum ST_KodGuvenlikKodu
    {
        TSD = 2,
        HZO = 3,
        OZL = 4,
        GZL = 5,
        CGZ = 6,
        KSO = 7
    }
}
