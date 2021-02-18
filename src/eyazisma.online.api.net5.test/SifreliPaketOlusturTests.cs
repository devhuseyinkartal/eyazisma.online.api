using eyazisma.online.api.Classes;
using eyazisma.online.api.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace eyazisma.online.api.net5.test
{
    [TestClass]
    public class SifreliPaketOlusturTests
    {
        [TestMethod]
        public void SifreliPaket_Olustur_Versiyon1X()
        {
            using (MemoryStream paketStreamIn = new MemoryStream(TestComponents.PAKET_V1X_BYTE_ARRAY()))
            {
                using (var paketStream = new MemoryStream())
                {
                    paketStreamIn.CopyTo(paketStream);
                    using (MemoryStream sifreliPaketStream = new MemoryStream())
                    {
                        PaketV1X.Oku(paketStream)
                                .BilesenleriAl((kritikHataVarMi, bilesenler, tumHatalar) =>
                                {

                                    SifreliPaket.Olustur(sifreliPaketStream)
                                                .Versiyon1X()
                                                .PaketOzetiEkle(bilesenler.PaketOzetiAl())
                                                .SifreliIcerikEkle(paketStreamIn, bilesenler.Ustveri.BelgeId)
                                                .OlusturanAta(bilesenler.Ustveri.Olusturan)
                                                .BelgeHedefIle(bilesenler.BelgeHedef)
                                                .BilesenleriOlustur()
                                                .Dogrula((hataVarMi, hatalar) =>
                                                {
                                                    Assert.IsFalse(hataVarMi);
                                                })
                                                .Kapat();
                                })
                                .Kapat();
                    }
                }
            }
        }

        [TestMethod]
        public void SifreliPaket_Olustur_Versiyon2X()
        {
            using (MemoryStream paketStreamIn = new MemoryStream(TestComponents.PAKET_V2X_BYTE_ARRAY()))
            {
                using (var paketStream = new MemoryStream())
                {
                    paketStreamIn.CopyTo(paketStream);
                    using (MemoryStream sifreliPaketStream = new MemoryStream())
                    {
                        PaketV2X.Oku(paketStream)
                                .BilesenleriAl((kritikHataVarMi, bilesenler, tumHatalar) =>
                                {

                                    SifreliPaket.Olustur(sifreliPaketStream)
                                                .Versiyon2X()
                                                .NihaiOzetEkle(bilesenler.NihaiOzetAl())
                                                .SifreliIcerikEkle(paketStreamIn, bilesenler.Ustveri.BelgeId)
                                                .OlusturanAta(bilesenler.Ustveri.Olusturan)
                                                .BelgeHedefIle(BelgeHedef.Kilavuz.HedeflerEkle(bilesenler.Ustveri.Dagitimlar.ToHedefler()).Olustur())
                                                .BilesenleriOlustur()
                                                .Dogrula((hataVarMi, hatalar) =>
                                                {
                                                    Assert.IsFalse(hataVarMi);
                                                })
                                                .Kapat();
                                })
                                .Kapat();
                    }
                }
            }
        }

        [TestMethod]
        public void SifreliPaketV1X_Olustur()
        {
            using (MemoryStream paketStreamIn = new MemoryStream(TestComponents.PAKET_V1X_BYTE_ARRAY()))
            {
                using (var paketStream = new MemoryStream())
                {
                    paketStreamIn.CopyTo(paketStream);
                    using (MemoryStream sifreliPaketStream = new MemoryStream())
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
                                                       Assert.IsFalse(hataVarMi);
                                                   })
                                                   .Kapat();
                                })
                                .Kapat();
                    }
                }
            }
        }

        [TestMethod]
        public void SifreliPaketV2X_Olustur()
        {
            using (MemoryStream paketStreamIn = new MemoryStream(TestComponents.PAKET_V2X_BYTE_ARRAY()))
            {
                using (var paketStream = new MemoryStream())
                {
                    paketStreamIn.CopyTo(paketStream);
                    using (MemoryStream sifreliPaketStream = new MemoryStream())
                    {
                        PaketV2X.Oku(paketStream)
                                .BilesenleriAl((kritikHataVarMi, bilesenler, tumHatalar) =>
                                {

                                    SifreliPaketV2X.Olustur(sifreliPaketStream)
                                                   .NihaiOzetEkle(bilesenler.NihaiOzetAl())
                                                   .SifreliIcerikEkle(paketStreamIn, bilesenler.Ustveri.BelgeId)
                                                   .OlusturanAta(bilesenler.Ustveri.Olusturan)
                                                   .BelgeHedefIle(BelgeHedef.Kilavuz.HedeflerEkle(bilesenler.Ustveri.Dagitimlar.ToHedefler()).Olustur())
                                                   .BilesenleriOlustur()
                                                   .Dogrula((hataVarMi, hatalar) =>
                                                   {
                                                       Assert.IsFalse(hataVarMi);
                                                   })
                                                   .Kapat();
                                })
                                .Kapat();
                    }
                }
            }
        }
    }
}
