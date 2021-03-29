using System;
using System.Xml.Serialization;

namespace eyazisma.online.api.Api.V2X
{
    [Serializable]
    [XmlType(Namespace = "urn:dpt:eyazisma:schema:xsd:PaketOzeti-2")]
    [XmlRoot("DigestItem", Namespace = "urn:dpt:eyazisma:schema:xsd:PaketOzeti-2", IsNullable = false)]
    public sealed class CT_DigestItem
    {
        [XmlElement(Order = 0)] public CT_DigestMethod DigestMethod { get; set; }

        [XmlElement(DataType = "base64Binary", Order = 1)]
        public byte[] DigestValue { get; set; }
    }
}