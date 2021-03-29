using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text.RegularExpressions;
using eyazisma.online.api.Classes;
using eyazisma.online.api.Enums;

namespace eyazisma.online.api.Extensions
{
    public static class DogrulamaExtensions
    {
        public static bool Dogrula(this KurumKurulus kurumKurulus, PaketVersiyonTuru paketVersiyon,
            ref List<DogrulamaHatasi> dogrulamaHatalariRef)
        {
            var dogrulamaHatalari = new List<DogrulamaHatasi>();

            if (kurumKurulus == null)
            {
                dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    Hata = nameof(KurumKurulus) + " boş olmamalıdır.",
                    HataTuru = DogrulamaHataTuru.Kritik
                });
            }
            else
            {
                if (string.IsNullOrWhiteSpace(kurumKurulus.KKK))
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(KurumKurulus.KKK) + " alanı boş olmamalıdır.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });
                else if (!kurumKurulus.KKK.DogrulaKKK())
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(KurumKurulus.KKK) + " alanı formatı uygun değildir.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });

                if (kurumKurulus.BYK != null)
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(KurumKurulus.BYK) + " alanı kullanımdan kaldırılmıştır.",
                        HataTuru = DogrulamaHataTuru.Onemli
                    });

                if (paketVersiyon == PaketVersiyonTuru.Versiyon2X)
                {
                    if (string.IsNullOrWhiteSpace(kurumKurulus.BirimKKK))
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            Hata = nameof(KurumKurulus.BirimKKK) + " alanı boş olmamalıdır.",
                            HataTuru = DogrulamaHataTuru.Uyari
                        });
                    else if (!kurumKurulus.BirimKKK.DogrulaKKK())
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            Hata = nameof(KurumKurulus.KKK) + " alanı formatı uygun değildir.",
                            HataTuru = DogrulamaHataTuru.Onemli
                        });
                }

                if (kurumKurulus.IletisimBilgisi != null)
                {
                    var hatalar = new List<DogrulamaHatasi>();
                    if (!kurumKurulus.IletisimBilgisi.Dogrula(paketVersiyon, ref hatalar))
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            AltDogrulamaHatalari = hatalar,
                            Hata = nameof(KurumKurulus.IletisimBilgisi) + " alanı doğrulanamamıştır.",
                            HataTuru = hatalar.GetDogrulamaHataTuru().Value
                        });
                }
                else
                {
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(KurumKurulus.IletisimBilgisi) + " alanı boş olmamalıdır.",
                        HataTuru = DogrulamaHataTuru.Uyari
                    });
                }
            }

            if (dogrulamaHatalari.Count > 0)
                dogrulamaHatalariRef.AddRange(dogrulamaHatalari);
            return dogrulamaHatalari.Count == 0;
        }

        public static bool Dogrula(this TuzelSahis tuzelSahis, PaketVersiyonTuru paketVersiyon,
            ref List<DogrulamaHatasi> dogrulamaHatalariRef)
        {
            var dogrulamaHatalari = new List<DogrulamaHatasi>();

            if (tuzelSahis == null)
            {
                dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    Hata = nameof(TuzelSahis) + " boş olmamalıdır.",
                    HataTuru = DogrulamaHataTuru.Kritik
                });
            }
            else
            {
                var hatalar = new List<DogrulamaHatasi>();
                if (!tuzelSahis.Id.Dogrula(paketVersiyon, ref hatalar))
                {
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        AltDogrulamaHatalari = hatalar,
                        Hata = nameof(TuzelSahis.Id) + " alanı doğrulanamamıştır.",
                        HataTuru = hatalar.GetDogrulamaHataTuru().Value
                    });
                }
                else
                {
                    if (!Constants.TUZEL_SAHIS_ID_SCHEMES.Contains(tuzelSahis.Id.SemaID))
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            Hata = nameof(TuzelSahis.Id.SemaID) + " alanı formatı uygun değildir. Geçerli formatlar: " +
                                   string.Join(",", Constants.TUZEL_SAHIS_ID_SCHEMES),
                            HataTuru = DogrulamaHataTuru.Uyari
                        });
                }

                if (tuzelSahis.Ad != null)
                {
                    hatalar = new List<DogrulamaHatasi>();
                    if (!tuzelSahis.Ad.Dogrula(paketVersiyon, ref hatalar))
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            AltDogrulamaHatalari = hatalar,
                            Hata = nameof(TuzelSahis.Ad) + " alanı doğrulanamamıştır.",
                            HataTuru = hatalar.GetDogrulamaHataTuru().Value
                        });
                }
                else
                {
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(TuzelSahis.Ad) + " alanı boş olmamalıdır.",
                        HataTuru = DogrulamaHataTuru.Uyari
                    });
                }

                if (tuzelSahis.IletisimBilgisi != null)
                {
                    hatalar = new List<DogrulamaHatasi>();
                    if (!tuzelSahis.IletisimBilgisi.Dogrula(paketVersiyon, ref hatalar))
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            AltDogrulamaHatalari = hatalar,
                            Hata = nameof(TuzelSahis.IletisimBilgisi) + " alanı doğrulanamamıştır.",
                            HataTuru = hatalar.GetDogrulamaHataTuru().Value
                        });
                }
                else
                {
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(TuzelSahis.IletisimBilgisi) + " alanı boş olmamalıdır.",
                        HataTuru = DogrulamaHataTuru.Uyari
                    });
                }
            }

            if (dogrulamaHatalari.Count > 0)

                dogrulamaHatalariRef.AddRange(dogrulamaHatalari);
            return dogrulamaHatalari.Count == 0;
        }

        public static bool Dogrula(this GercekSahis gercekSahis, PaketVersiyonTuru paketVersiyon,
            ref List<DogrulamaHatasi> dogrulamaHatalariRef)
        {
            var dogrulamaHatalari = new List<DogrulamaHatasi>();

            if (gercekSahis == null)
            {
                dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    Hata = nameof(GercekSahis) + " boş olmamalıdır.",
                    HataTuru = DogrulamaHataTuru.Kritik
                });
            }
            else
            {
                var hatalar = new List<DogrulamaHatasi>();
                if (!gercekSahis.Kisi.Dogrula(paketVersiyon, ref hatalar))
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        AltDogrulamaHatalari = hatalar,
                        Hata = nameof(GercekSahis.Kisi) + " alanı doğrulanamamıştır.",
                        HataTuru = hatalar.GetDogrulamaHataTuru().Value
                    });

                if (string.IsNullOrWhiteSpace(gercekSahis.TCKN))
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(GercekSahis.TCKN) + " alanı boş olmamalıdır.",
                        HataTuru = DogrulamaHataTuru.Uyari
                    });
                else if (!gercekSahis.TCKN.DogrulaTCKimlikNo())
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(GercekSahis.TCKN) + " alanı formatı uygun değildir.",
                        HataTuru = DogrulamaHataTuru.Onemli
                    });

                if (gercekSahis.Gorev != null)
                {
                    hatalar = new List<DogrulamaHatasi>();
                    if (!gercekSahis.Gorev.Dogrula(paketVersiyon, ref hatalar))
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            AltDogrulamaHatalari = hatalar,
                            Hata = nameof(GercekSahis.Gorev) + " alanı doğrulanamamıştır.",
                            HataTuru = hatalar.GetDogrulamaHataTuru().Value
                        });
                }
                else
                {
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(GercekSahis.Gorev) + " alanı boş olmamalıdır.",
                        HataTuru = DogrulamaHataTuru.Uyari
                    });
                }

                if (gercekSahis.IletisimBilgisi != null)
                {
                    hatalar = new List<DogrulamaHatasi>();
                    if (!gercekSahis.IletisimBilgisi.Dogrula(paketVersiyon, ref hatalar))
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            AltDogrulamaHatalari = hatalar,
                            Hata = nameof(GercekSahis.IletisimBilgisi) + " alanı doğrulanamamıştır.",
                            HataTuru = hatalar.GetDogrulamaHataTuru().Value
                        });
                }
                else
                {
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(GercekSahis.IletisimBilgisi) + " alanı boş olmamalıdır.",
                        HataTuru = DogrulamaHataTuru.Uyari
                    });
                }
            }

            if (dogrulamaHatalari.Count > 0)
                dogrulamaHatalariRef.AddRange(dogrulamaHatalari);
            return dogrulamaHatalari.Count == 0;
        }

        public static bool Dogrula(this TanimlayiciTip tanimlayiciTip, PaketVersiyonTuru paketVersiyon,
            ref List<DogrulamaHatasi> dogrulamaHatalariRef)
        {
            var dogrulamaHatalari = new List<DogrulamaHatasi>();

            if (tanimlayiciTip == null)
            {
                dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    Hata = nameof(tanimlayiciTip) + " boş olmamalıdır.",
                    HataTuru = DogrulamaHataTuru.Kritik
                });
            }
            else
            {
                if (string.IsNullOrWhiteSpace(tanimlayiciTip.SemaID))
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(tanimlayiciTip.SemaID) + " alanı boş olmamalıdır.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });
                if (string.IsNullOrWhiteSpace(tanimlayiciTip.Deger))
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(tanimlayiciTip.Deger) + " alanı boş olmamalıdır.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });
            }

            if (dogrulamaHatalari.Count > 0)
                dogrulamaHatalariRef.AddRange(dogrulamaHatalari);
            return dogrulamaHatalari.Count == 0;
        }

        public static bool Dogrula(this IsimTip isimTip, PaketVersiyonTuru paketVersiyon,
            ref List<DogrulamaHatasi> dogrulamaHatalariRef)
        {
            var dogrulamaHatalari = new List<DogrulamaHatasi>();

            if (isimTip == null)
            {
                dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    Hata = nameof(isimTip) + " boş olmamalıdır.",
                    HataTuru = DogrulamaHataTuru.Kritik
                });
            }
            else
            {
                if (string.IsNullOrWhiteSpace(isimTip.DilID))
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(isimTip.DilID) + " alanı boş olmamalıdır.",
                        HataTuru = DogrulamaHataTuru.Uyari
                    });
                if (string.IsNullOrWhiteSpace(isimTip.Deger))
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(isimTip.Deger) + " alanı boş olmamalıdır.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });
            }

            if (dogrulamaHatalari.Count > 0)
                dogrulamaHatalariRef.AddRange(dogrulamaHatalari);
            return dogrulamaHatalari.Count == 0;
        }

        public static bool Dogrula(this MetinTip metinTip, PaketVersiyonTuru paketVersiyon,
            ref List<DogrulamaHatasi> dogrulamaHatalariRef)
        {
            var dogrulamaHatalari = new List<DogrulamaHatasi>();

            if (metinTip == null)
            {
                dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    Hata = nameof(metinTip) + " boş olmamalıdır.",
                    HataTuru = DogrulamaHataTuru.Kritik
                });
            }
            else
            {
                if (string.IsNullOrWhiteSpace(metinTip.DilID))
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(metinTip.DilID) + " alanı boş olmamalıdır.",
                        HataTuru = DogrulamaHataTuru.Uyari
                    });
                if (string.IsNullOrWhiteSpace(metinTip.Deger))
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(metinTip.Deger) + " alanı boş olmamalıdır.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });
            }

            if (dogrulamaHatalari.Count > 0)
                dogrulamaHatalariRef.AddRange(dogrulamaHatalari);
            return dogrulamaHatalari.Count == 0;
        }

        public static bool Dogrula(this IdTip id, PaketVersiyonTuru paketVersiyon,
            ref List<DogrulamaHatasi> dogrulamaHatalariRef)
        {
            var dogrulamaHatalari = new List<DogrulamaHatasi>();

            if (id == null)
            {
                dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    Hata = nameof(id) + " boş olmamalıdır.",
                    HataTuru = DogrulamaHataTuru.Kritik
                });
            }
            else
            {
                if (string.IsNullOrWhiteSpace(id.Deger))
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(id.Deger) + " alanı boş olmamalıdır.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });
                else if (id.EYazismaIdMi && !Guid.TryParse(id.Deger, out _))
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(id.Deger) + " alanı formatı uygun değildir.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });
            }

            if (dogrulamaHatalari.Count > 0)
                dogrulamaHatalariRef.AddRange(dogrulamaHatalari);
            return dogrulamaHatalari.Count == 0;
        }

        public static bool Dogrula(this Kisi kisi, PaketVersiyonTuru paketVersiyon,
            ref List<DogrulamaHatasi> dogrulamaHatalariRef)
        {
            var dogrulamaHatalari = new List<DogrulamaHatasi>();

            if (kisi == null)
            {
                dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    Hata = nameof(kisi) + " boş olmamalıdır.",
                    HataTuru = DogrulamaHataTuru.Kritik
                });
            }
            else
            {
                var hatalar = new List<DogrulamaHatasi>();
                if (!kisi.IlkAdi.Dogrula(paketVersiyon, ref hatalar))
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        AltDogrulamaHatalari = hatalar,
                        Hata = nameof(kisi.IlkAdi) + " alanı doğrulanamamıştır.",
                        HataTuru = hatalar.GetDogrulamaHataTuru().Value
                    });

                if (kisi.IkinciAdi != null)
                {
                    hatalar = new List<DogrulamaHatasi>();
                    if (!kisi.IkinciAdi.Dogrula(paketVersiyon, ref hatalar))
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            AltDogrulamaHatalari = hatalar,
                            Hata = nameof(kisi.IkinciAdi) + " alanı doğrulanamamıştır.",
                            HataTuru = hatalar.GetDogrulamaHataTuru().Value
                        });
                }

                hatalar = new List<DogrulamaHatasi>();
                if (!kisi.Soyadi.Dogrula(paketVersiyon, ref hatalar))
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        AltDogrulamaHatalari = hatalar,
                        Hata = nameof(kisi.Soyadi) + " alanı doğrulanamamıştır.",
                        HataTuru = hatalar.GetDogrulamaHataTuru().Value
                    });

                if (kisi.Unvan != null)
                {
                    hatalar = new List<DogrulamaHatasi>();
                    if (!kisi.Unvan.Dogrula(paketVersiyon, ref hatalar))
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            AltDogrulamaHatalari = hatalar,
                            Hata = nameof(kisi.Unvan) + " alanı doğrulanamamıştır.",
                            HataTuru = hatalar.GetDogrulamaHataTuru().Value
                        });
                }
                else
                {
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(kisi.Unvan) + " boş olmamalıdır.",
                        HataTuru = DogrulamaHataTuru.Uyari
                    });
                }

                if (kisi.OnEk != null)
                {
                    hatalar = new List<DogrulamaHatasi>();
                    if (!kisi.OnEk.Dogrula(paketVersiyon, ref hatalar))
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            AltDogrulamaHatalari = hatalar,
                            Hata = nameof(kisi.OnEk) + " alanı doğrulanamamıştır.",
                            HataTuru = hatalar.GetDogrulamaHataTuru().Value
                        });
                }
            }

            if (dogrulamaHatalari.Count > 0)
                dogrulamaHatalariRef.AddRange(dogrulamaHatalari);
            return dogrulamaHatalari.Count == 0;
        }

        public static bool Dogrula(this Ozet ozet, PaketVersiyonTuru paketVersiyon,
            ref List<DogrulamaHatasi> dogrulamaHatalariRef)
        {
            var dogrulamaHatalari = new List<DogrulamaHatasi>();

            if (ozet == null)
            {
                dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    Hata = nameof(ozet) + " boş olmamalıdır.",
                    HataTuru = DogrulamaHataTuru.Kritik
                });
            }
            else
            {
                var hatalar = new List<DogrulamaHatasi>();
                if (!ozet.OzetAlgoritmasi.Dogrula(paketVersiyon, ref hatalar))
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        AltDogrulamaHatalari = hatalar,
                        Hata = nameof(ozet.OzetAlgoritmasi) + " alanı doğrulanamamıştır.",
                        HataTuru = hatalar.GetDogrulamaHataTuru().Value
                    });

                if (ozet.OzetDegeri == null || ozet.OzetDegeri.Length == 0)
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(ozet.OzetDegeri) + " alanı boş olmamalıdır.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });
            }

            if (dogrulamaHatalari.Count > 0)
                dogrulamaHatalariRef.AddRange(dogrulamaHatalari);
            return dogrulamaHatalari.Count == 0;
        }

        public static bool Dogrula(this OzetAlgoritmasi ozetAlgoritmasi, PaketVersiyonTuru paketVersiyon,
            ref List<DogrulamaHatasi> dogrulamaHatalariRef)
        {
            var dogrulamaHatalari = new List<DogrulamaHatasi>();

            if (ozetAlgoritmasi == null)
            {
                dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    Hata = nameof(ozetAlgoritmasi) + " boş olmamalıdır.",
                    HataTuru = DogrulamaHataTuru.Kritik
                });
            }
            else
            {
                if (ozetAlgoritmasi.Algoritma == OzetAlgoritmaTuru.YOK)
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(ozetAlgoritmasi.Algoritma) + " alanı " + nameof(OzetAlgoritmaTuru.YOK) +
                               " olmamalıdır.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });
                else if (paketVersiyon == PaketVersiyonTuru.Versiyon1X &&
                         ozetAlgoritmasi.Algoritma == OzetAlgoritmaTuru.SHA384)
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(OzetAlgoritmaTuru.SHA384) +
                               " özet algoritması yalnızca e-Yazışma API 2.X versiyonlarında kullanılabilir.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });
                else if (paketVersiyon == PaketVersiyonTuru.Versiyon2X &&
                         (ozetAlgoritmasi.Algoritma == OzetAlgoritmaTuru.RIPEMD160 ||
                          ozetAlgoritmasi.Algoritma == OzetAlgoritmaTuru.SHA1))
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(OzetAlgoritmaTuru.SHA1) + " özet algoritması ve " +
                               nameof(OzetAlgoritmaTuru.RIPEMD160) + " özet algoritması kullanımdan kaldırılmıştır.",
                        HataTuru = DogrulamaHataTuru.Onemli
                    });
            }

            if (dogrulamaHatalari.Count > 0)
                dogrulamaHatalariRef.AddRange(dogrulamaHatalari);
            return dogrulamaHatalari.Count == 0;
        }

        public static bool Dogrula(this KonulmamisEk konulmamisEk, PaketVersiyonTuru paketVersiyon,
            ref List<DogrulamaHatasi> dogrulamaHatalariRef)
        {
            var dogrulamaHatalari = new List<DogrulamaHatasi>();

            if (konulmamisEk == null)
            {
                dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    Hata = nameof(konulmamisEk) + " boş olmamalıdır.",
                    HataTuru = DogrulamaHataTuru.Kritik
                });
            }
            else
            {
                if (konulmamisEk.EkIdDeger == Guid.Empty)
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(konulmamisEk) + " alanı boş olmamalıdır.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });
            }

            if (dogrulamaHatalari.Count > 0)
                dogrulamaHatalariRef.AddRange(dogrulamaHatalari);
            return dogrulamaHatalari.Count == 0;
        }

        public static bool Dogrula(this Dagitim dagitim, PaketVersiyonTuru paketVersiyon,
            ref List<DogrulamaHatasi> dogrulamaHatalariRef)
        {
            var dogrulamaHatalari = new List<DogrulamaHatasi>();

            if (dagitim == null)
            {
                dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    Hata = nameof(Dagitim) + " boş olmamalıdır.",
                    HataTuru = DogrulamaHataTuru.Kritik
                });
            }
            else
            {
                if (dagitim.Oge == null)
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(Dagitim) + " alanı boş olmamalıdır.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });

                if (!(dagitim.Oge is KurumKurulus || dagitim.Oge is TuzelSahis || dagitim.Oge is GercekSahis))
                {
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(Dagitim.Oge) + " alanı için " + typeof(KurumKurulus).Name + "," +
                               typeof(TuzelSahis).Name + " veya " + typeof(GercekSahis).Name + " tipinde olmalıdır.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });
                }
                else if (dagitim.Oge is KurumKurulus kurumKurulus)
                {
                    var hatalar = new List<DogrulamaHatasi>();
                    if (!kurumKurulus.Dogrula(paketVersiyon, ref hatalar))
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            AltDogrulamaHatalari = hatalar,
                            Hata = nameof(Dagitim.Oge) + " alanı doğrulanamamıştır.",
                            HataTuru = hatalar.GetDogrulamaHataTuru().Value
                        });
                }
                else if (dagitim.Oge is TuzelSahis tuzelSahis)
                {
                    var hatalar = new List<DogrulamaHatasi>();
                    if (!tuzelSahis.Dogrula(paketVersiyon, ref hatalar))
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            AltDogrulamaHatalari = hatalar,
                            Hata = nameof(Dagitim.Oge) + " alanı doğrulanamamıştır.",
                            HataTuru = hatalar.GetDogrulamaHataTuru().Value
                        });
                }
                else if (dagitim.Oge is GercekSahis gercekSahis)
                {
                    var hatalar = new List<DogrulamaHatasi>();
                    if (!gercekSahis.Dogrula(paketVersiyon, ref hatalar))
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            AltDogrulamaHatalari = hatalar,
                            Hata = nameof(Dagitim.Oge) + " alanı doğrulanamamıştır.",
                            HataTuru = hatalar.GetDogrulamaHataTuru().Value
                        });
                }

                if (dagitim.IvedilikTuru == IvedilikTuru.CIV || dagitim.IvedilikTuru == IvedilikTuru.IVD)
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(Dagitim.IvedilikTuru) + " alanı için " + nameof(IvedilikTuru.CIV) + " ve " +
                               nameof(IvedilikTuru.IVD) + " seçeneği kullanımdan kaldırılmıştır.",
                        HataTuru = DogrulamaHataTuru.Onemli
                    });

                if (dagitim.Miat == default && dagitim.IvedilikTuru == IvedilikTuru.GNL)
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(Dagitim.IvedilikTuru) + " alanı " + nameof(IvedilikTuru.GNL) +
                               " olması durumunda " + nameof(Dagitim.Miat) + " alanı boş olmamalıdır.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });

                if (dagitim.KonulmamisEkler != null)
                {
                    if (dagitim.KonulmamisEkler.Count == 0)
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            Hata = nameof(Dagitim.KonulmamisEkler) + " alanı en az bir tane " +
                                   typeof(KonulmamisEk).Name + " tipinde öğe içermelidir.",
                            HataTuru = DogrulamaHataTuru.Uyari
                        });
                    else
                        foreach (var konulmamisEk in dagitim.KonulmamisEkler)
                        {
                            var hatalar = new List<DogrulamaHatasi>();
                            if (!konulmamisEk.Dogrula(paketVersiyon, ref hatalar))
                                dogrulamaHatalari.Add(new DogrulamaHatasi
                                {
                                    AltDogrulamaHatalari = hatalar,
                                    Hata = nameof(konulmamisEk) + " alanı doğrulanamamıştır.",
                                    HataTuru = hatalar.GetDogrulamaHataTuru().Value
                                });
                        }
                }
            }

            if (dogrulamaHatalari.Count > 0)
                dogrulamaHatalariRef.AddRange(dogrulamaHatalari);
            return dogrulamaHatalari.Count == 0;
        }

        public static bool Dogrula(this Ek ek, PaketVersiyonTuru paketVersiyon,
            ref List<DogrulamaHatasi> dogrulamaHatalariRef)
        {
            var dogrulamaHatalari = new List<DogrulamaHatasi>();

            if (ek == null)
            {
                dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    Hata = nameof(Ek) + " boş olmamalıdır.",
                    HataTuru = DogrulamaHataTuru.Kritik
                });
            }
            else
            {
                var hatalar = new List<DogrulamaHatasi>();
                if (!ek.Id.Dogrula(paketVersiyon, ref hatalar))
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        AltDogrulamaHatalari = hatalar,
                        Hata = nameof(Ek.Id) + " alanı doğrulanamamıştır.",
                        HataTuru = hatalar.GetDogrulamaHataTuru().Value
                    });

                if (ek.Ad != null)
                {
                    hatalar = new List<DogrulamaHatasi>();
                    if (!ek.Ad.Dogrula(paketVersiyon, ref hatalar))
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            AltDogrulamaHatalari = hatalar,
                            Hata = nameof(Ek.Ad) + " alanı doğrulanamamıştır.",
                            HataTuru = hatalar.GetDogrulamaHataTuru().Value
                        });
                }
                else
                {
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(Ek.Ad) + " alanı boş olmamalıdır.",
                        HataTuru = DogrulamaHataTuru.Uyari
                    });
                }

                if (ek.SiraNo < 1)
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(Ek.SiraNo) + " alanı değeri 0'dan büyük olmalıdır.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });

                if (ek.Aciklama != null)
                {
                    hatalar = new List<DogrulamaHatasi>();
                    if (!ek.Aciklama.Dogrula(paketVersiyon, ref hatalar))
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            AltDogrulamaHatalari = hatalar,
                            Hata = nameof(Ek.Aciklama) + " alanı doğrulanamamıştır.",
                            HataTuru = DogrulamaHataTuru.Onemli
                        });
                }

                if (ek.Tur == EkTuru.DED)
                {
                    if (string.IsNullOrWhiteSpace(ek.DosyaAdi))
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            Hata = nameof(Ek.Tur) + " alanı değeri " + nameof(EkTuru.DED) + " olması durumunda " +
                                   nameof(Ek.DosyaAdi) + " alanı boş olmamalıdır.",
                            HataTuru = DogrulamaHataTuru.Kritik
                        });
                    if (string.IsNullOrWhiteSpace(ek.MimeTuru))
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            Hata = nameof(Ek.Tur) + " alanı değeri " + nameof(EkTuru.DED) + " olması durumunda " +
                                   nameof(Ek.MimeTuru) + " alanı boş olmamalıdır.",
                            HataTuru = DogrulamaHataTuru.Kritik
                        });
                }
                else if (ek.Tur == EkTuru.HRF)
                {
                    if (ek.ImzaliMi)
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            Hata = nameof(EkTuru.HRF) + " türündeki ekler, imzalı olarak pakete eklenemezler.",
                            HataTuru = DogrulamaHataTuru.Kritik
                        });

                    if (string.IsNullOrWhiteSpace(ek.Referans))
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            Hata = nameof(Ek.Tur) + " alanı değeri " + nameof(EkTuru.HRF) + " olması durumunda " +
                                   nameof(Ek.Referans) + " alanı boş olmamalıdır.",
                            HataTuru = DogrulamaHataTuru.Kritik
                        });
                    else if (!ek.Referans.DogrulaWebAdresi())
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            Hata = nameof(Ek.Referans) + " alanı formatı uygun değildir.",
                            HataTuru = DogrulamaHataTuru.Onemli
                        });

                    if (ek.Ozet != null)
                    {
                        hatalar = new List<DogrulamaHatasi>();
                        if (!ek.Ozet.Dogrula(paketVersiyon, ref hatalar))
                            dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                AltDogrulamaHatalari = hatalar,
                                Hata = nameof(Ek.Ozet) + " alanı doğrulanamamıştır.",
                                HataTuru = hatalar.GetDogrulamaHataTuru().Value
                            });
                    }
                }
                else if (ek.Tur == EkTuru.FZK)
                {
                    if (ek.ImzaliMi)
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            Hata = nameof(EkTuru.FZK) + " türündeki ekler, imzalı olarak pakete eklenemezler.",
                            HataTuru = DogrulamaHataTuru.Kritik
                        });
                }

                if (ek.OzId != null)
                {
                    hatalar = new List<DogrulamaHatasi>();
                    if (!ek.OzId.Dogrula(paketVersiyon, ref hatalar))
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            AltDogrulamaHatalari = hatalar,
                            Hata = nameof(Ek.OzId) + " alanı doğrulanamamıştır.",
                            HataTuru = hatalar.GetDogrulamaHataTuru().Value
                        });
                }
            }

            if (dogrulamaHatalari.Count > 0)
                dogrulamaHatalariRef.AddRange(dogrulamaHatalari);
            return dogrulamaHatalari.Count == 0;
        }

        public static bool Dogrula(this Ilgi ilgi, PaketVersiyonTuru paketVersiyon,
            ref List<DogrulamaHatasi> dogrulamaHatalariRef)
        {
            var dogrulamaHatalari = new List<DogrulamaHatasi>();

            if (ilgi == null)
            {
                dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    Hata = nameof(ilgi) + " boş olmamalıdır.",
                    HataTuru = DogrulamaHataTuru.Kritik
                });
            }
            else
            {
                var hatalar = new List<DogrulamaHatasi>();
                if (!ilgi.Id.Dogrula(paketVersiyon, ref hatalar))
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        AltDogrulamaHatalari = hatalar,
                        Hata = nameof(ilgi.Id) + " alanı doğrulanamamıştır.",
                        HataTuru = hatalar.GetDogrulamaHataTuru().Value
                    });

                if (ilgi.Etiket == default(char) || char.IsWhiteSpace(ilgi.Etiket))
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(ilgi.Etiket) + " alanı boş olmamalıdır.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });

                if (ilgi.OzId != null)
                {
                    hatalar = new List<DogrulamaHatasi>();
                    if (!ilgi.OzId.Dogrula(paketVersiyon, ref hatalar))
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            AltDogrulamaHatalari = hatalar,
                            Hata = nameof(ilgi.OzId) + " alanı doğrulanamamıştır.",
                            HataTuru = hatalar.GetDogrulamaHataTuru().Value
                        });
                }

                if (!new Regex("^[a-zA-ZğüşöçİĞÜŞÖÇ]{1}$").IsMatch(ilgi.Etiket.ToString()))
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(ilgi.Etiket) + " alanı alfanümerik Türkçe karakterlerden oluşmalıdır.",
                        HataTuru = DogrulamaHataTuru.Uyari
                    });

                if (ilgi.Ad != null)
                {
                    hatalar = new List<DogrulamaHatasi>();
                    if (!ilgi.Ad.Dogrula(paketVersiyon, ref hatalar))
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            AltDogrulamaHatalari = hatalar,
                            Hata = nameof(ilgi.Ad) + " alanı doğrulanamamıştır.",
                            HataTuru = hatalar.GetDogrulamaHataTuru().Value
                        });
                }
                else
                {
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(ilgi.Ad) + " alanı boş olmamalıdır.",
                        HataTuru = DogrulamaHataTuru.Uyari
                    });
                }

                if (ilgi.Aciklama != null)
                {
                    hatalar = new List<DogrulamaHatasi>();
                    if (!ilgi.Aciklama.Dogrula(paketVersiyon, ref hatalar))
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            AltDogrulamaHatalari = hatalar,
                            Hata = nameof(ilgi.Aciklama) + " alanı doğrulanamamıştır.",
                            HataTuru = hatalar.GetDogrulamaHataTuru().Value
                        });
                }
            }

            if (dogrulamaHatalari.Count > 0)
                dogrulamaHatalariRef.AddRange(dogrulamaHatalari);
            return dogrulamaHatalari.Count == 0;
        }

        public static bool Dogrula(this Ilgili ilgili, PaketVersiyonTuru paketVersiyon,
            ref List<DogrulamaHatasi> dogrulamaHatalariRef)
        {
            var dogrulamaHatalari = new List<DogrulamaHatasi>();

            if (ilgili == null)
            {
                dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    Hata = nameof(ilgili) + " boş olmamalıdır.",
                    HataTuru = DogrulamaHataTuru.Kritik
                });
            }
            else
            {
                if (ilgili.Oge == null)
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(ilgili) + " alanı boş olmamalıdır.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });

                if (!(ilgili.Oge is KurumKurulus || ilgili.Oge is TuzelSahis || ilgili.Oge is GercekSahis))
                {
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(ilgili.Oge) + " alanı için " + typeof(KurumKurulus).Name + "," +
                               typeof(TuzelSahis).Name + " veya " + typeof(GercekSahis).Name + " tipinde olmalıdır.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });
                }
                else if (ilgili.Oge is KurumKurulus kurumKurulus)
                {
                    var hatalar = new List<DogrulamaHatasi>();
                    if (!kurumKurulus.Dogrula(paketVersiyon, ref hatalar))
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            AltDogrulamaHatalari = hatalar,
                            Hata = nameof(ilgili.Oge) + " alanı doğrulanamamıştır.",
                            HataTuru = hatalar.GetDogrulamaHataTuru().Value
                        });
                }
                else if (ilgili.Oge is TuzelSahis tuzelSahis)
                {
                    var hatalar = new List<DogrulamaHatasi>();
                    if (!tuzelSahis.Dogrula(paketVersiyon, ref hatalar))
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            AltDogrulamaHatalari = hatalar,
                            Hata = nameof(ilgili.Oge) + " alanı doğrulanamamıştır.",
                            HataTuru = hatalar.GetDogrulamaHataTuru().Value
                        });
                }
                else if (ilgili.Oge is GercekSahis gercekSahis)
                {
                    var hatalar = new List<DogrulamaHatasi>();
                    if (!gercekSahis.Dogrula(paketVersiyon, ref hatalar))
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            AltDogrulamaHatalari = hatalar,
                            Hata = nameof(ilgili.Oge) + " alanı doğrulanamamıştır.",
                            HataTuru = hatalar.GetDogrulamaHataTuru().Value
                        });
                }
            }

            if (dogrulamaHatalari.Count > 0)
                dogrulamaHatalariRef.AddRange(dogrulamaHatalari);
            return dogrulamaHatalari.Count == 0;
        }

        public static bool Dogrula(this Ustveri ustveri, PaketVersiyonTuru paketVersiyon,
            ref List<DogrulamaHatasi> dogrulamaHatalariRef)
        {
            var dogrulamaHatalari = new List<DogrulamaHatasi>();

            if (ustveri == null)
            {
                dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    Hata = nameof(ustveri) + " boş olmamalıdır.",
                    HataTuru = DogrulamaHataTuru.Kritik
                });
            }
            else
            {
                if (ustveri.BelgeId == Guid.Empty)
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(ustveri.BelgeId) + " alanı boş olmamalıdır.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });

                if (paketVersiyon == PaketVersiyonTuru.Versiyon1X && string.IsNullOrWhiteSpace(ustveri.BelgeNo))
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(ustveri.BelgeNo) + " alanı boş olmamalıdır.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });
                else if (paketVersiyon == PaketVersiyonTuru.Versiyon2X && !string.IsNullOrWhiteSpace(ustveri.BelgeNo))
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(ustveri.BelgeNo) + " yalnızca E-Yazışma 1.X versiyonlarında kullanılabilir.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });

                if (paketVersiyon == PaketVersiyonTuru.Versiyon1X && ustveri.Tarih == DateTime.MinValue)
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(ustveri.Tarih) + " alanı geçersizdir.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });
                else if (paketVersiyon == PaketVersiyonTuru.Versiyon2X && ustveri.Tarih != DateTime.MinValue)
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(ustveri.Tarih) + " yalnızca E-Yazışma 1.X versiyonlarında kullanılabilir.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });

                if (paketVersiyon == PaketVersiyonTuru.Versiyon2X && (ustveri.GuvenlikKodu == GuvenlikKoduTuru.KSO ||
                                                                      ustveri.GuvenlikKodu == GuvenlikKoduTuru.TSD))
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(ustveri.GuvenlikKodu) + " alanı için " + nameof(GuvenlikKoduTuru.KSO) + " ve " +
                               nameof(GuvenlikKoduTuru.TSD) + " seçeneği kullanımdan kaldırılmıştır.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });

                if (paketVersiyon == PaketVersiyonTuru.Versiyon1X && ustveri.GuvenlikKoduGecerlilikTarihi.HasValue &&
                    ustveri.GuvenlikKoduGecerlilikTarihi.Value < ustveri.Tarih)
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(ustveri.GuvenlikKoduGecerlilikTarihi) + " alanı " + nameof(ustveri.Tarih) +
                               " değerine eşit ya da daha ileri bir tarih olmalıdır.",
                        HataTuru = DogrulamaHataTuru.Onemli
                    });

                if (ustveri.Dagitimlar == null || ustveri.Dagitimlar.Count == 0)
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(ustveri.Dagitimlar) + " alanı boş olmamalıdır ve en az bir tane " +
                               typeof(Dagitim).Name + " tipinde öğe içermelidir.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });
                else
                    foreach (var dagitim in ustveri.Dagitimlar)
                    {
                        var hatalar = new List<DogrulamaHatasi>();
                        if (!dagitim.Dogrula(paketVersiyon, ref hatalar))
                            dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                AltDogrulamaHatalari = hatalar,
                                Hata = nameof(Dagitim) + " alanı doğrulanamamıştır.",
                                HataTuru = hatalar.GetDogrulamaHataTuru().Value
                            });
                    }

                if (string.IsNullOrWhiteSpace(ustveri.DosyaAdi))
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(ustveri.DosyaAdi) + " alanı boş olmamalıdır.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });

                if (ustveri.Ekler != null && ustveri.Ekler.Count > 0)
                    foreach (var ek in ustveri.Ekler)
                    {
                        var hatalar = new List<DogrulamaHatasi>();
                        if (!ek.Dogrula(paketVersiyon, ref hatalar))
                            dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                AltDogrulamaHatalari = hatalar,
                                Hata = nameof(Ek) + " alanı doğrulanamamıştır.",
                                HataTuru = hatalar.GetDogrulamaHataTuru().Value
                            });
                    }

                if (ustveri.Ilgiler != null && ustveri.Ilgiler.Count > 0)
                    foreach (var ilgi in ustveri.Ilgiler)
                    {
                        var hatalar = new List<DogrulamaHatasi>();
                        if (!ilgi.Dogrula(paketVersiyon, ref hatalar))
                            dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                AltDogrulamaHatalari = hatalar,
                                Hata = nameof(ilgi) + " alanı doğrulanamamıştır.",
                                HataTuru = hatalar.GetDogrulamaHataTuru().Value
                            });
                    }

                if (ustveri.Ilgililer != null && ustveri.Ilgililer.Count > 0)
                    foreach (var ilgili in ustveri.Ilgililer)
                    {
                        var hatalar = new List<DogrulamaHatasi>();
                        if (!ilgili.Dogrula(paketVersiyon, ref hatalar))
                            dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                AltDogrulamaHatalari = hatalar,
                                Hata = nameof(ilgili) + " alanı doğrulanamamıştır.",
                                HataTuru = hatalar.GetDogrulamaHataTuru().Value
                            });
                    }

                var hatalar1 = new List<DogrulamaHatasi>();
                if (!ustveri.Konu.Dogrula(paketVersiyon, ref hatalar1))
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        AltDogrulamaHatalari = hatalar1,
                        Hata = nameof(ustveri.Konu) + " alanı doğrulanamamıştır.",
                        HataTuru = hatalar1.GetDogrulamaHataTuru().Value
                    });

                hatalar1 = new List<DogrulamaHatasi>();
                if (!ustveri.Olusturan.Dogrula(paketVersiyon, ref hatalar1))
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        AltDogrulamaHatalari = hatalar1,
                        Hata = nameof(ustveri.Olusturan) + " alanı doğrulanamamıştır.",
                        HataTuru = hatalar1.GetDogrulamaHataTuru().Value
                    });

                if (ustveri.OzId != null)
                {
                    hatalar1 = new List<DogrulamaHatasi>();
                    if (!ustveri.OzId.Dogrula(paketVersiyon, ref hatalar1))
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            AltDogrulamaHatalari = hatalar1,
                            Hata = nameof(ustveri.OzId) + " alanı doğrulanamamıştır.",
                            HataTuru = hatalar1.GetDogrulamaHataTuru().Value
                        });
                }
                else
                {
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(ustveri.OzId) + " alanı boş olmamalıdır.",
                        HataTuru = DogrulamaHataTuru.Uyari
                    });
                }
            }

            if (dogrulamaHatalari.Count > 0)
                dogrulamaHatalariRef.AddRange(dogrulamaHatalari);
            return dogrulamaHatalari.Count == 0;
        }

        public static bool Dogrula(this NihaiUstveri nihaiUstveri, PaketVersiyonTuru paketVersiyon,
            ref List<DogrulamaHatasi> dogrulamaHatalariRef)
        {
            var dogrulamaHatalari = new List<DogrulamaHatasi>();

            if (nihaiUstveri == null)
            {
                dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    Hata = nameof(nihaiUstveri) + " boş olmamalıdır.",
                    HataTuru = DogrulamaHataTuru.Kritik
                });
            }
            else
            {
                if (string.IsNullOrWhiteSpace(nihaiUstveri.BelgeNo))
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(nihaiUstveri.BelgeNo) + " alanı boş olmamalıdır.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });

                if (nihaiUstveri.Tarih == DateTime.MinValue)
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(nihaiUstveri.Tarih) + " alanı değeri geçersizdir.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });

                if (nihaiUstveri.BelgeImzalar == null || nihaiUstveri.BelgeImzalar.Count == 0)
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(nihaiUstveri.BelgeImzalar) + " alanı boş olmamalıdır ve en az bir tane " +
                               typeof(Imza).Name + " tipinde item içermelidir.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });
                else
                    foreach (var imza in nihaiUstveri.BelgeImzalar)
                    {
                        var hatalar = new List<DogrulamaHatasi>();
                        if (!imza.Dogrula(paketVersiyon, ref hatalar))
                            dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                AltDogrulamaHatalari = hatalar,
                                Hata = nameof(imza) + " alanı doğrulanamamıştır.",
                                HataTuru = hatalar.GetDogrulamaHataTuru().Value
                            });
                    }
            }

            if (dogrulamaHatalari.Count > 0)
                dogrulamaHatalariRef.AddRange(dogrulamaHatalari);
            return dogrulamaHatalari.Count == 0;
        }

        public static bool Dogrula(this Olusturan olusturan, PaketVersiyonTuru paketVersiyon,
            ref List<DogrulamaHatasi> dogrulamaHatalariRef)
        {
            var dogrulamaHatalari = new List<DogrulamaHatasi>();

            if (olusturan == null)
            {
                dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    Hata = nameof(olusturan) + " boş olmamalıdır.",
                    HataTuru = DogrulamaHataTuru.Kritik
                });
            }
            else
            {
                if (olusturan.Oge == null)
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(olusturan) + " alanı boş olmamalıdır.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });

                if (!(olusturan.Oge is KurumKurulus || olusturan.Oge is TuzelSahis || olusturan.Oge is GercekSahis))
                {
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(olusturan.Oge) + " alanı için " + typeof(KurumKurulus).Name + "," +
                               typeof(TuzelSahis).Name + " veya " + typeof(GercekSahis).Name + " tipinde olmalıdır.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });
                }
                else if (olusturan.Oge is KurumKurulus kurumKurulus)
                {
                    var hatalar = new List<DogrulamaHatasi>();
                    if (!kurumKurulus.Dogrula(paketVersiyon, ref hatalar))
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            AltDogrulamaHatalari = hatalar,
                            Hata = nameof(olusturan.Oge) + " alanı doğrulanamamıştır.",
                            HataTuru = hatalar.GetDogrulamaHataTuru().Value
                        });
                }
                else if (olusturan.Oge is TuzelSahis tuzelSahis)
                {
                    var hatalar = new List<DogrulamaHatasi>();
                    if (!tuzelSahis.Dogrula(paketVersiyon, ref hatalar))
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            AltDogrulamaHatalari = hatalar,
                            Hata = nameof(olusturan.Oge) + " alanı doğrulanamamıştır.",
                            HataTuru = hatalar.GetDogrulamaHataTuru().Value
                        });
                }
                else if (olusturan.Oge is GercekSahis gercekSahis)
                {
                    var hatalar = new List<DogrulamaHatasi>();
                    if (!gercekSahis.Dogrula(paketVersiyon, ref hatalar))
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            AltDogrulamaHatalari = hatalar,
                            Hata = nameof(olusturan.Oge) + " alanı doğrulanamamıştır.",
                            HataTuru = hatalar.GetDogrulamaHataTuru().Value
                        });
                }
            }

            if (dogrulamaHatalari.Count > 0)
                dogrulamaHatalariRef.AddRange(dogrulamaHatalari);
            return dogrulamaHatalari.Count == 0;
        }

        public static bool Dogrula(this Hedef hedef, PaketVersiyonTuru paketVersiyon,
            ref List<DogrulamaHatasi> dogrulamaHatalariRef)
        {
            var dogrulamaHatalari = new List<DogrulamaHatasi>();

            if (hedef == null)
            {
                dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    Hata = nameof(Hedef) + " boş olmamalıdır.",
                    HataTuru = DogrulamaHataTuru.Kritik
                });
            }
            else
            {
                if (hedef.Oge == null)
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(Hedef.Oge) + " alanı boş olmamalıdır.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });

                if (!(hedef.Oge is KurumKurulus || hedef.Oge is TuzelSahis || hedef.Oge is GercekSahis))
                {
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(hedef.Oge) + " alanı için " + typeof(KurumKurulus).Name + "," +
                               typeof(TuzelSahis).Name + " veya " + typeof(GercekSahis).Name + " tipinde olmalıdır.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });
                }
                else if (hedef.Oge is KurumKurulus kurumKurulus)
                {
                    var hatalar = new List<DogrulamaHatasi>();
                    if (!kurumKurulus.Dogrula(paketVersiyon, ref hatalar))
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            AltDogrulamaHatalari = hatalar,
                            Hata = nameof(hedef.Oge) + " alanı doğrulanamamıştır.",
                            HataTuru = hatalar.GetDogrulamaHataTuru().Value
                        });
                }
                else if (hedef.Oge is TuzelSahis tuzelSahis)
                {
                    var hatalar = new List<DogrulamaHatasi>();
                    if (!tuzelSahis.Dogrula(paketVersiyon, ref hatalar))
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            AltDogrulamaHatalari = hatalar,
                            Hata = nameof(hedef.Oge) + " alanı doğrulanamamıştır.",
                            HataTuru = hatalar.GetDogrulamaHataTuru().Value
                        });
                }
                else if (hedef.Oge is GercekSahis gercekSahis)
                {
                    var hatalar = new List<DogrulamaHatasi>();
                    if (!gercekSahis.Dogrula(paketVersiyon, ref hatalar))
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            AltDogrulamaHatalari = hatalar,
                            Hata = nameof(hedef.Oge) + " alanı doğrulanamamıştır.",
                            HataTuru = hatalar.GetDogrulamaHataTuru().Value
                        });
                }
            }

            if (dogrulamaHatalari.Count > 0)
                dogrulamaHatalariRef.AddRange(dogrulamaHatalari);
            return dogrulamaHatalari.Count == 0;
        }

        public static bool Dogrula(this IletisimBilgisi iletisimBilgisi, PaketVersiyonTuru paketVersiyon,
            ref List<DogrulamaHatasi> dogrulamaHatalariRef)
        {
            var dogrulamaHatalari = new List<DogrulamaHatasi>();

            if (iletisimBilgisi == null)
            {
                dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    Hata = nameof(iletisimBilgisi) + " boş olmamalıdır.",
                    HataTuru = DogrulamaHataTuru.Kritik
                });
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(iletisimBilgisi.Telefon) && !iletisimBilgisi.Telefon.DogrulaTelefon())
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(iletisimBilgisi.Telefon) + " alanı formatı uygun değildir.",
                        HataTuru = DogrulamaHataTuru.Onemli
                    });
                else
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(iletisimBilgisi.Telefon) + " alanı boş olmamalıdır.",
                        HataTuru = DogrulamaHataTuru.Uyari
                    });

                if (!string.IsNullOrWhiteSpace(iletisimBilgisi.TelefonDiger) &&
                    !iletisimBilgisi.TelefonDiger.DogrulaTelefon())
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(iletisimBilgisi.TelefonDiger) + " alanı formatı uygun değildir.",
                        HataTuru = DogrulamaHataTuru.Uyari
                    });

                if (!string.IsNullOrWhiteSpace(iletisimBilgisi.Faks) && !iletisimBilgisi.Faks.DogrulaTelefon())
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(iletisimBilgisi.Faks) + " alanı formatı uygun değildir.",
                        HataTuru = DogrulamaHataTuru.Uyari
                    });
                else
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(iletisimBilgisi.Faks) + " alanı boş olmamalıdır.",
                        HataTuru = DogrulamaHataTuru.Uyari
                    });

                if (!string.IsNullOrWhiteSpace(iletisimBilgisi.EPosta) &&
                    !iletisimBilgisi.EPosta.ToLower().DogrulaEPosta())
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(iletisimBilgisi.EPosta) + " alanı formatı uygun değildir.",
                        HataTuru = DogrulamaHataTuru.Onemli
                    });
                else
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(iletisimBilgisi.EPosta) + " alanı boş olmamalıdır.",
                        HataTuru = DogrulamaHataTuru.Uyari
                    });

                if (!string.IsNullOrWhiteSpace(iletisimBilgisi.WebAdresi) &&
                    !iletisimBilgisi.WebAdresi.ToLower().DogrulaWebAdresi())
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(iletisimBilgisi.WebAdresi) + " alanı formatı uygun değildir.",
                        HataTuru = DogrulamaHataTuru.Onemli
                    });
                else
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(iletisimBilgisi.WebAdresi) + " alanı boş olmamalıdır.",
                        HataTuru = DogrulamaHataTuru.Uyari
                    });

                if (paketVersiyon == PaketVersiyonTuru.Versiyon2X)
                {
                    if (!string.IsNullOrWhiteSpace(iletisimBilgisi.KepAdresi) &&
                        !iletisimBilgisi.KepAdresi.ToLower().DogrulaKepAdresi())
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            Hata = nameof(iletisimBilgisi.KepAdresi) + " alanı formatı uygun değildir.",
                            HataTuru = DogrulamaHataTuru.Onemli
                        });
                    else
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            Hata = nameof(iletisimBilgisi.KepAdresi) + " alanı boş olmamalıdır.",
                            HataTuru = DogrulamaHataTuru.Uyari
                        });
                }

                if (iletisimBilgisi.Adres != null)
                {
                    var hatalar = new List<DogrulamaHatasi>();
                    if (!iletisimBilgisi.Adres.Dogrula(paketVersiyon, ref hatalar))
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            AltDogrulamaHatalari = hatalar,
                            Hata = nameof(iletisimBilgisi.Adres) + " alanı doğrulanamamıştır.",
                            HataTuru = hatalar.GetDogrulamaHataTuru().Value
                        });

                    if (iletisimBilgisi.Ilce != null)
                    {
                        hatalar = new List<DogrulamaHatasi>();
                        if (!iletisimBilgisi.Ilce.Dogrula(paketVersiyon, ref hatalar))
                            dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                AltDogrulamaHatalari = hatalar,
                                Hata = nameof(iletisimBilgisi.Ilce) + " alanı doğrulanamamıştır.",
                                HataTuru = hatalar.GetDogrulamaHataTuru().Value
                            });
                    }
                    else
                    {
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            Hata = nameof(iletisimBilgisi.Ilce) + " alanı boş olmamalıdır.",
                            HataTuru = DogrulamaHataTuru.Uyari
                        });
                    }

                    if (iletisimBilgisi.Il != null)
                    {
                        hatalar = new List<DogrulamaHatasi>();
                        if (!iletisimBilgisi.Il.Dogrula(paketVersiyon, ref hatalar))
                            dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                AltDogrulamaHatalari = hatalar,
                                Hata = nameof(iletisimBilgisi.Il) + " alanı doğrulanamamıştır.",
                                HataTuru = hatalar.GetDogrulamaHataTuru().Value
                            });
                    }
                    else
                    {
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            Hata = nameof(iletisimBilgisi.Il) + " alanı boş olmamalıdır.",
                            HataTuru = DogrulamaHataTuru.Uyari
                        });
                    }

                    if (iletisimBilgisi.Ulke != null)
                    {
                        hatalar = new List<DogrulamaHatasi>();
                        if (!iletisimBilgisi.Ulke.Dogrula(paketVersiyon, ref hatalar))
                            dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                AltDogrulamaHatalari = hatalar,
                                Hata = nameof(iletisimBilgisi.Ulke) + " alanı doğrulanamamıştır.",
                                HataTuru = hatalar.GetDogrulamaHataTuru().Value
                            });
                    }
                    else
                    {
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            Hata = nameof(iletisimBilgisi.Ulke) + " alanı boş olmamalıdır.",
                            HataTuru = DogrulamaHataTuru.Uyari
                        });
                    }
                }
                else
                {
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(iletisimBilgisi.Adres) + " alanı boş olmamalıdır.",
                        HataTuru = DogrulamaHataTuru.Uyari
                    });
                }
            }

            if (dogrulamaHatalari.Count > 0)
                dogrulamaHatalariRef.AddRange(dogrulamaHatalari);
            return dogrulamaHatalari.Count == 0;
        }

        public static bool Dogrula(this BelgeHedef belgeHedef, PaketVersiyonTuru paketVersiyon,
            ref List<DogrulamaHatasi> dogrulamaHatalariRef)
        {
            var dogrulamaHatalari = new List<DogrulamaHatasi>();

            if (belgeHedef == null)
            {
                dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    Hata = nameof(belgeHedef) + " boş olmamalıdır.",
                    HataTuru = DogrulamaHataTuru.Kritik
                });
            }
            else
            {
                if (belgeHedef.Hedefler == null || belgeHedef.Hedefler.Count == 0)
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(belgeHedef) + " alanı boş olmamalıdır.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });
                else
                    foreach (var hedef in belgeHedef.Hedefler)
                    {
                        var hatalar = new List<DogrulamaHatasi>();
                        if (!hedef.Dogrula(paketVersiyon, ref hatalar))
                            dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                AltDogrulamaHatalari = hatalar,
                                Hata = nameof(Hedef) + " alanı doğrulanamamıştır.",
                                HataTuru = hatalar.GetDogrulamaHataTuru().Value
                            });
                    }
            }

            if (dogrulamaHatalari.Count > 0)
                dogrulamaHatalariRef.AddRange(dogrulamaHatalari);
            return dogrulamaHatalari.Count == 0;
        }

        public static bool Dogrula(this Imza imza, PaketVersiyonTuru paketVersiyon,
            ref List<DogrulamaHatasi> dogrulamaHatalariRef)
        {
            var dogrulamaHatalari = new List<DogrulamaHatasi>();

            if (imza == null)
            {
                dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    Hata = nameof(imza) + " boş olmamalıdır.",
                    HataTuru = DogrulamaHataTuru.Kritik
                });
            }
            else
            {
                if (imza.Imzalayan == null)
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(imza.Imzalayan) + " alanı boş olmamalıdır.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });

                var hatalar = new List<DogrulamaHatasi>();
                if (!imza.Imzalayan.Dogrula(paketVersiyon, ref hatalar))
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        AltDogrulamaHatalari = hatalar,
                        Hata = nameof(imza.Imzalayan) + " alanı doğrulanamamıştır.",
                        HataTuru = hatalar.GetDogrulamaHataTuru().Value
                    });

                if (imza.YetkiDevreden != null)
                {
                    hatalar = new List<DogrulamaHatasi>();
                    if (!imza.YetkiDevreden.Dogrula(paketVersiyon, ref hatalar))
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            AltDogrulamaHatalari = hatalar,
                            Hata = nameof(imza.YetkiDevreden) + " alanı doğrulanamamıştır.",
                            HataTuru = hatalar.GetDogrulamaHataTuru().Value
                        });
                }

                if (imza.VekaletVeren != null)
                {
                    hatalar = new List<DogrulamaHatasi>();
                    if (!imza.VekaletVeren.Dogrula(paketVersiyon, ref hatalar))
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            AltDogrulamaHatalari = hatalar,
                            Hata = nameof(imza.VekaletVeren) + " alanı doğrulanamamıştır.",
                            HataTuru = hatalar.GetDogrulamaHataTuru().Value
                        });
                }

                if (!string.IsNullOrWhiteSpace(imza.TCYK))
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(imza.TCYK) + " alanı kullanımdan kaldırılmıştır.",
                        HataTuru = DogrulamaHataTuru.Onemli
                    });

                if (imza.Aciklama != null)
                {
                    hatalar = new List<DogrulamaHatasi>();
                    if (!imza.Aciklama.Dogrula(paketVersiyon, ref hatalar))
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            AltDogrulamaHatalari = hatalar,
                            Hata = nameof(imza.Aciklama) + " alanı doğrulanamamıştır.",
                            HataTuru = hatalar.GetDogrulamaHataTuru().Value
                        });
                }

                if (imza.Amac != null)
                {
                    hatalar = new List<DogrulamaHatasi>();
                    if (!imza.Amac.Dogrula(paketVersiyon, ref hatalar))
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            AltDogrulamaHatalari = hatalar,
                            Hata = nameof(imza.Amac) + " alanı doğrulanamamıştır.",
                            HataTuru = hatalar.GetDogrulamaHataTuru().Value
                        });
                }
                else
                {
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(imza.Amac) + " alanı boş olmamalıdır.",
                        HataTuru = DogrulamaHataTuru.Uyari
                    });
                }

                if (imza.Makam != null)
                {
                    hatalar = new List<DogrulamaHatasi>();
                    if (!imza.Makam.Dogrula(paketVersiyon, ref hatalar))
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            AltDogrulamaHatalari = hatalar,
                            Hata = nameof(imza.Makam) + " alanı doğrulanamamıştır.",
                            HataTuru = hatalar.GetDogrulamaHataTuru().Value
                        });
                }
            }

            if (dogrulamaHatalari.Count > 0)
                dogrulamaHatalariRef.AddRange(dogrulamaHatalari);
            return dogrulamaHatalari.Count == 0;
        }

        public static bool Dogrula(this BelgeImza belgeImza, PaketVersiyonTuru paketVersiyon,
            ref List<DogrulamaHatasi> dogrulamaHatalariRef)
        {
            var dogrulamaHatalari = new List<DogrulamaHatasi>();

            if (belgeImza == null)
            {
                dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    Hata = nameof(belgeImza) + " boş olmamalıdır.",
                    HataTuru = DogrulamaHataTuru.Kritik
                });
            }
            else
            {
                if (belgeImza.Imzalar == null || belgeImza.Imzalar.Count == 0)
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(belgeImza.Imzalar) + " alanı boş olmamalıdır ve en az bir tane " +
                               typeof(Imza).Name + " tipinde item içermelidir.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });
                else
                    foreach (var imza in belgeImza.Imzalar)
                    {
                        var hatalar = new List<DogrulamaHatasi>();
                        if (!imza.Dogrula(paketVersiyon, ref hatalar))
                            dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                AltDogrulamaHatalari = hatalar,
                                Hata = nameof(imza) + " alanı doğrulanamamıştır.",
                                HataTuru = hatalar.GetDogrulamaHataTuru().Value
                            });
                    }
            }

            if (dogrulamaHatalari.Count > 0)
                dogrulamaHatalariRef.AddRange(dogrulamaHatalari);
            return dogrulamaHatalari.Count == 0;
        }

        public static bool Dogrula(this Referans referans, PaketVersiyonTuru paketVersiyon,
            ref List<DogrulamaHatasi> dogrulamaHatalariRef)
        {
            var dogrulamaHatalari = new List<DogrulamaHatasi>();

            if (referans == null)
            {
                dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    Hata = nameof(referans) + " boş olmamalıdır.",
                    HataTuru = DogrulamaHataTuru.Kritik
                });
            }
            else
            {
                var hatalar = new List<DogrulamaHatasi>();
                if (!referans.Ozet.Dogrula(paketVersiyon, ref hatalar))
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        AltDogrulamaHatalari = hatalar,
                        Hata = nameof(referans.Ozet) + " alanı doğrulanamamıştır.",
                        HataTuru = hatalar.GetDogrulamaHataTuru().Value
                    });

                if (paketVersiyon == PaketVersiyonTuru.Versiyon2X)
                {
                    hatalar = new List<DogrulamaHatasi>();
                    if (!referans.Ozet.Dogrula(paketVersiyon, ref hatalar))
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            AltDogrulamaHatalari = hatalar,
                            Hata = nameof(referans.Ozet1) + " alanı doğrulanamamıştır.",
                            HataTuru = hatalar.GetDogrulamaHataTuru().Value
                        });
                }
            }

            if (dogrulamaHatalari.Count > 0)
                dogrulamaHatalariRef.AddRange(dogrulamaHatalari);
            return dogrulamaHatalari.Count == 0;
        }

        public static bool Dogrula(this HEYSK heysk, PaketVersiyonTuru paketVersiyon,
            ref List<DogrulamaHatasi> dogrulamaHatalariRef)
        {
            var dogrulamaHatalari = new List<DogrulamaHatasi>();

            if (heysk == null)
            {
                dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    Hata = nameof(heysk) + " boş olmamalıdır.",
                    HataTuru = DogrulamaHataTuru.Kritik
                });
            }
            else
            {
                if (heysk.Kod <= 0)
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(heysk.Kod) + " alanı 0'dan büyük olmalıdır.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });

                if (string.IsNullOrWhiteSpace(heysk.Ad))
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(heysk.Ad) + " alanı boş olmamalıdır.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });
            }

            if (dogrulamaHatalari.Count > 0)
                dogrulamaHatalariRef.AddRange(dogrulamaHatalari);
            return dogrulamaHatalari.Count == 0;
        }

        public static bool Dogrula(this UstYazi ustYazi, PaketVersiyonTuru paketVersiyon,
            ref List<DogrulamaHatasi> dogrulamaHatalariRef)
        {
            var dogrulamaHatalari = new List<DogrulamaHatasi>();

            if (ustYazi == null)
            {
                dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    Hata = nameof(ustYazi) + " boş olmamalıdır.",
                    HataTuru = DogrulamaHataTuru.Kritik
                });
            }
            else
            {
                if (ustYazi.Dosya == null || ustYazi.Dosya.Length == 0)
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(ustYazi.Dosya) + " alanı boş olmamalıdır.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });
                if (string.IsNullOrWhiteSpace(ustYazi.DosyaAdi))
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(ustYazi.DosyaAdi) + " alanı boş olmamalıdır.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });
                if (string.IsNullOrWhiteSpace(ustYazi.MimeTuru))
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(ustYazi.MimeTuru) + " alanı boş olmamalıdır.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });
            }

            if (dogrulamaHatalari.Count > 0)
                dogrulamaHatalariRef.AddRange(dogrulamaHatalari);
            return dogrulamaHatalari.Count == 0;
        }

        public static bool Dogrula(this EkDosya ekDosya, PaketVersiyonTuru paketVersiyon,
            ref List<DogrulamaHatasi> dogrulamaHatalariRef)
        {
            var dogrulamaHatalari = new List<DogrulamaHatasi>();

            if (ekDosya == null)
            {
                dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    Hata = nameof(EkDosya) + " boş olmamalıdır.",
                    HataTuru = DogrulamaHataTuru.Kritik
                });
            }
            else
            {
                if (ekDosya.Dosya == null || ekDosya.Dosya.Length == 0)
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(EkDosya.Dosya) + " alanı boş olmamalıdır.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });

                if (string.IsNullOrWhiteSpace(ekDosya.DosyaAdi))
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(EkDosya.DosyaAdi) + " alanı boş olmamalıdır.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });

                if (ekDosya.Ek == null)
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(EkDosya.Ek) + " alanı boş olmamalıdır.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });

                if (ekDosya.Ek.Tur != EkTuru.DED)
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = "Ek türü " + nameof(EkTuru.DED) + " olmayan eklere ait " + nameof(EkDosya) +
                               "bileşeni eklenemez.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });

                var hatalar = new List<DogrulamaHatasi>();
                ekDosya.Ek.Dogrula(paketVersiyon, ref hatalar);
                if (hatalar.Count > 0)
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        AltDogrulamaHatalari = hatalar,
                        Hata = nameof(EkDosya.Ek) + " alanı doğrulanamamıştır.",
                        HataTuru = hatalar.GetDogrulamaHataTuru().Value
                    });
            }

            if (dogrulamaHatalari.Count > 0)
                dogrulamaHatalariRef.AddRange(dogrulamaHatalari);
            return dogrulamaHatalari.Count == 0;
        }

        public static bool Dogrula(this SDP sdp, PaketVersiyonTuru paketVersiyon,
            ref List<DogrulamaHatasi> dogrulamaHatalariRef)
        {
            var dogrulamaHatalari = new List<DogrulamaHatasi>();

            if (sdp == null)
            {
                dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    Hata = nameof(sdp) + " boş olmamalıdır.",
                    HataTuru = DogrulamaHataTuru.Kritik
                });
            }
            else
            {
                if (string.IsNullOrWhiteSpace(sdp.Kod))
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(sdp.Kod) + " alanı boş olmamalıdır.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });
                else if (!sdp.Kod.DogrulaSdpKod())
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(sdp.Kod) + " alanı formatı uygun değildir.",
                        HataTuru = DogrulamaHataTuru.Onemli
                    });
                else if (string.IsNullOrWhiteSpace(sdp.Ad))
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(sdp.Ad) + " alanı boş olmamalıdır.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });
            }

            if (dogrulamaHatalari.Count > 0)
                dogrulamaHatalariRef.AddRange(dogrulamaHatalari);
            return dogrulamaHatalari.Count == 0;
        }

        public static bool Dogrula(this DogrulamaBilgisi dogrulamaBilgisi, PaketVersiyonTuru paketVersiyon,
            ref List<DogrulamaHatasi> dogrulamaHatalariRef)
        {
            var dogrulamaHatalari = new List<DogrulamaHatasi>();

            if (dogrulamaBilgisi == null)
            {
                dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    Hata = nameof(dogrulamaBilgisi) + " boş olmamalıdır.",
                    HataTuru = DogrulamaHataTuru.Kritik
                });
            }
            else
            {
                if (string.IsNullOrWhiteSpace(dogrulamaBilgisi.DogrulamaAdresi))
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(dogrulamaBilgisi.DogrulamaAdresi) + " alanı boş olmamalıdır.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });

                else if (!dogrulamaBilgisi.DogrulamaAdresi.DogrulaWebAdresi())
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(dogrulamaBilgisi.DogrulamaAdresi) + " alanı formatı uygun değildir.",
                        HataTuru = DogrulamaHataTuru.Onemli
                    });
            }

            if (dogrulamaHatalari.Count > 0)
                dogrulamaHatalariRef.AddRange(dogrulamaHatalari);
            return dogrulamaHatalari.Count == 0;
        }

        public static bool Dogrula(this ParafOzeti parafOzeti, PaketVersiyonTuru paketVersiyon,
            ref List<DogrulamaHatasi> dogrulamaHatalariRef)
        {
            var dogrulamaHatalari = new List<DogrulamaHatasi>();

            if (parafOzeti == null)
            {
                dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    Hata = nameof(parafOzeti) + " boş olmamalıdır.",
                    HataTuru = DogrulamaHataTuru.Kritik
                });
            }
            else
            {
                if (parafOzeti.Id == Guid.Empty)
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(parafOzeti.Id) + " alanı boş olmamalıdır.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });

                if (parafOzeti.Referanslar == null || parafOzeti.Referanslar.Count == 0)
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(parafOzeti.Referanslar) + " alanı boş olmamalıdır ve en az bir tane " +
                               typeof(Referans).Name + " tipinde öğe içermelidir.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });
                else
                    foreach (var referans in parafOzeti.Referanslar)
                    {
                        var hatalar = new List<DogrulamaHatasi>();
                        if (!referans.Dogrula(paketVersiyon, ref hatalar))
                            dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                AltDogrulamaHatalari = hatalar,
                                Hata = nameof(referans) + " alanı doğrulanamamıştır.",
                                HataTuru = hatalar.GetDogrulamaHataTuru().Value
                            });
                    }
            }

            if (dogrulamaHatalari.Count > 0)
                dogrulamaHatalariRef.AddRange(dogrulamaHatalari);
            return dogrulamaHatalari.Count == 0;
        }

        public static bool Dogrula(this PaketOzeti paketOzeti, PaketVersiyonTuru paketVersiyon,
            ref List<DogrulamaHatasi> dogrulamaHatalariRef)
        {
            var dogrulamaHatalari = new List<DogrulamaHatasi>();

            if (paketOzeti == null)
            {
                dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    Hata = nameof(paketOzeti) + " boş olmamalıdır.",
                    HataTuru = DogrulamaHataTuru.Kritik
                });
            }
            else
            {
                if (paketOzeti.Id == Guid.Empty)
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(paketOzeti.Id) + " alanı boş olmamalıdır.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });

                if (paketOzeti.Referanslar == null || paketOzeti.Referanslar.Count == 0)
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(paketOzeti.Referanslar) + " alanı boş olmamalıdır ve en az bir tane " +
                               typeof(Referans).Name + " tipinde öğe içermelidir.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });
                else
                    foreach (var referans in paketOzeti.Referanslar)
                    {
                        var hatalar = new List<DogrulamaHatasi>();
                        if (!referans.Dogrula(paketVersiyon, ref hatalar))
                            dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                AltDogrulamaHatalari = hatalar,
                                Hata = nameof(referans) + " alanı doğrulanamamıştır.",
                                HataTuru = hatalar.GetDogrulamaHataTuru().Value
                            });
                    }
            }

            if (dogrulamaHatalari.Count > 0)
                dogrulamaHatalariRef.AddRange(dogrulamaHatalari);
            return dogrulamaHatalari.Count == 0;
        }

        public static bool Dogrula(this NihaiOzet nihaiOzet, PaketVersiyonTuru paketVersiyon,
            ref List<DogrulamaHatasi> dogrulamaHatalariRef)
        {
            var dogrulamaHatalari = new List<DogrulamaHatasi>();

            if (nihaiOzet == null)
            {
                dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    Hata = nameof(nihaiOzet) + " boş olmamalıdır.",
                    HataTuru = DogrulamaHataTuru.Kritik
                });
            }
            else
            {
                if (nihaiOzet.Id == Guid.Empty)
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(nihaiOzet.Id) + " alanı boş olmamalıdır.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });

                if (nihaiOzet.Referanslar == null || nihaiOzet.Referanslar.Count == 0)
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(nihaiOzet.Referanslar) + " alanı boş olmamalıdır ve en az bir tane " +
                               typeof(Referans).Name + " tipinde öğe içermelidir.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });
                else
                    foreach (var referans in nihaiOzet.Referanslar)
                    {
                        var hatalar = new List<DogrulamaHatasi>();
                        if (!referans.Dogrula(paketVersiyon, ref hatalar))
                            dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                AltDogrulamaHatalari = hatalar,
                                Hata = nameof(referans) + " alanı doğrulanamamıştır.",
                                HataTuru = hatalar.GetDogrulamaHataTuru().Value
                            });
                    }
            }

            if (dogrulamaHatalari.Count > 0)
                dogrulamaHatalariRef.AddRange(dogrulamaHatalari);
            return dogrulamaHatalari.Count == 0;
        }

        internal static bool IlgileriDogrula(this Ustveri ustveri, PaketVersiyonTuru paketVersiyon,
            ref List<DogrulamaHatasi> dogrulamaHatalariRef)
        {
            var dogrulamaHatalari = new List<DogrulamaHatasi>();

            if (ustveri.Ilgiler != null && ustveri.Ilgiler.Count > 0)
            {
                if (ustveri.Ilgiler.Select(p => p.Etiket).Distinct().Count() < ustveri.Ilgiler.Count)
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = "Paket içerisine aynı Etiket değerine sahip birden fazla ilgi eklenmemelidir.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });

                var alfabe = "abcçdefgğhıijklmnoöprsştuüvyz";
                var etiketler = ustveri.Ilgiler.Select(p => p.Etiket.ToString().ToLower()).OrderBy(x => x).ToArray();

                if (etiketler[0] != "a")
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = "Paket içerisine eklenen ilgilerin Etiket değerleri \"a\" harfi ile başlamalıdır.",
                        HataTuru = DogrulamaHataTuru.Uyari
                    });

                if (string.Join("", etiketler) != alfabe.Substring(0, etiketler.Length))
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata =
                            "Paket içerisine eklenen ilgilerin Etiket değerleri alfabe sıralamasına göre yapılmamıştır.",
                        HataTuru = DogrulamaHataTuru.Uyari
                    });

                foreach (var ilgi in ustveri.Ilgiler)
                {
                    var hatalar = new List<DogrulamaHatasi>();
                    if (!ilgi.Dogrula(paketVersiyon, ref hatalar))
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            AltDogrulamaHatalari = hatalar,
                            Hata = nameof(ilgi) + " alanı doğrulanamamıştır.",
                            HataTuru = hatalar.GetDogrulamaHataTuru().Value
                        });
                }

                var ekOlarakBelirtilmisIlgiler = ustveri.Ilgiler.Where(ilgi => ilgi.EkIdDeger.HasValue).ToArray();
                if (ekOlarakBelirtilmisIlgiler.Length > 0)
                {
                    if (ustveri.Ekler == null || ustveri.Ekler.Count == 0)
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            Hata =
                                "Ustveri bileşeninde verilen ilgilerden, ek olarak belirtilmiş ilgi için, paket içerisine eklenmiş Ek bileşeni bulunamamıştır.",
                            HataTuru = DogrulamaHataTuru.Kritik
                        });
                    else
                        foreach (var ekOlarakBelirtilmisIlgi in ekOlarakBelirtilmisIlgiler)
                            if (!ustveri.Ekler.Any(ek =>
                                string.Compare(ek.Id.Deger, ekOlarakBelirtilmisIlgi.EkIdDeger.ToString(), true) == 0))
                            {
                                dogrulamaHatalari.Add(new DogrulamaHatasi
                                {
                                    Hata =
                                        "Ustveri bileşeninde verilen ilgilerden, ek olarak belirtilmiş ilgi için, paket içerisine eklenmiş Ek bileşeni bulunamamıştır.",
                                    HataTuru = DogrulamaHataTuru.Kritik
                                });
                                break;
                            }
                }
            }

            if (dogrulamaHatalari.Count > 0)
                dogrulamaHatalariRef.AddRange(dogrulamaHatalari);
            return dogrulamaHatalari.Count == 0;
        }

        internal static bool EkleriDogrula(this Ustveri ustveri,
            Package package,
            PaketVersiyonTuru paketVersiyon,
            ref List<DogrulamaHatasi> dogrulamaHatalariRef,
            TanimlayiciTip dagitimTanimlayici = null)
        {
            var dogrulamaHatalari = new List<DogrulamaHatasi>();

            if (ustveri.Ekler != null && ustveri.Ekler.Count > 0)
            {
                if (ustveri.Ekler.Any(p => p.SiraNo <= 0))
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = "Paket içerisine SiraNo alanı '1'den küçük olan ek eklenmemelidir.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });

                if (ustveri.Ekler.Select(p => p.SiraNo).Distinct().Count() < ustveri.Ekler.Count)
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = "Paket içerisine aynı SiraNo değerine sahip birden fazla ek eklenmemelidir.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });

                foreach (var ek in ustveri.Ekler)
                {
                    var hatalar = new List<DogrulamaHatasi>();
                    if (!ek.Dogrula(paketVersiyon, ref hatalar))
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            AltDogrulamaHatalari = hatalar,
                            Hata = nameof(Ek) + " alanı doğrulanamamıştır.",
                            HataTuru = hatalar.GetDogrulamaHataTuru().Value
                        });
                }

                if (ustveri.Dagitimlar == null || ustveri.Dagitimlar.Count == 0)
                {
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = "Paket içerisine eklenmiş dağıtımlar bulunamamıştır.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });
                }
                else
                {
                    Dagitim dagitim = null;
                    if (ustveri.Dagitimlar != null && ustveri.Dagitimlar.Count == 0)
                        foreach (var dagitimOge in ustveri.Dagitimlar)
                            if (dagitim.Oge is KurumKurulus kurumKurulus)
                            {
                                if (!string.IsNullOrWhiteSpace(kurumKurulus.KKK) &&
                                    kurumKurulus.KKK == dagitimTanimlayici.Deger)
                                {
                                    dagitim = dagitimOge;
                                    break;
                                }
                            }
                            else if (dagitim.Oge is TuzelSahis tuzelSahis)
                            {
                                if (tuzelSahis.Id != null && string.IsNullOrWhiteSpace(tuzelSahis.Id.Deger) &&
                                    tuzelSahis.Id.Deger == dagitimTanimlayici.Deger)
                                {
                                    dagitim = dagitimOge;
                                    break;
                                }
                            }
                            else if (dagitim.Oge is GercekSahis gercekSahis)
                            {
                                if (!string.IsNullOrWhiteSpace(gercekSahis.TCKN) &&
                                    gercekSahis.TCKN == dagitimTanimlayici.Deger)
                                {
                                    dagitim = dagitimOge;
                                    break;
                                }
                            }

                    foreach (var ustveriEki in ustveri.Ekler)
                        if (ustveriEki.Tur == EkTuru.DED && !package.EkDosyaExists(ustveriEki.Id))
                            if (dagitimTanimlayici == null || dagitim == null || dagitim.KonulmamisEkler == null ||
                                dagitim.KonulmamisEkler.Count == 0 || !dagitim.KonulmamisEkler.Any(p =>
                                    string.Compare(p.EkIdDeger.ToString(), ustveriEki.Id.Deger, true) == 0))
                                dogrulamaHatalari.Add(new DogrulamaHatasi
                                {
                                    Hata = string.Format(
                                        "Ustveri bileşeni için eklenen ek, paket içerisine eklenmemiştir. EkId: {0}",
                                        ustveriEki.Id.Deger),
                                    HataTuru = DogrulamaHataTuru.Kritik
                                });
                }
            }

            if ((ustveri.Ekler == null || ustveri.Ekler.Count == 0) &&
                (package.GetRelationshipsByType(Constants.RELATION_TYPE_EK).Count() > 0 ||
                 package.GetRelationshipsByType(Constants.RELATION_TYPE_IMZASIZEK).Count() > 0))
            {
                dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    Hata = "Paket içerisine eklenmiş eklerin hiç biri, üstveri bileşeninde belirtilmemiştir.",
                    HataTuru = DogrulamaHataTuru.Kritik
                });
            }
            else
            {
                foreach (var relationship in package.GetRelationshipsByType(Constants.RELATION_TYPE_EK))
                {
                    var relationshipEki = ustveri.Ekler.SingleOrDefault(re =>
                        string.Compare(relationship.Id, Constants.ID_ROOT_EK(re.Id.Deger), true) == 0);

                    if (relationshipEki == default(Ek))
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            Hata = string.Format(
                                "Paket içerisine eklenmiş ek, üstveri bileşeninde belirtilmemiştir. EkId: {0}",
                                relationship.Id.Replace("IdEk_", "")),
                            HataTuru = DogrulamaHataTuru.Kritik
                        });
                    else if (relationshipEki.Tur != EkTuru.DED)
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            Hata = string.Format(
                                "Paket içerisine eklenmiş ek, üstveri bileşeninde " + nameof(EkTuru.DED) +
                                " olarak belirtilmelidir. EkId: {0}", relationship.Id.Replace("IdEk_", "")),
                            HataTuru = DogrulamaHataTuru.Kritik
                        });
                }

                foreach (var relationship in package.GetRelationshipsByType(Constants.RELATION_TYPE_IMZASIZEK))
                {
                    var relationshipEki = ustveri.Ekler.SingleOrDefault(re =>
                        string.Compare(relationship.Id, Constants.ID_ROOT_IMZASIZEK(re.Id.Deger), true) == 0);

                    if (relationshipEki == default(Ek))
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            Hata = string.Format(
                                "Paket içerisine eklenmiş ek, üstveri bileşeninde belirtilmemiştir. EkId: {0}",
                                relationship.Id.Replace("IdImzasizEk_", "")),
                            HataTuru = DogrulamaHataTuru.Kritik
                        });
                    else if (relationshipEki.Tur != EkTuru.DED)
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            Hata = string.Format(
                                "Paket içerisine eklenmiş ek, üstveri bileşeninde " + nameof(EkTuru.DED) +
                                " olarak belirtilmelidir. EkId: {0}", relationship.Id.Replace("IdImzasizEk_", "")),
                            HataTuru = DogrulamaHataTuru.Kritik
                        });
                }
            }

            if (dogrulamaHatalari.Count > 0)
                dogrulamaHatalariRef.AddRange(dogrulamaHatalari);
            return dogrulamaHatalari.Count == 0;
        }

        internal static bool NihaiOzetDogrula(this NihaiOzet nihaiOzet,
            Package package,
            PaketVersiyonTuru paketVersiyon,
            ref List<DogrulamaHatasi> dogrulamaHatalariRef,
            Ustveri ustveri,
            TanimlayiciTip dagitimTanimlayici = null)
        {
            var dogrulamaHatalari = new List<DogrulamaHatasi>();

            if (nihaiOzet == null)
            {
                dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    Hata = nameof(nihaiOzet) + " boş olmamalıdır.",
                    HataTuru = DogrulamaHataTuru.Kritik
                });
            }
            else
            {
                if (!package.UstveriExists())
                {
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = "Ustveri bileşeni yoktur.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });
                }
                else
                {
                    var readedUstveriUri = PackUriHelper.CreatePartUri(package
                        .GetRelationshipsByType(Constants.RELATION_TYPE_USTVERI).First().TargetUri);
                    if (!nihaiOzet.Referanslar.Any(p => string.Compare(p.URI, readedUstveriUri.ToString(), true) == 0))
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            Hata = "NihaiOzet bileşeninde Ustveri özet alanı yoktur.",
                            HataTuru = DogrulamaHataTuru.Kritik
                        });
                }

                if (!package.UstYaziExists())
                {
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = "UstYazi bileşeni yoktur.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });
                }
                else
                {
                    var readedUstYaziUri = PackUriHelper.CreatePartUri(package
                        .GetRelationshipsByType(Constants.RELATION_TYPE_USTYAZI).First().TargetUri);
                    if (!nihaiOzet.Referanslar.Any(p => string.Compare(p.URI, readedUstYaziUri.ToString(), true) == 0))
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            Hata = "NihaiOzet bileşeninde UstYazi özet alanı yoktur.",
                            HataTuru = DogrulamaHataTuru.Kritik
                        });
                }

                if (paketVersiyon == PaketVersiyonTuru.Versiyon1X)
                {
                    if (!package.BelgeHedefRelationExists())
                    {
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            Hata = "BelgeHedef bileşeni yoktur.",
                            HataTuru = DogrulamaHataTuru.Kritik
                        });
                    }
                    else
                    {
                        var readedBelgeHedefUri = PackUriHelper.CreatePartUri(package
                            .GetRelationshipsByType(Constants.RELATION_TYPE_BELGEHEDEF).First().TargetUri);
                        if (!nihaiOzet.Referanslar.Any(p =>
                            string.Compare(p.URI, readedBelgeHedefUri.ToString(), true) == 0))
                            dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                Hata = "NihaiOzet bileşeninde BelgeHedef özet alanı yoktur.",
                                HataTuru = DogrulamaHataTuru.Kritik
                            });
                    }

                    if (!package.BelgeImzaRelationExists())
                    {
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            Hata = "BelgeImza bileşeni yoktur.",
                            HataTuru = DogrulamaHataTuru.Kritik
                        });
                    }
                    else
                    {
                        var readedBelgeImzaUri = PackUriHelper.CreatePartUri(package
                            .GetRelationshipsByType(Constants.RELATION_TYPE_BELGEIMZA).First().TargetUri);
                        if (!nihaiOzet.Referanslar.Any(p =>
                            string.Compare(p.URI, readedBelgeImzaUri.ToString(), true) == 0))
                            dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                Hata = "NihaiOzet bileşeninde BelgeImza özet alanı yoktur.",
                                HataTuru = DogrulamaHataTuru.Kritik
                            });
                    }
                }

                if (!package.PaketOzetiExists())
                {
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = "PaketOzeti bileşeni yoktur.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });
                }
                else
                {
                    var relImza = package.GetPart(Constants.URI_PAKETOZETI)
                        .GetRelationshipsByType(Constants.RELATION_TYPE_IMZA);
                    if (relImza == null || relImza.Count() == 0)
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            Hata = "Imza bileşeni yoktur.",
                            HataTuru = DogrulamaHataTuru.Kritik
                        });
                    else if (nihaiOzet.Referanslar.Count(p =>
                        string.Compare(p.URI, PackUriHelper.CreatePartUri(relImza.First().TargetUri).ToString(),
                            true) == 0) == 0)
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            Hata = "NihaiOzet bileşeninde Imza özet alanı yoktur.",
                            HataTuru = DogrulamaHataTuru.Kritik
                        });
                }

                if (!package.CoreRelationExists())
                {
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = "Core bileşeni yoktur.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });
                }
                else
                {
                    var readedCoreUri = package.GetRelationshipsByType(Constants.RELATION_TYPE_CORE).First().TargetUri;
                    if (nihaiOzet.Referanslar.Count(p =>
                        string.Compare(p.URI, PackUriHelper.CreatePartUri(readedCoreUri).ToString(), true) == 0) == 0)
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            Hata = "NihaiOzet bileşeninde Core özet alanı yoktur.",
                            HataTuru = DogrulamaHataTuru.Kritik
                        });
                }

                foreach (var referans in nihaiOzet.Referanslar)
                {
                    var hatalar = new List<DogrulamaHatasi>();
                    if (!referans.ReferansDogrula(package, paketVersiyon, ref hatalar, ustveri, dagitimTanimlayici))
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            AltDogrulamaHatalari = hatalar,
                            Hata = nameof(referans) + " alanı doğrulanamamıştır.",
                            HataTuru = hatalar.GetDogrulamaHataTuru().Value
                        });
                }
            }

            if (dogrulamaHatalari.Count > 0)
                dogrulamaHatalariRef.AddRange(dogrulamaHatalari);
            return dogrulamaHatalari.Count == 0;
        }

        internal static bool PaketOzetiDogrula(this PaketOzeti paketOzeti,
            Package package,
            PaketVersiyonTuru paketVersiyon,
            ref List<DogrulamaHatasi> dogrulamaHatalariRef,
            Ustveri ustveri,
            TanimlayiciTip dagitimTanimlayici = null)
        {
            var dogrulamaHatalari = new List<DogrulamaHatasi>();

            if (paketOzeti == null)
            {
                dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    Hata = nameof(paketOzeti) + " boş olmamalıdır.",
                    HataTuru = DogrulamaHataTuru.Kritik
                });
            }
            else
            {
                if (!package.UstveriExists())
                {
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = "Ustveri bileşeni yoktur.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });
                }
                else
                {
                    var readedUstveriUri = PackUriHelper.CreatePartUri(package
                        .GetRelationshipsByType(Constants.RELATION_TYPE_USTVERI).First().TargetUri);
                    if (!paketOzeti.Referanslar.Any(p => string.Compare(p.URI, readedUstveriUri.ToString(), true) == 0))
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            Hata = "PaketOzeti bileşeninde Ustveri özet alanı yoktur.",
                            HataTuru = DogrulamaHataTuru.Kritik
                        });
                }

                if (!package.UstYaziExists())
                {
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = "UstYazi bileşeni yoktur.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });
                }
                else
                {
                    var readedUstYaziUri = PackUriHelper.CreatePartUri(package
                        .GetRelationshipsByType(Constants.RELATION_TYPE_USTYAZI).First().TargetUri);
                    if (!paketOzeti.Referanslar.Any(p => string.Compare(p.URI, readedUstYaziUri.ToString(), true) == 0))
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            Hata = "PaketOzeti bileşeninde UstYazi özet alanı yoktur.",
                            HataTuru = DogrulamaHataTuru.Kritik
                        });
                }

                if (paketVersiyon == PaketVersiyonTuru.Versiyon1X)
                {
                    if (!package.BelgeHedefRelationExists())
                    {
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            Hata = "BelgeHedef bileşeni yoktur.",
                            HataTuru = DogrulamaHataTuru.Kritik
                        });
                    }
                    else
                    {
                        var readedBelgeHedefUri = PackUriHelper.CreatePartUri(package
                            .GetRelationshipsByType(Constants.RELATION_TYPE_BELGEHEDEF).First().TargetUri);
                        if (!paketOzeti.Referanslar.Any(p =>
                            string.Compare(p.URI, readedBelgeHedefUri.ToString(), true) == 0))
                            dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                Hata = "PaketOzeti bileşeninde BelgeHedef özet alanı yoktur.",
                                HataTuru = DogrulamaHataTuru.Kritik
                            });
                    }
                }

                foreach (var referans in paketOzeti.Referanslar)
                {
                    var hatalar = new List<DogrulamaHatasi>();
                    if (!referans.ReferansDogrula(package, paketVersiyon, ref hatalar, ustveri, dagitimTanimlayici))
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            AltDogrulamaHatalari = hatalar,
                            Hata = nameof(referans) + " alanı doğrulanamamıştır.",
                            HataTuru = hatalar.GetDogrulamaHataTuru().Value
                        });
                }
            }

            if (dogrulamaHatalari.Count > 0)
                dogrulamaHatalariRef.AddRange(dogrulamaHatalari);
            return dogrulamaHatalari.Count == 0;
        }

        internal static bool ParafOzetiDogrula(this ParafOzeti parafOzeti,
            Package package,
            PaketVersiyonTuru paketVersiyon,
            ref List<DogrulamaHatasi> dogrulamaHatalariRef,
            Ustveri ustveri,
            TanimlayiciTip dagitimTanimlayici = null)
        {
            var dogrulamaHatalari = new List<DogrulamaHatasi>();

            if (parafOzeti == null)
            {
                dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    Hata = nameof(parafOzeti) + " boş olmamalıdır.",
                    HataTuru = DogrulamaHataTuru.Kritik
                });
            }
            else
            {
                if (!package.UstveriExists())
                {
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = "Ustveri bileşeni yoktur.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });
                }
                else
                {
                    var readedUstveriUri = PackUriHelper.CreatePartUri(package
                        .GetRelationshipsByType(Constants.RELATION_TYPE_USTVERI).First().TargetUri);
                    if (!parafOzeti.Referanslar.Any(p => string.Compare(p.URI, readedUstveriUri.ToString(), true) == 0))
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            Hata = "ParafOzeti bileşeninde Ustveri özet alanı yoktur.",
                            HataTuru = DogrulamaHataTuru.Kritik
                        });
                }

                if (!package.UstYaziExists())
                {
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = "UstYazi bileşeni yoktur.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });
                }
                else
                {
                    var readedUstYaziUri = PackUriHelper.CreatePartUri(package
                        .GetRelationshipsByType(Constants.RELATION_TYPE_USTYAZI).First().TargetUri);
                    if (!parafOzeti.Referanslar.Any(p => string.Compare(p.URI, readedUstYaziUri.ToString(), true) == 0))
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            Hata = "ParafOzeti bileşeninde UstYazi özet alanı yoktur.",
                            HataTuru = DogrulamaHataTuru.Kritik
                        });
                }

                foreach (var referans in parafOzeti.Referanslar)
                {
                    var hatalar = new List<DogrulamaHatasi>();
                    if (!referans.ReferansDogrula(package, paketVersiyon, ref hatalar, ustveri, dagitimTanimlayici))
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            AltDogrulamaHatalari = hatalar,
                            Hata = nameof(referans) + " alanı doğrulanamamıştır.",
                            HataTuru = hatalar.GetDogrulamaHataTuru().Value
                        });
                }
            }

            if (dogrulamaHatalari.Count > 0)
                dogrulamaHatalariRef.AddRange(dogrulamaHatalari);
            return dogrulamaHatalari.Count == 0;
        }

        internal static bool ReferansDogrula(this Referans referans,
            Package package,
            PaketVersiyonTuru paketVersiyon,
            ref List<DogrulamaHatasi> dogrulamaHatalariRef,
            Ustveri ustveri,
            TanimlayiciTip dagitimTanimlayici = null)
        {
            var dogrulamaHatalari = new List<DogrulamaHatasi>();

            if (referans == null)
            {
                dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    Hata = nameof(referans) + " boş olmamalıdır.",
                    HataTuru = DogrulamaHataTuru.Kritik
                });
            }
            else
            {
                if (referans.URI == null)
                {
                    dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(referans.URI) + " alanı boş olmamalıdır.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });
                }
                else
                {
                    var hatalar = new List<DogrulamaHatasi>();
                    if (!referans.Ozet.Dogrula(paketVersiyon, ref hatalar))
                    {
                        hatalar.ForEach(hata =>
                        {
                            hata.Uri = referans.URI;
                            hata.Hata = hata.Hata + " URI: " + referans.URI;
                        });
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            AltDogrulamaHatalari = hatalar,
                            Hata = nameof(referans.Ozet) + " alanı doğrulanamamıştır.",
                            HataTuru = hatalar.GetDogrulamaHataTuru().Value
                        });
                    }
                    else
                    {
                        if (referans.Ozet.OzetAlgoritmasi.Algoritma == OzetAlgoritmaTuru.YOK)
                            dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                Hata = nameof(referans.Ozet.OzetAlgoritmasi.Algoritma) +
                                       " alanı değeri desteklenmemektedir. URI: " + referans.URI,
                                Uri = referans.URI,
                                HataTuru = DogrulamaHataTuru.Kritik
                            });
                        else if (paketVersiyon == PaketVersiyonTuru.Versiyon2X &&
                                 (referans.Ozet.OzetAlgoritmasi.Algoritma == OzetAlgoritmaTuru.RIPEMD160 ||
                                  referans.Ozet.OzetAlgoritmasi.Algoritma == OzetAlgoritmaTuru.SHA1))
                            dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                Hata = nameof(OzetAlgoritmaTuru.SHA1) + " özet algoritması ve " +
                                       nameof(OzetAlgoritmaTuru.RIPEMD160) +
                                       " özet algoritması kullanımdan kaldırılmıştır. URI: " + referans.URI,
                                Uri = referans.URI,
                                HataTuru = DogrulamaHataTuru.Onemli
                            });
                        else if (paketVersiyon == PaketVersiyonTuru.Versiyon1X &&
                                 referans.Ozet.OzetAlgoritmasi.Algoritma == OzetAlgoritmaTuru.SHA384)
                            dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                Hata = nameof(OzetAlgoritmaTuru.SHA384) +
                                       " özet algoritması yalnızca e-Yazışma API 2.X versiyonlarında kullanılabilir. URI: " +
                                       referans.URI,
                                Uri = referans.URI,
                                HataTuru = DogrulamaHataTuru.Kritik
                            });
                    }

                    if (paketVersiyon == PaketVersiyonTuru.Versiyon2X)
                    {
                        hatalar = new List<DogrulamaHatasi>();

                        if (!referans.Ozet1.Dogrula(paketVersiyon, ref hatalar))
                        {
                            hatalar.ForEach(hata =>
                            {
                                hata.Uri = referans.URI;
                                hata.Hata = hata.Hata + " URI: " + referans.URI;
                            });
                            dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                AltDogrulamaHatalari = hatalar,
                                Hata = nameof(referans.Ozet1) + " alanı doğrulanamamıştır.",
                                HataTuru = hatalar.GetDogrulamaHataTuru().Value
                            });
                        }
                        else
                        {
                            if (referans.Ozet1.OzetAlgoritmasi.Algoritma == OzetAlgoritmaTuru.YOK)
                                dogrulamaHatalari.Add(new DogrulamaHatasi
                                {
                                    Hata = nameof(referans.Ozet1.OzetAlgoritmasi.Algoritma) +
                                           " alanı değeri desteklenmemektedir. URI: " + referans.URI,
                                    Uri = referans.URI,
                                    HataTuru = DogrulamaHataTuru.Kritik
                                });
                            else if (paketVersiyon == PaketVersiyonTuru.Versiyon2X &&
                                     (referans.Ozet1.OzetAlgoritmasi.Algoritma == OzetAlgoritmaTuru.RIPEMD160 ||
                                      referans.Ozet1.OzetAlgoritmasi.Algoritma == OzetAlgoritmaTuru.SHA1))
                                dogrulamaHatalari.Add(new DogrulamaHatasi
                                {
                                    Hata = nameof(OzetAlgoritmaTuru.SHA1) + " özet algoritması ve " +
                                           nameof(OzetAlgoritmaTuru.RIPEMD160) +
                                           " özet algoritması kullanımdan kaldırılmıştır. URI: " + referans.URI,
                                    Uri = referans.URI,
                                    HataTuru = DogrulamaHataTuru.Onemli
                                });
                            else if (paketVersiyon == PaketVersiyonTuru.Versiyon1X &&
                                     referans.Ozet1.OzetAlgoritmasi.Algoritma == OzetAlgoritmaTuru.SHA384)
                                dogrulamaHatalari.Add(new DogrulamaHatasi
                                {
                                    Hata = nameof(OzetAlgoritmaTuru.SHA384) +
                                           " özet algoritması yalnızca e-Yazışma API 2.X versiyonlarında kullanılabilir. URI: " +
                                           referans.URI,
                                    Uri = referans.URI,
                                    HataTuru = DogrulamaHataTuru.Kritik
                                });

                            if (referans.Ozet.OzetAlgoritmasi.Algoritma == referans.Ozet1.OzetAlgoritmasi.Algoritma)
                                dogrulamaHatalari.Add(new DogrulamaHatasi
                                {
                                    Hata = nameof(referans.Ozet.OzetAlgoritmasi.Algoritma) + " alanı değeri ile " +
                                           nameof(referans.Ozet1.OzetAlgoritmasi.Algoritma) +
                                           " alanı değeri aynı olmamalıdır. URI: " + referans.URI,
                                    Uri = referans.URI,
                                    HataTuru = DogrulamaHataTuru.Kritik
                                });

                            if (referans.Ozet.OzetAlgoritmasi.Algoritma != OzetAlgoritmaTuru.SHA512 &&
                                referans.Ozet1.OzetAlgoritmasi.Algoritma != OzetAlgoritmaTuru.SHA512)
                                dogrulamaHatalari.Add(new DogrulamaHatasi
                                {
                                    Hata = "En az bir " + nameof(OzetAlgoritmasi.Algoritma) + " alanı değeri " +
                                           nameof(OzetAlgoritmaTuru.SHA512) + " türünde olmalıdır. URI: " +
                                           referans.URI,
                                    Uri = referans.URI,
                                    HataTuru = DogrulamaHataTuru.Kritik
                                });
                        }
                    }

                    if (referans.Type != null && referans.Type == Constants.HARICI_PAKET_BILESENI_REFERANS_TIPI)
                    {
                        if (dogrulamaHatalari.Count > 0)
                            dogrulamaHatalariRef.AddRange(dogrulamaHatalari);
                        return dogrulamaHatalari.Count == 0;
                    }

                    Stream referansStream;
                    try
                    {
                        referansStream = package
                            .GetPart(PackUriHelper.CreatePartUri(new Uri(Uri.UnescapeDataString(referans.URI),
                                UriKind.Relative))).GetStream();
                    }
                    catch (Exception ex)
                    {
                        if (paketVersiyon == PaketVersiyonTuru.Versiyon2X &&
                            string.Compare(referans.URI, Constants.URI_PARAFIMZA_STRING, true) == 0 ||
                            !referans.URI.StartsWith("/Ekler/") ||
                            dagitimTanimlayici == null || ustveri.Dagitimlar == null || ustveri.Dagitimlar.Count == 0 ||
                            ustveri.Ekler == null || ustveri.Ekler.Count == 0
                        )
                        {
                            dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                Hata = string.Format("Paket bileşeni alınamamıştır. URI: {0}.", referans.URI),
                                HataTuru = DogrulamaHataTuru.Kritik,
                                Uri = referans.URI,
                                InnerException = ex
                            });
                        }
                        else
                        {
                            var ekAdi = referans.URI.Replace("/Ekler/", "");

                            var ek = ustveri.Ekler.FirstOrDefault(p => string.Compare(p.DosyaAdi, ekAdi, true) == 0);
                            if (ek == null)
                            {
                                dogrulamaHatalari.Add(new DogrulamaHatasi
                                {
                                    Hata = string.Format("Paket bileşeni alınamamıştır. URI: {0}.", referans.URI),
                                    HataTuru = DogrulamaHataTuru.Kritik,
                                    Uri = referans.URI,
                                    InnerException = ex
                                });
                            }
                            else
                            {
                                Dagitim dagitim = null;
                                if (ustveri.Dagitimlar != null && ustveri.Dagitimlar.Count == 0)
                                    foreach (var dagitimOge in ustveri.Dagitimlar)
                                        if (dagitim.Oge is KurumKurulus kurumKurulus)
                                        {
                                            if (!string.IsNullOrWhiteSpace(kurumKurulus.KKK) &&
                                                kurumKurulus.KKK == dagitimTanimlayici.Deger)
                                            {
                                                dagitim = dagitimOge;
                                                break;
                                            }
                                        }
                                        else if (dagitim.Oge is TuzelSahis tuzelSahis)
                                        {
                                            if (tuzelSahis.Id != null &&
                                                string.IsNullOrWhiteSpace(tuzelSahis.Id.Deger) &&
                                                tuzelSahis.Id.Deger == dagitimTanimlayici.Deger)
                                            {
                                                dagitim = dagitimOge;
                                                break;
                                            }
                                        }
                                        else if (dagitim.Oge is GercekSahis gercekSahis)
                                        {
                                            if (!string.IsNullOrWhiteSpace(gercekSahis.TCKN) &&
                                                gercekSahis.TCKN == dagitimTanimlayici.Deger)
                                            {
                                                dagitim = dagitimOge;
                                                break;
                                            }
                                        }

                                if (dagitim == null ||
                                    dagitim.KonulmamisEkler == null ||
                                    dagitim.KonulmamisEkler.Count == 0 ||
                                    !dagitim.KonulmamisEkler.Any(p =>
                                        string.Compare(p.EkIdDeger.ToString(), ek.Id.Deger, true) == 0))
                                    dogrulamaHatalari.Add(new DogrulamaHatasi
                                    {
                                        Hata = string.Format("Paket bileşeni alınamamıştır. URI: {0}.", referans.URI),
                                        HataTuru = DogrulamaHataTuru.Kritik,
                                        Uri = referans.URI,
                                        InnerException = ex
                                    });
                            }
                        }

                        if (dogrulamaHatalari.Count > 0)
                            dogrulamaHatalariRef.AddRange(dogrulamaHatalari);
                        return dogrulamaHatalari.Count == 0;
                    }

                    byte[] computedDigestValue = null;
                    try
                    {
                        computedDigestValue = referans.Ozet.OzetAlgoritmasi.Algoritma.CalculateHash(referansStream);
                    }
                    catch (Exception ex)
                    {
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            Hata = string.Format("Paket bileşenine ait hash hesaplanamamıştır. URI: {0}.",
                                referans.URI),
                            HataTuru = DogrulamaHataTuru.Kritik,
                            Uri = referans.URI,
                            InnerException = ex
                        });
                    }

                    if (computedDigestValue != null && !computedDigestValue.SequenceEqual(referans.Ozet.OzetDegeri))
                        dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            Hata = string.Format("Paket bileşenine ait hashler eşit değildir. URI: {0}.", referans.URI),
                            HataTuru = DogrulamaHataTuru.Kritik,
                            Uri = referans.URI
                        });

                    if (paketVersiyon == PaketVersiyonTuru.Versiyon2X)
                    {
                        referansStream.Position = 0;
                        byte[] computedDigestValue1 = null;
                        try
                        {
                            computedDigestValue1 =
                                referans.Ozet1.OzetAlgoritmasi.Algoritma.CalculateHash(referansStream);
                        }
                        catch (Exception ex)
                        {
                            dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                Hata = string.Format("Paket bileşenine ait hash hesaplanamamıştır. URI: {0}.",
                                    referans.URI),
                                HataTuru = DogrulamaHataTuru.Kritik,
                                Uri = referans.URI,
                                InnerException = ex
                            });
                        }

                        if (computedDigestValue1 != null &&
                            !computedDigestValue.SequenceEqual(referans.Ozet.OzetDegeri))
                            dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                Hata = string.Format("Paket bileşenine ait hashler eşit değildir. URI: {0}.",
                                    referans.URI),
                                HataTuru = DogrulamaHataTuru.Kritik,
                                Uri = referans.URI
                            });
                    }
                }
            }

            if (dogrulamaHatalari.Count > 0)
                dogrulamaHatalariRef.AddRange(dogrulamaHatalari);
            return dogrulamaHatalari.Count == 0;
        }

        internal static bool DogrulaTelefon(this string telefon)
        {
            if (string.IsNullOrWhiteSpace(telefon))
                return false;

            return new Regex(
                @"^(?:(\+\d{1,3})?)(?:[\s]?)((\(\d{3}\))|(\d{3}))(?:[.\s]?)(\d{3})(?:[-\.\s]?)(\d{2})(?:[\s]?)(\d{2})(?!\d)$",
                RegexOptions.IgnoreCase | RegexOptions.CultureInvariant).IsMatch(telefon);
        }

        internal static bool DogrulaEPosta(this string eposta)
        {
            if (string.IsNullOrWhiteSpace(eposta))
                return false;

            try
            {
                // Normalize the domain
                eposta = Regex.Replace(eposta, @"(@)(.+)$", DomainMapper,
                    RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    var domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(eposta,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        internal static bool DogrulaWebAdresi(this string webAdresi)
        {
            if (string.IsNullOrWhiteSpace(webAdresi))
                return false;

            return new Regex(
                @"^(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&%\$#_]*)?$",
                RegexOptions.IgnoreCase | RegexOptions.CultureInvariant).IsMatch(webAdresi);
        }

        internal static bool DogrulaTCKimlikNo(this string tcKimlikNo)
        {
            if (string.IsNullOrWhiteSpace(tcKimlikNo) || tcKimlikNo.Length != 11 ||
                !new Regex(@"^\d{11}$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant).IsMatch(tcKimlikNo))
                return false;

            long ATCNO, BTCNO, TcNo;
            long C1, C2, C3, C4, C5, C6, C7, C8, C9, Q1, Q2;

            TcNo = long.Parse(tcKimlikNo);

            ATCNO = TcNo / 100;
            BTCNO = TcNo / 100;

            C1 = ATCNO % 10;
            ATCNO = ATCNO / 10;
            C2 = ATCNO % 10;
            ATCNO = ATCNO / 10;
            C3 = ATCNO % 10;
            ATCNO = ATCNO / 10;
            C4 = ATCNO % 10;
            ATCNO = ATCNO / 10;
            C5 = ATCNO % 10;
            ATCNO = ATCNO / 10;
            C6 = ATCNO % 10;
            ATCNO = ATCNO / 10;
            C7 = ATCNO % 10;
            ATCNO = ATCNO / 10;
            C8 = ATCNO % 10;
            ATCNO = ATCNO / 10;
            C9 = ATCNO % 10;
            ATCNO = ATCNO / 10;
            Q1 = (10 - ((C1 + C3 + C5 + C7 + C9) * 3 + C2 + C4 + C6 + C8) % 10) % 10;
            Q2 = (10 - ((C2 + C4 + C6 + C8 + Q1) * 3 + C1 + C3 + C5 + C7 + C9) % 10) % 10;

            return BTCNO * 100 + Q1 * 10 + Q2 == TcNo;
        }

        internal static bool DogrulaKKK(this string kkk)
        {
            if (string.IsNullOrWhiteSpace(kkk))
                return false;

            return new Regex(@"^[1-9]{1}[0-9]{7}$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant)
                .IsMatch(kkk);
        }

        internal static bool DogrulaKepAdresi(this string kepAdresi)
        {
            if (kepAdresi.DogrulaEPosta())
                return Constants.KEPHS_DOMAINS.Contains(kepAdresi.Split('@')[1]);

            return false;
        }

        internal static bool DogrulaSdpKod(this string kod)
        {
            if (string.IsNullOrWhiteSpace(kod))
                return false;

            return new Regex(@"^([0-9][0-9][0-9])(\.[0-9][0-9])?(\.[0-9][0-9])?(\.[0-9][0-9])?$",
                RegexOptions.IgnoreCase | RegexOptions.CultureInvariant).IsMatch(kod);
        }

        internal static DogrulamaHataTuru? GetDogrulamaHataTuru(this List<DogrulamaHatasi> dogrulamaHatalari)
        {
            if (dogrulamaHatalari != null && dogrulamaHatalari.Count > 0)
            {
                if (dogrulamaHatalari.Any(p => p.HataTuru == DogrulamaHataTuru.Kritik))
                    return DogrulamaHataTuru.Kritik;
                if (dogrulamaHatalari.Any(p => p.HataTuru == DogrulamaHataTuru.Onemli))
                    return DogrulamaHataTuru.Onemli;
                return DogrulamaHataTuru.Uyari;
            }

            return null;
        }
    }
}