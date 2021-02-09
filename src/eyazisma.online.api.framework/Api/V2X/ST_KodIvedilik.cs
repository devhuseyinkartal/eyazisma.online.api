using System;
using System.Xml.Serialization;

namespace eyazisma.online.api.Api.V2X
{
    [Serializable()]
    [XmlType(Namespace = "urn:dpt:eyazisma:schema:xsd:KodIvedilik-2")]
    public enum ST_KodIvedilik
    {
        NRM = 1,
        ACL = 2,
        GNL = 3
    }
}
