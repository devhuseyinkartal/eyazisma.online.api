using System;
using System.IO;
using eyazisma.online.api.Classes;
using eyazisma.online.api.Enums;
using eyazisma.online.api.Extensions;

namespace eyazisma.online.api.net5.test
{
    public static class TestComponents
    {
        private static string BASE_DIRECTORY =>
            AppDomain.CurrentDomain.BaseDirectory.Replace(@"\bin\Debug\net5.0", string.Empty);

        public static string TEST_BASE_DIRECTORY => Path.Combine(BASE_DIRECTORY, "testFiles");
        public static string RESULT_BASE_DIRECTORY => Path.Combine(BASE_DIRECTORY, "results");

        public static string USTYAZI_FILE_PATH => Path.Combine(TEST_BASE_DIRECTORY, "ustyazi.pdf");
        public static string USTYAZI_FILE_NAME => Path.GetFileName(USTYAZI_FILE_PATH);

        public static string EK1_FILE_PATH => Path.Combine(TEST_BASE_DIRECTORY, "ek1.pdf");
        public static string EK1_FILE_NAME => Path.GetFileName(EK1_FILE_PATH);

        public static string EK2_FILE_PATH => Path.Combine(TEST_BASE_DIRECTORY, "ek2.pdf");
        public static string EK2_FILE_NAME => Path.GetFileName(EK2_FILE_PATH);

        public static string MIME_TURU_PDF => "application/pdf";

        public static string PAKET_V1X_FILE_PATH => Path.Combine(TEST_BASE_DIRECTORY, "paketv1x.eyp");

        public static string PAKET_V2X_FILE_PATH => Path.Combine(TEST_BASE_DIRECTORY, "paketv2x.eyp");

        public static string SIFRELI_PAKET_V1X_FILE_PATH => Path.Combine(TEST_BASE_DIRECTORY, "sifrelipaketv1x.eyps");

        public static string SIFRELI_PAKET_V2X_FILE_PATH => Path.Combine(TEST_BASE_DIRECTORY, "sifrelipaketv2x.eyps");

        public static string SIFRELI_PAKET_V1X_SIFRELI_ICERIK_BOS_FILE_PATH =>
            Path.Combine(TEST_BASE_DIRECTORY, "sifrelipaketv1xicerikbos.eyps");

        public static string SIFRELI_PAKET_V2X_SIFRELI_ICERIK_BOS_FILE_PATH =>
            Path.Combine(TEST_BASE_DIRECTORY, "sifrelipaketv2xicerikbos.eyps");

        public static byte[] PAKET_V1X_BYTE_ARRAY()
        {
            using (var ms = new MemoryStream())
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
                        .AdIle(IsimTip.Kilavuz.DegerAta("TÜRKİYE VAKIFLAR BANKASI TÜRK ANONİM ORTAKLIĞI-KIZILAY ŞUBESİ")
                            .Olustur())
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
                    .DosyaAdiAta(EK1_FILE_NAME)
                    .MimeTuruAta(MIME_TURU_PDF)
                    .AdIle(MetinTip.Kilavuz.DegerAta("Ek 1").Olustur())
                    .SiraNoAta(1)
                    .ImzaliMiAta(true)
                    .Olustur();

                var ek2 = Ek.Kilavuz
                    .DahiliElektronikDosya()
                    .IdAta(IdTip.Kilavuz.DegerAta(Guid.NewGuid().ToString()).EYazismaIdMiAta(false).Olustur())
                    .DosyaAdiAta(EK2_FILE_NAME)
                    .MimeTuruAta(MIME_TURU_PDF)
                    .AdIle(MetinTip.Kilavuz.DegerAta("Ek 2").Olustur())
                    .SiraNoAta(2)
                    .ImzaliMiAta(true)
                    .Olustur();

                Paket.Olustur(ms)
                    .Versiyon1X()
                    .UstYaziAta(UstYazi.Kilavuz
                        .DosyaAta(USTYAZI_FILE_PATH)
                        .DosyaAdiAta(USTYAZI_FILE_NAME)
                        .MimeTuruAta(MIME_TURU_PDF)
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
                        .DosyaAdiAta(belgeId + ".eyp")
                        .Olustur())
                    .BelgeHedefAta(BelgeHedef.Kilavuz
                        .HedefEkle(Hedef.Kilavuz.OgeAta((TuzelSahis) dagitim2.Oge).Olustur())
                        .DigerHedefEkle(Hedef.Kilavuz.OgeAta((KurumKurulus) dagitim3.Oge).Olustur())
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
                        .DosyaAta(EK1_FILE_PATH)
                        .DosyaAdiAta(EK1_FILE_NAME)
                        .Olustur())
                    .EkDosyaIle(EkDosya.Kilavuz
                        .EkAta(ek2)
                        .DosyaAta(EK2_FILE_PATH)
                        .DosyaAdiAta(EK2_FILE_NAME)
                        .Olustur())
                    .BilesenleriOlustur()
                    .ImzaEkle(paketOzetiStream => { return new byte[] {1, 2, 3, 4}; })
                    .MuhurEkle(nihaiOzetStream => { return new byte[] {1, 2, 3, 4}; })
                    .Dogrula((kritikHataVarMi, sonuclar) =>
                    {
                        if (kritikHataVarMi)
                            throw new ApplicationException("Paket oluşturulurken kritik hata oluşmuştur.");
                    })
                    .Kapat();

                return ms.ToArray();
            }
        }

        public static byte[] PAKET_V2X_BYTE_ARRAY()
        {
            using (var ms = new MemoryStream())
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
                        .AdIle(IsimTip.Kilavuz.DegerAta("TÜRKİYE VAKIFLAR BANKASI TÜRK ANONİM ORTAKLIĞI-KIZILAY ŞUBESİ")
                            .Olustur())
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
                    .DosyaAdiAta(EK1_FILE_NAME)
                    .MimeTuruAta(MIME_TURU_PDF)
                    .AdIle(MetinTip.Kilavuz.DegerAta("Ek 1").Olustur())
                    .SiraNoAta(1)
                    .ImzaliMiAta(true)
                    .Olustur();

                var ek2 = Ek.Kilavuz
                    .DahiliElektronikDosya()
                    .IdAta(IdTip.Kilavuz.DegerAta(Guid.NewGuid().ToString()).EYazismaIdMiAta(false).Olustur())
                    .DosyaAdiAta(EK2_FILE_NAME)
                    .MimeTuruAta(MIME_TURU_PDF)
                    .AdIle(MetinTip.Kilavuz.DegerAta("Ek 2").Olustur())
                    .SiraNoAta(2)
                    .ImzaliMiAta(true)
                    .Olustur();

                Paket.Olustur(ms)
                    .Versiyon2X()
                    .UstYaziAta(UstYazi.Kilavuz
                        .DosyaAta(USTYAZI_FILE_PATH)
                        .DosyaAdiAta(USTYAZI_FILE_NAME)
                        .MimeTuruAta(MIME_TURU_PDF)
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
                        .DosyaAdiAta(belgeId + ".eyp")
                        .DogrulamaBilgisiAta(DogrulamaBilgisi.Kilavuz.AdresAta("").Olustur())
                        .Olustur())
                    .EkDosyaIle(EkDosya.Kilavuz
                        .EkAta(ek1)
                        .DosyaAta(EK1_FILE_PATH)
                        .DosyaAdiAta(EK1_FILE_NAME)
                        .Olustur())
                    .EkDosyaIle(EkDosya.Kilavuz
                        .EkAta(ek2)
                        .DosyaAta(EK2_FILE_PATH)
                        .DosyaAdiAta(EK2_FILE_NAME)
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
                    .MuhurEkle(nihaiOzetStream => { return new byte[] {1, 2, 3, 4}; })
                    .Dogrula((kritikHataVarMi, sonuclar) =>
                    {
                        if (kritikHataVarMi)
                            throw new ApplicationException("Paket oluşturulurken kritik hata oluşmuştur.");
                    })
                    .Kapat();

                return ms.ToArray();
            }
        }

        public static byte[] SIFRELI_PAKET_V1X_BYTE_ARRAY()
        {
            using (var paketStreamIn = new MemoryStream(PAKET_V1X_BYTE_ARRAY()))
            {
                using (var paketStream = new MemoryStream())
                {
                    paketStreamIn.CopyTo(paketStream);
                    using (var sifreliPaketStream = new MemoryStream())
                    {
                        PaketV1X.Oku(paketStream)
                            .BilesenleriAl((kritikHataVarMi, bilesenler, tumHatalar) =>
                            {
                                SifreliPaketV1X.Olustur(sifreliPaketStream)
                                    .PaketOzetiEkle(bilesenler.PaketOzetiAl())
                                    .SifreliIcerikEkle(paketStreamIn, bilesenler.Ustveri.BelgeId)
                                    .OlusturanAta(bilesenler.Ustveri.Olusturan)
                                    .BelgeHedefIle(bilesenler.BelgeHedef)
                                    .BilesenleriOlustur()
                                    .Dogrula((hataVarMi, hatalar) =>
                                    {
                                        if (hataVarMi)
                                            throw new ApplicationException(
                                                "Şifreli paket oluşturulurken kritik hata oluşmuştur.");
                                    })
                                    .Kapat();
                            })
                            .Kapat();
                        return sifreliPaketStream.ToArray();
                    }
                }
            }
        }

        public static byte[] SIFRELI_PAKET_V2X_BYTE_ARRAY()
        {
            using (var paketStreamIn = new MemoryStream(PAKET_V2X_BYTE_ARRAY()))
            {
                using (var paketStream = new MemoryStream())
                {
                    paketStreamIn.CopyTo(paketStream);
                    using (var sifreliPaketStream = new MemoryStream())
                    {
                        PaketV2X.Oku(paketStream)
                            .BilesenleriAl((kritikHataVarMi, bilesenler, tumHatalar) =>
                            {
                                SifreliPaketV2X.Olustur(sifreliPaketStream)
                                    .NihaiOzetEkle(bilesenler.NihaiOzetAl())
                                    .SifreliIcerikEkle(paketStreamIn, bilesenler.Ustveri.BelgeId)
                                    .OlusturanAta(bilesenler.Ustveri.Olusturan)
                                    .BelgeHedefIle(BelgeHedef.Kilavuz
                                        .HedeflerEkle(bilesenler.Ustveri.Dagitimlar.ToHedefler())
                                        .Olustur())
                                    .BilesenleriOlustur()
                                    .Dogrula((hataVarMi, hatalar) =>
                                    {
                                        if (hataVarMi)
                                            throw new ApplicationException(
                                                "Şifreli paket oluşturulurken kritik hata oluşmuştur.");
                                    })
                                    .Kapat();
                            })
                            .Kapat();
                        return sifreliPaketStream.ToArray();
                    }
                }
            }
        }
    }
}