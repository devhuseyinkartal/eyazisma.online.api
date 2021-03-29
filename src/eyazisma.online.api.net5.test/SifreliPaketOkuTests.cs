using System;
using System.IO;
using eyazisma.online.api.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eyazisma.online.api.net5.test
{
    [TestClass]
    public class SifreliPaketOkuTests
    {
        [TestMethod]
        public void SifreliPaket_VersiyonAl_PaketDosyaYoluBos()
        {
            Assert.ThrowsException<ArgumentNullException>(() => { SifreliPaket.SifreliPaketVersiyonuAl(""); });
        }

        [TestMethod]
        public void SifreliPaket_VersiyonAl_PaketDosyaYoluGecersiz()
        {
            Assert.ThrowsException<FileNotFoundException>(() =>
            {
                SifreliPaket.SifreliPaketVersiyonuAl(@"C:\fakepath.eyps");
            });
        }

        [TestMethod]
        public void SifreliPaket_VersiyonAl_Versiyon1XBasarili()
        {
            using (var ms = new MemoryStream(TestComponents.SIFRELI_PAKET_V1X_BYTE_ARRAY()))
            {
                Assert.AreEqual(PaketVersiyonTuru.Versiyon1X, SifreliPaket.SifreliPaketVersiyonuAl(ms));
            }

            Assert.AreEqual(PaketVersiyonTuru.Versiyon1X,
                SifreliPaket.SifreliPaketVersiyonuAl(TestComponents.SIFRELI_PAKET_V1X_FILE_PATH));
        }

        [TestMethod]
        public void SifreliPaket_VersiyonAl_Versiyon1XBasarisiz()
        {
            using (var ms = new MemoryStream(TestComponents.SIFRELI_PAKET_V2X_BYTE_ARRAY()))
            {
                Assert.AreNotEqual(PaketVersiyonTuru.Versiyon1X, SifreliPaket.SifreliPaketVersiyonuAl(ms));
            }

            Assert.AreNotEqual(PaketVersiyonTuru.Versiyon1X,
                SifreliPaket.SifreliPaketVersiyonuAl(TestComponents.SIFRELI_PAKET_V2X_FILE_PATH));
        }

        [TestMethod]
        public void SifreliPaket_VersiyonAl_Versiyon2XBasarili()
        {
            using (var ms = new MemoryStream(TestComponents.SIFRELI_PAKET_V2X_BYTE_ARRAY()))
            {
                Assert.AreEqual(PaketVersiyonTuru.Versiyon2X, SifreliPaket.SifreliPaketVersiyonuAl(ms));
            }

            Assert.AreEqual(PaketVersiyonTuru.Versiyon2X,
                SifreliPaket.SifreliPaketVersiyonuAl(TestComponents.SIFRELI_PAKET_V2X_FILE_PATH));
        }

        [TestMethod]
        public void SifreliPaket_VersiyonAl_Versiyon2XBasarisiz()
        {
            using (var ms = new MemoryStream(TestComponents.SIFRELI_PAKET_V1X_BYTE_ARRAY()))
            {
                Assert.AreNotEqual(PaketVersiyonTuru.Versiyon2X, SifreliPaket.SifreliPaketVersiyonuAl(ms));
            }

            Assert.AreNotEqual(PaketVersiyonTuru.Versiyon2X,
                SifreliPaket.SifreliPaketVersiyonuAl(TestComponents.SIFRELI_PAKET_V1X_FILE_PATH));
        }

        [TestMethod]
        public void SifreliPaket_SifreliIcerikAl_PaketDosyaYoluBos()
        {
            Assert.ThrowsException<ArgumentNullException>(() => { SifreliPaket.SifreliIcerikAl(""); });
        }

        [TestMethod]
        public void SifreliPaket_SifreliIcerikAl_PaketDosyaYoluGecersiz()
        {
            Assert.ThrowsException<FileNotFoundException>(() => { SifreliPaket.SifreliIcerikAl(@"C:\fakepath.eyps"); });
        }

        [TestMethod]
        public void SifreliPaket_SifreliIcerilAl_Versiyon1XBasarili()
        {
            using (var ms = new MemoryStream(TestComponents.SIFRELI_PAKET_V1X_BYTE_ARRAY()))
            {
                var sifreliIcerik = SifreliPaket.SifreliIcerikAl(ms);

                Assert.IsTrue(sifreliIcerik != null && sifreliIcerik.Length > 0);
            }

            var sifreliIcerik2 = SifreliPaket.SifreliIcerikAl(TestComponents.SIFRELI_PAKET_V1X_FILE_PATH);

            Assert.IsTrue(sifreliIcerik2 != null && sifreliIcerik2.Length > 0);
        }

        [TestMethod]
        public void SifreliPaket_SifreliIcerilAl_Versiyon1XBasarisiz()
        {
            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                SifreliPaket.SifreliIcerikAl(TestComponents.SIFRELI_PAKET_V1X_SIFRELI_ICERIK_BOS_FILE_PATH);
            });
        }

        [TestMethod]
        public void SifreliPaket_SifreliIcerilAl_Versiyon2XBasarili()
        {
            using (var ms = new MemoryStream(TestComponents.SIFRELI_PAKET_V2X_BYTE_ARRAY()))
            {
                var sifreliIcerik = SifreliPaket.SifreliIcerikAl(ms);

                Assert.IsTrue(sifreliIcerik != null && sifreliIcerik.Length > 0);
            }

            var sifreliIcerik2 = SifreliPaket.SifreliIcerikAl(TestComponents.SIFRELI_PAKET_V2X_FILE_PATH);

            Assert.IsTrue(sifreliIcerik2 != null && sifreliIcerik2.Length > 0);
        }

        [TestMethod]
        public void SifreliPaket_SifreliIcerilAl_Versiyon2XBasarisiz()
        {
            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                SifreliPaket.SifreliIcerikAl(TestComponents.SIFRELI_PAKET_V2X_SIFRELI_ICERIK_BOS_FILE_PATH);
            });
        }

        [TestMethod]
        public void SifreliPaket_Oku_Versiyon1X()
        {
            using (var sifreliPaketStream = new MemoryStream(TestComponents.SIFRELI_PAKET_V1X_BYTE_ARRAY()))
            {
                SifreliPaket.Oku(sifreliPaketStream)
                    .Versiyon1XIse((sKritikHataVarMi, sBilesenler, sTumHatalar) =>
                    {
                        Assert.IsFalse(sKritikHataVarMi);
                    })
                    .Versiyon2XIse((sKritikHataVarMi, sBilesenler, sTumHatalar) =>
                    {
                        Assert.Fail("Şifreli paket versiyonu yanlış okunmuştur.");
                    })
                    .Kapat();
            }

            SifreliPaket.Oku(TestComponents.SIFRELI_PAKET_V1X_FILE_PATH)
                .Versiyon1XIse((sKritikHataVarMi, sBilesenler, sTumHatalar) => { Assert.IsFalse(sKritikHataVarMi); })
                .Versiyon2XIse((sKritikHataVarMi, sBilesenler, sTumHatalar) =>
                {
                    Assert.Fail("Şifreli paket versiyonu yanlış okunmuştur.");
                })
                .Kapat();
        }

        [TestMethod]
        public void SifreliPaket_Oku_Versiyon2X()
        {
            using (var sifreliPaketStream = new MemoryStream(TestComponents.SIFRELI_PAKET_V2X_BYTE_ARRAY()))
            {
                SifreliPaket.Oku(sifreliPaketStream)
                    .Versiyon1XIse((sKritikHataVarMi, sBilesenler, sTumHatalar) =>
                    {
                        Assert.Fail("Şifreli paket versiyonu yanlış okunmuştur.");
                    })
                    .Versiyon2XIse((sKritikHataVarMi, sBilesenler, sTumHatalar) =>
                    {
                        Assert.IsFalse(sKritikHataVarMi);
                    })
                    .Kapat();
            }

            SifreliPaket.Oku(TestComponents.SIFRELI_PAKET_V2X_FILE_PATH)
                .Versiyon1XIse((sKritikHataVarMi, sBilesenler, sTumHatalar) =>
                {
                    Assert.Fail("Şifreli paket versiyonu yanlış okunmuştur.");
                })
                .Versiyon2XIse((sKritikHataVarMi, sBilesenler, sTumHatalar) => { Assert.IsFalse(sKritikHataVarMi); })
                .Kapat();
        }

        [TestMethod]
        public void SifreliPaketV1X_Oku_PaketDosyaYoluBos()
        {
            Assert.ThrowsException<ArgumentNullException>(() => { SifreliPaketV1X.Oku(""); });
        }

        [TestMethod]
        public void SifreliPaketV1X_Oku_PaketDosyaYoluGecersiz()
        {
            Assert.ThrowsException<FileNotFoundException>(() => { SifreliPaketV1X.Oku(@"C:\fakepath.eyps"); });
        }

        [TestMethod]
        public void SifreliPaketV1X_Oku()
        {
            using (var sifreliPaketStream = new MemoryStream(TestComponents.SIFRELI_PAKET_V1X_BYTE_ARRAY()))
            {
                SifreliPaketV1X.Oku(sifreliPaketStream)
                    .BilesenleriAl((sKritikHataVarMi, sBilesenler, sTumHatalar) =>
                    {
                        Assert.IsFalse(sKritikHataVarMi);
                    })
                    .Kapat();
            }

            SifreliPaketV1X.Oku(TestComponents.SIFRELI_PAKET_V1X_FILE_PATH)
                .BilesenleriAl((sKritikHataVarMi, sBilesenler, sTumHatalar) => { Assert.IsFalse(sKritikHataVarMi); })
                .Kapat();
        }

        [TestMethod]
        public void SifreliPaketV2X_Oku_PaketDosyaYoluBos()
        {
            Assert.ThrowsException<ArgumentNullException>(() => { SifreliPaketV2X.Oku(""); });
        }

        [TestMethod]
        public void SifreliPaketV2X_Oku_PaketDosyaYoluGecersiz()
        {
            Assert.ThrowsException<FileNotFoundException>(() => { SifreliPaketV2X.Oku(@"C:\fakepath.eyps"); });
        }

        [TestMethod]
        public void SifreliPaketV2X_Oku()
        {
            using (var sifreliPaketStream = new MemoryStream(TestComponents.SIFRELI_PAKET_V2X_BYTE_ARRAY()))
            {
                SifreliPaketV2X.Oku(sifreliPaketStream)
                    .BilesenleriAl((sKritikHataVarMi, sBilesenler, sTumHatalar) =>
                    {
                        Assert.IsFalse(sKritikHataVarMi);
                    })
                    .Kapat();
            }

            SifreliPaketV2X.Oku(TestComponents.SIFRELI_PAKET_V2X_FILE_PATH)
                .BilesenleriAl((sKritikHataVarMi, sBilesenler, sTumHatalar) => { Assert.IsFalse(sKritikHataVarMi); })
                .Kapat();
        }
    }
}