using System;
using System.Xml.Serialization;

namespace eyazisma.online.api.Api.V1X
{
    [Serializable]
    [XmlType(Namespace = "urn:dpt:eyazisma:schema:xsd:KodEkTuru-1")]
    public enum ST_KodEkTuru
    {
        DED = 1,
        HRF = 3,
        FZK = 2
    }
}