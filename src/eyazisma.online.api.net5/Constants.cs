using System;
using System.IO.Packaging;

namespace eyazisma.online.api
{
    internal static class Constants
    {
        /// <summary>Ustveri bileşeni ilişki türü.</summary>
        public const string RELATION_TYPE_USTVERI = "http://eyazisma.dpt/iliskiler/ustveri";
        /// <summary>Ustveri bileşeni ilişki türü.</summary>
        public const string RELATION_TYPE_NIHAIUSTVERI = "http://eyazisma.dpt/iliskiler/nihaiustveri";
        /// <summary>BelgeHedef bileşeni ilişki türü.</summary>
        public const string RELATION_TYPE_BELGEHEDEF = "http://eyazisma.dpt/iliskiler/belgehedef";
        /// <summary>BelgeImza bileşeni ilişki türü.</summary>
        public const string RELATION_TYPE_BELGEIMZA = "http://eyazisma.dpt/iliskiler/belgeimza";
        /// <summary>PaketOzeti bileşeni ilişki türü.</summary>
        public const string RELATION_TYPE_PAKETOZETI = "http://eyazisma.dpt/iliskiler/PaketOzeti";
        /// <summary>ParafOzeti bileşeni ilişki türü.</summary>
        public const string RELATION_TYPE_PARAFOZETI = "http://eyazisma.dpt/iliskiler/parafozeti";
        /// <summary>NihaiOzet bileşeni ilişki türü.</summary>
        public const string RELATION_TYPE_NIHAIOZET = "http://eyazisma.dpt/iliskiler/nihaiozet";
        /// <summary>UstYazi bileşeni ilişki türü.</summary>
        public const string RELATION_TYPE_USTYAZI = "http://eyazisma.dpt/iliskiler/ustyazi";
        /// <summary>Imzali PaketOzeti bileşeni ilişki türü.</summary>
        public const string RELATION_TYPE_IMZA = "http://eyazisma.dpt/iliskiler/imzacades";
        /// <summary>Imzali ParafOzeti bileşeni ilişki türü.</summary>
        public const string RELATION_TYPE_PARAFIMZA = "http://eyazisma.dpt/iliskiler/parafimzacades";
        /// <summary>Mühürlü NihaiOzet bileşeni ilişki türü.</summary>
        public const string RELATION_TYPE_MUHUR = "http://eyazisma.dpt/iliskiler/muhurcades";
        /// <summary>Ek bileşeni ilişki türü.</summary>
        public const string RELATION_TYPE_EK = "http://eyazisma.dpt/iliskiler/ek";
        /// <summary>İmzasız ek bileşeni ilişki türü.</summary>
        public const string RELATION_TYPE_IMZASIZEK = "http://eyazisma.dpt/iliskiler/imzasizEk";
        /// <summary>Core bileşeni ilişki türü.</summary>
        public const string RELATION_TYPE_CORE = "http://schemas.openxmlformats.org/package/2006/relationships/metadata/core-properties";
        /// <summary>ArşivOzellikleri bileşeni ilişki türü. Bu bileşenin tanımı ileride yapılacaktır.</summary>
        public const string RELATION_TYPE_ARSIVOZELLIKLERI = "http://eyazisma.dpt/iliskiler/arsivozellikleri";
        /// <summary>SifreliIcerik bileşeni ilişki türü.</summary>
        public const string RELATION_TYPE_SIFRELIICERIK = "http://eyazisma.dpt/iliskiler/sifreliicerik";
        /// <summary>SifreliIcerikBilgisi bileşeni ilişki türü.</summary>
        public const string RELATION_TYPE_SIFRELIICERIKBILGISI = "http://eyazisma.dpt/iliskiler/sifreliicerikbilgisi";

        /// <summary>W3C XML Signature Syntax and Processing, SHA1 namespace</summary>
        public const string ALGORITHM_SHA1 = "http://www.w3.org/2000/09/xmldsig#sha1";
        /// <summary>W3C XML Signature Syntax and Processing, SHA256 namespace</summary>
        public const string ALGORITHM_SHA256 = "http://www.w3.org/2001/04/xmlenc#sha256";
        /// <summary>W3C XML Signature Syntax and Processing, SHA384 namespace</summary>
        public const string ALGORITHM_SHA384 = "http://www.w3.org/2001/04/xmldsig-more#sha384";
        /// <summary>W3C XML Signature Syntax and Processing, SHA512 namespace</summary>
        public const string ALGORITHM_SHA512 = "http://www.w3.org/2001/04/xmlenc#sha512";
        /// <summary>W3C XML Signature Syntax and Processing, RIPEMD160 namespace</summary>
        public const string ALGORITHM_RIPEMD160 = "http://www.w3.org/2001/04/xmlenc#ripemd160";

        /// <summary> Paket Özeti ve Nihai Özete eklenen paket dışı bileşenlere ilişkin tip değeri</summary>
        public const string HARICI_PAKET_BILESENI_REFERANS_TIPI = "http://eyazisma.dpt/bilesen#harici";
        /// <summary> Paket Özeti ve Nihai Özete eklenen paket içi bileşenlere ilişkin tip değeri</summary>
        public const string DAHILI_PAKET_BILESENI_REFERANS_TIPI = "http://eyazisma.dpt/bilesen#dahili";

        /// <summary>Şifreleme Yöntemi</summary>
        public const string SIFRELEME_YONTEMI = "Elektronik Belgeleri Açık Anahtar Altyapısı Kullanarak Güvenli İşleme Rehberi";
        /// <summary>Şifreleme Yöntemi Rehberi URI</summary>
        public const string SIFRELEME_YONTEMI_URI_1 = "http://www.kamusm.gov.tr/dokumanlar/belgeler";
        /// <summary>Şifreleme Yöntemi Rehberi URI</summary>
        public const string SIFRELEME_YONTEMI_URI_2 = "http://www.e-yazisma.gov.tr/SitePages/dokumanlar.aspx";
        /// <summary>Şifreleme Yöntemi Versiyonu</summary> 
        public const string SIFRELEME_YONTEMI_VERSIYONU = "1.4";

        /// <summary>e-Yazışma paketi kategorisi</summary>
        public const string PAKET_KATEGORI = "RESMIYAZISMA";
        /// <summary>Şifreli e-Yazışma paketi kategorisi</summary>
        public const string SIFRELI_PAKET_KATEGORI = "RESMIYAZISMA/SIFRELI";
        /// <summary>e-Yazışma paketi MIME türü</summary>
        public const string PAKET_MIMETURU = "application/eyazisma";
        /// <summary>e-Yazışma paketi versiyonu</summary>
        public const string PAKET_VERSIYON_V1X = "1.3";
        public const string PAKET_VERSIYON_V2X = "2.0";
        /// <summary> Paket revizyonu</summary>
        public const string PAKET_REVIZYON = "DotNet/eyazisma.online.api {0}";

        /// <summary> PaketOzeti bileşenine ait URI.</summary>
        public const string URI_PAKETOZETI_STRING = "/PaketOzeti/PaketOzeti.xml";
        public static Uri URI_PAKETOZETI => PackUriHelper.CreatePartUri(new Uri(URI_PAKETOZETI_STRING, UriKind.Relative));

        /// <summary> ParafOzeti bileşenine ait URI.</summary>
        public const string URI_PARAFOZETI_STRING = "/ParafOzeti/ParafOzeti.xml";
        public static Uri URI_PARAFOZETI = PackUriHelper.CreatePartUri(new Uri(URI_PARAFOZETI_STRING, UriKind.Relative));

        /// <summary> NihaiOzet bileşenine ait URI.</summary>
        public const string URI_NIHAIOZET_STRING = "/NihaiOzet/NihaiOzet.xml";
        public static Uri URI_NIHAIOZET = PackUriHelper.CreatePartUri(new Uri(URI_NIHAIOZET_STRING, UriKind.Relative));

        /// <summary> Ustveri bileşenine ait URI.</summary>
        public const string URI_USTVERI_STRING = "/Ustveri/Ustveri.xml";
        public static Uri URI_USTVERI = PackUriHelper.CreatePartUri(new Uri(URI_USTVERI_STRING, UriKind.Relative));

        /// <summary> NihaiUstveri bileşenine ait URI.</summary>
        public const string URI_NIHAIUSTVERI_STRING = "/NihaiUstveri/NihaiUstveri.xml";
        public static Uri URI_NIHAIUSTVERI = PackUriHelper.CreatePartUri(new Uri(URI_NIHAIUSTVERI_STRING, UriKind.Relative));

        /// <summary> BelgeHedef bileşenine ait URI.</summary>
        public const string URI_BELGEHEDEF_STRING = "/BelgeHedef/BelgeHedef.xml";
        public static Uri URI_BELGEHEDEF = PackUriHelper.CreatePartUri(new Uri(URI_BELGEHEDEF_STRING, UriKind.Relative));

        /// <summary> Imzalar bileşenine ait URI.</summary>
        public const string URI_BELGEIMZA_STRING = "/Imzalar/BelgeImza.xml";
        public static Uri URI_BELGEIMZA = PackUriHelper.CreatePartUri(new Uri(URI_BELGEIMZA_STRING, UriKind.Relative));

        /// <summary> UstYazi bileşenine ait URI formatı.</summary>
        public const string URI_FORMAT_USTYAZI_STRING = "/UstYazi/{0}";

        /// <summary> Ek bileşenine ait URI.</summary>
        public const string URI_ROOT_EK_STRING = "Ekler";

        /// <summary> İmzasızEk bileşenine ait URI.</summary>
        public const string URI_ROOT_IMZASIZEK_STRING = "ImzasizEkler";

        /// <summary> Imzali PaketOzeti Imza bileşenine ait URI.</summary>
        public const string URI_IMZA_STRING = "/Imzalar/ImzaCades.imz";
        public static Uri URI_IMZA = PackUriHelper.CreatePartUri(new Uri(URI_IMZA_STRING, UriKind.Relative));

        /// <summary> Imzali ParafOzeti Imza bileşenine ait URI.</summary>
        public const string URI_PARAFIMZA_STRING = "/Paraflar/ParafImzaCades.imz";
        public static Uri URI_PARAFIMZA = PackUriHelper.CreatePartUri(new Uri(URI_PARAFIMZA_STRING, UriKind.Relative));

        /// <summary> Mühürlü NihaiOzet bileşenine ait URI.</summary>
        public const string URI_MUHUR_V1X_STRING = "/Muhurler/MuhurCades.imz";
        public static Uri URI_MUHUR_V1X = PackUriHelper.CreatePartUri(new Uri(URI_MUHUR_V1X_STRING, UriKind.Relative));
        public const string URI_MUHUR_V2X_STRING = "/Muhur/MuhurCades.imz";
        public static Uri URI_MUHUR_V2X = PackUriHelper.CreatePartUri(new Uri(URI_MUHUR_V2X_STRING, UriKind.Relative));

        /// <summary> ArşivOzellikleri bileşenine ait URI. Bu bileşenin tanımı ileride yapılacaktır..</summary>
        public const string URI_ARSIVOZELLIKLERI_STRING = "/Arsiv/ArsivOzellikleri.xml";
        public static Uri URI_ARSIVOZELLIKLERI = PackUriHelper.CreatePartUri(new Uri(URI_ARSIVOZELLIKLERI_STRING, UriKind.Relative));

        /// <summary> SifreliIcerik bileşenine ait URI.</summary>
        public const string URI_FORMAT_SIFRELIICERIK_V1X_STRING = "/SifreliIcerik/{0}.eyp";
        public const string URI_FORMAT_SIFRELIICERIK_V2X_STRING = "/SifreliIcerik/{0}";

        /// <summary> SifreliIcerikBilgisi bileşenine ait URI.</summary>
        public const string URI_SIFRELIICERIKBILGISI_STRING = "/SifreliIcerikBilgisi/SifreliIcerikBilgisi.xml";
        public static Uri URI_SIFRELIICERIKBILGISI = PackUriHelper.CreatePartUri(new Uri(URI_SIFRELIICERIKBILGISI_STRING, UriKind.Relative));

        /// <summary> Ek bileşeni ilişkisi Id formatı.</summary>
        public static string ID_ROOT_EK(string id) => string.Format("IdEk_{0}", id);
        /// <summary> İmzasız Ek bileşeni ilişkisi Id formatı.</summary>
        public static string ID_ROOT_IMZASIZEK(string id) => string.Format("IdImzasizEk_{0}", id);
        /// <summary> İmzalı PaketOzeti bileşeni ilişkisi Id'si.</summary>
        public const string ID_IMZA = "IdImzaCades";
        /// <summary> İmzalı ParafOzetu bileşeni ilişkisi Id'si.</summary>
        public const string ID_PARAFIMZA = "IdParafImzaCades";
        /// <summary> İmzalı NihaiOzet bileşeni ilişkisi Id'si.</summary>
        public const string ID_MUHUR = "IdMuhurCades";
        /// <summary> BelgeImza bileşeni ilişkisi Id'si.</summary>
        public const string ID_BELGEIMZA = "IdBelgeImza";
        /// <summary> UstYazi bileşeni ilişkisi Id'si.</summary>
        public const string ID_USTYAZI = "IdUstYazi";
        /// <summary> Ustveri bileşeni ilişkisi Id'si.</summary>
        public const string ID_USTVERI = "IdUstveri";
        /// <summary> NihaiUstveri bileşeni ilişkisi Id'si.</summary>
        public const string ID_NIHAIUSTVERI = "IdNihaiUstveri";
        /// <summary> BelgeHedef bileşeni ilişkisi Id'si.</summary>
        public const string ID_BELGEHEDEF = "IdBelgeHedef";
        /// <summary> PaketOzeti bileşeni ilişkisi Id'si.</summary>
        public const string ID_PaketOzeti = "IdPaketOzeti";
        /// <summary> ParafOzeti bileşeni ilişkisi Id'si.</summary>
        public const string ID_PARAFOZETI = "IdParafOzeti";
        /// <summary> NihaiOzet bileşeni ilişkisi Id'si.</summary>
        public const string ID_NIHAIOZET = "IdNihaiOzet";
        /// <summary> SifreliIcerik bileşeni ilişkisi Id'si.</summary>
        public const string ID_SIFRELIICERIK = "IdSifreliIcerik";
        /// <summary> Ek bileşeni ilişkisi Id'si.</summary>
        public const string ID_SIFRELIICERIKBILGISI = "IdSifreliIcerikBilgisi";

        /// <summary> XML Mime türü.</summary>
        public const string MIME_XML = "application/xml";
        /// <summary> XML Octet-stream türü.</summary>
        public const string MIME_OCTETSTREAM = "application/octet-stream";

        public const string MIME_PKCS7MIME = "application/pkcs7-mime";

        public static string[] KEPHS_DOMAINS = new string[]
        {
            "hs01.kep.tr",
            "hs02.kep.tr",
            "hs03.kep.tr",
            "hs04.kep.tr",
            "hs05.kep.tr",
            "hs06.kep.tr",
            "hs07.kep.tr",
            "hs08.kep.tr"
        };

        public static string[] TUZEL_SAHIS_ID_SCHEMES = new string[]
        {
            "MERSIS",
            "VBYS",
            "DERBIS",
            "PARBIS",
            "SENBIS"
        };
    }
}
