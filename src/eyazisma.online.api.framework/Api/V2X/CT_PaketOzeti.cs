using System;
using System.Xml.Serialization;

namespace eyazisma.online.api.framework.Api.V2X
{
    [Serializable()]
    [XmlType(Namespace = "urn:dpt:eyazisma:schema:xsd:PaketOzeti-2")]
    [XmlRoot("PaketOzeti", Namespace = "urn:dpt:eyazisma:schema:xsd:PaketOzeti-2", IsNullable = false)]
    public sealed class CT_PaketOzeti
    {
        [XmlElement("Reference", Order = 0)]
        public CT_Reference[] Reference
        { get; set; }

        [XmlAttribute(DataType = "ID")]
        public string Id { get; set; }
    }
}
