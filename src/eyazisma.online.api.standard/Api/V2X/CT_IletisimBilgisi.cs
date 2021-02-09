﻿using System;
using System.Xml.Serialization;

namespace eyazisma.online.api.Api.V2X
{
    [Serializable()]
    [XmlType(Namespace = "urn:dpt:eyazisma:schema:xsd:Tipler-2")]
    public sealed class CT_IletisimBilgisi
    {
        [XmlElement(Order = 0)]
        public string Telefon { get; set; }

        [XmlElement(Order = 1)]
        public string TelefonDiger { get; set; }

        [XmlElement(Order = 2)]
        public string EPosta { get; set; }

        [XmlElement(Order = 3)]
        public string KepAdresi { get; set; }

        [XmlElement(Order = 4)]
        public string Faks { get; set; }

        [XmlElement(Order = 5)]
        public string WebAdresi { get; set; }

        [XmlElement(Order = 6)]
        public TextType Adres { get; set; }

        [XmlElement(Order = 7)]
        public NameType Il { get; set; }

        [XmlElement(Order = 8)]
        public NameType Ilce { get; set; }

        [XmlElement(Order = 9)]
        public NameType Ulke { get; set; }
    }
}
