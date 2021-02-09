using System;
using System.Xml.Serialization;

namespace eyazisma.online.api.framework.Api.V2X
{
    [Serializable()]
    [XmlType(Namespace = "urn:dpt:eyazisma:schema:xsd:NihaiOzet-2")]
    [XmlRoot("NihaiOzet", Namespace = "urn:dpt:eyazisma:schema:xsd:NihaiOzet-2", IsNullable = false)]
    public sealed class CT_NihaiOzet
    {
        [XmlElement("Reference", Namespace = "urn:dpt:eyazisma:schema:xsd:PaketOzeti-2", Order = 0)]
        public CT_Reference[] Reference { get; set; }

        [XmlAttribute(DataType = "ID")]
        public string Id { get; set; }
    }
}
