using System;
using System.IO;
using eyazisma.online.api.Classes;
using eyazisma.online.api.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eyazisma.online.api.framework.test
{
    [TestClass]
    public class PaketGuncelleTests
    {
        [TestMethod]
        public void Paket_Guncelle_ImzaEkle_Versiyon1X()
        {
            byte[] paket = null;
            using (var ms = new MemoryStream())
            {
                var belgeId = Guid.NewGuid();
                var dagitim1 = Dagitim.Kilavuz
                    .OgeAta(KurumKurulus.Kilavuz
                        .Versiyon1X()
                        .KKKAta("28119270")
                        .AdIle(IsimTip.Kilavuz.DegerAta("BİLGİ İŞLEM BAŞKANLIĞI").Olustur())
                        .IletisimBilgisiIle(IletisimBilgisi.Kilavuz
                            .Versiyon1X()
                            .TelefonIle("+90 312 420 75 59")
                            .FaksIle("+90 312 420 75 59")
                            .EPostaIle("yavuz.beyribey@tbmm.gov.tr")
                            .WebAdresiIle("https://www.tbmm.gov.tr")
                            .AdresIle(MetinTip.Kilavuz.DegerAta("TBMM BİLGİ İŞLEM BAŞKANLIĞI BAKANLIKLAR").Olustur())
                            .IlceIle(IsimTip.Kilavuz.DegerAta("ÇANKAYA").Olustur())
                            .IlIle(IsimTip.Kilavuz.DegerAta("ANKARA").Olustur())
                            .UlkeIle(IsimTip.Kilavuz.DegerAta("TÜRKİYE").Olustur())
                            .Olustur())
                        .Olustur())
                    .IvedilikTuruAta(IvedilikTuru.NRM)
                    .DagitimTuruAta(DagitimTuru.BLG)
                    .Olustur();

                var olusturan = Olusturan.Kilavuz
                    .OgeAta(KurumKurulus.Kilavuz
                        .Versiyon1X()
                        .KKKAta("61648174")
                        .AdIle(IsimTip.Kilavuz.DegerAta("BİLGİ İŞLEM DAİRESİ BAŞKANLIĞI").Olustur())
                        .IletisimBilgisiIle(IletisimBilgisi.Kilavuz
                            .Versiyon1X()
                            .TelefonIle("+90 422 46 00")
                            .FaksIle("+90 312 417 49 66")
                            .EPostaIle("bilgiislemdairesibaskanligi@icisleri.gov.tr")
                            .WebAdresiIle("https://www.icisleribilgiislem.gov.tr")
                            .AdresIle(MetinTip.Kilavuz.DegerAta("İNÖNÜ BULVARI NO:4 BAKANLIKLAR").Olustur())
                            .IlceIle(IsimTip.Kilavuz.DegerAta("ÇANKAYA").Olustur())
                            .IlIle(IsimTip.Kilavuz.DegerAta("ANKARA").Olustur())
                            .UlkeIle(IsimTip.Kilavuz.DegerAta("TÜRKİYE").Olustur())
                            .Olustur())
                        .Olustur())
                    .Olustur();

                Paket.Olustur(ms)
                    .Versiyon1X()
                    .UstYaziAta(UstYazi.Kilavuz
                        .DosyaAta(TestComponents.USTYAZI_FILE_PATH)
                        .DosyaAdiAta(TestComponents.USTYAZI_FILE_NAME)
                        .MimeTuruAta(TestComponents.MIME_TURU_PDF)
                        .Olustur())
                    .UstveriAta(Ustveri.Kilavuz
                        .Versiyon1X()
                        .BelgeIdAta(belgeId)
                        .KonuAta(MetinTip.Kilavuz.DegerAta("Olustur Versiyon 1X").Olustur())
                        .TarihAta(DateTime.UtcNow)
                        .BelgeNoAta("E-24312041-702.03-1")
                        .GuvenlikKoduAta(GuvenlikKoduTuru.YOK)
                        .DagitimAta(dagitim1)
                        .DilIle("tur")
                        .OlusturanAta(olusturan)
                        .DosyaAdiAta(belgeId + ".eyp")
                        .Olustur())
                    .BelgeHedefAta(BelgeHedef.Kilavuz
                        .HedefEkle(Hedef.Kilavuz.OgeAta((KurumKurulus) dagitim1.Oge).Olustur())
                        .Olustur())
                    .BelgeImzaIle(BelgeImza.Kilavuz
                        .ImzaEkle(Imza.Kilavuz
                            .ImzalayanAta(GercekSahis.Kilavuz
                                .KisiAta(Kisi.Kilavuz
                                    .IlkAdiAta(IsimTip.Kilavuz.DegerAta("Ali İhsan").Olustur())
                                    .SoyadiAta(IsimTip.Kilavuz.DegerAta("TOKDEMİR").Olustur())
                                    .Olustur())
                                .Olustur())
                            .TarihIle(DateTime.UtcNow)
                            .Olustur())
                        .Olustur())
                    .BilesenleriOlustur()
                    .Dogrula((kritikHataVarMi, sonuclar) =>
                    {
                        if (kritikHataVarMi)
                            Assert.Fail("Paket oluşturma esnasında hata oluşmuştur.");
                    })
                    .Kapat();
                paket = ms.ToArray();
            }

            using (var ms2 = new MemoryStream())
            {
                ms2.Write(paket, 0, paket.Length);
                Paket.Guncelle(ms2)
                    .Versiyon1XIse(paketGuncellenecek =>
                    {
                        paketGuncellenecek.ImzaEkle(new byte[] {1, 2, 3, 4})
                            .Dogrula((hataVarMi, hatalar) => { Assert.IsFalse(hataVarMi); })
                            .Kapat();
                    })
                    .Kapat();
            }
        }

        [TestMethod]
        public void Paket_Guncelle_MuhurEkle_Versiyon1X()
        {
            byte[] paket = null;
            using (var ms = new MemoryStream())
            {
                var belgeId = Guid.NewGuid();
                var dagitim1 = Dagitim.Kilavuz
                    .OgeAta(KurumKurulus.Kilavuz
                        .Versiyon1X()
                        .KKKAta("28119270")
                        .AdIle(IsimTip.Kilavuz.DegerAta("BİLGİ İŞLEM BAŞKANLIĞI").Olustur())
                        .IletisimBilgisiIle(IletisimBilgisi.Kilavuz
                            .Versiyon1X()
                            .TelefonIle("+90 312 420 75 59")
                            .FaksIle("+90 312 420 75 59")
                            .EPostaIle("yavuz.beyribey@tbmm.gov.tr")
                            .WebAdresiIle("https://www.tbmm.gov.tr")
                            .AdresIle(MetinTip.Kilavuz.DegerAta("TBMM BİLGİ İŞLEM BAŞKANLIĞI BAKANLIKLAR").Olustur())
                            .IlceIle(IsimTip.Kilavuz.DegerAta("ÇANKAYA").Olustur())
                            .IlIle(IsimTip.Kilavuz.DegerAta("ANKARA").Olustur())
                            .UlkeIle(IsimTip.Kilavuz.DegerAta("TÜRKİYE").Olustur())
                            .Olustur())
                        .Olustur())
                    .IvedilikTuruAta(IvedilikTuru.NRM)
                    .DagitimTuruAta(DagitimTuru.BLG)
                    .Olustur();

                var olusturan = Olusturan.Kilavuz
                    .OgeAta(KurumKurulus.Kilavuz
                        .Versiyon1X()
                        .KKKAta("61648174")
                        .AdIle(IsimTip.Kilavuz.DegerAta("BİLGİ İŞLEM DAİRESİ BAŞKANLIĞI").Olustur())
                        .IletisimBilgisiIle(IletisimBilgisi.Kilavuz
                            .Versiyon1X()
                            .TelefonIle("+90 422 46 00")
                            .FaksIle("+90 312 417 49 66")
                            .EPostaIle("bilgiislemdairesibaskanligi@icisleri.gov.tr")
                            .WebAdresiIle("https://www.icisleribilgiislem.gov.tr")
                            .AdresIle(MetinTip.Kilavuz.DegerAta("İNÖNÜ BULVARI NO:4 BAKANLIKLAR").Olustur())
                            .IlceIle(IsimTip.Kilavuz.DegerAta("ÇANKAYA").Olustur())
                            .IlIle(IsimTip.Kilavuz.DegerAta("ANKARA").Olustur())
                            .UlkeIle(IsimTip.Kilavuz.DegerAta("TÜRKİYE").Olustur())
                            .Olustur())
                        .Olustur())
                    .Olustur();

                Paket.Olustur(ms)
                    .Versiyon1X()
                    .UstYaziAta(UstYazi.Kilavuz
                        .DosyaAta(TestComponents.USTYAZI_FILE_PATH)
                        .DosyaAdiAta(TestComponents.USTYAZI_FILE_NAME)
                        .MimeTuruAta(TestComponents.MIME_TURU_PDF)
                        .Olustur())
                    .UstveriAta(Ustveri.Kilavuz
                        .Versiyon1X()
                        .BelgeIdAta(belgeId)
                        .KonuAta(MetinTip.Kilavuz.DegerAta("Olustur Versiyon 1X").Olustur())
                        .TarihAta(DateTime.UtcNow)
                        .BelgeNoAta("E-24312041-702.03-1")
                        .GuvenlikKoduAta(GuvenlikKoduTuru.YOK)
                        .DagitimAta(dagitim1)
                        .DilIle("tur")
                        .OlusturanAta(olusturan)
                        .DosyaAdiAta(belgeId + ".eyp")
                        .Olustur())
                    .BelgeHedefAta(BelgeHedef.Kilavuz
                        .HedefEkle(Hedef.Kilavuz.OgeAta((KurumKurulus) dagitim1.Oge).Olustur())
                        .Olustur())
                    .BelgeImzaIle(BelgeImza.Kilavuz
                        .ImzaEkle(Imza.Kilavuz
                            .ImzalayanAta(GercekSahis.Kilavuz
                                .KisiAta(Kisi.Kilavuz
                                    .IlkAdiAta(IsimTip.Kilavuz.DegerAta("Ali İhsan").Olustur())
                                    .SoyadiAta(IsimTip.Kilavuz.DegerAta("TOKDEMİR").Olustur())
                                    .Olustur())
                                .Olustur())
                            .TarihIle(DateTime.UtcNow)
                            .Olustur())
                        .Olustur())
                    .BilesenleriOlustur()
                    .ImzaEkle(paketOzetiStream => { return new byte[] {1, 2, 3, 4}; })
                    .Dogrula((kritikHataVarMi, sonuclar) =>
                    {
                        if (kritikHataVarMi)
                            Assert.Fail("Paket oluşturma esnasında hata oluşmuştur.");
                    })
                    .Kapat();
                paket = ms.ToArray();
            }

            using (var ms2 = new MemoryStream())
            {
                ms2.Write(paket, 0, paket.Length);
                Paket.Guncelle(ms2)
                    .Versiyon1XIse(paketGuncellenecek =>
                    {
                        paketGuncellenecek.MuhurEkle(new byte[] {1, 2, 3, 4})
                            .Dogrula((hataVarMi, hatalar) => { Assert.IsFalse(hataVarMi); })
                            .Kapat();
                    })
                    .Kapat();
            }
        }

        [TestMethod]
        public void PaketV1X_Guncelle_ImzaEkle()
        {
            byte[] paket = null;
            using (var ms = new MemoryStream())
            {
                var belgeId = Guid.NewGuid();
                var dagitim1 = Dagitim.Kilavuz
                    .OgeAta(KurumKurulus.Kilavuz
                        .Versiyon1X()
                        .KKKAta("28119270")
                        .AdIle(IsimTip.Kilavuz.DegerAta("BİLGİ İŞLEM BAŞKANLIĞI").Olustur())
                        .IletisimBilgisiIle(IletisimBilgisi.Kilavuz
                            .Versiyon1X()
                            .TelefonIle("+90 312 420 75 59")
                            .FaksIle("+90 312 420 75 59")
                            .EPostaIle("yavuz.beyribey@tbmm.gov.tr")
                            .WebAdresiIle("https://www.tbmm.gov.tr")
                            .AdresIle(MetinTip.Kilavuz.DegerAta("TBMM BİLGİ İŞLEM BAŞKANLIĞI BAKANLIKLAR").Olustur())
                            .IlceIle(IsimTip.Kilavuz.DegerAta("ÇANKAYA").Olustur())
                            .IlIle(IsimTip.Kilavuz.DegerAta("ANKARA").Olustur())
                            .UlkeIle(IsimTip.Kilavuz.DegerAta("TÜRKİYE").Olustur())
                            .Olustur())
                        .Olustur())
                    .IvedilikTuruAta(IvedilikTuru.NRM)
                    .DagitimTuruAta(DagitimTuru.BLG)
                    .Olustur();

                var olusturan = Olusturan.Kilavuz
                    .OgeAta(KurumKurulus.Kilavuz
                        .Versiyon1X()
                        .KKKAta("61648174")
                        .AdIle(IsimTip.Kilavuz.DegerAta("BİLGİ İŞLEM DAİRESİ BAŞKANLIĞI").Olustur())
                        .IletisimBilgisiIle(IletisimBilgisi.Kilavuz
                            .Versiyon1X()
                            .TelefonIle("+90 422 46 00")
                            .FaksIle("+90 312 417 49 66")
                            .EPostaIle("bilgiislemdairesibaskanligi@icisleri.gov.tr")
                            .WebAdresiIle("https://www.icisleribilgiislem.gov.tr")
                            .AdresIle(MetinTip.Kilavuz.DegerAta("İNÖNÜ BULVARI NO:4 BAKANLIKLAR").Olustur())
                            .IlceIle(IsimTip.Kilavuz.DegerAta("ÇANKAYA").Olustur())
                            .IlIle(IsimTip.Kilavuz.DegerAta("ANKARA").Olustur())
                            .UlkeIle(IsimTip.Kilavuz.DegerAta("TÜRKİYE").Olustur())
                            .Olustur())
                        .Olustur())
                    .Olustur();

                PaketV1X.Olustur(ms)
                    .UstYaziAta(UstYazi.Kilavuz
                        .DosyaAta(TestComponents.USTYAZI_FILE_PATH)
                        .DosyaAdiAta(TestComponents.USTYAZI_FILE_NAME)
                        .MimeTuruAta(TestComponents.MIME_TURU_PDF)
                        .Olustur())
                    .UstveriAta(Ustveri.Kilavuz
                        .Versiyon1X()
                        .BelgeIdAta(belgeId)
                        .KonuAta(MetinTip.Kilavuz.DegerAta("Olustur Versiyon 1X").Olustur())
                        .TarihAta(DateTime.UtcNow)
                        .BelgeNoAta("E-24312041-702.03-1")
                        .GuvenlikKoduAta(GuvenlikKoduTuru.YOK)
                        .DagitimAta(dagitim1)
                        .DilIle("tur")
                        .OlusturanAta(olusturan)
                        .DosyaAdiAta(belgeId + ".eyp")
                        .Olustur())
                    .BelgeHedefAta(BelgeHedef.Kilavuz
                        .HedefEkle(Hedef.Kilavuz.OgeAta((KurumKurulus) dagitim1.Oge).Olustur())
                        .Olustur())
                    .BelgeImzaIle(BelgeImza.Kilavuz
                        .ImzaEkle(Imza.Kilavuz
                            .ImzalayanAta(GercekSahis.Kilavuz
                                .KisiAta(Kisi.Kilavuz
                                    .IlkAdiAta(IsimTip.Kilavuz.DegerAta("Ali İhsan").Olustur())
                                    .SoyadiAta(IsimTip.Kilavuz.DegerAta("TOKDEMİR").Olustur())
                                    .Olustur())
                                .Olustur())
                            .TarihIle(DateTime.UtcNow)
                            .Olustur())
                        .Olustur())
                    .BilesenleriOlustur()
                    .Dogrula((kritikHataVarMi, sonuclar) =>
                    {
                        if (kritikHataVarMi)
                            Assert.Fail("Paket oluşturma esnasında hata oluşmuştur.");
                    })
                    .Kapat();
                paket = ms.ToArray();
            }

            using (var ms2 = new MemoryStream())
            {
                ms2.Write(paket, 0, paket.Length);
                PaketV1X.Guncelle(ms2)
                    .ImzaEkle(new byte[] {1, 2, 3, 4})
                    .Dogrula((hataVarMi, hatalar) => { Assert.IsFalse(hataVarMi); })
                    .Kapat();
            }
        }

        [TestMethod]
        public void PaketV1X_Guncelle_MuhurEkle()
        {
            byte[] paket = null;
            using (var ms = new MemoryStream())
            {
                var belgeId = Guid.NewGuid();
                var dagitim1 = Dagitim.Kilavuz
                    .OgeAta(KurumKurulus.Kilavuz
                        .Versiyon1X()
                        .KKKAta("28119270")
                        .AdIle(IsimTip.Kilavuz.DegerAta("BİLGİ İŞLEM BAŞKANLIĞI").Olustur())
                        .IletisimBilgisiIle(IletisimBilgisi.Kilavuz
                            .Versiyon1X()
                            .TelefonIle("+90 312 420 75 59")
                            .FaksIle("+90 312 420 75 59")
                            .EPostaIle("yavuz.beyribey@tbmm.gov.tr")
                            .WebAdresiIle("https://www.tbmm.gov.tr")
                            .AdresIle(MetinTip.Kilavuz.DegerAta("TBMM BİLGİ İŞLEM BAŞKANLIĞI BAKANLIKLAR").Olustur())
                            .IlceIle(IsimTip.Kilavuz.DegerAta("ÇANKAYA").Olustur())
                            .IlIle(IsimTip.Kilavuz.DegerAta("ANKARA").Olustur())
                            .UlkeIle(IsimTip.Kilavuz.DegerAta("TÜRKİYE").Olustur())
                            .Olustur())
                        .Olustur())
                    .IvedilikTuruAta(IvedilikTuru.NRM)
                    .DagitimTuruAta(DagitimTuru.BLG)
                    .Olustur();

                var olusturan = Olusturan.Kilavuz
                    .OgeAta(KurumKurulus.Kilavuz
                        .Versiyon1X()
                        .KKKAta("61648174")
                        .AdIle(IsimTip.Kilavuz.DegerAta("BİLGİ İŞLEM DAİRESİ BAŞKANLIĞI").Olustur())
                        .IletisimBilgisiIle(IletisimBilgisi.Kilavuz
                            .Versiyon1X()
                            .TelefonIle("+90 422 46 00")
                            .FaksIle("+90 312 417 49 66")
                            .EPostaIle("bilgiislemdairesibaskanligi@icisleri.gov.tr")
                            .WebAdresiIle("https://www.icisleribilgiislem.gov.tr")
                            .AdresIle(MetinTip.Kilavuz.DegerAta("İNÖNÜ BULVARI NO:4 BAKANLIKLAR").Olustur())
                            .IlceIle(IsimTip.Kilavuz.DegerAta("ÇANKAYA").Olustur())
                            .IlIle(IsimTip.Kilavuz.DegerAta("ANKARA").Olustur())
                            .UlkeIle(IsimTip.Kilavuz.DegerAta("TÜRKİYE").Olustur())
                            .Olustur())
                        .Olustur())
                    .Olustur();

                PaketV1X.Olustur(ms)
                    .UstYaziAta(UstYazi.Kilavuz
                        .DosyaAta(TestComponents.USTYAZI_FILE_PATH)
                        .DosyaAdiAta(TestComponents.USTYAZI_FILE_NAME)
                        .MimeTuruAta(TestComponents.MIME_TURU_PDF)
                        .Olustur())
                    .UstveriAta(Ustveri.Kilavuz
                        .Versiyon1X()
                        .BelgeIdAta(belgeId)
                        .KonuAta(MetinTip.Kilavuz.DegerAta("Olustur Versiyon 1X").Olustur())
                        .TarihAta(DateTime.UtcNow)
                        .BelgeNoAta("E-24312041-702.03-1")
                        .GuvenlikKoduAta(GuvenlikKoduTuru.YOK)
                        .DagitimAta(dagitim1)
                        .DilIle("tur")
                        .OlusturanAta(olusturan)
                        .DosyaAdiAta(belgeId + ".eyp")
                        .Olustur())
                    .BelgeHedefAta(BelgeHedef.Kilavuz
                        .HedefEkle(Hedef.Kilavuz.OgeAta((KurumKurulus) dagitim1.Oge).Olustur())
                        .Olustur())
                    .BelgeImzaIle(BelgeImza.Kilavuz
                        .ImzaEkle(Imza.Kilavuz
                            .ImzalayanAta(GercekSahis.Kilavuz
                                .KisiAta(Kisi.Kilavuz
                                    .IlkAdiAta(IsimTip.Kilavuz.DegerAta("Ali İhsan").Olustur())
                                    .SoyadiAta(IsimTip.Kilavuz.DegerAta("TOKDEMİR").Olustur())
                                    .Olustur())
                                .Olustur())
                            .TarihIle(DateTime.UtcNow)
                            .Olustur())
                        .Olustur())
                    .BilesenleriOlustur()
                    .ImzaEkle(paketOzetiStream => { return new byte[] {1, 2, 3, 4}; })
                    .Dogrula((kritikHataVarMi, sonuclar) =>
                    {
                        if (kritikHataVarMi)
                            Assert.Fail("Paket oluşturma esnasında hata oluşmuştur.");
                    })
                    .Kapat();
                paket = ms.ToArray();
            }

            using (var ms2 = new MemoryStream())
            {
                ms2.Write(paket, 0, paket.Length);
                PaketV1X.Guncelle(ms2)
                    .MuhurEkle(new byte[] {1, 2, 3, 4})
                    .Dogrula((hataVarMi, hatalar) => { Assert.IsFalse(hataVarMi); })
                    .Kapat();
            }
        }

        [TestMethod]
        public void Paket_Guncelle_ParafImzaEkle_Versiyon2X()
        {
            byte[] paket = null;
            using (var ms = new MemoryStream())
            {
                var belgeId = Guid.NewGuid();
                var dagitim1 = Dagitim.Kilavuz
                    .OgeAta(KurumKurulus.Kilavuz
                        .Versiyon2X()
                        .KKKAta("28119270")
                        .AdIle(IsimTip.Kilavuz.DegerAta("BİLGİ İŞLEM BAŞKANLIĞI").Olustur())
                        .IletisimBilgisiIle(IletisimBilgisi.Kilavuz
                            .Versiyon2X()
                            .TelefonIle("+90 312 420 75 59")
                            .FaksIle("+90 312 420 75 59")
                            .EPostaIle("yavuz.beyribey@tbmm.gov.tr")
                            .WebAdresiIle("https://www.tbmm.gov.tr")
                            .AdresIle(MetinTip.Kilavuz.DegerAta("TBMM BİLGİ İŞLEM BAŞKANLIĞI BAKANLIKLAR").Olustur())
                            .IlceIle(IsimTip.Kilavuz.DegerAta("ÇANKAYA").Olustur())
                            .IlIle(IsimTip.Kilavuz.DegerAta("ANKARA").Olustur())
                            .UlkeIle(IsimTip.Kilavuz.DegerAta("TÜRKİYE").Olustur())
                            .Olustur())
                        .Olustur())
                    .IvedilikTuruAta(IvedilikTuru.NRM)
                    .DagitimTuruAta(DagitimTuru.BLG)
                    .Olustur();

                var olusturan = Olusturan.Kilavuz
                    .OgeAta(KurumKurulus.Kilavuz
                        .Versiyon2X()
                        .KKKAta("61648174")
                        .AdIle(IsimTip.Kilavuz.DegerAta("BİLGİ İŞLEM DAİRESİ BAŞKANLIĞI").Olustur())
                        .IletisimBilgisiIle(IletisimBilgisi.Kilavuz
                            .Versiyon2X()
                            .TelefonIle("+90 422 46 00")
                            .FaksIle("+90 312 417 49 66")
                            .EPostaIle("bilgiislemdairesibaskanligi@icisleri.gov.tr")
                            .WebAdresiIle("https://www.icisleribilgiislem.gov.tr")
                            .AdresIle(MetinTip.Kilavuz.DegerAta("İNÖNÜ BULVARI NO:4 BAKANLIKLAR").Olustur())
                            .IlceIle(IsimTip.Kilavuz.DegerAta("ÇANKAYA").Olustur())
                            .IlIle(IsimTip.Kilavuz.DegerAta("ANKARA").Olustur())
                            .UlkeIle(IsimTip.Kilavuz.DegerAta("TÜRKİYE").Olustur())
                            .Olustur())
                        .Olustur())
                    .Olustur();

                Paket.Olustur(ms)
                    .Versiyon2X()
                    .UstYaziAta(UstYazi.Kilavuz
                        .DosyaAta(TestComponents.USTYAZI_FILE_PATH)
                        .DosyaAdiAta(TestComponents.USTYAZI_FILE_NAME)
                        .MimeTuruAta(TestComponents.MIME_TURU_PDF)
                        .Olustur())
                    .UstveriAta(Ustveri.Kilavuz
                        .Versiyon2X()
                        .BelgeIdAta(belgeId)
                        .KonuAta(MetinTip.Kilavuz.DegerAta("Olustur Versiyon 2X").Olustur())
                        .GuvenlikKoduAta(GuvenlikKoduTuru.YOK)
                        .DagitimAta(dagitim1)
                        .DilIle("tur")
                        .OlusturanAta(olusturan)
                        .DosyaAdiAta(belgeId + ".eyp")
                        .DogrulamaBilgisiAta(DogrulamaBilgisi.Kilavuz.AdresAta("").Olustur())
                        .Olustur())
                    .BilesenleriOlustur(true)
                    .Dogrula((kritikHataVarMi, sonuclar) =>
                    {
                        if (kritikHataVarMi)
                            Assert.Fail("Paket oluşturma esnasında hata oluşmuştur.");
                    })
                    .Kapat();
                paket = ms.ToArray();
            }

            using (var ms2 = new MemoryStream())
            {
                ms2.Write(paket, 0, paket.Length);
                Paket.Guncelle(ms2)
                    .Versiyon2XIse(paketGuncellenecek =>
                    {
                        paketGuncellenecek.ParafImzaEkle(new byte[] {1, 2, 3, 4})
                            .Dogrula((hataVarMi, hatalar) => { Assert.IsFalse(hataVarMi); })
                            .Kapat();
                    })
                    .Kapat();
            }
        }

        [TestMethod]
        public void Paket_Guncelle_ImzaEkle_Versiyon2X()
        {
            byte[] paket = null;
            using (var ms = new MemoryStream())
            {
                var belgeId = Guid.NewGuid();
                var dagitim1 = Dagitim.Kilavuz
                    .OgeAta(KurumKurulus.Kilavuz
                        .Versiyon2X()
                        .KKKAta("28119270")
                        .AdIle(IsimTip.Kilavuz.DegerAta("BİLGİ İŞLEM BAŞKANLIĞI").Olustur())
                        .IletisimBilgisiIle(IletisimBilgisi.Kilavuz
                            .Versiyon2X()
                            .TelefonIle("+90 312 420 75 59")
                            .FaksIle("+90 312 420 75 59")
                            .EPostaIle("yavuz.beyribey@tbmm.gov.tr")
                            .WebAdresiIle("https://www.tbmm.gov.tr")
                            .AdresIle(MetinTip.Kilavuz.DegerAta("TBMM BİLGİ İŞLEM BAŞKANLIĞI BAKANLIKLAR").Olustur())
                            .IlceIle(IsimTip.Kilavuz.DegerAta("ÇANKAYA").Olustur())
                            .IlIle(IsimTip.Kilavuz.DegerAta("ANKARA").Olustur())
                            .UlkeIle(IsimTip.Kilavuz.DegerAta("TÜRKİYE").Olustur())
                            .Olustur())
                        .Olustur())
                    .IvedilikTuruAta(IvedilikTuru.NRM)
                    .DagitimTuruAta(DagitimTuru.BLG)
                    .Olustur();

                var olusturan = Olusturan.Kilavuz
                    .OgeAta(KurumKurulus.Kilavuz
                        .Versiyon2X()
                        .KKKAta("61648174")
                        .AdIle(IsimTip.Kilavuz.DegerAta("BİLGİ İŞLEM DAİRESİ BAŞKANLIĞI").Olustur())
                        .IletisimBilgisiIle(IletisimBilgisi.Kilavuz
                            .Versiyon2X()
                            .TelefonIle("+90 422 46 00")
                            .FaksIle("+90 312 417 49 66")
                            .EPostaIle("bilgiislemdairesibaskanligi@icisleri.gov.tr")
                            .WebAdresiIle("https://www.icisleribilgiislem.gov.tr")
                            .AdresIle(MetinTip.Kilavuz.DegerAta("İNÖNÜ BULVARI NO:4 BAKANLIKLAR").Olustur())
                            .IlceIle(IsimTip.Kilavuz.DegerAta("ÇANKAYA").Olustur())
                            .IlIle(IsimTip.Kilavuz.DegerAta("ANKARA").Olustur())
                            .UlkeIle(IsimTip.Kilavuz.DegerAta("TÜRKİYE").Olustur())
                            .Olustur())
                        .Olustur())
                    .Olustur();

                Paket.Olustur(ms)
                    .Versiyon2X()
                    .UstYaziAta(UstYazi.Kilavuz
                        .DosyaAta(TestComponents.USTYAZI_FILE_PATH)
                        .DosyaAdiAta(TestComponents.USTYAZI_FILE_NAME)
                        .MimeTuruAta(TestComponents.MIME_TURU_PDF)
                        .Olustur())
                    .UstveriAta(Ustveri.Kilavuz
                        .Versiyon2X()
                        .BelgeIdAta(belgeId)
                        .KonuAta(MetinTip.Kilavuz.DegerAta("Olustur Versiyon 2X").Olustur())
                        .GuvenlikKoduAta(GuvenlikKoduTuru.YOK)
                        .DagitimAta(dagitim1)
                        .DilIle("tur")
                        .OlusturanAta(olusturan)
                        .DosyaAdiAta(belgeId + ".eyp")
                        .DogrulamaBilgisiAta(DogrulamaBilgisi.Kilavuz.AdresAta("").Olustur())
                        .Olustur())
                    .BilesenleriOlustur(true)
                    .ParafImzaEkle(parafOzetiStream => { return new byte[] {1, 2, 3, 4}; })
                    .Dogrula((kritikHataVarMi, sonuclar) => { Assert.IsFalse(kritikHataVarMi); })
                    .Kapat();
                paket = ms.ToArray();
            }

            using (var ms2 = new MemoryStream())
            {
                ms2.Write(paket, 0, paket.Length);
                Paket.Guncelle(ms2)
                    .Versiyon2XIse(paketGuncellenecek =>
                    {
                        paketGuncellenecek.ImzaEkle(new byte[] {1, 2, 3, 4})
                            .NihaiUstveriAta(NihaiUstveri.Kilavuz
                                .TarihAta(DateTime.UtcNow)
                                .BelgeNoAta("E-24312041-702.03-1")
                                .BelgeImzaAta(Imza.Kilavuz
                                    .ImzalayanAta(GercekSahis.Kilavuz
                                        .KisiAta(Kisi.Kilavuz
                                            .IlkAdiAta(IsimTip.Kilavuz.DegerAta("Ali İhsan").Olustur())
                                            .SoyadiAta(IsimTip.Kilavuz.DegerAta("TOKDEMİR").Olustur())
                                            .Olustur())
                                        .Olustur())
                                    .TarihIle(DateTime.UtcNow)
                                    .Olustur())
                                .Olustur())
                            .BilesenleriOlustur()
                            .Dogrula((hataVarMi, hatalar) => { Assert.IsFalse(hataVarMi); })
                            .Kapat();
                    })
                    .Kapat();
            }
        }

        [TestMethod]
        public void Paket_Guncelle_MuhurEkle_Versiyon2X()
        {
            byte[] paket = null;
            using (var ms = new MemoryStream())
            {
                var belgeId = Guid.NewGuid();
                var dagitim1 = Dagitim.Kilavuz
                    .OgeAta(KurumKurulus.Kilavuz
                        .Versiyon2X()
                        .KKKAta("28119270")
                        .AdIle(IsimTip.Kilavuz.DegerAta("BİLGİ İŞLEM BAŞKANLIĞI").Olustur())
                        .IletisimBilgisiIle(IletisimBilgisi.Kilavuz
                            .Versiyon2X()
                            .TelefonIle("+90 312 420 75 59")
                            .FaksIle("+90 312 420 75 59")
                            .EPostaIle("yavuz.beyribey@tbmm.gov.tr")
                            .WebAdresiIle("https://www.tbmm.gov.tr")
                            .AdresIle(MetinTip.Kilavuz.DegerAta("TBMM BİLGİ İŞLEM BAŞKANLIĞI BAKANLIKLAR").Olustur())
                            .IlceIle(IsimTip.Kilavuz.DegerAta("ÇANKAYA").Olustur())
                            .IlIle(IsimTip.Kilavuz.DegerAta("ANKARA").Olustur())
                            .UlkeIle(IsimTip.Kilavuz.DegerAta("TÜRKİYE").Olustur())
                            .Olustur())
                        .Olustur())
                    .IvedilikTuruAta(IvedilikTuru.NRM)
                    .DagitimTuruAta(DagitimTuru.BLG)
                    .Olustur();

                var olusturan = Olusturan.Kilavuz
                    .OgeAta(KurumKurulus.Kilavuz
                        .Versiyon2X()
                        .KKKAta("61648174")
                        .AdIle(IsimTip.Kilavuz.DegerAta("BİLGİ İŞLEM DAİRESİ BAŞKANLIĞI").Olustur())
                        .IletisimBilgisiIle(IletisimBilgisi.Kilavuz
                            .Versiyon2X()
                            .TelefonIle("+90 422 46 00")
                            .FaksIle("+90 312 417 49 66")
                            .EPostaIle("bilgiislemdairesibaskanligi@icisleri.gov.tr")
                            .WebAdresiIle("https://www.icisleribilgiislem.gov.tr")
                            .AdresIle(MetinTip.Kilavuz.DegerAta("İNÖNÜ BULVARI NO:4 BAKANLIKLAR").Olustur())
                            .IlceIle(IsimTip.Kilavuz.DegerAta("ÇANKAYA").Olustur())
                            .IlIle(IsimTip.Kilavuz.DegerAta("ANKARA").Olustur())
                            .UlkeIle(IsimTip.Kilavuz.DegerAta("TÜRKİYE").Olustur())
                            .Olustur())
                        .Olustur())
                    .Olustur();

                Paket.Olustur(ms)
                    .Versiyon2X()
                    .UstYaziAta(UstYazi.Kilavuz
                        .DosyaAta(TestComponents.USTYAZI_FILE_PATH)
                        .DosyaAdiAta(TestComponents.USTYAZI_FILE_NAME)
                        .MimeTuruAta(TestComponents.MIME_TURU_PDF)
                        .Olustur())
                    .UstveriAta(Ustveri.Kilavuz
                        .Versiyon2X()
                        .BelgeIdAta(belgeId)
                        .KonuAta(MetinTip.Kilavuz.DegerAta("Olustur Versiyon 2X").Olustur())
                        .GuvenlikKoduAta(GuvenlikKoduTuru.YOK)
                        .DagitimAta(dagitim1)
                        .DilIle("tur")
                        .OlusturanAta(olusturan)
                        .DosyaAdiAta(belgeId + ".eyp")
                        .DogrulamaBilgisiAta(DogrulamaBilgisi.Kilavuz.AdresAta("").Olustur())
                        .Olustur())
                    .BilesenleriOlustur(true)
                    .ParafImzaEkle(parafOzetiStream => { return new byte[] {1, 2, 3, 4}; })
                    .ImzaEkle(paketOzetiStream => { return new byte[] {1, 2, 3, 4}; })
                    .NihaiUstveriAta(NihaiUstveri.Kilavuz
                        .TarihAta(DateTime.UtcNow)
                        .BelgeNoAta("E-24312041-702.03-1")
                        .BelgeImzaAta(Imza.Kilavuz
                            .ImzalayanAta(GercekSahis.Kilavuz
                                .KisiAta(Kisi.Kilavuz
                                    .IlkAdiAta(IsimTip.Kilavuz.DegerAta("Ali İhsan").Olustur())
                                    .SoyadiAta(IsimTip.Kilavuz.DegerAta("TOKDEMİR").Olustur())
                                    .Olustur())
                                .Olustur())
                            .TarihIle(DateTime.UtcNow)
                            .Olustur())
                        .Olustur())
                    .BilesenleriOlustur()
                    .Dogrula((kritikHataVarMi, sonuclar) => { Assert.IsFalse(kritikHataVarMi); })
                    .Kapat();
                paket = ms.ToArray();
            }

            using (var ms2 = new MemoryStream())
            {
                ms2.Write(paket, 0, paket.Length);
                Paket.Guncelle(ms2)
                    .Versiyon2XIse(paketGuncellenecek =>
                    {
                        paketGuncellenecek.MuhurEkle(new byte[] {1, 2, 3, 4})
                            .Dogrula((hataVarMi, hatalar) => { Assert.IsFalse(hataVarMi); })
                            .Kapat();
                    })
                    .Kapat();
            }
        }

        [TestMethod]
        public void PaketV2X_Guncelle_ParafImzaEkle()
        {
            byte[] paket = null;
            using (var ms = new MemoryStream())
            {
                var belgeId = Guid.NewGuid();
                var dagitim1 = Dagitim.Kilavuz
                    .OgeAta(KurumKurulus.Kilavuz
                        .Versiyon2X()
                        .KKKAta("28119270")
                        .AdIle(IsimTip.Kilavuz.DegerAta("BİLGİ İŞLEM BAŞKANLIĞI").Olustur())
                        .IletisimBilgisiIle(IletisimBilgisi.Kilavuz
                            .Versiyon2X()
                            .TelefonIle("+90 312 420 75 59")
                            .FaksIle("+90 312 420 75 59")
                            .EPostaIle("yavuz.beyribey@tbmm.gov.tr")
                            .WebAdresiIle("https://www.tbmm.gov.tr")
                            .AdresIle(MetinTip.Kilavuz.DegerAta("TBMM BİLGİ İŞLEM BAŞKANLIĞI BAKANLIKLAR").Olustur())
                            .IlceIle(IsimTip.Kilavuz.DegerAta("ÇANKAYA").Olustur())
                            .IlIle(IsimTip.Kilavuz.DegerAta("ANKARA").Olustur())
                            .UlkeIle(IsimTip.Kilavuz.DegerAta("TÜRKİYE").Olustur())
                            .Olustur())
                        .Olustur())
                    .IvedilikTuruAta(IvedilikTuru.NRM)
                    .DagitimTuruAta(DagitimTuru.BLG)
                    .Olustur();

                var olusturan = Olusturan.Kilavuz
                    .OgeAta(KurumKurulus.Kilavuz
                        .Versiyon2X()
                        .KKKAta("61648174")
                        .AdIle(IsimTip.Kilavuz.DegerAta("BİLGİ İŞLEM DAİRESİ BAŞKANLIĞI").Olustur())
                        .IletisimBilgisiIle(IletisimBilgisi.Kilavuz
                            .Versiyon2X()
                            .TelefonIle("+90 422 46 00")
                            .FaksIle("+90 312 417 49 66")
                            .EPostaIle("bilgiislemdairesibaskanligi@icisleri.gov.tr")
                            .WebAdresiIle("https://www.icisleribilgiislem.gov.tr")
                            .AdresIle(MetinTip.Kilavuz.DegerAta("İNÖNÜ BULVARI NO:4 BAKANLIKLAR").Olustur())
                            .IlceIle(IsimTip.Kilavuz.DegerAta("ÇANKAYA").Olustur())
                            .IlIle(IsimTip.Kilavuz.DegerAta("ANKARA").Olustur())
                            .UlkeIle(IsimTip.Kilavuz.DegerAta("TÜRKİYE").Olustur())
                            .Olustur())
                        .Olustur())
                    .Olustur();

                PaketV2X.Olustur(ms)
                    .UstYaziAta(UstYazi.Kilavuz
                        .DosyaAta(TestComponents.USTYAZI_FILE_PATH)
                        .DosyaAdiAta(TestComponents.USTYAZI_FILE_NAME)
                        .MimeTuruAta(TestComponents.MIME_TURU_PDF)
                        .Olustur())
                    .UstveriAta(Ustveri.Kilavuz
                        .Versiyon2X()
                        .BelgeIdAta(belgeId)
                        .KonuAta(MetinTip.Kilavuz.DegerAta("Olustur Versiyon 2X").Olustur())
                        .GuvenlikKoduAta(GuvenlikKoduTuru.YOK)
                        .DagitimAta(dagitim1)
                        .DilIle("tur")
                        .OlusturanAta(olusturan)
                        .DosyaAdiAta(belgeId + ".eyp")
                        .DogrulamaBilgisiAta(DogrulamaBilgisi.Kilavuz.AdresAta("").Olustur())
                        .Olustur())
                    .BilesenleriOlustur(true)
                    .Dogrula((kritikHataVarMi, sonuclar) =>
                    {
                        if (kritikHataVarMi)
                            Assert.Fail("Paket oluşturma esnasında hata oluşmuştur.");
                    })
                    .Kapat();
                paket = ms.ToArray();
            }

            using (var ms2 = new MemoryStream())
            {
                ms2.Write(paket, 0, paket.Length);
                Paket.Guncelle(ms2)
                    .Versiyon2XIse(paketGuncellenecek =>
                    {
                        paketGuncellenecek.ParafImzaEkle(new byte[] {1, 2, 3, 4})
                            .Dogrula((hataVarMi, hatalar) => { Assert.IsFalse(hataVarMi); })
                            .Kapat();
                    })
                    .Kapat();
            }
        }

        [TestMethod]
        public void PaketV2X_Guncelle_ImzaEkle()
        {
            byte[] paket = null;
            using (var ms = new MemoryStream())
            {
                var belgeId = Guid.NewGuid();
                var dagitim1 = Dagitim.Kilavuz
                    .OgeAta(KurumKurulus.Kilavuz
                        .Versiyon2X()
                        .KKKAta("28119270")
                        .AdIle(IsimTip.Kilavuz.DegerAta("BİLGİ İŞLEM BAŞKANLIĞI").Olustur())
                        .IletisimBilgisiIle(IletisimBilgisi.Kilavuz
                            .Versiyon2X()
                            .TelefonIle("+90 312 420 75 59")
                            .FaksIle("+90 312 420 75 59")
                            .EPostaIle("yavuz.beyribey@tbmm.gov.tr")
                            .WebAdresiIle("https://www.tbmm.gov.tr")
                            .AdresIle(MetinTip.Kilavuz.DegerAta("TBMM BİLGİ İŞLEM BAŞKANLIĞI BAKANLIKLAR").Olustur())
                            .IlceIle(IsimTip.Kilavuz.DegerAta("ÇANKAYA").Olustur())
                            .IlIle(IsimTip.Kilavuz.DegerAta("ANKARA").Olustur())
                            .UlkeIle(IsimTip.Kilavuz.DegerAta("TÜRKİYE").Olustur())
                            .Olustur())
                        .Olustur())
                    .IvedilikTuruAta(IvedilikTuru.NRM)
                    .DagitimTuruAta(DagitimTuru.BLG)
                    .Olustur();

                var olusturan = Olusturan.Kilavuz
                    .OgeAta(KurumKurulus.Kilavuz
                        .Versiyon2X()
                        .KKKAta("61648174")
                        .AdIle(IsimTip.Kilavuz.DegerAta("BİLGİ İŞLEM DAİRESİ BAŞKANLIĞI").Olustur())
                        .IletisimBilgisiIle(IletisimBilgisi.Kilavuz
                            .Versiyon2X()
                            .TelefonIle("+90 422 46 00")
                            .FaksIle("+90 312 417 49 66")
                            .EPostaIle("bilgiislemdairesibaskanligi@icisleri.gov.tr")
                            .WebAdresiIle("https://www.icisleribilgiislem.gov.tr")
                            .AdresIle(MetinTip.Kilavuz.DegerAta("İNÖNÜ BULVARI NO:4 BAKANLIKLAR").Olustur())
                            .IlceIle(IsimTip.Kilavuz.DegerAta("ÇANKAYA").Olustur())
                            .IlIle(IsimTip.Kilavuz.DegerAta("ANKARA").Olustur())
                            .UlkeIle(IsimTip.Kilavuz.DegerAta("TÜRKİYE").Olustur())
                            .Olustur())
                        .Olustur())
                    .Olustur();

                PaketV2X.Olustur(ms)
                    .UstYaziAta(UstYazi.Kilavuz
                        .DosyaAta(TestComponents.USTYAZI_FILE_PATH)
                        .DosyaAdiAta(TestComponents.USTYAZI_FILE_NAME)
                        .MimeTuruAta(TestComponents.MIME_TURU_PDF)
                        .Olustur())
                    .UstveriAta(Ustveri.Kilavuz
                        .Versiyon2X()
                        .BelgeIdAta(belgeId)
                        .KonuAta(MetinTip.Kilavuz.DegerAta("Olustur Versiyon 2X").Olustur())
                        .GuvenlikKoduAta(GuvenlikKoduTuru.YOK)
                        .DagitimAta(dagitim1)
                        .DilIle("tur")
                        .OlusturanAta(olusturan)
                        .DosyaAdiAta(belgeId + ".eyp")
                        .DogrulamaBilgisiAta(DogrulamaBilgisi.Kilavuz.AdresAta("").Olustur())
                        .Olustur())
                    .BilesenleriOlustur(true)
                    .ParafImzaEkle(parafOzetiStream => { return new byte[] {1, 2, 3, 4}; })
                    .Dogrula((kritikHataVarMi, sonuclar) => { Assert.IsFalse(kritikHataVarMi); })
                    .Kapat();
                paket = ms.ToArray();
            }

            using (var ms2 = new MemoryStream())
            {
                ms2.Write(paket, 0, paket.Length);
                PaketV2X.Guncelle(ms2)
                    .ImzaEkle(new byte[] {1, 2, 3, 4})
                    .NihaiUstveriAta(NihaiUstveri.Kilavuz
                        .TarihAta(DateTime.UtcNow)
                        .BelgeNoAta("E-24312041-702.03-1")
                        .BelgeImzaAta(Imza.Kilavuz
                            .ImzalayanAta(GercekSahis.Kilavuz
                                .KisiAta(Kisi.Kilavuz
                                    .IlkAdiAta(IsimTip.Kilavuz.DegerAta("Ali İhsan").Olustur())
                                    .SoyadiAta(IsimTip.Kilavuz.DegerAta("TOKDEMİR").Olustur())
                                    .Olustur())
                                .Olustur())
                            .TarihIle(DateTime.UtcNow)
                            .Olustur())
                        .Olustur())
                    .BilesenleriOlustur()
                    .Dogrula((hataVarMi, hatalar) => { Assert.IsFalse(hataVarMi); })
                    .Kapat();
            }
        }

        [TestMethod]
        public void PaketV2X_Guncelle_MuhurEkle()
        {
            byte[] paket = null;
            using (var ms = new MemoryStream())
            {
                var belgeId = Guid.NewGuid();
                var dagitim1 = Dagitim.Kilavuz
                    .OgeAta(KurumKurulus.Kilavuz
                        .Versiyon2X()
                        .KKKAta("28119270")
                        .AdIle(IsimTip.Kilavuz.DegerAta("BİLGİ İŞLEM BAŞKANLIĞI").Olustur())
                        .IletisimBilgisiIle(IletisimBilgisi.Kilavuz
                            .Versiyon2X()
                            .TelefonIle("+90 312 420 75 59")
                            .FaksIle("+90 312 420 75 59")
                            .EPostaIle("yavuz.beyribey@tbmm.gov.tr")
                            .WebAdresiIle("https://www.tbmm.gov.tr")
                            .AdresIle(MetinTip.Kilavuz.DegerAta("TBMM BİLGİ İŞLEM BAŞKANLIĞI BAKANLIKLAR").Olustur())
                            .IlceIle(IsimTip.Kilavuz.DegerAta("ÇANKAYA").Olustur())
                            .IlIle(IsimTip.Kilavuz.DegerAta("ANKARA").Olustur())
                            .UlkeIle(IsimTip.Kilavuz.DegerAta("TÜRKİYE").Olustur())
                            .Olustur())
                        .Olustur())
                    .IvedilikTuruAta(IvedilikTuru.NRM)
                    .DagitimTuruAta(DagitimTuru.BLG)
                    .Olustur();

                var olusturan = Olusturan.Kilavuz
                    .OgeAta(KurumKurulus.Kilavuz
                        .Versiyon2X()
                        .KKKAta("61648174")
                        .AdIle(IsimTip.Kilavuz.DegerAta("BİLGİ İŞLEM DAİRESİ BAŞKANLIĞI").Olustur())
                        .IletisimBilgisiIle(IletisimBilgisi.Kilavuz
                            .Versiyon2X()
                            .TelefonIle("+90 422 46 00")
                            .FaksIle("+90 312 417 49 66")
                            .EPostaIle("bilgiislemdairesibaskanligi@icisleri.gov.tr")
                            .WebAdresiIle("https://www.icisleribilgiislem.gov.tr")
                            .AdresIle(MetinTip.Kilavuz.DegerAta("İNÖNÜ BULVARI NO:4 BAKANLIKLAR").Olustur())
                            .IlceIle(IsimTip.Kilavuz.DegerAta("ÇANKAYA").Olustur())
                            .IlIle(IsimTip.Kilavuz.DegerAta("ANKARA").Olustur())
                            .UlkeIle(IsimTip.Kilavuz.DegerAta("TÜRKİYE").Olustur())
                            .Olustur())
                        .Olustur())
                    .Olustur();

                PaketV2X.Olustur(ms)
                    .UstYaziAta(UstYazi.Kilavuz
                        .DosyaAta(TestComponents.USTYAZI_FILE_PATH)
                        .DosyaAdiAta(TestComponents.USTYAZI_FILE_NAME)
                        .MimeTuruAta(TestComponents.MIME_TURU_PDF)
                        .Olustur())
                    .UstveriAta(Ustveri.Kilavuz
                        .Versiyon2X()
                        .BelgeIdAta(belgeId)
                        .KonuAta(MetinTip.Kilavuz.DegerAta("Olustur Versiyon 2X").Olustur())
                        .GuvenlikKoduAta(GuvenlikKoduTuru.YOK)
                        .DagitimAta(dagitim1)
                        .DilIle("tur")
                        .OlusturanAta(olusturan)
                        .DosyaAdiAta(belgeId + ".eyp")
                        .DogrulamaBilgisiAta(DogrulamaBilgisi.Kilavuz.AdresAta("").Olustur())
                        .Olustur())
                    .BilesenleriOlustur(true)
                    .ParafImzaEkle(parafOzetiStream => { return new byte[] {1, 2, 3, 4}; })
                    .ImzaEkle(paketOzetiStream => { return new byte[] {1, 2, 3, 4}; })
                    .NihaiUstveriAta(NihaiUstveri.Kilavuz
                        .TarihAta(DateTime.UtcNow)
                        .BelgeNoAta("E-24312041-702.03-1")
                        .BelgeImzaAta(Imza.Kilavuz
                            .ImzalayanAta(GercekSahis.Kilavuz
                                .KisiAta(Kisi.Kilavuz
                                    .IlkAdiAta(IsimTip.Kilavuz.DegerAta("Ali İhsan").Olustur())
                                    .SoyadiAta(IsimTip.Kilavuz.DegerAta("TOKDEMİR").Olustur())
                                    .Olustur())
                                .Olustur())
                            .TarihIle(DateTime.UtcNow)
                            .Olustur())
                        .Olustur())
                    .BilesenleriOlustur()
                    .Dogrula((kritikHataVarMi, sonuclar) => { Assert.IsFalse(kritikHataVarMi); })
                    .Kapat();
                paket = ms.ToArray();
            }

            using (var ms2 = new MemoryStream())
            {
                ms2.Write(paket, 0, paket.Length);
                PaketV2X.Guncelle(ms2)
                    .MuhurEkle(new byte[] {1, 2, 3, 4})
                    .Dogrula((hataVarMi, hatalar) => { Assert.IsFalse(hataVarMi); })
                    .Kapat();
            }
        }
    }
}