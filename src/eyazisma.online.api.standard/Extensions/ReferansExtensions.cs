using System;
using System.Collections.Generic;
using eyazisma.online.api.Classes;
using eyazisma.online.api.Enums;

namespace eyazisma.online.api.Extensions
{
    public static class ReferansExtensions
    {
        public static void ReferansEkle(this PaketOzeti paketOzeti,
            PaketVersiyonTuru paketVersiyon,
            OzetAlgoritmaTuru ozetAlgoritma,
            byte[] ozetDegeri,
            Uri uri,
            byte[] ozetDegeriSha512 = null,
            bool hariciBilesenMi = false)
        {
            if (ozetAlgoritma == OzetAlgoritmaTuru.YOK)
                throw new ArgumentException(
                    nameof(ozetAlgoritma) + " değeri " + nameof(OzetAlgoritmaTuru.YOK) + " olmamalıdır.",
                    nameof(ozetAlgoritma));
            if (ozetAlgoritma == OzetAlgoritmaTuru.SHA384 && paketVersiyon == PaketVersiyonTuru.Versiyon1X)
                throw new ArgumentException(
                    nameof(OzetAlgoritmaTuru.SHA384) +
                    " özet algoritması yalnızca e-Yazışma API 2.X versiyonlarında kullanılabilir.",
                    nameof(ozetAlgoritma));

            if (paketOzeti.Referanslar != null && paketOzeti.Referanslar.Count > 0)
            {
                var oncekiReferans = paketOzeti.Referanslar.Find(p =>
                    string.Compare(p.URI, uri.ToString(), StringComparison.InvariantCultureIgnoreCase) == 0);
                if (oncekiReferans != default(Referans))
                    paketOzeti.Referanslar.Remove(oncekiReferans);
            }
            else
            {
                paketOzeti.Referanslar = new List<Referans>();
            }

            var yeniReferans = new Referans
            {
                Ozet = Ozet.Kilavuz.OzetAlgoritmasiAta(OzetAlgoritmasi.Kilavuz.AlgoritmaAta(ozetAlgoritma).Olustur())
                    .OzetDegeriAta(ozetDegeri).Olustur(),
                URI = uri.ToString(),
                Type = hariciBilesenMi
                    ? Constants.HARICI_PAKET_BILESENI_REFERANS_TIPI
                    : Constants.DAHILI_PAKET_BILESENI_REFERANS_TIPI
            };

            if (paketVersiyon == PaketVersiyonTuru.Versiyon2X)
                yeniReferans.Ozet1 = Ozet.Kilavuz
                    .OzetAlgoritmasiAta(OzetAlgoritmasi.Kilavuz.AlgoritmaAta(OzetAlgoritmaTuru.SHA512).Olustur())
                    .OzetDegeriAta(ozetDegeriSha512).Olustur();

            paketOzeti.Referanslar.Add(yeniReferans);
        }

        public static void ReferansEkle(this ParafOzeti parafOzeti,
            PaketVersiyonTuru paketVersiyon,
            OzetAlgoritmaTuru ozetAlgoritma,
            byte[] ozetDegeri,
            Uri uri,
            byte[] ozetDegeriSha512 = null,
            bool hariciBilesenMi = false)
        {
            if (ozetAlgoritma == OzetAlgoritmaTuru.YOK)
                throw new ArgumentException(
                    nameof(ozetAlgoritma) + " değeri " + nameof(OzetAlgoritmaTuru.YOK) + " olmamalıdır.",
                    nameof(ozetAlgoritma));
            if (ozetAlgoritma == OzetAlgoritmaTuru.SHA384 && paketVersiyon == PaketVersiyonTuru.Versiyon1X)
                throw new ArgumentException(
                    nameof(OzetAlgoritmaTuru.SHA384) +
                    " özet algoritması yalnızca e-Yazışma API 2.X versiyonlarında kullanılabilir.",
                    nameof(ozetAlgoritma));

            if (parafOzeti.Referanslar != null && parafOzeti.Referanslar.Count > 0)
            {
                var oncekiReferans = parafOzeti.Referanslar.Find(p =>
                    string.Compare(p.URI, uri.ToString(), StringComparison.InvariantCultureIgnoreCase) == 0);
                if (oncekiReferans != default(Referans))
                    parafOzeti.Referanslar.Remove(oncekiReferans);
            }
            else
            {
                parafOzeti.Referanslar = new List<Referans>();
            }

            var yeniReferans = new Referans
            {
                Ozet = Ozet.Kilavuz.OzetAlgoritmasiAta(OzetAlgoritmasi.Kilavuz.AlgoritmaAta(ozetAlgoritma).Olustur())
                    .OzetDegeriAta(ozetDegeri).Olustur(),
                URI = uri.ToString(),
                Type = hariciBilesenMi
                    ? Constants.HARICI_PAKET_BILESENI_REFERANS_TIPI
                    : Constants.DAHILI_PAKET_BILESENI_REFERANS_TIPI
            };

            if (paketVersiyon == PaketVersiyonTuru.Versiyon2X)
                yeniReferans.Ozet1 = Ozet.Kilavuz
                    .OzetAlgoritmasiAta(OzetAlgoritmasi.Kilavuz.AlgoritmaAta(OzetAlgoritmaTuru.SHA512).Olustur())
                    .OzetDegeriAta(ozetDegeriSha512).Olustur();

            parafOzeti.Referanslar.Add(yeniReferans);
        }

        public static void ReferansEkle(this NihaiOzet nihaiOzet,
            PaketVersiyonTuru paketVersiyon,
            OzetAlgoritmaTuru ozetAlgoritma,
            byte[] ozetDegeri,
            Uri uri,
            byte[] ozetDegeriSha512 = null)
        {
            if (ozetAlgoritma == OzetAlgoritmaTuru.YOK)
                throw new ArgumentException(
                    nameof(ozetAlgoritma) + " değeri " + nameof(OzetAlgoritmaTuru.YOK) + " olmamalıdır.",
                    nameof(ozetAlgoritma));
            if (ozetAlgoritma == OzetAlgoritmaTuru.SHA384 && paketVersiyon == PaketVersiyonTuru.Versiyon1X)
                throw new ArgumentException(
                    nameof(OzetAlgoritmaTuru.SHA384) +
                    " özet algoritması yalnızca e-Yazışma API 2.X versiyonlarında kullanılabilir.",
                    nameof(ozetAlgoritma));

            if (nihaiOzet.Referanslar != null && nihaiOzet.Referanslar.Count > 0)
            {
                var oncekiReferans = nihaiOzet.Referanslar.Find(p =>
                    string.Compare(p.URI, uri.ToString(), StringComparison.InvariantCultureIgnoreCase) == 0);
                if (oncekiReferans != default(Referans))
                    nihaiOzet.Referanslar.Remove(oncekiReferans);
            }
            else
            {
                nihaiOzet.Referanslar = new List<Referans>();
            }

            var yeniReferans = new Referans
            {
                Ozet = Ozet.Kilavuz.OzetAlgoritmasiAta(OzetAlgoritmasi.Kilavuz.AlgoritmaAta(ozetAlgoritma).Olustur())
                    .OzetDegeriAta(ozetDegeri).Olustur(),
                URI = uri.ToString(),
                Type = Constants.DAHILI_PAKET_BILESENI_REFERANS_TIPI
            };

            if (paketVersiyon == PaketVersiyonTuru.Versiyon2X)
                yeniReferans.Ozet1 = Ozet.Kilavuz
                    .OzetAlgoritmasiAta(OzetAlgoritmasi.Kilavuz.AlgoritmaAta(OzetAlgoritmaTuru.SHA512).Olustur())
                    .OzetDegeriAta(ozetDegeriSha512).Olustur();

            nihaiOzet.Referanslar.Add(yeniReferans);
        }
    }
}