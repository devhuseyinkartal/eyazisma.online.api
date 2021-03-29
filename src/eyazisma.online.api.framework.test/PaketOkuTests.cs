using System;
using System.IO;
using eyazisma.online.api.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eyazisma.online.api.framework.test
{
    [TestClass]
    public class PaketOkuTests
    {
        [TestMethod]
        public void Paket_VersiyonAl_PaketDosyaYoluBos()
        {
            Assert.ThrowsException<ArgumentNullException>(() => { Paket.PaketVersiyonuAl(""); });
        }

        [TestMethod]
        public void Paket_VersiyonAl_PaketDosyaYoluGecersiz()
        {
            Assert.ThrowsException<FileNotFoundException>(() => { Paket.PaketVersiyonuAl(@"C:\fakepath.eyps"); });
        }

        [TestMethod]
        public void Paket_VersiyonAl_Versiyon1XBasarili()
        {
            using (var ms = new MemoryStream(TestComponents.PAKET_V1X_BYTE_ARRAY()))
            {
                Assert.AreEqual(PaketVersiyonTuru.Versiyon1X, Paket.PaketVersiyonuAl(ms));
            }

            Assert.AreEqual(PaketVersiyonTuru.Versiyon1X, Paket.PaketVersiyonuAl(TestComponents.PAKET_V1X_FILE_PATH));
        }

        [TestMethod]
        public void Paket_VersiyonAl_Versiyon1XBasarisiz()
        {
            using (var ms = new MemoryStream(TestComponents.PAKET_V2X_BYTE_ARRAY()))
            {
                Assert.AreNotEqual(PaketVersiyonTuru.Versiyon1X, Paket.PaketVersiyonuAl(ms));
            }

            Assert.AreNotEqual(PaketVersiyonTuru.Versiyon1X,
                Paket.PaketVersiyonuAl(TestComponents.PAKET_V2X_FILE_PATH));
        }

        [TestMethod]
        public void Paket_VersiyonAl_Versiyon2XBasarili()
        {
            using (var ms = new MemoryStream(TestComponents.PAKET_V2X_BYTE_ARRAY()))
            {
                Assert.AreEqual(PaketVersiyonTuru.Versiyon2X, Paket.PaketVersiyonuAl(ms));
            }

            Assert.AreEqual(PaketVersiyonTuru.Versiyon2X, Paket.PaketVersiyonuAl(TestComponents.PAKET_V2X_FILE_PATH));
        }

        [TestMethod]
        public void Paket_VersiyonAl_Versiyon2XBasarisiz()
        {
            using (var ms = new MemoryStream(TestComponents.PAKET_V1X_BYTE_ARRAY()))
            {
                Assert.AreNotEqual(PaketVersiyonTuru.Versiyon2X, Paket.PaketVersiyonuAl(ms));
            }

            Assert.AreNotEqual(PaketVersiyonTuru.Versiyon2X,
                Paket.PaketVersiyonuAl(TestComponents.PAKET_V1X_FILE_PATH));
        }

        [TestMethod]
        public void Paket_Oku_Versiyon1X()
        {
            using (var paketStream = new MemoryStream(TestComponents.PAKET_V1X_BYTE_ARRAY()))
            {
                Paket.Oku(paketStream)
                    .Versiyon1XIse((sKritikHataVarMi, sBilesenler, sTumHatalar) =>
                    {
                        Assert.IsFalse(sKritikHataVarMi);
                    })
                    .Versiyon2XIse((sKritikHataVarMi, sBilesenler, sTumHatalar) =>
                    {
                        Assert.Fail("Paket versiyonu yanlış okunmuştur.");
                    })
                    .Kapat();
            }

            Paket.Oku(TestComponents.PAKET_V1X_FILE_PATH)
                .Versiyon1XIse((sKritikHataVarMi, sBilesenler, sTumHatalar) => { Assert.IsFalse(sKritikHataVarMi); })
                .Versiyon2XIse((sKritikHataVarMi, sBilesenler, sTumHatalar) =>
                {
                    Assert.Fail("Paket versiyonu yanlış okunmuştur.");
                })
                .Kapat();
        }

        [TestMethod]
        public void Paket_Oku_Versiyon2X()
        {
            using (var paketStream = new MemoryStream(TestComponents.PAKET_V2X_BYTE_ARRAY()))
            {
                Paket.Oku(paketStream)
                    .Versiyon1XIse((sKritikHataVarMi, sBilesenler, sTumHatalar) =>
                    {
                        Assert.Fail("Paket versiyonu yanlış okunmuştur.");
                    })
                    .Versiyon2XIse((sKritikHataVarMi, sBilesenler, sTumHatalar) =>
                    {
                        Assert.IsFalse(sKritikHataVarMi);
                    })
                    .Kapat();
            }

            Paket.Oku(TestComponents.PAKET_V2X_FILE_PATH)
                .Versiyon1XIse((sKritikHataVarMi, sBilesenler, sTumHatalar) =>
                {
                    Assert.Fail("Paket versiyonu yanlış okunmuştur.");
                })
                .Versiyon2XIse((sKritikHataVarMi, sBilesenler, sTumHatalar) => { Assert.IsFalse(sKritikHataVarMi); })
                .Kapat();
        }

        [TestMethod]
        public void PaketV1X_Oku_PaketDosyaYoluBos()
        {
            Assert.ThrowsException<ArgumentNullException>(() => { PaketV1X.Oku(""); });
        }

        [TestMethod]
        public void PaketV1X_Oku_PaketDosyaYoluGecersiz()
        {
            Assert.ThrowsException<FileNotFoundException>(() => { PaketV1X.Oku(@"C:\fakepath.eyps"); });
        }

        [TestMethod]
        public void PaketV1X_Oku()
        {
            using (var paketStream = new MemoryStream(TestComponents.PAKET_V1X_BYTE_ARRAY()))
            {
                PaketV1X.Oku(paketStream)
                    .BilesenleriAl((sKritikHataVarMi, sBilesenler, sTumHatalar) =>
                    {
                        Assert.IsFalse(sKritikHataVarMi);
                    })
                    .Kapat();
            }

            PaketV1X.Oku(TestComponents.PAKET_V1X_FILE_PATH)
                .BilesenleriAl((sKritikHataVarMi, sBilesenler, sTumHatalar) => { Assert.IsFalse(sKritikHataVarMi); })
                .Kapat();
        }

        [TestMethod]
        public void PaketV2X_Oku_PaketDosyaYoluBos()
        {
            Assert.ThrowsException<ArgumentNullException>(() => { PaketV2X.Oku(""); });
        }

        [TestMethod]
        public void PaketV2X_Oku_PaketDosyaYoluGecersiz()
        {
            Assert.ThrowsException<FileNotFoundException>(() => { PaketV2X.Oku(@"C:\fakepath.eyps"); });
        }

        [TestMethod]
        public void PaketV2X_Oku()
        {
            using (var paketStream = new MemoryStream(TestComponents.PAKET_V2X_BYTE_ARRAY()))
            {
                PaketV2X.Oku(paketStream)
                    .BilesenleriAl((sKritikHataVarMi, sBilesenler, sTumHatalar) =>
                    {
                        Assert.IsFalse(sKritikHataVarMi);
                    })
                    .Kapat();
            }

            PaketV2X.Oku(TestComponents.PAKET_V2X_FILE_PATH)
                .BilesenleriAl((sKritikHataVarMi, sBilesenler, sTumHatalar) => { Assert.IsFalse(sKritikHataVarMi); })
                .Kapat();
        }
    }
}