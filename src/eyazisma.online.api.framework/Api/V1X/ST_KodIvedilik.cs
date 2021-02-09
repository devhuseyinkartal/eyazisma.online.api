using System;
using System.Xml.Serialization;

namespace eyazisma.online.api.framework.Api.V1X
{
    [Serializable()]
    [XmlType(Namespace = "urn:dpt:eyazisma:schema:xsd:KodIvedilik-1")]
    public enum ST_KodIvedilik
    {
        NRM = 1,
        IVD = 4,
        CIV = 5,
        GNL = 3,
    }
}
