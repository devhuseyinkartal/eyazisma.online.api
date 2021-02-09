using eyazisma.online.api.Classes;
using eyazisma.online.api.Enums;
using System;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace eyazisma.online.api.Extensions
{
    internal static class PackageExtensions
    {
        public static PaketVersiyonTuru GetPaketVersiyon(this Package package)
        {
            var paketVersiyon = PaketVersiyonTuru.TespitEdilemedi;

            if (!string.IsNullOrEmpty(package.PackageProperties.Version))
            {
                var versionParts = package.PackageProperties.Version.Split('.');
                if (versionParts.Length == 2)
                {
                    if (int.TryParse(versionParts[0], out var firstPart))
                    {
                        switch (firstPart)
                        {
                            case 1: { paketVersiyon = PaketVersiyonTuru.Versiyon1X; break; }
                            case 2: { paketVersiyon = PaketVersiyonTuru.Versiyon2X; break; }
                        }
                    }
                }
            }

            return paketVersiyon;
        }

        public static bool UstYaziExists(this Package package) => !(package.GetRelationshipsByType(Constants.RELATION_TYPE_USTYAZI) == null || package.GetRelationshipsByType(Constants.RELATION_TYPE_USTYAZI).Count() == 0);

        public static Stream GetUstYaziStream(this Package package) => package.UstYaziExists() ? package.GetPart(package.GetRelationshipsByType(Constants.RELATION_TYPE_USTYAZI).First().TargetUri).GetStream() : null;

        public static void AddUstYazi(this Package package, UstYazi ustYazi)
        {
            var partUstYaziUri = new Uri(string.Format(Constants.URI_FORMAT_USTYAZI_STRING, ustYazi.DosyaAdi.EncodePath()), UriKind.Relative);
            var partUstYazi = package.CreatePart(partUstYaziUri, ustYazi.MimeTuru, CompressionOption.Maximum);
            ustYazi.Dosya.CopyTo(partUstYazi.GetStream());
            package.CreateRelationship(partUstYazi.Uri, TargetMode.Internal, Constants.RELATION_TYPE_USTYAZI, Constants.ID_USTYAZI);
        }

        public static bool EkDosyaExists(this Package package, IdTip id) => package.RelationshipExists(Constants.ID_ROOT_EK(id.Deger.ToString().ToUpperInvariant())) ||
                                                                            package.RelationshipExists(Constants.ID_ROOT_IMZASIZEK(id.Deger.ToString().ToUpperInvariant()));

        public static Stream GetEkDosyaStream(this Package package, IdTip id)
        {
            var ekIdUri = Constants.ID_ROOT_EK(id.Deger.ToString().ToUpperInvariant());
            var ekIdImzasizUri = Constants.ID_ROOT_IMZASIZEK(id.Deger.ToString().ToUpperInvariant());

            if (package.RelationshipExists(ekIdUri))
                return package.GetPart(package.GetRelationship(ekIdUri).TargetUri).GetStream();
            else if (package.RelationshipExists(ekIdImzasizUri))
                return package.GetPart(package.GetRelationship(ekIdImzasizUri).TargetUri).GetStream();
            return null;
        }

        public static bool DeleteEkDosya(this Package package, IdTip id)
        {
            var ekIdUri = Constants.ID_ROOT_EK(id.Deger.ToString().ToUpperInvariant());
            var ekIdImzasizUri = Constants.ID_ROOT_IMZASIZEK(id.Deger.ToString().ToUpperInvariant());

            if (package.RelationshipExists(ekIdUri))
            {
                package.DeletePart(package.GetRelationship(ekIdUri).TargetUri);
                package.DeleteRelationship(ekIdUri);
                return true;
            }
            if (package.RelationshipExists(ekIdImzasizUri))
            {
                package.DeletePart(package.GetRelationship(ekIdImzasizUri).TargetUri);
                package.DeleteRelationship(ekIdImzasizUri);
                return true;
            }
            return false;
        }

        public static void AddEkDosya(this Package package, EkDosya ekDosya)
        {
            string klasorAdi;
            string iliskiAdi;
            string id;
            if (!ekDosya.Ek.ImzaliMi)
            {
                klasorAdi = Constants.URI_ROOT_IMZASIZEK_STRING;
                iliskiAdi = Constants.RELATION_TYPE_IMZASIZEK;
                id = Constants.ID_ROOT_IMZASIZEK(ekDosya.Ek.Id.Deger.ToString().ToUpperInvariant());
            }
            else
            {
                klasorAdi = Constants.URI_ROOT_EK_STRING;
                iliskiAdi = Constants.RELATION_TYPE_EK;
                id = Constants.ID_ROOT_EK(ekDosya.Ek.Id.Deger.ToString().ToUpperInvariant());
            }
            var partEkDosyaUri = new Uri(string.Format("/{0}/{1}", klasorAdi, ekDosya.DosyaAdi.EncodePath()), UriKind.Relative);
            var partEkDosya = package.CreatePart(partEkDosyaUri, ekDosya.Ek.MimeTuru, CompressionOption.Maximum);
            ekDosya.Dosya.CopyTo(partEkDosya.GetStream());
            package.CreateRelationship(partEkDosya.Uri, TargetMode.Internal, iliskiAdi, id);
        }

        public static bool BelgeHedefUriExists(this Package package) => package.PartExists(Constants.URI_BELGEHEDEF);

        public static bool BelgeHedefRelationExists(this Package package) => !(package.GetRelationshipsByType(Constants.RELATION_TYPE_BELGEHEDEF) == null || package.GetRelationshipsByType(Constants.RELATION_TYPE_BELGEHEDEF).Count() == 0);

        public static Stream GetBelgeHedefStream(this Package package) => package.BelgeHedefUriExists() ? package.GetPart(Constants.URI_BELGEHEDEF).GetStream() : null;

        public static void DeleteBelgeHedef(this Package package)
        {
            if (package.BelgeHedefUriExists())
            {
                package.DeletePart(Constants.URI_BELGEHEDEF);
                package.DeleteRelationship(Constants.ID_BELGEHEDEF);
            }
        }

        public static void GenerateBelgeHedef(this Package package, BelgeHedef belgeHedef)
        {
            if (belgeHedef != null && belgeHedef.Hedefler != null && belgeHedef.Hedefler.Count > 0)
            {
                var partBelgeHedef = package.CreatePart(Constants.URI_BELGEHEDEF, Constants.MIME_XML, CompressionOption.Maximum);
                package.CreateRelationship(partBelgeHedef.Uri, TargetMode.Internal, Constants.RELATION_TYPE_BELGEHEDEF, Constants.ID_BELGEHEDEF);
                var xmlSerializer = new XmlSerializer(typeof(Api.V1X.CT_BelgeHedef));
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("tipler", "urn:dpt:eyazisma:schema:xsd:Tipler-1");
                XmlTextWriter xmlTextWriter = new XmlTextWriter(partBelgeHedef.GetStream(), Encoding.UTF8)
                {
                    Formatting = Formatting.Indented
                };
                xmlSerializer.Serialize(xmlTextWriter, belgeHedef.ToV1XCT_BelgeHedef(), ns);
            }
        }

        public static bool BelgeImzaUriExists(this Package package) => package.PartExists(Constants.URI_BELGEIMZA);

        public static bool BelgeImzaRelationExists(this Package package) => !(package.GetRelationshipsByType(Constants.RELATION_TYPE_BELGEIMZA) == null || package.GetRelationshipsByType(Constants.RELATION_TYPE_BELGEIMZA).Count() == 0);

        public static Stream GetBelgeImzaStream(this Package package) => package.BelgeImzaUriExists() ? package.GetPart(Constants.URI_BELGEIMZA).GetStream() : null;

        public static void DeleteBelgeImza(this Package package)
        {
            if (package.BelgeImzaUriExists())
            {
                package.DeletePart(Constants.URI_BELGEIMZA);
                package.DeleteRelationship(Constants.ID_BELGEIMZA);
            }
        }

        public static void GenerateBelgeImza(this Package package, BelgeImza belgeImza)
        {
            if (belgeImza != null && belgeImza.Imzalar != null && belgeImza.Imzalar.Count > 0)
            {
                var partBelgeImza = package.CreatePart(Constants.URI_BELGEIMZA, Constants.MIME_XML, CompressionOption.Maximum);
                package.CreateRelationship(partBelgeImza.Uri, TargetMode.Internal, Constants.RELATION_TYPE_BELGEIMZA, Constants.ID_BELGEIMZA);
                var xmlSerializer = new XmlSerializer(typeof(Api.V1X.CT_BelgeImza));
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("tipler", "urn:dpt:eyazisma:schema:xsd:Tipler-1");
                XmlTextWriter xmlTextWriter = new XmlTextWriter(partBelgeImza.GetStream(), Encoding.UTF8)
                {
                    Formatting = Formatting.Indented
                };
                xmlSerializer.Serialize(xmlTextWriter, belgeImza.ToV1XCT_BelgeImza(), ns);
            }
        }

        public static bool ImzaExists(this Package package) => package.PartExists(Constants.URI_IMZA);

        public static Stream GetImzaStream(this Package package) => package.ImzaExists() ? package.GetPart(Constants.URI_IMZA).GetStream() : null;

        public static void DeleteImza(this Package package)
        {
            if (package.ImzaExists())
            {
                package.DeletePart(Constants.URI_IMZA);
                package.GetPart(Constants.URI_PAKETOZETI).DeleteRelationship(Constants.ID_IMZA);
            }
        }

        public static void AddImza(this Package package, byte[] imza)
        {
            PackagePart partImza = package.CreatePart(Constants.URI_IMZA, Constants.MIME_OCTETSTREAM, CompressionOption.Maximum);
            partImza.GetStream().Write(imza, 0, imza.Length);
            package.GetPart(Constants.URI_PAKETOZETI).CreateRelationship(PackUriHelper.GetRelativeUri(Constants.URI_PAKETOZETI, Constants.URI_IMZA), TargetMode.Internal, Constants.RELATION_TYPE_IMZA, Constants.ID_IMZA);
        }

        public static bool ParafImzaExists(this Package package) => package.PartExists(Constants.URI_PARAFIMZA);

        public static Stream GetParafImzaStream(this Package package) => package.ParafImzaExists() ? package.GetPart(Constants.URI_PARAFIMZA).GetStream() : null;

        public static void DeleteParafImza(this Package package)
        {
            if (package.ParafImzaExists())
            {
                package.DeletePart(Constants.URI_PARAFIMZA);
                package.GetPart(Constants.URI_PARAFIMZA).DeleteRelationship(Constants.ID_PARAFIMZA);
            }
        }

        public static void AddParafImza(this Package package, byte[] imza)
        {
            if (package.ParafImzaExists())
            {
                package.DeletePart(Constants.URI_PARAFIMZA);
                package.GetPart(Constants.URI_PAKETOZETI).DeleteRelationship(Constants.ID_PARAFOZETI);
            }

            PackagePart partImza = package.CreatePart(Constants.URI_PARAFIMZA, Constants.MIME_OCTETSTREAM, CompressionOption.Maximum);
            partImza.GetStream().Write(imza, 0, imza.Length);
            package.GetPart(Constants.URI_PARAFOZETI).CreateRelationship(PackUriHelper.GetRelativeUri(Constants.URI_PARAFOZETI, Constants.URI_PARAFIMZA), TargetMode.Internal, Constants.RELATION_TYPE_PARAFIMZA, Constants.ID_PARAFIMZA);
        }

        public static bool MuhurExists(this Package package, PaketVersiyonTuru paketVersiyon) => package.PartExists(paketVersiyon == PaketVersiyonTuru.Versiyon1X ? Constants.URI_MUHUR_V1X : Constants.URI_MUHUR_V2X);

        public static Stream GetMuhurStream(this Package package, PaketVersiyonTuru paketVersiyon) => package.MuhurExists(paketVersiyon) ?
                                                                                                      package.GetPart(paketVersiyon == PaketVersiyonTuru.Versiyon1X ? Constants.URI_MUHUR_V1X : Constants.URI_MUHUR_V2X).GetStream() : null;
        public static void DeleteMuhur(this Package package, PaketVersiyonTuru paketVersiyon)
        {
            var partMuhurUri = paketVersiyon == PaketVersiyonTuru.Versiyon1X ? Constants.URI_MUHUR_V1X : Constants.URI_MUHUR_V2X;

            if (package.MuhurExists(paketVersiyon))
            {
                package.DeletePart(partMuhurUri);
                package.GetPart(Constants.URI_NIHAIOZET).DeleteRelationship(Constants.ID_MUHUR);
            }
        }

        public static void AddMuhur(this Package package, byte[] muhur, PaketVersiyonTuru paketVersiyon)
        {
            var partMuhurUri = paketVersiyon == PaketVersiyonTuru.Versiyon1X ? Constants.URI_MUHUR_V1X : Constants.URI_MUHUR_V2X;

            PackagePart partMuhur = package.CreatePart(partMuhurUri, Constants.MIME_OCTETSTREAM, CompressionOption.Maximum);
            partMuhur.GetStream().Write(muhur, 0, muhur.Length);
            package.GetPart(Constants.URI_NIHAIOZET).CreateRelationship(PackUriHelper.GetRelativeUri(Constants.URI_NIHAIOZET, partMuhurUri), TargetMode.Internal, Constants.RELATION_TYPE_MUHUR, Constants.ID_MUHUR);
        }

        public static bool NihaiUstveriExists(this Package package) => package.PartExists(Constants.URI_NIHAIUSTVERI);

        public static Stream GetNihaiUstveriStream(this Package package) => package.NihaiUstveriExists() ? package.GetPart(Constants.URI_NIHAIUSTVERI).GetStream() : null;

        public static void DeleteNihaiUstveri(this Package package)
        {
            if (package.NihaiUstveriExists())
            {
                package.DeletePart(Constants.URI_NIHAIUSTVERI);
                package.DeleteRelationship(Constants.ID_NIHAIUSTVERI);
            }
        }

        public static void GenerateNihaiUstveri(this Package package, NihaiUstveri nihaiUstveri)
        {
            if (nihaiUstveri != null)
            {
                var partNihaiUstveri = package.CreatePart(Constants.URI_NIHAIUSTVERI, Constants.MIME_XML, CompressionOption.Normal);
                package.CreateRelationship(partNihaiUstveri.Uri, TargetMode.Internal, Constants.RELATION_TYPE_NIHAIUSTVERI, Constants.ID_NIHAIUSTVERI);

                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("tipler", "urn:dpt:eyazisma:schema:xsd:Tipler-2");
                var x = new XmlSerializer(typeof(Api.V2X.CT_NihaiUstveri));
                XmlTextWriter xmlTextWriter = new XmlTextWriter(partNihaiUstveri.GetStream(), Encoding.UTF8)
                {
                    Formatting = Formatting.Indented
                };
                x.Serialize(xmlTextWriter, nihaiUstveri.ToV2XCT_NihaiUstveri(), ns);
            }
        }

        public static bool NihaiOzetExists(this Package package) => package.PartExists(Constants.URI_NIHAIOZET);

        public static Stream GetNihaiOzetStream(this Package package) => package.NihaiOzetExists() ? package.GetPart(Constants.URI_NIHAIOZET).GetStream() : null;

        public static void GenerateNihaiOzet(this Package package, NihaiOzet nihaiOzet, PaketVersiyonTuru paketVersiyon)
        {
            if (nihaiOzet != null && nihaiOzet.Referanslar != null && nihaiOzet.Referanslar.Count > 0)
            {
                object nihaiOzetObject = null;
                Type nihaiOzetType = null;

                switch (paketVersiyon)
                {
                    case PaketVersiyonTuru.Versiyon1X:
                        {
                            nihaiOzetObject = nihaiOzet.ToV1XCT_NihaiOzet();
                            nihaiOzetType = typeof(Api.V1X.CT_NihaiOzet);
                            break;
                        }
                    case PaketVersiyonTuru.Versiyon2X:
                        {
                            nihaiOzetObject = nihaiOzet.ToV2XCT_NihaiOzet();
                            nihaiOzetType = typeof(Api.V2X.CT_NihaiOzet);
                            break;
                        }
                }
                var partNihaiOzet = package.CreatePart(Constants.URI_NIHAIOZET, Constants.MIME_XML, CompressionOption.Maximum);
                package.CreateRelationship(partNihaiOzet.Uri, TargetMode.Internal, Constants.RELATION_TYPE_NIHAIOZET, Constants.ID_NIHAIOZET);
                var xmlSerializer = new XmlSerializer(nihaiOzetType);
                XmlTextWriter xmlTextWriter = new XmlTextWriter(partNihaiOzet.GetStream(), Encoding.UTF8)
                {
                    Formatting = Formatting.Indented
                };
                xmlSerializer.Serialize(xmlTextWriter, nihaiOzetObject);
            }
        }

        public static void DeleteNihaiOzet(this Package package)
        {
            if (package.NihaiOzetExists())
            {
                package.DeletePart(Constants.URI_NIHAIOZET);
                package.DeleteRelationship(Constants.ID_NIHAIOZET);
            }
        }

        public static void AddNihaiOzet(this Package package, Stream nihaiOzetStream)
        {
            var partNihaiOzet = package.CreatePart(Constants.URI_NIHAIOZET, Constants.MIME_XML, CompressionOption.Maximum);
            package.CreateRelationship(partNihaiOzet.Uri, TargetMode.Internal, Constants.RELATION_TYPE_NIHAIOZET, Constants.ID_NIHAIOZET);
            nihaiOzetStream.CopyTo(partNihaiOzet.GetStream());
        }

        public static bool PaketOzetiExists(this Package package) => package.PartExists(Constants.URI_PAKETOZETI);

        public static Stream GetPaketOzetiStream(this Package package) => package.PaketOzetiExists() ? package.GetPart(Constants.URI_PAKETOZETI).GetStream() : null;

        public static void DeletePaketOzeti(this Package package)
        {
            if (package.PaketOzetiExists())
            {
                package.DeletePart(Constants.URI_PAKETOZETI);
                package.DeleteRelationship(Constants.ID_PaketOzeti);
            }
        }

        public static void GeneratePaketOzeti(this Package package, PaketOzeti PaketOzeti, PaketVersiyonTuru paketVersiyon)
        {
            if (PaketOzeti != null && PaketOzeti.Referanslar != null && PaketOzeti.Referanslar.Count > 0)
            {
                object PaketOzetiObject = null;
                Type PaketOzetiType = null;

                switch (paketVersiyon)
                {
                    case PaketVersiyonTuru.Versiyon1X:
                        {
                            PaketOzetiObject = PaketOzeti.ToV1XCT_PaketOzeti();
                            PaketOzetiType = typeof(Api.V1X.CT_PaketOzeti);
                            break;
                        }
                    case PaketVersiyonTuru.Versiyon2X:
                        {
                            PaketOzetiObject = PaketOzeti.ToV2XCT_PaketOzeti();
                            PaketOzetiType = typeof(Api.V2X.CT_PaketOzeti);
                            break;
                        }
                }
                var partPaketOzeti = package.CreatePart(Constants.URI_PAKETOZETI, Constants.MIME_XML, CompressionOption.Maximum);
                package.CreateRelationship(partPaketOzeti.Uri, TargetMode.Internal, Constants.RELATION_TYPE_PAKETOZETI, Constants.ID_PaketOzeti);
                var xmlSerializer = new XmlSerializer(PaketOzetiType);
                XmlTextWriter xmlTextWriter = new XmlTextWriter(partPaketOzeti.GetStream(), Encoding.UTF8)
                {
                    Formatting = Formatting.Indented
                };
                xmlSerializer.Serialize(xmlTextWriter, PaketOzetiObject);
            }
        }

        public static void AddPaketOzeti(this Package package, Stream paketOzetiStream)
        {
            var partPaketOzeti = package.CreatePart(Constants.URI_PAKETOZETI, Constants.MIME_XML, CompressionOption.Maximum);
            package.CreateRelationship(partPaketOzeti.Uri, TargetMode.Internal, Constants.RELATION_TYPE_PAKETOZETI, Constants.ID_PaketOzeti);
            paketOzetiStream.CopyTo(partPaketOzeti.GetStream());
        }

        public static bool ParafOzetiExists(this Package package) => package.PartExists(Constants.URI_PARAFOZETI);

        public static Stream GetParafOzetiStream(this Package package) => package.PaketOzetiExists() ? package.GetPart(Constants.URI_PARAFOZETI).GetStream() : null;

        public static void DeleteParafOzeti(this Package package)
        {
            if (package.ParafOzetiExists())
            {
                package.DeletePart(Constants.URI_PARAFOZETI);
                package.DeleteRelationship(Constants.ID_PARAFOZETI);
            }
        }

        public static void GenerateParafOzeti(this Package package, ParafOzeti parafOzeti)
        {
            if (parafOzeti != null && parafOzeti.Referanslar != null && parafOzeti.Referanslar.Count > 0)
            {
                var partParafOzeti = package.CreatePart(Constants.URI_PARAFOZETI, Constants.MIME_XML, CompressionOption.Maximum);
                package.CreateRelationship(partParafOzeti.Uri, TargetMode.Internal, Constants.RELATION_TYPE_PARAFOZETI, Constants.ID_PARAFOZETI);
                var xmlSerializer = new XmlSerializer(typeof(Api.V2X.CT_ParafOzeti));
                XmlTextWriter xmlTextWriter = new XmlTextWriter(partParafOzeti.GetStream(), Encoding.UTF8)
                {
                    Formatting = Formatting.Indented
                };
                xmlSerializer.Serialize(xmlTextWriter, parafOzeti.ToV2XCT_ParafOzeti());
            }
        }

        public static bool UstveriExists(this Package package) => package.PartExists(Constants.URI_USTVERI);

        public static Stream GetUstveriStream(this Package package) => package.UstveriExists() ? package.GetPart(Constants.URI_USTVERI).GetStream() : null;

        public static void DeleteUstveri(this Package package)
        {
            if (package.UstveriExists())
            {
                package.DeletePart(Constants.URI_USTVERI);
                package.DeleteRelationship(Constants.ID_USTVERI);
            }
        }

        public static void GenerateUstveri(this Package package, Ustveri ustveri, PaketVersiyonTuru paketVersiyon)
        {
            if (ustveri != null)
            {
                string nsString = null;
                object ustveriObject = null;
                Type ustveriType = null;

                switch (paketVersiyon)
                {
                    case PaketVersiyonTuru.Versiyon1X:
                        {
                            ustveriObject = ustveri.V1XCT_Ustveri();
                            nsString = "urn:dpt:eyazisma:schema:xsd:Tipler-1";
                            ustveriType = typeof(Api.V1X.CT_Ustveri);
                            break;
                        }
                    case PaketVersiyonTuru.Versiyon2X:
                        {
                            ustveriObject = ustveri.V2XCT_Ustveri();
                            nsString = "urn:dpt:eyazisma:schema:xsd:Tipler-2";
                            ustveriType = typeof(Api.V2X.CT_Ustveri);
                            break;
                        }
                }

                var partUstveri = package.CreatePart(Constants.URI_USTVERI, Constants.MIME_XML, CompressionOption.Normal);
                package.CreateRelationship(partUstveri.Uri, TargetMode.Internal, Constants.RELATION_TYPE_USTVERI, Constants.ID_USTVERI);

                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("tipler", nsString);
                var x = new XmlSerializer(ustveriType);
                XmlTextWriter xmlTextWriter = new XmlTextWriter(partUstveri.GetStream(), Encoding.UTF8)
                {
                    Formatting = Formatting.Indented
                };
                x.Serialize(xmlTextWriter, ustveriObject, ns);
            }
        }

        public static bool CoreRelationExists(this Package package) => package.GetRelationshipsByType(Constants.RELATION_TYPE_CORE) == null || package.GetRelationshipsByType(Constants.RELATION_TYPE_CORE).Count() == 0;

        public static void GenerateCore(this Package package, Ustveri ustveri, PaketVersiyonTuru paketVersiyon)
        {
            package.PackageProperties.Identifier = ustveri.BelgeId.ToString().ToUpperInvariant();
            if (!package.PackageProperties.Created.HasValue)
                package.PackageProperties.Created = DateTime.Now;
            package.PackageProperties.Creator = ustveri.Olusturan.GenerateOlusturanAd();
            package.PackageProperties.Subject = ustveri.Konu.Deger;
            package.PackageProperties.Category = Constants.PAKET_KATEGORI;
            package.PackageProperties.ContentType = Constants.PAKET_MIMETURU;
            package.PackageProperties.Version = paketVersiyon == PaketVersiyonTuru.Versiyon1X ? Constants.PAKET_VERSIYON_V1X : Constants.PAKET_VERSIYON_V2X;
            package.PackageProperties.Revision = string.Format(Constants.PAKET_REVIZYON, System.Reflection.Assembly.GetAssembly(typeof(Paket)).GetName().Version);
            package.Flush();
        }

        public static void AddSifreliIcerik(this Package package, Stream sifreliIcerikStream, Guid paketId)
        {
            var partSifreliIcerikUri = new Uri(string.Format(Constants.URI_FORMAT_SIFRELIICERIK_V1X_STRING, Uri.EscapeDataString(paketId.ToString().ToUpperInvariant())), UriKind.Relative);
            var partSifreliIcerik = package.CreatePart(partSifreliIcerikUri, "application/pkcs7-mime", CompressionOption.Maximum);
            sifreliIcerikStream.CopyTo(partSifreliIcerik.GetStream());

            package.CreateRelationship(partSifreliIcerik.Uri, TargetMode.Internal, Constants.RELATION_TYPE_SIFRELIICERIK, Constants.ID_SIFRELIICERIK);
        }

        public static bool SifreliIcerikExists(this Package package) => !(package.GetRelationshipsByType(Constants.RELATION_TYPE_SIFRELIICERIK) == null || package.GetRelationshipsByType(Constants.RELATION_TYPE_SIFRELIICERIK).Count() == 0);

        public static Stream GetSifreliIcerikStream(this Package package) => package.SifreliIcerikExists() ? package.GetPart(package.GetRelationshipsByType(Constants.RELATION_TYPE_SIFRELIICERIK).First().TargetUri).GetStream() : null;

        public static string GetSifreliIcerikDosyasiAdi(this Package package) => package.SifreliIcerikExists() ? package.GetRelationshipsByType(Constants.RELATION_TYPE_SIFRELIICERIK).First().TargetUri.OriginalString.Split('/').Last() : null;

        public static void DeleteSifreliIcerikBilgisi(this Package package)
        {
            if (package.SifreliIcerikBilgisiExists())
            {
                package.DeletePart(Constants.URI_SIFRELIICERIKBILGISI);
                package.DeleteRelationship(Constants.ID_SIFRELIICERIKBILGISI);
            }
        }

        public static void GenerateSifreliIcerikBilgisi(this Package package, SifreliIcerikBilgisi sifreliIcerikBilgisi, PaketVersiyonTuru paketVersiyon)
        {
            if (sifreliIcerikBilgisi != null)
            {
                object sifreliIcerikBilgisiObject = null;
                Type sifreliIcerikBilgisiType = null;

                switch (paketVersiyon)
                {
                    case PaketVersiyonTuru.Versiyon1X:
                        {
                            sifreliIcerikBilgisiObject = sifreliIcerikBilgisi.ToV1XCT_SifreliIcerikBilgisi();
                            sifreliIcerikBilgisiType = typeof(Api.V1X.CT_SifreliIcerikBilgisi);
                            break;
                        }
                    case PaketVersiyonTuru.Versiyon2X:
                        {
                            sifreliIcerikBilgisiObject = sifreliIcerikBilgisi.ToV2XCT_SifreliIcerikBilgisi();
                            sifreliIcerikBilgisiType = typeof(Api.V2X.CT_SifreliIcerikBilgisi);
                            break;
                        }
                }
                var partSifreliIcerikBilgisi = package.CreatePart(Constants.URI_SIFRELIICERIKBILGISI, Constants.MIME_XML, CompressionOption.Maximum);
                package.CreateRelationship(partSifreliIcerikBilgisi.Uri, TargetMode.Internal, Constants.RELATION_TYPE_SIFRELIICERIKBILGISI, Constants.ID_SIFRELIICERIKBILGISI);
                var xmlSerializer = new XmlSerializer(sifreliIcerikBilgisiType);
                XmlTextWriter xmlTextWriter = new XmlTextWriter(partSifreliIcerikBilgisi.GetStream(), Encoding.UTF8)
                {
                    Formatting = Formatting.Indented
                };
                xmlSerializer.Serialize(xmlTextWriter, sifreliIcerikBilgisiObject);
            }
        }

        public static bool SifreliIcerikBilgisiExists(this Package package) => package.PartExists(Constants.URI_SIFRELIICERIKBILGISI);

        public static void SetPaketGuncellemeTarihi(this Package package, DateTime? guncellemeTarihi) => package.PackageProperties.Modified = guncellemeTarihi;

        public static void SetPaketSonGuncelleyen(this Package package, string sonGuncelleyen) => package.PackageProperties.LastModifiedBy = sonGuncelleyen;

        public static void SetPaketDurumu(this Package package, string durum) => package.PackageProperties.ContentStatus = durum;

        public static void SetPaketAciklamasi(this Package package, string aciklama) => package.PackageProperties.Description = aciklama;

        public static void SetPaketSonYazdirilmaTarihi(this Package package, DateTime? sonYazdirilmaTarihi) => package.PackageProperties.LastPrinted = sonYazdirilmaTarihi;

        public static void SetPaketAnahtarKelimeleri(this Package package, string anahtarKelimeler) => package.PackageProperties.Keywords = anahtarKelimeler;

        public static void SetPaketOlusturulmaTarihi(this Package package, DateTime? olusturulmaTarihi) => package.PackageProperties.Created = olusturulmaTarihi;

        public static void SetPaketBasligi(this Package package, string baslik) => package.PackageProperties.Title = baslik;

        public static void SetPaketDili(this Package package, string dil) => package.PackageProperties.Language = dil;

    }
}
