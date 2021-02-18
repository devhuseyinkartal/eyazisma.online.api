using eyazisma.online.api.Classes;
using eyazisma.online.api.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace eyazisma.online.api.net5.test
{
    [TestClass]
    public class PaketOlusturTests
    {
        [TestMethod]
        public void Paket_Olustur_Versiyon1X()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                var belgeId = Guid.NewGuid();
                var dagitim1 = Dagitim.Kilavuz
                                      .OgeAta(GercekSahis.Kilavuz
                                                         .KisiAta(Kisi.Kilavuz
                                                                      .IlkAdiAta(IsimTip.Kilavuz.DegerAta("HÜSEYİN").Olustur())
                                                                      .SoyadiAta(IsimTip.Kilavuz.DegerAta("KARTAL").Olustur())
                                                                      .Olustur())
                                                         .TCKNIle("11111111110")
                                                         .IletisimBilgisiIle(IletisimBilgisi.Kilavuz
                                                                                            .Versiyon1X()
                                                                                            .TelefonIle("+90 555 555 55 55")
                                                                                            .EPostaIle("huseyin@huseyinkartal.com")
                                                                                            .WebAdresiIle("http://www.huseyinkartal.com")
                                                                                            .AdresIle(MetinTip.Kilavuz.DegerAta("MAH. CD. NO: 5/5").Olustur())
                                                                                            .IlceIle(IsimTip.Kilavuz.DegerAta("ALTINDAĞ").Olustur())
                                                                                            .IlIle(IsimTip.Kilavuz.DegerAta("ANKARA").Olustur())
                                                                                            .UlkeIle(IsimTip.Kilavuz.DegerAta("TÜRKİYE").Olustur())
                                                                                            .Olustur())
                                                         .Olustur())
                                      .IvedilikTuruAta(IvedilikTuru.NRM)
                                      .DagitimTuruAta(DagitimTuru.BLG)
                                      .Olustur();

                var dagitim2 = Dagitim.Kilavuz
                                      .OgeAta(TuzelSahis.Kilavuz
                                                        .IdAta(TanimlayiciTip.Kilavuz.SemaIDAta("MERSIS").DegerAta("0922003497008217").Olustur())
                                                        .AdIle(IsimTip.Kilavuz.DegerAta("TÜRKİYE VAKIFLAR BANKASI TÜRK ANONİM ORTAKLIĞI-KIZILAY ŞUBESİ").Olustur())
                                                        .IletisimBilgisiIle(IletisimBilgisi.Kilavuz
                                                                                           .Versiyon1X()
                                                                                           .AdresIle(MetinTip.Kilavuz.DegerAta("KIZILAY MAHALLESİ İZMİR 1 CAD. NO: 2 A").Olustur())
                                                                                           .IlceIle(IsimTip.Kilavuz.DegerAta("ÇANKAYA").Olustur())
                                                                                           .IlIle(IsimTip.Kilavuz.DegerAta("ANKARA").Olustur())
                                                                                           .UlkeIle(IsimTip.Kilavuz.DegerAta("TÜRKİYE").Olustur())
                                                                                           .Olustur())
                                                        .Olustur())
                                      .IvedilikTuruAta(IvedilikTuru.NRM)
                                      .DagitimTuruAta(DagitimTuru.GRG)
                                      .Olustur();

                var dagitim3 = Dagitim.Kilavuz
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

                var ek1 = Ek.Kilavuz
                            .DahiliElektronikDosya()
                            .IdAta(IdTip.Kilavuz.DegerAta(Guid.NewGuid().ToString()).EYazismaIdMiAta(false).Olustur())
                            .DosyaAdiAta(TestComponents.EK1_FILE_NAME)
                            .MimeTuruAta(TestComponents.MIME_TURU_PDF)
                            .AdIle(MetinTip.Kilavuz.DegerAta("Ek 1").Olustur())
                            .SiraNoAta(1)
                            .ImzaliMiAta(true)
                            .Olustur();

                var ek2 = Ek.Kilavuz
                            .DahiliElektronikDosya()
                            .IdAta(IdTip.Kilavuz.DegerAta(Guid.NewGuid().ToString()).EYazismaIdMiAta(false).Olustur())
                            .DosyaAdiAta(TestComponents.EK2_FILE_NAME)
                            .MimeTuruAta(TestComponents.MIME_TURU_PDF)
                            .AdIle(MetinTip.Kilavuz.DegerAta("Ek 2").Olustur())
                            .SiraNoAta(2)
                            .ImzaliMiAta(true)
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
                                        .DagitimAta(dagitim2)
                                        .DagitimAta(dagitim3)
                                        .EkIle(ek1)
                                        .EkIle(ek2)
                                        .DilIle("tur")
                                        .OlusturanAta(olusturan)
                                        .DosyaAdiAta(belgeId.ToString() + ".eyp")
                                        .Olustur())
                     .BelgeHedefAta(BelgeHedef.Kilavuz
                                              .HedefEkle(Hedef.Kilavuz.OgeAta((TuzelSahis)dagitim2.Oge).Olustur())
                                              .DigerHedefEkle(Hedef.Kilavuz.OgeAta((KurumKurulus)dagitim3.Oge).Olustur())
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
                     .EkDosyaIle(EkDosya.Kilavuz
                                        .EkAta(ek1)
                                        .DosyaAta(TestComponents.EK1_FILE_PATH)
                                        .DosyaAdiAta(TestComponents.EK1_FILE_NAME)
                                        .Olustur())
                     .EkDosyaIle(EkDosya.Kilavuz
                                        .EkAta(ek2)
                                        .DosyaAta(TestComponents.EK2_FILE_PATH)
                                        .DosyaAdiAta(TestComponents.EK2_FILE_NAME)
                                        .Olustur())
                     .BilesenleriOlustur()
                     .ImzaEkle((paketOzetiStream) =>
                     {
                         return new byte[] { 1, 2, 3, 4 };
                     })
                     .MuhurEkle((nihaiOzetStream) =>
                     {
                         return new byte[] { 1, 2, 3, 4 };
                     })
                     .Dogrula((kritikHataVarMi, sonuclar) =>
                     {
                         Assert.IsFalse(kritikHataVarMi);
                     })
                     .Kapat();
            }
        }

        [TestMethod]
        public void Paket_Olustur_Versiyon2X()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                var belgeId = Guid.NewGuid();
                var dagitim1 = Dagitim.Kilavuz
                                      .OgeAta(GercekSahis.Kilavuz
                                                         .KisiAta(Kisi.Kilavuz
                                                                      .IlkAdiAta(IsimTip.Kilavuz.DegerAta("HÜSEYİN").Olustur())
                                                                      .SoyadiAta(IsimTip.Kilavuz.DegerAta("KARTAL").Olustur())
                                                                      .Olustur())
                                                         .TCKNIle("11111111110")
                                                         .IletisimBilgisiIle(IletisimBilgisi.Kilavuz
                                                                                            .Versiyon1X()
                                                                                            .TelefonIle("+90 555 555 55 55")
                                                                                            .EPostaIle("huseyin@huseyinkartal.com")
                                                                                            .WebAdresiIle("http://www.huseyinkartal.com")
                                                                                            .AdresIle(MetinTip.Kilavuz.DegerAta("MAH. CD. NO: 5/5").Olustur())
                                                                                            .IlceIle(IsimTip.Kilavuz.DegerAta("ALTINDAĞ").Olustur())
                                                                                            .IlIle(IsimTip.Kilavuz.DegerAta("ANKARA").Olustur())
                                                                                            .UlkeIle(IsimTip.Kilavuz.DegerAta("TÜRKİYE").Olustur())
                                                                                            .Olustur())
                                                         .Olustur())
                                      .IvedilikTuruAta(IvedilikTuru.NRM)
                                      .DagitimTuruAta(DagitimTuru.BLG)
                                      .Olustur();

                var dagitim2 = Dagitim.Kilavuz
                                      .OgeAta(TuzelSahis.Kilavuz
                                                        .IdAta(TanimlayiciTip.Kilavuz.SemaIDAta("MERSIS").DegerAta("0922003497008217").Olustur())
                                                        .AdIle(IsimTip.Kilavuz.DegerAta("TÜRKİYE VAKIFLAR BANKASI TÜRK ANONİM ORTAKLIĞI-KIZILAY ŞUBESİ").Olustur())
                                                        .IletisimBilgisiIle(IletisimBilgisi.Kilavuz
                                                                                           .Versiyon1X()
                                                                                           .AdresIle(MetinTip.Kilavuz.DegerAta("KIZILAY MAHALLESİ İZMİR 1 CAD. NO: 2 A").Olustur())
                                                                                           .IlceIle(IsimTip.Kilavuz.DegerAta("ÇANKAYA").Olustur())
                                                                                           .IlIle(IsimTip.Kilavuz.DegerAta("ANKARA").Olustur())
                                                                                           .UlkeIle(IsimTip.Kilavuz.DegerAta("TÜRKİYE").Olustur())
                                                                                           .Olustur())
                                                        .Olustur())
                                      .IvedilikTuruAta(IvedilikTuru.NRM)
                                      .DagitimTuruAta(DagitimTuru.GRG)
                                      .Olustur();

                var dagitim3 = Dagitim.Kilavuz
                                      .OgeAta(KurumKurulus.Kilavuz
                                                          .Versiyon1X()
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

                var ek1 = Ek.Kilavuz
                            .DahiliElektronikDosya()
                            .IdAta(IdTip.Kilavuz.DegerAta(Guid.NewGuid().ToString()).EYazismaIdMiAta(false).Olustur())
                            .DosyaAdiAta(TestComponents.EK1_FILE_NAME)
                            .MimeTuruAta(TestComponents.MIME_TURU_PDF)
                            .AdIle(MetinTip.Kilavuz.DegerAta("Ek 1").Olustur())
                            .SiraNoAta(1)
                            .ImzaliMiAta(true)
                            .Olustur();

                var ek2 = Ek.Kilavuz
                            .DahiliElektronikDosya()
                            .IdAta(IdTip.Kilavuz.DegerAta(Guid.NewGuid().ToString()).EYazismaIdMiAta(false).Olustur())
                            .DosyaAdiAta(TestComponents.EK2_FILE_NAME)
                            .MimeTuruAta(TestComponents.MIME_TURU_PDF)
                            .AdIle(MetinTip.Kilavuz.DegerAta("Ek 2").Olustur())
                            .SiraNoAta(2)
                            .ImzaliMiAta(true)
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
                                        .DagitimAta(dagitim2)
                                        .DagitimAta(dagitim3)
                                        .EkIle(ek1)
                                        .EkIle(ek2)
                                        .DilIle("tur")
                                        .OlusturanAta(olusturan)
                                        .DosyaAdiAta(belgeId.ToString() + ".eyp")
                                        .DogrulamaBilgisiAta(DogrulamaBilgisi.Kilavuz.AdresAta("").Olustur())
                                        .Olustur())
                     .EkDosyaIle(EkDosya.Kilavuz
                                        .EkAta(ek1)
                                        .DosyaAta(TestComponents.EK1_FILE_PATH)
                                        .DosyaAdiAta(TestComponents.EK1_FILE_NAME)
                                        .Olustur())
                     .EkDosyaIle(EkDosya.Kilavuz
                                        .EkAta(ek2)
                                        .DosyaAta(TestComponents.EK2_FILE_PATH)
                                        .DosyaAdiAta(TestComponents.EK2_FILE_NAME)
                                        .Olustur())
                     .BilesenleriOlustur(true)
                     .ParafImzaEkle((parafOzetiStream) =>
                     {
                         return new byte[] { 1, 2, 3, 4 };
                     })
                     .ImzaEkle((paketOzetiStream) =>
                     {
                         return new byte[] { 1, 2, 3, 4 };
                     })
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
                     .MuhurEkle((nihaiOzetStream) =>
                     {
                         return new byte[] { 1, 2, 3, 4 };
                     })
                     .Dogrula((kritikHataVarMi, sonuclar) =>
                     {
                         Assert.IsFalse(kritikHataVarMi);
                     })
                     .Kapat();
            }
        }

        [TestMethod]
        public void PaketV1X_Olustur()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                var belgeId = Guid.NewGuid();
                var dagitim1 = Dagitim.Kilavuz
                                      .OgeAta(GercekSahis.Kilavuz
                                                         .KisiAta(Kisi.Kilavuz
                                                                      .IlkAdiAta(IsimTip.Kilavuz.DegerAta("HÜSEYİN").Olustur())
                                                                      .SoyadiAta(IsimTip.Kilavuz.DegerAta("KARTAL").Olustur())
                                                                      .Olustur())
                                                         .TCKNIle("11111111110")
                                                         .IletisimBilgisiIle(IletisimBilgisi.Kilavuz
                                                                                            .Versiyon1X()
                                                                                            .TelefonIle("+90 555 555 55 55")
                                                                                            .EPostaIle("huseyin@huseyinkartal.com")
                                                                                            .WebAdresiIle("http://www.huseyinkartal.com")
                                                                                            .AdresIle(MetinTip.Kilavuz.DegerAta("MAH. CD. NO: 5/5").Olustur())
                                                                                            .IlceIle(IsimTip.Kilavuz.DegerAta("ALTINDAĞ").Olustur())
                                                                                            .IlIle(IsimTip.Kilavuz.DegerAta("ANKARA").Olustur())
                                                                                            .UlkeIle(IsimTip.Kilavuz.DegerAta("TÜRKİYE").Olustur())
                                                                                            .Olustur())
                                                         .Olustur())
                                      .IvedilikTuruAta(IvedilikTuru.NRM)
                                      .DagitimTuruAta(DagitimTuru.BLG)
                                      .Olustur();

                var dagitim2 = Dagitim.Kilavuz
                                      .OgeAta(TuzelSahis.Kilavuz
                                                        .IdAta(TanimlayiciTip.Kilavuz.SemaIDAta("MERSIS").DegerAta("0922003497008217").Olustur())
                                                        .AdIle(IsimTip.Kilavuz.DegerAta("TÜRKİYE VAKIFLAR BANKASI TÜRK ANONİM ORTAKLIĞI-KIZILAY ŞUBESİ").Olustur())
                                                        .IletisimBilgisiIle(IletisimBilgisi.Kilavuz
                                                                                           .Versiyon1X()
                                                                                           .AdresIle(MetinTip.Kilavuz.DegerAta("KIZILAY MAHALLESİ İZMİR 1 CAD. NO: 2 A").Olustur())
                                                                                           .IlceIle(IsimTip.Kilavuz.DegerAta("ÇANKAYA").Olustur())
                                                                                           .IlIle(IsimTip.Kilavuz.DegerAta("ANKARA").Olustur())
                                                                                           .UlkeIle(IsimTip.Kilavuz.DegerAta("TÜRKİYE").Olustur())
                                                                                           .Olustur())
                                                        .Olustur())
                                      .IvedilikTuruAta(IvedilikTuru.NRM)
                                      .DagitimTuruAta(DagitimTuru.GRG)
                                      .Olustur();

                var dagitim3 = Dagitim.Kilavuz
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

                var ek1 = Ek.Kilavuz
                            .DahiliElektronikDosya()
                            .IdAta(IdTip.Kilavuz.DegerAta(Guid.NewGuid().ToString()).EYazismaIdMiAta(false).Olustur())
                            .DosyaAdiAta(TestComponents.EK1_FILE_NAME)
                            .MimeTuruAta(TestComponents.MIME_TURU_PDF)
                            .AdIle(MetinTip.Kilavuz.DegerAta("Ek 1").Olustur())
                            .SiraNoAta(1)
                            .ImzaliMiAta(true)
                            .Olustur();

                var ek2 = Ek.Kilavuz
                            .DahiliElektronikDosya()
                            .IdAta(IdTip.Kilavuz.DegerAta(Guid.NewGuid().ToString()).EYazismaIdMiAta(false).Olustur())
                            .DosyaAdiAta(TestComponents.EK2_FILE_NAME)
                            .MimeTuruAta(TestComponents.MIME_TURU_PDF)
                            .AdIle(MetinTip.Kilavuz.DegerAta("Ek 2").Olustur())
                            .SiraNoAta(2)
                            .ImzaliMiAta(true)
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
                                           .DagitimAta(dagitim2)
                                           .DagitimAta(dagitim3)
                                           .EkIle(ek1)
                                           .EkIle(ek2)
                                           .DilIle("tur")
                                           .OlusturanAta(olusturan)
                                           .DosyaAdiAta(belgeId.ToString() + ".eyp")
                                           .Olustur())
                        .BelgeHedefAta(BelgeHedef.Kilavuz
                                                 .HedefEkle(Hedef.Kilavuz.OgeAta((TuzelSahis)dagitim2.Oge).Olustur())
                                                 .DigerHedefEkle(Hedef.Kilavuz.OgeAta((KurumKurulus)dagitim3.Oge).Olustur())
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
                        .EkDosyaIle(EkDosya.Kilavuz
                                           .EkAta(ek1)
                                           .DosyaAta(TestComponents.EK1_FILE_PATH)
                                           .DosyaAdiAta(TestComponents.EK1_FILE_NAME)
                                           .Olustur())
                        .EkDosyaIle(EkDosya.Kilavuz
                                           .EkAta(ek2)
                                           .DosyaAta(TestComponents.EK2_FILE_PATH)
                                           .DosyaAdiAta(TestComponents.EK2_FILE_NAME)
                                           .Olustur())
                        .BilesenleriOlustur()
                        .ImzaEkle((paketOzetiStream) =>
                        {
                            return new byte[] { 1, 2, 3, 4 };
                        })
                        .MuhurEkle((nihaiOzetStream) =>
                        {
                            return new byte[] { 1, 2, 3, 4 };
                        })
                        .Dogrula((kritikHataVarMi, sonuclar) =>
                        {
                            Assert.IsFalse(kritikHataVarMi);
                        })
                        .Kapat();
            }
        }

        [TestMethod]
        public void PaketV2X_Olustur()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                var belgeId = Guid.NewGuid();
                var dagitim1 = Dagitim.Kilavuz
                                      .OgeAta(GercekSahis.Kilavuz
                                                         .KisiAta(Kisi.Kilavuz
                                                                      .IlkAdiAta(IsimTip.Kilavuz.DegerAta("HÜSEYİN").Olustur())
                                                                      .SoyadiAta(IsimTip.Kilavuz.DegerAta("KARTAL").Olustur())
                                                                      .Olustur())
                                                         .TCKNIle("11111111110")
                                                         .IletisimBilgisiIle(IletisimBilgisi.Kilavuz
                                                                                            .Versiyon1X()
                                                                                            .TelefonIle("+90 555 555 55 55")
                                                                                            .EPostaIle("huseyin@huseyinkartal.com")
                                                                                            .WebAdresiIle("http://www.huseyinkartal.com")
                                                                                            .AdresIle(MetinTip.Kilavuz.DegerAta("MAH. CD. NO: 5/5").Olustur())
                                                                                            .IlceIle(IsimTip.Kilavuz.DegerAta("ALTINDAĞ").Olustur())
                                                                                            .IlIle(IsimTip.Kilavuz.DegerAta("ANKARA").Olustur())
                                                                                            .UlkeIle(IsimTip.Kilavuz.DegerAta("TÜRKİYE").Olustur())
                                                                                            .Olustur())
                                                         .Olustur())
                                      .IvedilikTuruAta(IvedilikTuru.NRM)
                                      .DagitimTuruAta(DagitimTuru.BLG)
                                      .Olustur();

                var dagitim2 = Dagitim.Kilavuz
                                      .OgeAta(TuzelSahis.Kilavuz
                                                        .IdAta(TanimlayiciTip.Kilavuz.SemaIDAta("MERSIS").DegerAta("0922003497008217").Olustur())
                                                        .AdIle(IsimTip.Kilavuz.DegerAta("TÜRKİYE VAKIFLAR BANKASI TÜRK ANONİM ORTAKLIĞI-KIZILAY ŞUBESİ").Olustur())
                                                        .IletisimBilgisiIle(IletisimBilgisi.Kilavuz
                                                                                           .Versiyon1X()
                                                                                           .AdresIle(MetinTip.Kilavuz.DegerAta("KIZILAY MAHALLESİ İZMİR 1 CAD. NO: 2 A").Olustur())
                                                                                           .IlceIle(IsimTip.Kilavuz.DegerAta("ÇANKAYA").Olustur())
                                                                                           .IlIle(IsimTip.Kilavuz.DegerAta("ANKARA").Olustur())
                                                                                           .UlkeIle(IsimTip.Kilavuz.DegerAta("TÜRKİYE").Olustur())
                                                                                           .Olustur())
                                                        .Olustur())
                                      .IvedilikTuruAta(IvedilikTuru.NRM)
                                      .DagitimTuruAta(DagitimTuru.GRG)
                                      .Olustur();

                var dagitim3 = Dagitim.Kilavuz
                                      .OgeAta(KurumKurulus.Kilavuz
                                                          .Versiyon1X()
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

                var ek1 = Ek.Kilavuz
                            .DahiliElektronikDosya()
                            .IdAta(IdTip.Kilavuz.DegerAta(Guid.NewGuid().ToString()).EYazismaIdMiAta(false).Olustur())
                            .DosyaAdiAta(TestComponents.EK1_FILE_NAME)
                            .MimeTuruAta(TestComponents.MIME_TURU_PDF)
                            .AdIle(MetinTip.Kilavuz.DegerAta("Ek 1").Olustur())
                            .SiraNoAta(1)
                            .ImzaliMiAta(true)
                            .Olustur();

                var ek2 = Ek.Kilavuz
                            .DahiliElektronikDosya()
                            .IdAta(IdTip.Kilavuz.DegerAta(Guid.NewGuid().ToString()).EYazismaIdMiAta(false).Olustur())
                            .DosyaAdiAta(TestComponents.EK2_FILE_NAME)
                            .MimeTuruAta(TestComponents.MIME_TURU_PDF)
                            .AdIle(MetinTip.Kilavuz.DegerAta("Ek 2").Olustur())
                            .SiraNoAta(2)
                            .ImzaliMiAta(true)
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
                                           .DagitimAta(dagitim2)
                                           .DagitimAta(dagitim3)
                                           .EkIle(ek1)
                                           .EkIle(ek2)
                                           .DilIle("tur")
                                           .OlusturanAta(olusturan)
                                           .DosyaAdiAta(belgeId.ToString() + ".eyp")
                                           .DogrulamaBilgisiAta(DogrulamaBilgisi.Kilavuz.AdresAta("").Olustur())
                                           .Olustur())
                        .EkDosyaIle(EkDosya.Kilavuz
                                           .EkAta(ek1)
                                           .DosyaAta(TestComponents.EK1_FILE_PATH)
                                           .DosyaAdiAta(TestComponents.EK1_FILE_NAME)
                                           .Olustur())
                        .EkDosyaIle(EkDosya.Kilavuz
                                           .EkAta(ek2)
                                           .DosyaAta(TestComponents.EK2_FILE_PATH)
                                           .DosyaAdiAta(TestComponents.EK2_FILE_NAME)
                                           .Olustur())
                        .BilesenleriOlustur(true)
                        .ParafImzaEkle((parafOzetiStream) =>
                        {
                            return new byte[] { 1, 2, 3, 4 };
                        })
                        .ImzaEkle((paketOzetiStream) =>
                        {
                            return new byte[] { 1, 2, 3, 4 };
                        })
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
                        .MuhurEkle((nihaiOzetStream) =>
                        {
                            return new byte[] { 1, 2, 3, 4 };
                        })
                        .Dogrula((kritikHataVarMi, sonuclar) =>
                        {
                            Assert.IsFalse(kritikHataVarMi);
                        })
                        .Kapat();
            }
        }
    }
}
