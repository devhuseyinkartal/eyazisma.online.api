using System;
using System.Xml.Serialization;

namespace eyazisma.online.api.Api.V1X
{
    [Serializable]
    [XmlType(Namespace = "urn:dpt:eyazisma:schema:xsd:KodDagitimTuru-1")]
    public enum ST_KodDagitimTuru
    {
        GRG = 1,
        BLG = 2
    }
}