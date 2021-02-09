using eyazisma.online.api.Classes;
using eyazisma.online.api.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace eyazisma.online.api.Extensions
{
    internal static class ConversionExtensions
    {
        public static Api.V1X.CT_SifreliIcerikBilgisi ToV1XCT_SifreliIcerikBilgisi(this SifreliIcerikBilgisi sifreliIcerikBilgisi)
        {
            if (sifreliIcerikBilgisi == null)
                return null;
            else
                return new Api.V1X.CT_SifreliIcerikBilgisi
                {
                    Id = sifreliIcerikBilgisi.Id,
                    URI = sifreliIcerikBilgisi.URI.ToArray(),
                    Version = sifreliIcerikBilgisi.Versiyon,
                    Yontem = sifreliIcerikBilgisi.Yontem
                };
        }

        public static Api.V2X.CT_SifreliIcerikBilgisi ToV2XCT_SifreliIcerikBilgisi(this SifreliIcerikBilgisi sifreliIcerikBilgisi)
        {
            if (sifreliIcerikBilgisi == null)
                return null;
            else
                return new Api.V2X.CT_SifreliIcerikBilgisi
                {
                    Id = sifreliIcerikBilgisi.Id,
                    URI = sifreliIcerikBilgisi.URI.ToArray(),
                    Version = sifreliIcerikBilgisi.Versiyon,
                    Yontem = sifreliIcerikBilgisi.Yontem
                };
        }

        public static SifreliIcerikBilgisi ToSifreliIcerikBilgisi(this Api.V1X.CT_SifreliIcerikBilgisi sifreliIcerikBilgisi)
        {
            if (sifreliIcerikBilgisi == null)
                return null;
            else
                return new SifreliIcerikBilgisi
                {
                    Id = sifreliIcerikBilgisi.Id,
                    URI = sifreliIcerikBilgisi.URI.ToList(),
                    Versiyon = sifreliIcerikBilgisi.Version,
                    Yontem = sifreliIcerikBilgisi.Yontem
                };
        }

        public static SifreliIcerikBilgisi ToSifreliIcerikBilgisi(this Api.V2X.CT_SifreliIcerikBilgisi sifreliIcerikBilgisi)
        {
            if (sifreliIcerikBilgisi == null)
                return null;
            else
                return new SifreliIcerikBilgisi
                {
                    Id = sifreliIcerikBilgisi.Id,
                    URI = sifreliIcerikBilgisi.URI.ToList(),
                    Versiyon = sifreliIcerikBilgisi.Version,
                    Yontem = sifreliIcerikBilgisi.Yontem
                };
        }

        public static Api.V1X.CT_BelgeImza ToV1XCT_BelgeImza(this BelgeImza belgeImza)
        {
            if (belgeImza == null)
                return null;
            else
            {
                var belgeImzaV1X = new Api.V1X.CT_BelgeImza();
                if (belgeImza.Imzalar != null && belgeImza.Imzalar.Count > 0)
                    belgeImza.Imzalar.ForEach((imza) => { belgeImzaV1X.ImzaListesi = belgeImzaV1X.ImzaListesi.Add(imza.ToV1XCT_Imza()); });

                return belgeImzaV1X;
            }
        }

        public static BelgeImza ToBelgeImza(this Api.V1X.CT_BelgeImza belgeImzaV1X)
        {
            if (belgeImzaV1X == null)
                return null;
            else
            {
                var belgeImza = new BelgeImza();
                if (belgeImzaV1X.ImzaListesi != null && belgeImzaV1X.ImzaListesi.Length > 0)
                {
                    belgeImza.Imzalar = new List<Imza>();
                    Array.ForEach(belgeImzaV1X.ImzaListesi, (imza) => { belgeImza.Imzalar.Add(imza.ToImza()); });
                }

                return belgeImza;
            }
        }

        public static Api.V1X.CT_BelgeHedef ToV1XCT_BelgeHedef(this BelgeHedef belgeHedef)
        {
            if (belgeHedef == null)
                return null;
            else
            {
                Api.V1X.CT_BelgeHedef belgeHedefV1X = new Api.V1X.CT_BelgeHedef();

                foreach (var hedef in belgeHedef.Hedefler.Where(x => x.Oge != null))
                {
                    if (hedef.Oge is KurumKurulus kurumKurulus)
                        belgeHedefV1X.HedefListesi = belgeHedefV1X.HedefListesi.Add(new Api.V1X.CT_Hedef { Item = kurumKurulus.ToV1XCT_KurumKurulus() });
                    else if (hedef.Oge is TuzelSahis tuzelSahis)
                        belgeHedefV1X.HedefListesi = belgeHedefV1X.HedefListesi.Add(new Api.V1X.CT_Hedef { Item = tuzelSahis.ToV1XCT_TuzelSahis() });
                    else if (hedef.Oge is GercekSahis gercekSahis)
                        belgeHedefV1X.HedefListesi = belgeHedefV1X.HedefListesi.Add(new Api.V1X.CT_Hedef { Item = gercekSahis.ToV1XCT_GercekSahis() });
                }

                return belgeHedefV1X;
            }
        }

        public static Api.V2X.CT_BelgeHedef ToV2XCT_BelgeHedef(this BelgeHedef belgeHedef)
        {
            if (belgeHedef == null)
                return null;
            else
            {
                Api.V2X.CT_BelgeHedef belgeHedefV2X = new Api.V2X.CT_BelgeHedef();

                foreach (var hedef in belgeHedef.Hedefler.Where(x => x.Oge != null))
                {
                    if (hedef.Oge is KurumKurulus kurumKurulus)
                        belgeHedefV2X.HedefListesi = belgeHedefV2X.HedefListesi.Add(new Api.V2X.CT_Hedef { Item = kurumKurulus.ToV2XCT_KurumKurulus() });
                    else if (hedef.Oge is TuzelSahis tuzelSahis)
                        belgeHedefV2X.HedefListesi = belgeHedefV2X.HedefListesi.Add(new Api.V2X.CT_Hedef { Item = tuzelSahis.ToV2XCT_TuzelSahis() });
                    else if (hedef.Oge is GercekSahis gercekSahis)
                        belgeHedefV2X.HedefListesi = belgeHedefV2X.HedefListesi.Add(new Api.V2X.CT_Hedef { Item = gercekSahis.ToV2XCT_GercekSahis() });
                }

                return belgeHedefV2X;
            }
        }

        public static BelgeHedef ToBelgeHedef(this Api.V1X.CT_BelgeHedef belgeHedefV1X)
        {
            if (belgeHedefV1X == null)
                return null;
            else
            {
                BelgeHedef belgeHedef = new BelgeHedef() { Hedefler = new List<Hedef>() };

                foreach (var hedef in belgeHedefV1X.HedefListesi.Where(x => x.Item != null))
                {
                    if (hedef.Item is Api.V1X.CT_KurumKurulus kurumKurulus)
                        belgeHedef.Hedefler.Add(new Hedef { Oge = kurumKurulus.ToKurumKurulus() });
                    else if (hedef.Item is Api.V1X.CT_TuzelSahis tuzelSahis)
                        belgeHedef.Hedefler.Add(new Hedef { Oge = tuzelSahis.ToTuzelSahis() });
                    else if (hedef.Item is Api.V1X.CT_GercekSahis gercekSahis)
                        belgeHedef.Hedefler.Add(new Hedef { Oge = gercekSahis.ToGercekSahis() });
                }

                return belgeHedef;
            }
        }

        public static BelgeHedef ToBelgeHedef(this Api.V2X.CT_BelgeHedef belgeHedefV2X)
        {
            if (belgeHedefV2X == null)
                return null;
            else
            {
                BelgeHedef belgeHedef = new BelgeHedef() { Hedefler = new List<Hedef>() };

                foreach (var hedef in belgeHedefV2X.HedefListesi.Where(x => x.Item != null))
                {
                    if (hedef.Item is Api.V2X.CT_KurumKurulus kurumKurulus)
                        belgeHedef.Hedefler.Add(new Hedef { Oge = kurumKurulus.ToKurumKurulus() });
                    else if (hedef.Item is Api.V2X.CT_TuzelSahis tuzelSahis)
                        belgeHedef.Hedefler.Add(new Hedef { Oge = tuzelSahis.ToTuzelSahis() });
                    else if (hedef.Item is Api.V2X.CT_GercekSahis gercekSahis)
                        belgeHedef.Hedefler.Add(new Hedef { Oge = gercekSahis.ToGercekSahis() });
                }

                return belgeHedef;
            }
        }

        public static Api.V1X.CT_Dagitim ToV1XCT_Dagitim(this Dagitim dagitim)
        {
            if (dagitim == null)
                return null;
            else
            {
                var dagitimV1X = new Api.V1X.CT_Dagitim();
                dagitimV1X.DagitimTuru = (Api.V1X.ST_KodDagitimTuru)Convert.ToInt32(dagitim.DagitimTuru);
                dagitimV1X.Ivedilik = (Api.V1X.ST_KodIvedilik)Convert.ToInt32(dagitim.IvedilikTuru);
                dagitimV1X.Miat = dagitim.Miat.Ticks.ToString();
                if (dagitim.KonulmamisEkler != null && dagitim.KonulmamisEkler.Count > 0)
                    dagitim.KonulmamisEkler.ForEach((konulmamisEk) => { dagitimV1X.KonulmamisEkListesi = dagitimV1X.KonulmamisEkListesi.Add(konulmamisEk.ToV1XCT_KonulmamisEk()); });

                if (dagitim.Oge is KurumKurulus kurumKurulus)
                    dagitimV1X.Item = kurumKurulus.ToV1XCT_KurumKurulus();
                else if (dagitim.Oge is TuzelSahis tuzelSahis)
                    dagitimV1X.Item = tuzelSahis.ToV1XCT_TuzelSahis();
                else if (dagitim.Oge is GercekSahis gercekSahis)
                    dagitimV1X.Item = gercekSahis.ToV1XCT_GercekSahis();

                return dagitimV1X;
            }
        }

        public static Api.V2X.CT_Dagitim ToV2XCT_Dagitim(this Dagitim dagitim)
        {
            if (dagitim == null)
                return null;
            else
            {
                var dagitimV2X = new Api.V2X.CT_Dagitim();
                dagitimV2X.DagitimTuru = (Api.V2X.ST_KodDagitimTuru)Convert.ToInt32(dagitim.DagitimTuru);
                dagitimV2X.Ivedilik = (Api.V2X.ST_KodIvedilik)Convert.ToInt32(dagitim.IvedilikTuru);
                dagitimV2X.Miat = dagitim.Miat.Ticks.ToString();
                if (dagitim.KonulmamisEkler != null && dagitim.KonulmamisEkler.Count > 0)
                    dagitim.KonulmamisEkler.ForEach((konulmamisEk) => { dagitimV2X.KonulmamisEkListesi = dagitimV2X.KonulmamisEkListesi.Add(konulmamisEk.ToV2XCT_KonulmamisEk()); });

                if (dagitim.Oge is KurumKurulus kurumKurulus)
                    dagitimV2X.Item = kurumKurulus.ToV2XCT_KurumKurulus();
                else if (dagitim.Oge is TuzelSahis tuzelSahis)
                    dagitimV2X.Item = tuzelSahis.ToV2XCT_TuzelSahis();
                else if (dagitim.Oge is GercekSahis gercekSahis)
                    dagitimV2X.Item = gercekSahis.ToV2XCT_GercekSahis();

                return dagitimV2X;
            }
        }

        public static Dagitim ToDagitim(this Api.V1X.CT_Dagitim dagitimV1X)
        {
            if (dagitimV1X == null)
                return null;
            else
            {
                var dagitim = new Dagitim();
                dagitim.DagitimTuru = (DagitimTuru)Convert.ToInt32(dagitimV1X.DagitimTuru);
                dagitim.IvedilikTuru = (IvedilikTuru)Convert.ToInt32(dagitimV1X.Ivedilik);

                if (!string.IsNullOrWhiteSpace(dagitimV1X.Miat))
                    dagitim.Miat = TimeSpan.FromTicks(Convert.ToInt64(dagitimV1X.Miat));

                if (dagitimV1X.KonulmamisEkListesi != null && dagitimV1X.KonulmamisEkListesi.Length > 0)
                {
                    dagitim.KonulmamisEkler = new List<KonulmamisEk>();
                    Array.ForEach(dagitimV1X.KonulmamisEkListesi, (konulmamisEk) => { dagitim.KonulmamisEkler.Add(konulmamisEk.ToKonulmamisEk()); });
                }

                if (dagitimV1X.Item is Api.V1X.CT_KurumKurulus kurumKurulus)
                    dagitim.Oge = kurumKurulus.ToKurumKurulus();
                else if (dagitimV1X.Item is Api.V1X.CT_TuzelSahis tuzelSahis)
                    dagitim.Oge = tuzelSahis.ToTuzelSahis();
                else if (dagitimV1X.Item is Api.V1X.CT_GercekSahis gercekSahis)
                    dagitim.Oge = gercekSahis.ToGercekSahis();

                return dagitim;
            }
        }

        public static Dagitim ToDagitim(this Api.V2X.CT_Dagitim dagitimV2X)
        {
            if (dagitimV2X == null)
                return null;
            else
            {
                var dagitim = new Dagitim();
                dagitim.DagitimTuru = (DagitimTuru)Convert.ToInt32(dagitimV2X.DagitimTuru);
                dagitim.IvedilikTuru = (IvedilikTuru)Convert.ToInt32(dagitimV2X.Ivedilik);

                if (!string.IsNullOrWhiteSpace(dagitimV2X.Miat))
                    dagitim.Miat = TimeSpan.FromTicks(Convert.ToInt64(dagitimV2X.Miat));

                if (dagitimV2X.KonulmamisEkListesi != null && dagitimV2X.KonulmamisEkListesi.Length > 0)
                {
                    dagitim.KonulmamisEkler = new List<KonulmamisEk>();
                    Array.ForEach(dagitimV2X.KonulmamisEkListesi, (konulmamisEk) => { dagitim.KonulmamisEkler.Add(konulmamisEk.ToKonulmamisEk()); });
                }

                if (dagitimV2X.Item is Api.V2X.CT_KurumKurulus kurumKurulus)
                    dagitim.Oge = kurumKurulus.ToKurumKurulus();
                else if (dagitimV2X.Item is Api.V2X.CT_TuzelSahis tuzelSahis)
                    dagitim.Oge = tuzelSahis.ToTuzelSahis();
                else if (dagitimV2X.Item is Api.V2X.CT_GercekSahis gercekSahis)
                    dagitim.Oge = gercekSahis.ToGercekSahis();

                return dagitim;
            }
        }

        public static Api.V2X.CT_DigestItem ToV2XCT_DigestItem(this Ozet ozet)
        {
            if (ozet == null)
                return null;
            else
                return new Api.V2X.CT_DigestItem
                {
                    DigestMethod = ozet.OzetAlgoritmasi.ToV2XCT_DigestMethod(),
                    DigestValue = ozet.OzetDegeri
                };
        }

        public static Ozet ToOzet(this Api.V2X.CT_DigestItem digestItemV2X)
        {
            if (digestItemV2X == null)
                return null;
            else
                return new Ozet
                {
                    OzetAlgoritmasi = digestItemV2X.DigestMethod.ToOzetAlgoritmasi(),
                    OzetDegeri = digestItemV2X.DigestValue
                };
        }

        public static Api.V1X.CT_DigestMethod ToV1XCT_DigestMethod(this OzetAlgoritmasi ozetAlgoritmasi)
        {
            if (ozetAlgoritmasi == null)
                return null;
            else
                return new Api.V1X.CT_DigestMethod
                {
                    Algorithm = ozetAlgoritmasi.Algoritma.ToXmlNameSpace(),
                    Any = ozetAlgoritmasi.Any
                };
        }

        public static Api.V2X.CT_DigestMethod ToV2XCT_DigestMethod(this OzetAlgoritmasi ozetAlgoritmasi)
        {
            if (ozetAlgoritmasi == null)
                return null;
            else
                return new Api.V2X.CT_DigestMethod
                {
                    Algorithm = ozetAlgoritmasi.Algoritma.ToXmlNameSpace(),
                    Any = ozetAlgoritmasi.Any
                };
        }

        public static OzetAlgoritmasi ToOzetAlgoritmasi(this Api.V1X.CT_DigestMethod digestMethodV1X)
        {
            if (digestMethodV1X == null)
                return null;
            else
                return new OzetAlgoritmasi
                {
                    Algoritma = digestMethodV1X.Algorithm.ToOzetAlgoritmaTuru(),
                    Any = digestMethodV1X.Any
                };
        }

        public static OzetAlgoritmasi ToOzetAlgoritmasi(this Api.V2X.CT_DigestMethod digestMethodV2X)
        {
            if (digestMethodV2X == null)
                return null;
            else
                return new OzetAlgoritmasi
                {
                    Algoritma = digestMethodV2X.Algorithm.ToOzetAlgoritmaTuru(),
                    Any = digestMethodV2X.Any
                };
        }

        public static Api.V2X.CT_DogrulamaBilgisi ToV2XCT_DogrulamaBilgisi(this DogrulamaBilgisi dogrulamaBilgisi)
        {
            if (dogrulamaBilgisi == null)
                return null;
            else
                return new Api.V2X.CT_DogrulamaBilgisi
                {
                    DogrulamaAdresi = dogrulamaBilgisi.DogrulamaAdresi
                };
        }

        public static DogrulamaBilgisi ToDogrulamaBilgisi(this Api.V2X.CT_DogrulamaBilgisi dogrulamaBilgisiV2X)
        {
            if (dogrulamaBilgisiV2X == null)
                return null;
            else
                return new DogrulamaBilgisi
                {
                    DogrulamaAdresi = dogrulamaBilgisiV2X.DogrulamaAdresi
                };
        }

        public static Api.V1X.CT_Ek ToV1XCT_Ek(this Ek ek)
        {
            if (ek == null)
                return null;
            else
                return new Api.V1X.CT_Ek
                {
                    Aciklama = ek.Aciklama.ToV1XTextType(),
                    Ad = ek.Ad.ToV1XTextType(),
                    BelgeNo = ek.BelgeNo,
                    DosyaAdi = ek.DosyaAdi,
                    Id = ek.Id.ToV1XCT_Id(),
                    ImzaliMi = ek.ImzaliMi,
                    ImzaliMiSpecified = ek.Tur != EkTuru.FZK,
                    MimeTuru = ek.MimeTuru,
                    Ozet = ek.Ozet.ToV1XCT_Ozet(),
                    OzId = ek.OzId.ToV1XIdentifierType(),
                    Referans = ek.Referans,
                    SiraNo = ek.SiraNo,
                    Tur = (Api.V1X.ST_KodEkTuru)Convert.ToInt32(ek.Tur)
                };
        }

        public static Api.V2X.CT_Ek ToV2XCT_Ek(this Ek ek)
        {
            if (ek == null)
                return null;
            else
                return new Api.V2X.CT_Ek
                {
                    Aciklama = ek.Aciklama.ToV2XTextType(),
                    Ad = ek.Ad.ToV2XTextType(),
                    BelgeNo = ek.BelgeNo,
                    DosyaAdi = ek.DosyaAdi,
                    Id = ek.Id.ToV2XCT_Id(),
                    ImzaliMi = ek.ImzaliMi,
                    ImzaliMiSpecified = ek.Tur != EkTuru.FZK,
                    MimeTuru = ek.MimeTuru,
                    Ozet = ek.Ozet.ToV2XCT_Ozet(),
                    OzId = ek.OzId.ToV2XIdentifierType(),
                    Referans = ek.Referans,
                    SiraNo = ek.SiraNo,
                    Tur = (Api.V2X.ST_KodEkTuru)Convert.ToInt32(ek.Tur)
                };
        }

        public static Ek ToEk(this Api.V1X.CT_Ek ekV1X)
        {
            if (ekV1X == null)
                return null;
            else
                return new Ek
                {
                    Aciklama = ekV1X.Aciklama.ToMetinTip(),
                    Ad = ekV1X.Ad.ToMetinTip(),
                    BelgeNo = ekV1X.BelgeNo,
                    DosyaAdi = ekV1X.DosyaAdi,
                    Id = ekV1X.Id.ToId(),
                    ImzaliMi = ekV1X.ImzaliMi,
                    MimeTuru = ekV1X.MimeTuru,
                    Ozet = ekV1X.Ozet.ToOzet(),
                    OzId = ekV1X.OzId.ToTanimlayiciTip(),
                    Referans = ekV1X.Referans,
                    SiraNo = ekV1X.SiraNo,
                    Tur = (EkTuru)Convert.ToInt32(ekV1X.Tur)
                };
        }

        public static Ek ToEk(this Api.V2X.CT_Ek ekV2X)
        {
            if (ekV2X == null)
                return null;
            else
                return new Ek
                {
                    Aciklama = ekV2X.Aciklama.ToMetinTip(),
                    Ad = ekV2X.Ad.ToMetinTip(),
                    BelgeNo = ekV2X.BelgeNo,
                    DosyaAdi = ekV2X.DosyaAdi,
                    Id = ekV2X.Id.ToId(),
                    ImzaliMi = ekV2X.ImzaliMi,
                    MimeTuru = ekV2X.MimeTuru,
                    Ozet = ekV2X.Ozet.ToOzet(),
                    OzId = ekV2X.OzId.ToTanimlayiciTip(),
                    Referans = ekV2X.Referans,
                    SiraNo = ekV2X.SiraNo,
                    Tur = (EkTuru)Convert.ToInt32(ekV2X.Tur)
                };
        }

        public static Api.V1X.CT_GercekSahis ToV1XCT_GercekSahis(this GercekSahis gercekSahis)
        {
            if (gercekSahis == null)
                return null;
            else
                return new Api.V1X.CT_GercekSahis
                {
                    Gorev = gercekSahis.Gorev.ToV1XTextType(),
                    IletisimBilgisi = gercekSahis.IletisimBilgisi.ToV1XCT_IletisimBilgisi(),
                    Kisi = gercekSahis.Kisi.ToV1XCT_Kisi(),
                    TCKN = gercekSahis.TCKN
                };
        }

        public static Api.V2X.CT_GercekSahis ToV2XCT_GercekSahis(this GercekSahis gercekSahis)
        {
            if (gercekSahis == null)
                return null;
            else
                return new Api.V2X.CT_GercekSahis
                {
                    Gorev = gercekSahis.Gorev.ToV2XTextType(),
                    IletisimBilgisi = gercekSahis.IletisimBilgisi.ToV2XCT_IletisimBilgisi(),
                    Kisi = gercekSahis.Kisi.ToV2XCT_Kisi(),
                    TCKN = gercekSahis.TCKN
                };
        }

        public static GercekSahis ToGercekSahis(this Api.V1X.CT_GercekSahis gercekSahisV1X)
        {
            if (gercekSahisV1X == null)
                return null;
            else
                return new GercekSahis
                {
                    Gorev = gercekSahisV1X.Gorev.ToMetinTip(),
                    IletisimBilgisi = gercekSahisV1X.IletisimBilgisi.ToIletisimBilgisi(),
                    Kisi = gercekSahisV1X.Kisi.ToKisi(),
                    TCKN = gercekSahisV1X.TCKN
                };
        }

        public static GercekSahis ToGercekSahis(this Api.V2X.CT_GercekSahis gercekSahisV2X)
        {
            if (gercekSahisV2X == null)
                return null;
            else
                return new GercekSahis
                {
                    Gorev = gercekSahisV2X.Gorev.ToMetinTip(),
                    IletisimBilgisi = gercekSahisV2X.IletisimBilgisi.ToIletisimBilgisi(),
                    Kisi = gercekSahisV2X.Kisi.ToKisi(),
                    TCKN = gercekSahisV2X.TCKN
                };
        }

        public static Api.V1X.CT_Hedef ToV1XCT_Hedef(this Hedef hedef)
        {
            if (hedef == null)
                return null;
            else
            {
                var hedefV1X = new Api.V1X.CT_Hedef();
                if (hedef.Oge is KurumKurulus kurumKurulus)
                    hedefV1X.Item = kurumKurulus.ToV1XCT_KurumKurulus();
                else if (hedef.Oge is TuzelSahis tuzelSahis)
                    hedefV1X.Item = tuzelSahis.ToV1XCT_TuzelSahis();
                else if (hedef.Oge is GercekSahis gercekSahis)
                    hedefV1X.Item = gercekSahis.ToV1XCT_GercekSahis();

                return hedefV1X;
            }
        }

        public static Api.V2X.CT_Hedef ToV2XCT_Hedef(this Hedef hedef)
        {
            if (hedef == null)
                return null;
            else
            {
                var hedefV2X = new Api.V2X.CT_Hedef();
                if (hedef.Oge is KurumKurulus kurumKurulus)
                    hedefV2X.Item = kurumKurulus.ToV2XCT_KurumKurulus();
                else if (hedef.Oge is TuzelSahis tuzelSahis)
                    hedefV2X.Item = tuzelSahis.ToV2XCT_TuzelSahis();
                else if (hedef.Oge is GercekSahis gercekSahis)
                    hedefV2X.Item = gercekSahis.ToV2XCT_GercekSahis();

                return hedefV2X;
            }
        }

        public static Hedef ToHedef(this Api.V1X.CT_Hedef hedefV1X)
        {
            if (hedefV1X == null)
                return null;
            else
            {
                var hedef = new Hedef();
                if (hedefV1X.Item is Api.V1X.CT_KurumKurulus kurumKurulus)
                    hedef.Oge = kurumKurulus.ToKurumKurulus();
                else if (hedefV1X.Item is Api.V1X.CT_TuzelSahis tuzelSahis)
                    hedef.Oge = tuzelSahis.ToTuzelSahis();
                else if (hedefV1X.Item is Api.V1X.CT_GercekSahis gercekSahis)
                    hedef.Oge = gercekSahis.ToGercekSahis();

                return hedef;
            }
        }

        public static Hedef ToHedef(this Api.V2X.CT_Hedef hedefV2X)
        {
            if (hedefV2X == null)
                return null;
            else
            {
                var hedef = new Hedef();
                if (hedefV2X.Item is Api.V2X.CT_KurumKurulus kurumKurulus)
                    hedef.Oge = kurumKurulus.ToKurumKurulus();
                else if (hedefV2X.Item is Api.V2X.CT_TuzelSahis tuzelSahis)
                    hedef.Oge = tuzelSahis.ToTuzelSahis();
                else if (hedefV2X.Item is Api.V2X.CT_GercekSahis gercekSahis)
                    hedef.Oge = gercekSahis.ToGercekSahis();

                return hedef;
            }
        }

        public static Api.V2X.CT_HEYSK ToV2XCT_HEYSK(this HEYSK heysk)
        {
            if (heysk == null)
                return null;
            else
                return new Api.V2X.CT_HEYSK
                {
                    Ad = heysk.Ad,
                    Kod = heysk.Kod,
                    Tanim = heysk.Tanim
                };
        }

        public static HEYSK ToHEYSK(this Api.V2X.CT_HEYSK heyskV2X)
        {
            if (heyskV2X == null)
                return null;
            else
                return new HEYSK
                {
                    Ad = heyskV2X.Ad,
                    Kod = heyskV2X.Kod,
                    Tanim = heyskV2X.Tanim
                };
        }

        public static Api.V1X.CT_Id ToV1XCT_Id(this IdTip id)
        {
            if (id == null)
                return null;
            else
                return new Api.V1X.CT_Id
                {
                    EYazismaIdMi = id.EYazismaIdMi,
                    EYazismaIdMiSpecified = true,
                    Value = id.Deger.ToString().ToUpperInvariant()
                };
        }

        public static Api.V2X.CT_Id ToV2XCT_Id(this IdTip id)
        {
            if (id == null)
                return null;
            else
                return new Api.V2X.CT_Id
                {
                    EYazismaIdMi = id.EYazismaIdMi,
                    EYazismaIdMiSpecified = true,
                    Value = id.Deger.ToString().ToUpperInvariant()
                };
        }

        public static IdTip ToId(this Api.V1X.CT_Id idV1X)
        {
            if (idV1X == null)
                return null;
            else
                return new IdTip
                {
                    EYazismaIdMi = idV1X.EYazismaIdMi,
                    Deger = idV1X.Value
                };
        }

        public static IdTip ToId(this Api.V2X.CT_Id idV2X)
        {
            if (idV2X == null)
                return null;
            else
                return new IdTip
                {
                    EYazismaIdMi = idV2X.EYazismaIdMi,
                    Deger = idV2X.Value
                };
        }

        public static Api.V1X.CT_IletisimBilgisi ToV1XCT_IletisimBilgisi(this IletisimBilgisi iletisimBilgisi)
        {
            if (iletisimBilgisi == null)
                return null;
            else
                return new Api.V1X.CT_IletisimBilgisi
                {
                    Adres = iletisimBilgisi.Adres.ToV1XTextType(),
                    EPosta = iletisimBilgisi.EPosta,
                    Faks = iletisimBilgisi.Faks,
                    Il = iletisimBilgisi.Il.ToV1XNameType(),
                    Ilce = iletisimBilgisi.Ilce.ToV1XNameType(),
                    Telefon = iletisimBilgisi.Telefon,
                    TelefonDiger = iletisimBilgisi.TelefonDiger,
                    Ulke = iletisimBilgisi.Ulke.ToV1XNameType(),
                    WebAdresi = iletisimBilgisi.WebAdresi
                };
        }

        public static Api.V2X.CT_IletisimBilgisi ToV2XCT_IletisimBilgisi(this IletisimBilgisi iletisimBilgisi)
        {
            if (iletisimBilgisi == null)
                return null;
            else
                return new Api.V2X.CT_IletisimBilgisi
                {
                    Adres = iletisimBilgisi.Adres.ToV2XTextType(),
                    EPosta = iletisimBilgisi.EPosta,
                    Faks = iletisimBilgisi.Faks,
                    Il = iletisimBilgisi.Il.ToV2XNameType(),
                    Ilce = iletisimBilgisi.Ilce.ToV2XNameType(),
                    Telefon = iletisimBilgisi.Telefon,
                    TelefonDiger = iletisimBilgisi.TelefonDiger,
                    Ulke = iletisimBilgisi.Ulke.ToV2XNameType(),
                    WebAdresi = iletisimBilgisi.WebAdresi,
                    KepAdresi = iletisimBilgisi.KepAdresi
                };
        }

        public static IletisimBilgisi ToIletisimBilgisi(this Api.V1X.CT_IletisimBilgisi iletisimBilgisiV1X)
        {
            if (iletisimBilgisiV1X == null)
                return null;
            else
                return new IletisimBilgisi
                {
                    Adres = iletisimBilgisiV1X.Adres.ToMetinTip(),
                    EPosta = iletisimBilgisiV1X.EPosta,
                    Faks = iletisimBilgisiV1X.Faks,
                    Il = iletisimBilgisiV1X.Il.ToIsimTip(),
                    Ilce = iletisimBilgisiV1X.Ilce.ToIsimTip(),
                    Telefon = iletisimBilgisiV1X.Telefon,
                    TelefonDiger = iletisimBilgisiV1X.TelefonDiger,
                    Ulke = iletisimBilgisiV1X.Ulke.ToIsimTip(),
                    WebAdresi = iletisimBilgisiV1X.WebAdresi
                };
        }

        public static IletisimBilgisi ToIletisimBilgisi(this Api.V2X.CT_IletisimBilgisi iletisimBilgisiV2X)
        {
            if (iletisimBilgisiV2X == null)
                return null;
            else
                return new IletisimBilgisi
                {
                    Adres = iletisimBilgisiV2X.Adres.ToMetinTip(),
                    EPosta = iletisimBilgisiV2X.EPosta,
                    Faks = iletisimBilgisiV2X.Faks,
                    Il = iletisimBilgisiV2X.Il.ToIsimTip(),
                    Ilce = iletisimBilgisiV2X.Ilce.ToIsimTip(),
                    Telefon = iletisimBilgisiV2X.Telefon,
                    TelefonDiger = iletisimBilgisiV2X.TelefonDiger,
                    Ulke = iletisimBilgisiV2X.Ulke.ToIsimTip(),
                    WebAdresi = iletisimBilgisiV2X.WebAdresi,
                    KepAdresi = iletisimBilgisiV2X.KepAdresi
                };
        }

        public static Api.V1X.CT_Ilgi ToV1XCT_Ilgi(this Ilgi ilgi)
        {
            if (ilgi == null)
                return null;
            else
            {
                var ilgiV1X = new Api.V1X.CT_Ilgi
                {
                    Aciklama = ilgi.Aciklama.ToV1XTextType(),
                    Ad = ilgi.Ad.ToV1XTextType(),
                    BelgeNo = ilgi.BelgeNo,
                    EkId = ilgi.EkIdDeger.ToString().ToUpperInvariant(),
                    Etiket = ilgi.Etiket.ToString(),
                    Id = ilgi.Id.ToV1XCT_Id(),
                    OzId = ilgi.OzId.ToV1XIdentifierType()
                };

                if (ilgi.Tarih.HasValue)
                {
                    ilgiV1X.Tarih = ilgi.Tarih.Value;
                    ilgiV1X.TarihSpecified = true;
                }

                return ilgiV1X;
            }
        }

        public static Api.V2X.CT_Ilgi ToV2XCT_Ilgi(this Ilgi ilgi)
        {
            if (ilgi == null)
                return null;
            else
            {
                var ilgiV2X = new Api.V2X.CT_Ilgi
                {
                    Aciklama = ilgi.Aciklama.ToV2XTextType(),
                    Ad = ilgi.Ad.ToV2XTextType(),
                    BelgeNo = ilgi.BelgeNo,
                    EkId = ilgi.EkIdDeger.ToString().ToUpperInvariant(),
                    Etiket = ilgi.Etiket.ToString(),
                    Id = ilgi.Id.ToV2XCT_Id(),
                    OzId = ilgi.OzId.ToV2XIdentifierType()
                };

                if (ilgi.Tarih.HasValue)
                {
                    ilgiV2X.Tarih = ilgi.Tarih.Value;
                    ilgiV2X.TarihSpecified = true;
                }

                return ilgiV2X;
            }
        }

        public static Ilgi ToIlgi(this Api.V1X.CT_Ilgi ilgiV1X)
        {
            if (ilgiV1X == null)
                return null;
            else
            {
                var ilgi = new Ilgi
                {
                    Aciklama = ilgiV1X.Aciklama.ToMetinTip(),
                    Ad = ilgiV1X.Ad.ToMetinTip(),
                    BelgeNo = ilgiV1X.BelgeNo,
                    EkIdDeger = Guid.Parse(ilgiV1X.EkId),
                    Etiket = Convert.ToChar(ilgiV1X.Etiket),
                    Id = ilgiV1X.Id.ToId(),
                    OzId = ilgiV1X.OzId.ToTanimlayiciTip()
                };

                if (ilgiV1X.TarihSpecified)
                    ilgi.Tarih = ilgiV1X.Tarih;

                return ilgi;
            }
        }

        public static Ilgi ToIlgi(this Api.V2X.CT_Ilgi ilgiV2X)
        {
            if (ilgiV2X == null)
                return null;
            else
            {
                var ilgi = new Ilgi
                {
                    Aciklama = ilgiV2X.Aciklama.ToMetinTip(),
                    Ad = ilgiV2X.Ad.ToMetinTip(),
                    BelgeNo = ilgiV2X.BelgeNo,
                    EkIdDeger = Guid.Parse(ilgiV2X.EkId),
                    Etiket = Convert.ToChar(ilgiV2X.Etiket),
                    Id = ilgiV2X.Id.ToId(),
                    OzId = ilgiV2X.OzId.ToTanimlayiciTip()
                };

                if (ilgiV2X.TarihSpecified)
                    ilgi.Tarih = ilgiV2X.Tarih;

                return ilgi;
            }
        }

        public static Api.V1X.CT_Ilgili ToV1XCT_Ilgili(this Ilgili ilgili)
        {
            if (ilgili == null)
                return null;
            else
            {
                var ilgiliV1X = new Api.V1X.CT_Ilgili();
                if (ilgili.Oge is KurumKurulus kurumKurulus)
                    ilgiliV1X.Item = kurumKurulus.ToV1XCT_KurumKurulus();
                else if (ilgili.Oge is TuzelSahis tuzelSahis)
                    ilgiliV1X.Item = tuzelSahis.ToV1XCT_TuzelSahis();
                else if (ilgili.Oge is GercekSahis gercekSahis)
                    ilgiliV1X.Item = gercekSahis.ToV1XCT_GercekSahis();

                return ilgiliV1X;
            }
        }

        public static Api.V2X.CT_Ilgili ToV2XCT_Ilgili(this Ilgili ilgili)
        {
            if (ilgili == null)
                return null;
            else
            {
                var ilgiliV2X = new Api.V2X.CT_Ilgili();
                if (ilgili.Oge is KurumKurulus kurumKurulus)
                    ilgiliV2X.Item = kurumKurulus.ToV2XCT_KurumKurulus();
                else if (ilgili.Oge is TuzelSahis tuzelSahis)
                    ilgiliV2X.Item = tuzelSahis.ToV2XCT_TuzelSahis();
                else if (ilgili.Oge is GercekSahis gercekSahis)
                    ilgiliV2X.Item = gercekSahis.ToV2XCT_GercekSahis();

                return ilgiliV2X;
            }
        }

        public static Ilgili ToIlgili(this Api.V1X.CT_Ilgili ilgiliV1X)
        {
            if (ilgiliV1X == null)
                return null;
            else
            {
                var ilgili = new Ilgili();
                if (ilgiliV1X.Item is Api.V1X.CT_KurumKurulus kurumKurulus)
                    ilgili.Oge = kurumKurulus.ToKurumKurulus();
                else if (ilgiliV1X.Item is Api.V1X.CT_TuzelSahis tuzelSahis)
                    ilgili.Oge = tuzelSahis.ToTuzelSahis();
                else if (ilgiliV1X.Item is Api.V1X.CT_GercekSahis gercekSahis)
                    ilgili.Oge = gercekSahis.ToGercekSahis();

                return ilgili;
            }
        }

        public static Ilgili ToIlgili(this Api.V2X.CT_Ilgili ilgiliV2X)
        {
            if (ilgiliV2X == null)
                return null;
            else
            {
                var ilgili = new Ilgili();
                if (ilgiliV2X.Item is Api.V2X.CT_KurumKurulus kurumKurulus)
                    ilgili.Oge = kurumKurulus.ToKurumKurulus();
                else if (ilgiliV2X.Item is Api.V2X.CT_TuzelSahis tuzelSahis)
                    ilgili.Oge = tuzelSahis.ToTuzelSahis();
                else if (ilgiliV2X.Item is Api.V2X.CT_GercekSahis gercekSahis)
                    ilgili.Oge = gercekSahis.ToGercekSahis();

                return ilgili;
            }
        }

        public static Api.V1X.CT_Imza ToV1XCT_Imza(this Imza imza)
        {
            if (imza == null)
                return null;
            else
            {
                var imzaV1X = new Api.V1X.CT_Imza
                {
                    Aciklama = imza.Aciklama.ToV1XTextType(),
                    Amac = imza.Amac.ToV1XTextType(),
                    Imzalayan = imza.Imzalayan.ToV1XCT_GercekSahis(),
                    Makam = imza.Makam.ToV1XNameType(),
                    TCYK = imza.TCYK,
                    VekaletVeren = imza.VekaletVeren.ToV1XCT_GercekSahis(),
                    YetkiDevreden = imza.YetkiDevreden.ToV1XCT_GercekSahis()
                };

                if (imza.Tarih.HasValue)
                {
                    imzaV1X.Tarih = imza.Tarih.Value;
                    imzaV1X.TarihSpecified = true;
                }

                return imzaV1X;
            }
        }

        public static Api.V2X.CT_Imza ToV2XCT_Imza(this Imza imza)
        {
            if (imza == null)
                return null;
            else
            {
                var imzaV2X = new Api.V2X.CT_Imza
                {
                    Aciklama = imza.Aciklama.ToV2XTextType(),
                    Amac = imza.Amac.ToV2XTextType(),
                    Imzalayan = imza.Imzalayan.ToV2XCT_GercekSahis(),
                    Makam = imza.Makam.ToV2XNameType(),
                    VekaletVeren = imza.VekaletVeren.ToV2XCT_GercekSahis(),
                    YetkiDevreden = imza.YetkiDevreden.ToV2XCT_GercekSahis()
                };

                if (imza.Tarih.HasValue)
                {
                    imzaV2X.Tarih = imza.Tarih.Value;
                    imzaV2X.TarihSpecified = true;
                }

                return imzaV2X;
            }
        }

        public static Imza ToImza(this Api.V1X.CT_Imza imzaV1X)
        {
            if (imzaV1X == null)
                return null;
            else
            {
                var imza = new Imza
                {
                    Aciklama = imzaV1X.Aciklama.ToMetinTip(),
                    Amac = imzaV1X.Amac.ToMetinTip(),
                    Imzalayan = imzaV1X.Imzalayan.ToGercekSahis(),
                    Makam = imzaV1X.Makam.ToIsimTip(),
                    TCYK = imzaV1X.TCYK,
                    VekaletVeren = imzaV1X.VekaletVeren.ToGercekSahis(),
                    YetkiDevreden = imzaV1X.YetkiDevreden.ToGercekSahis()
                };

                if (imzaV1X.TarihSpecified)
                    imza.Tarih = imzaV1X.Tarih;

                return imza;
            }
        }

        public static Imza ToImza(this Api.V2X.CT_Imza imzaV2X)
        {
            if (imzaV2X == null)
                return null;
            else
            {
                var imza = new Imza
                {
                    Aciklama = imzaV2X.Aciklama.ToMetinTip(),
                    Amac = imzaV2X.Amac.ToMetinTip(),
                    Imzalayan = imzaV2X.Imzalayan.ToGercekSahis(),
                    Makam = imzaV2X.Makam.ToIsimTip(),
                    VekaletVeren = imzaV2X.VekaletVeren.ToGercekSahis(),
                    YetkiDevreden = imzaV2X.YetkiDevreden.ToGercekSahis()
                };

                if (imzaV2X.TarihSpecified)
                    imza.Tarih = imzaV2X.Tarih;

                return imza;
            }
        }

        public static Api.V1X.CT_Kisi ToV1XCT_Kisi(this Kisi kisi)
        {
            if (kisi == null)
                return null;
            else
                return new Api.V1X.CT_Kisi
                {
                    IkinciAdi = kisi.IkinciAdi.ToV1XNameType(),
                    IlkAdi = kisi.IlkAdi.ToV1XNameType(),
                    OnEk = kisi.OnEk.ToV1XTextType(),
                    Soyadi = kisi.Soyadi.ToV1XNameType(),
                    Unvan = kisi.Unvan.ToV1XNameType()
                };
        }

        public static Api.V2X.CT_Kisi ToV2XCT_Kisi(this Kisi kisi)
        {
            if (kisi == null)
                return null;
            else
                return new Api.V2X.CT_Kisi
                {
                    IkinciAdi = kisi.IkinciAdi.ToV2XNameType(),
                    IlkAdi = kisi.IlkAdi.ToV2XNameType(),
                    OnEk = kisi.OnEk.ToV2XTextType(),
                    Soyadi = kisi.Soyadi.ToV2XNameType(),
                    Unvan = kisi.Unvan.ToV2XNameType()
                };
        }

        public static Kisi ToKisi(this Api.V1X.CT_Kisi kisiV1X)
        {
            if (kisiV1X == null)
                return null;
            else
                return new Kisi
                {
                    IkinciAdi = kisiV1X.IkinciAdi.ToIsimTip(),
                    IlkAdi = kisiV1X.IlkAdi.ToIsimTip(),
                    OnEk = kisiV1X.OnEk.ToMetinTip(),
                    Soyadi = kisiV1X.Soyadi.ToIsimTip(),
                    Unvan = kisiV1X.Unvan.ToIsimTip()
                };
        }

        public static Kisi ToKisi(this Api.V2X.CT_Kisi kisiV2X)
        {
            if (kisiV2X == null)
                return null;
            else
                return new Kisi
                {
                    IkinciAdi = kisiV2X.IkinciAdi.ToIsimTip(),
                    IlkAdi = kisiV2X.IlkAdi.ToIsimTip(),
                    OnEk = kisiV2X.OnEk.ToMetinTip(),
                    Soyadi = kisiV2X.Soyadi.ToIsimTip(),
                    Unvan = kisiV2X.Unvan.ToIsimTip()
                };
        }

        public static Api.V1X.CT_KonulmamisEk ToV1XCT_KonulmamisEk(this KonulmamisEk konulmamisEk)
        {
            if (konulmamisEk == null)
                return null;
            else
                return new Api.V1X.CT_KonulmamisEk
                {
                    EkId = konulmamisEk.EkIdDeger.ToString().ToUpperInvariant()
                };
        }

        public static Api.V2X.CT_KonulmamisEk ToV2XCT_KonulmamisEk(this KonulmamisEk konulmamisEk)
        {
            if (konulmamisEk == null)
                return null;
            else
                return new Api.V2X.CT_KonulmamisEk
                {
                    EkId = konulmamisEk.EkIdDeger.ToString().ToUpperInvariant()
                };
        }

        public static KonulmamisEk ToKonulmamisEk(this Api.V1X.CT_KonulmamisEk konulmamisEkV1X)
        {
            if (konulmamisEkV1X == null)
                return null;
            else
                return new KonulmamisEk
                {
                    EkIdDeger = Guid.Parse(konulmamisEkV1X.EkId)
                };
        }

        public static KonulmamisEk ToKonulmamisEk(this Api.V2X.CT_KonulmamisEk konulmamisEkV2X)
        {
            if (konulmamisEkV2X == null)
                return null;
            else
                return new KonulmamisEk
                {
                    EkIdDeger = Guid.Parse(konulmamisEkV2X.EkId)
                };
        }

        public static Api.V1X.CT_KurumKurulus ToV1XCT_KurumKurulus(this KurumKurulus kurumKurulus)
        {
            if (kurumKurulus == null)
                return null;
            else
                return new Api.V1X.CT_KurumKurulus
                {
                    Adi = kurumKurulus.Ad.ToV1XNameType(),
                    IletisimBilgisi = kurumKurulus.IletisimBilgisi.ToV1XCT_IletisimBilgisi(),
                    KKK = kurumKurulus.KKK
                };
        }

        public static Api.V2X.CT_KurumKurulus ToV2XCT_KurumKurulus(this KurumKurulus kurumKurulus)
        {
            if (kurumKurulus == null)
                return null;
            else
                return new Api.V2X.CT_KurumKurulus
                {
                    Adi = kurumKurulus.Ad.ToV2XNameType(),
                    IletisimBilgisi = kurumKurulus.IletisimBilgisi.ToV2XCT_IletisimBilgisi(),
                    KKK = kurumKurulus.KKK,
                    BirimKKK = kurumKurulus.BirimKKK
                };
        }

        public static KurumKurulus ToKurumKurulus(this Api.V1X.CT_KurumKurulus kurumKurulusV1X)
        {
            if (kurumKurulusV1X == null)
                return null;
            else
                return new KurumKurulus
                {
                    Ad = kurumKurulusV1X.Adi.ToIsimTip(),
                    IletisimBilgisi = kurumKurulusV1X.IletisimBilgisi.ToIletisimBilgisi(),
                    KKK = kurumKurulusV1X.KKK
                };
        }

        public static KurumKurulus ToKurumKurulus(this Api.V2X.CT_KurumKurulus kurumKurulusV2X)
        {
            if (kurumKurulusV2X == null)
                return null;
            else
                return new KurumKurulus
                {
                    Ad = kurumKurulusV2X.Adi.ToIsimTip(),
                    IletisimBilgisi = kurumKurulusV2X.IletisimBilgisi.ToIletisimBilgisi(),
                    KKK = kurumKurulusV2X.KKK,
                    BirimKKK = kurumKurulusV2X.BirimKKK
                };
        }

        public static Api.V1X.CT_NihaiOzet ToV1XCT_NihaiOzet(this NihaiOzet nihaiOzet)
        {
            if (nihaiOzet == null)
                return null;
            else
            {
                var nihaiOzetV1X = new Api.V1X.CT_NihaiOzet();
                nihaiOzetV1X.Id = nihaiOzet.Id.ToString();
                if (nihaiOzet.Referanslar != null && nihaiOzet.Referanslar.Count > 0)
                    nihaiOzet.Referanslar.ForEach((referans) => { nihaiOzetV1X.Reference = nihaiOzetV1X.Reference.Add(referans.ToV1XCT_Reference()); });

                return nihaiOzetV1X;
            }
        }

        public static Api.V2X.CT_NihaiOzet ToV2XCT_NihaiOzet(this NihaiOzet nihaiOzet)
        {
            if (nihaiOzet == null)
                return null;
            else
            {
                var nihaiOzetV2X = new Api.V2X.CT_NihaiOzet();
                nihaiOzetV2X.Id = nihaiOzet.Id.ToString();
                if (nihaiOzet.Referanslar != null && nihaiOzet.Referanslar.Count > 0)
                    nihaiOzet.Referanslar.ForEach((referans) => { nihaiOzetV2X.Reference = nihaiOzetV2X.Reference.Add(referans.ToV2XCT_Reference()); });

                return nihaiOzetV2X;
            }
        }

        public static NihaiOzet ToNihaiOzet(this Api.V1X.CT_NihaiOzet nihaiOzetV1X)
        {
            if (nihaiOzetV1X == null)
                return null;
            else
            {
                var nihaiOzet = new NihaiOzet();
                nihaiOzet.Id = Guid.Parse(nihaiOzetV1X.Id);
                if (nihaiOzetV1X.Reference != null && nihaiOzetV1X.Reference.Length > 0)
                {
                    nihaiOzet.Referanslar = new List<Referans>();
                    Array.ForEach(nihaiOzetV1X.Reference, (reference) => { nihaiOzet.Referanslar.Add(reference.ToReferans()); });
                }

                return nihaiOzet;
            }
        }

        public static NihaiOzet ToNihaiOzet(this Api.V2X.CT_NihaiOzet nihaiOzetV2X)
        {
            if (nihaiOzetV2X == null)
                return null;
            else
            {
                var nihaiOzet = new NihaiOzet();
                nihaiOzet.Id = Guid.Parse(nihaiOzetV2X.Id);
                if (nihaiOzetV2X.Reference != null && nihaiOzetV2X.Reference.Length > 0)
                {
                    nihaiOzet.Referanslar = new List<Referans>();
                    Array.ForEach(nihaiOzetV2X.Reference, (reference) => { nihaiOzet.Referanslar.Add(reference.ToReferans()); });
                }

                return nihaiOzet;
            }
        }

        public static Api.V2X.CT_NihaiUstveri ToV2XCT_NihaiUstveri(this NihaiUstveri nihaiUstveri)
        {
            if (nihaiUstveri == null)
                return null;
            else
            {
                var nihaiUstveriV2X = new Api.V2X.CT_NihaiUstveri();
                nihaiUstveriV2X.BelgeNo = nihaiUstveri.BelgeNo;
                nihaiUstveriV2X.Tarih = nihaiUstveri.Tarih;
                if (nihaiUstveri.BelgeImzalar != null && nihaiUstveri.BelgeImzalar.Count > 0)
                    nihaiUstveri.BelgeImzalar.ForEach((belgeImza) => { nihaiUstveriV2X.BelgeImzalar = nihaiUstveriV2X.BelgeImzalar.Add(belgeImza.ToV2XCT_Imza()); });

                return nihaiUstveriV2X;
            }
        }

        public static NihaiUstveri ToNihaiUstveri(this Api.V2X.CT_NihaiUstveri nihaiUstveriV2X)
        {
            if (nihaiUstveriV2X == null)
                return null;
            else
            {
                var nihaiUstveri = new NihaiUstveri();
                nihaiUstveri.BelgeNo = nihaiUstveriV2X.BelgeNo;
                nihaiUstveri.Tarih = nihaiUstveriV2X.Tarih;
                if (nihaiUstveriV2X.BelgeImzalar != null && nihaiUstveriV2X.BelgeImzalar.Length > 0)
                {
                    nihaiUstveri.BelgeImzalar = new List<Imza>();
                    Array.ForEach(nihaiUstveriV2X.BelgeImzalar, (belgeImza) => { nihaiUstveri.BelgeImzalar.Add(belgeImza.ToImza()); });
                }

                return nihaiUstveri;
            }
        }

        public static Api.V1X.CT_Olusturan V1XCT_Olusturan(this Olusturan olusturan)
        {
            if (olusturan == null)
                return null;
            else
            {
                var olusturanV1X = new Api.V1X.CT_Olusturan();
                if (olusturan.Oge is KurumKurulus kurumKurulus)
                    olusturanV1X.Item = kurumKurulus.ToV1XCT_KurumKurulus();
                else if (olusturan.Oge is TuzelSahis tuzelSahis)
                    olusturanV1X.Item = tuzelSahis.ToV1XCT_TuzelSahis();
                else if (olusturan.Oge is GercekSahis gercekSahis)
                    olusturanV1X.Item = gercekSahis.ToV1XCT_GercekSahis();

                return olusturanV1X;
            }
        }

        public static Api.V2X.CT_Olusturan V2XCT_Olusturan(this Olusturan olusturan)
        {
            if (olusturan == null)
                return null;
            else
            {
                var olusturanV2X = new Api.V2X.CT_Olusturan();
                if (olusturan.Oge is KurumKurulus kurumKurulus)
                    olusturanV2X.Item = kurumKurulus.ToV2XCT_KurumKurulus();
                else if (olusturan.Oge is TuzelSahis tuzelSahis)
                    olusturanV2X.Item = tuzelSahis.ToV2XCT_TuzelSahis();
                else if (olusturan.Oge is GercekSahis gercekSahis)
                    olusturanV2X.Item = gercekSahis.ToV2XCT_GercekSahis();

                return olusturanV2X;
            }
        }

        public static Olusturan ToOlusturan(this Api.V1X.CT_Olusturan olusturanV1X)
        {
            if (olusturanV1X == null)
                return null;
            else
            {
                var olusturan = new Olusturan();
                if (olusturanV1X.Item is Api.V1X.CT_KurumKurulus kurumKurulus)
                    olusturan.Oge = kurumKurulus.ToKurumKurulus();
                else if (olusturanV1X.Item is Api.V1X.CT_TuzelSahis tuzelSahis)
                    olusturan.Oge = tuzelSahis.ToTuzelSahis();
                else if (olusturanV1X.Item is Api.V1X.CT_GercekSahis gercekSahis)
                    olusturan.Oge = gercekSahis.ToGercekSahis();

                return olusturan;
            }
        }

        public static Olusturan ToOlusturan(this Api.V2X.CT_Olusturan olusturanV2X)
        {
            if (olusturanV2X == null)
                return null;
            else
            {
                var olusturan = new Olusturan();
                if (olusturanV2X.Item is Api.V2X.CT_KurumKurulus kurumKurulus)
                    olusturan.Oge = kurumKurulus.ToKurumKurulus();
                else if (olusturanV2X.Item is Api.V2X.CT_TuzelSahis tuzelSahis)
                    olusturan.Oge = tuzelSahis.ToTuzelSahis();
                else if (olusturanV2X.Item is Api.V2X.CT_GercekSahis gercekSahis)
                    olusturan.Oge = gercekSahis.ToGercekSahis();

                return olusturan;
            }
        }

        public static Api.V1X.CT_Ozet ToV1XCT_Ozet(this Ozet ozet)
        {
            if (ozet == null)
                return null;
            else
                return new Api.V1X.CT_Ozet
                {
                    OzetAlgoritmasi = ozet.OzetAlgoritmasi.ToV1XCT_OzetAlgoritmasi(),
                    OzetDegeri = ozet.OzetDegeri
                };
        }

        public static Api.V2X.CT_Ozet ToV2XCT_Ozet(this Ozet ozet)
        {
            if (ozet == null)
                return null;
            else
                return new Api.V2X.CT_Ozet
                {
                    OzetAlgoritmasi = ozet.OzetAlgoritmasi.ToV2XCT_OzetAlgoritmasi(),
                    OzetDegeri = ozet.OzetDegeri
                };
        }

        public static Ozet ToOzet(this Api.V1X.CT_Ozet ozetV1X)
        {
            if (ozetV1X == null)
                return null;
            else
                return new Ozet
                {
                    OzetAlgoritmasi = ozetV1X.OzetAlgoritmasi.ToOzetAlgoritmasi(),
                    OzetDegeri = ozetV1X.OzetDegeri
                };
        }

        public static Ozet ToOzet(this Api.V2X.CT_Ozet ozetV2X)
        {
            if (ozetV2X == null)
                return null;
            else
                return new Ozet
                {
                    OzetAlgoritmasi = ozetV2X.OzetAlgoritmasi.ToOzetAlgoritmasi(),
                    OzetDegeri = ozetV2X.OzetDegeri
                };
        }

        public static Api.V1X.CT_OzetAlgoritmasi ToV1XCT_OzetAlgoritmasi(this OzetAlgoritmasi ozetAlgoritmasi)
        {
            if (ozetAlgoritmasi == null)
                return null;
            else
                return new Api.V1X.CT_OzetAlgoritmasi
                {
                    Algorithm = ozetAlgoritmasi.Algoritma.ToXmlNameSpace(),
                    Any = ozetAlgoritmasi.Any
                };
        }

        public static Api.V2X.CT_OzetAlgoritmasi ToV2XCT_OzetAlgoritmasi(this OzetAlgoritmasi ozetAlgoritmasi)
        {
            if (ozetAlgoritmasi == null)
                return null;
            else
                return new Api.V2X.CT_OzetAlgoritmasi
                {
                    Algorithm = ozetAlgoritmasi.Algoritma.ToXmlNameSpace(),
                    Any = ozetAlgoritmasi.Any
                };
        }

        public static OzetAlgoritmasi ToOzetAlgoritmasi(this Api.V1X.CT_OzetAlgoritmasi ozetAlgoritmasiV1X)
        {
            if (ozetAlgoritmasiV1X == null)
                return null;
            else
                return new OzetAlgoritmasi
                {
                    Algoritma = ozetAlgoritmasiV1X.Algorithm.ToOzetAlgoritmaTuru(),
                    Any = ozetAlgoritmasiV1X.Any
                };
        }

        public static OzetAlgoritmasi ToOzetAlgoritmasi(this Api.V2X.CT_OzetAlgoritmasi ozetAlgoritmasiV2X)
        {
            if (ozetAlgoritmasiV2X == null)
                return null;
            else
                return new OzetAlgoritmasi
                {
                    Algoritma = ozetAlgoritmasiV2X.Algorithm.ToOzetAlgoritmaTuru(),
                    Any = ozetAlgoritmasiV2X.Any
                };
        }

        public static Api.V1X.CT_PaketOzeti ToV1XCT_PaketOzeti(this PaketOzeti PaketOzeti)
        {
            if (PaketOzeti == null)
                return null;
            else
            {
                var PaketOzetiV1X = new Api.V1X.CT_PaketOzeti();
                PaketOzetiV1X.Id = PaketOzeti.Id.ToString();
                if (PaketOzeti.Referanslar != null && PaketOzeti.Referanslar.Count > 0)
                    PaketOzeti.Referanslar.ForEach((referans) => { PaketOzetiV1X.Reference = PaketOzetiV1X.Reference.Add(referans.ToV1XCT_Reference()); });

                return PaketOzetiV1X;
            }
        }

        public static Api.V2X.CT_PaketOzeti ToV2XCT_PaketOzeti(this PaketOzeti PaketOzeti)
        {
            if (PaketOzeti == null)
                return null;
            else
            {
                var PaketOzetiV2X = new Api.V2X.CT_PaketOzeti();
                PaketOzetiV2X.Id = PaketOzeti.Id.ToString();
                if (PaketOzeti.Referanslar != null && PaketOzeti.Referanslar.Count > 0)
                    PaketOzeti.Referanslar.ForEach((referans) => { PaketOzetiV2X.Reference = PaketOzetiV2X.Reference.Add(referans.ToV2XCT_Reference()); });

                return PaketOzetiV2X;
            }
        }

        public static PaketOzeti ToPaketOzeti(this Api.V1X.CT_PaketOzeti PaketOzetiV1X)
        {
            if (PaketOzetiV1X == null)
                return null;
            else
            {
                var PaketOzeti = new PaketOzeti();
                PaketOzeti.Id = Guid.Parse(PaketOzetiV1X.Id);
                if (PaketOzetiV1X.Reference != null && PaketOzetiV1X.Reference.Length > 0)
                {
                    PaketOzeti.Referanslar = new List<Referans>();
                    Array.ForEach(PaketOzetiV1X.Reference, (reference) => { PaketOzeti.Referanslar.Add(reference.ToReferans()); });
                }

                return PaketOzeti;
            }
        }

        public static PaketOzeti ToPaketOzeti(this Api.V2X.CT_PaketOzeti PaketOzetiV2X)
        {
            if (PaketOzetiV2X == null)
                return null;
            else
            {
                var PaketOzeti = new PaketOzeti();
                PaketOzeti.Id = Guid.Parse(PaketOzetiV2X.Id);
                if (PaketOzetiV2X.Reference != null && PaketOzetiV2X.Reference.Length > 0)
                {
                    PaketOzeti.Referanslar = new List<Referans>();
                    Array.ForEach(PaketOzetiV2X.Reference, (reference) => { PaketOzeti.Referanslar.Add(reference.ToReferans()); });
                }

                return PaketOzeti;
            }
        }

        public static Api.V2X.CT_ParafOzeti ToV2XCT_ParafOzeti(this ParafOzeti parafOzeti)
        {
            if (parafOzeti == null)
                return null;
            else
            {
                var parafOzetiV2X = new Api.V2X.CT_ParafOzeti();
                parafOzetiV2X.Id = parafOzeti.Id.ToString();
                if (parafOzeti.Referanslar != null && parafOzeti.Referanslar.Count > 0)
                    parafOzeti.Referanslar.ForEach((reference) => { parafOzetiV2X.Reference = parafOzetiV2X.Reference.Add(reference.ToV2XCT_Reference()); });

                return parafOzetiV2X;
            }
        }

        public static ParafOzeti ToParafOzeti(this Api.V2X.CT_ParafOzeti parafOzetiV2X)
        {
            if (parafOzetiV2X == null)
                return null;
            else
            {
                var parafOzeti = new ParafOzeti();
                parafOzeti.Id = Guid.Parse(parafOzetiV2X.Id);
                if (parafOzetiV2X.Reference != null && parafOzetiV2X.Reference.Length > 0)
                {
                    parafOzeti.Referanslar = new List<Referans>();
                    Array.ForEach(parafOzetiV2X.Reference, (reference) => { parafOzeti.Referanslar.Add(reference.ToReferans()); });
                }

                return parafOzeti;
            }
        }

        public static Api.V1X.CT_Reference ToV1XCT_Reference(this Referans referans)
        {
            if (referans == null)
                return null;
            else
                return new Api.V1X.CT_Reference
                {
                    DigestMethod = referans.Ozet.OzetAlgoritmasi.ToV1XCT_DigestMethod(),
                    DigestValue = referans.Ozet.OzetDegeri,
                    Id = referans.Id,
                    Type = referans.Type,
                    URI = referans.URI
                };
        }

        public static Api.V2X.CT_Reference ToV2XCT_Reference(this Referans referans)
        {
            if (referans == null)
                return null;
            else
                return new Api.V2X.CT_Reference
                {
                    DigestItem = referans.Ozet.ToV2XCT_DigestItem(),
                    DigestItem1 = referans.Ozet1.ToV2XCT_DigestItem(),
                    Id = referans.Id,
                    Type = referans.Type,
                    URI = referans.URI
                };
        }

        public static Referans ToReferans(this Api.V1X.CT_Reference referenceV1X)
        {
            if (referenceV1X == null)
                return null;
            else
                return new Referans
                {
                    Ozet = new Ozet
                    {
                        OzetAlgoritmasi = referenceV1X.DigestMethod.ToOzetAlgoritmasi(),
                        OzetDegeri = referenceV1X.DigestValue
                    },
                    Id = referenceV1X.Id,
                    Type = referenceV1X.Type,
                    URI = referenceV1X.URI
                };
        }

        public static Referans ToReferans(this Api.V2X.CT_Reference referenceV2X)
        {
            if (referenceV2X == null)
                return null;
            else
                return new Referans
                {
                    Ozet = referenceV2X.DigestItem.ToOzet(),
                    Ozet1 = referenceV2X.DigestItem1.ToOzet(),
                    Id = referenceV2X.Id,
                    Type = referenceV2X.Type,
                    URI = referenceV2X.URI
                };
        }

        public static Api.V2X.CT_SDP ToV2XCT_SDP(this SDP sdp)
        {
            if (sdp == null)
                return null;
            else
                return new Api.V2X.CT_SDP
                {
                    Aciklama = sdp.Aciklama,
                    Ad = sdp.Ad,
                    Kod = sdp.Kod
                };
        }

        public static SDP ToSDP(this Api.V2X.CT_SDP sdpV2X)
        {
            if (sdpV2X == null)
                return null;
            else
                return new SDP
                {
                    Aciklama = sdpV2X.Aciklama,
                    Ad = sdpV2X.Ad,
                    Kod = sdpV2X.Kod
                };
        }

        public static Api.V2X.CT_SDPBilgisi ToV2XCT_SDPBilgisi(this SDPBilgisi sdpBilgisi)
        {
            if (sdpBilgisi == null)
                return null;
            else
            {
                var sdpBilgisiV2X = new Api.V2X.CT_SDPBilgisi();
                sdpBilgisiV2X.AnaSdp = sdpBilgisi.AnaSdp.ToV2XCT_SDP();
                if (sdpBilgisi.DigerSdpler != null && sdpBilgisi.DigerSdpler.Count > 0)
                    sdpBilgisi.DigerSdpler.ForEach((sdp) => { sdpBilgisiV2X.DigerSdpler = sdpBilgisiV2X.DigerSdpler.Add(sdp.ToV2XCT_SDP()); });

                return sdpBilgisiV2X;
            }
        }

        public static SDPBilgisi ToSDPBilgisi(this Api.V2X.CT_SDPBilgisi sdpBilgisiV2X)
        {
            if (sdpBilgisiV2X == null)
                return null;
            else
            {
                var sdpBilgisi = new SDPBilgisi();
                sdpBilgisi.AnaSdp = sdpBilgisiV2X.AnaSdp.ToSDP();
                if (sdpBilgisiV2X.DigerSdpler != null && sdpBilgisiV2X.DigerSdpler.Length > 0)
                {
                    sdpBilgisi.DigerSdpler = new List<SDP>();
                    Array.ForEach(sdpBilgisiV2X.DigerSdpler, (sdp) => { sdpBilgisi.DigerSdpler.Add(sdp.ToSDP()); });
                }

                return sdpBilgisi;
            }
        }

        public static Api.V1X.CT_TuzelSahis ToV1XCT_TuzelSahis(this TuzelSahis tuzelSahis)
        {
            if (tuzelSahis == null)
                return null;
            else
                return new Api.V1X.CT_TuzelSahis
                {
                    Adi = tuzelSahis.Ad.ToV1XNameType(),
                    Id = tuzelSahis.Id.ToV1XIdentifierType(),
                    IletisimBilgisi = tuzelSahis.IletisimBilgisi.ToV1XCT_IletisimBilgisi()
                };
        }

        public static Api.V2X.CT_TuzelSahis ToV2XCT_TuzelSahis(this TuzelSahis tuzelSahis)
        {
            if (tuzelSahis == null)
                return null;
            else
                return new Api.V2X.CT_TuzelSahis
                {
                    Adi = tuzelSahis.Ad.ToV2XNameType(),
                    Id = tuzelSahis.Id.ToV2XIdentifierType(),
                    IletisimBilgisi = tuzelSahis.IletisimBilgisi.ToV2XCT_IletisimBilgisi()
                };
        }

        public static TuzelSahis ToTuzelSahis(this Api.V1X.CT_TuzelSahis tuzelSahisV1X)
        {
            if (tuzelSahisV1X == null)
                return null;
            else
                return new TuzelSahis
                {
                    Ad = tuzelSahisV1X.Adi.ToIsimTip(),
                    Id = tuzelSahisV1X.Id.ToTanimlayiciTip(),
                    IletisimBilgisi = tuzelSahisV1X.IletisimBilgisi.ToIletisimBilgisi()
                };
        }

        public static TuzelSahis ToTuzelSahis(this Api.V2X.CT_TuzelSahis tuzelSahisV2X)
        {
            if (tuzelSahisV2X == null)
                return null;
            else
                return new TuzelSahis
                {
                    Ad = tuzelSahisV2X.Adi.ToIsimTip(),
                    Id = tuzelSahisV2X.Id.ToTanimlayiciTip(),
                    IletisimBilgisi = tuzelSahisV2X.IletisimBilgisi.ToIletisimBilgisi()
                };
        }

        public static Api.V1X.CT_Ustveri V1XCT_Ustveri(this Ustveri ustveri)
        {
            if (ustveri == null)
                return null;
            else
            {
                var ustveriV1X = new Api.V1X.CT_Ustveri();
                ustveriV1X.BelgeId = ustveri.BelgeId.ToString().ToUpperInvariant();
                ustveriV1X.BelgeNo = ustveri.BelgeNo;

                if (ustveri.Dagitimlar != null && ustveri.Dagitimlar.Count > 0)
                    ustveri.Dagitimlar.ForEach((dagitim) => { ustveriV1X.DagitimListesi = ustveriV1X.DagitimListesi.Add(dagitim.ToV1XCT_Dagitim()); });

                ustveriV1X.Dil = ustveri.Dil;
                ustveriV1X.DosyaAdi = ustveri.DosyaAdi;

                if (ustveri.Ekler != null && ustveri.Ekler.Count > 0)
                    ustveri.Ekler.ForEach((ek) => { ustveriV1X.Ekler = ustveriV1X.Ekler.Add(ek.ToV1XCT_Ek()); });

                ustveriV1X.GuvenlikKodu = (Api.V1X.ST_KodGuvenlikKodu)Convert.ToInt32(ustveri.GuvenlikKodu);

                if (ustveriV1X.GuvenlikKoduGecerlilikTarihiSpecified)
                    ustveriV1X.GuvenlikKoduGecerlilikTarihi = ustveri.GuvenlikKoduGecerlilikTarihi;

                if (ustveri.Ilgiler != null && ustveri.Ilgiler.Count > 0)
                    ustveri.Ilgiler.ForEach((ilgi) => { ustveriV1X.Ilgiler = ustveriV1X.Ilgiler.Add(ilgi.ToV1XCT_Ilgi()); });

                if (ustveri.Ilgililer != null && ustveri.Ilgililer.Count > 0)
                    ustveri.Ilgililer.ForEach((ilgili) => { ustveriV1X.IlgiliListesi = ustveriV1X.IlgiliListesi.Add(ilgili.ToV1XCT_Ilgili()); });

                ustveriV1X.Konu = ustveri.Konu.ToV1XTextType();
                ustveriV1X.MimeTuru = ustveri.MimeTuru;
                ustveriV1X.Olusturan = ustveri.Olusturan.V1XCT_Olusturan();
                ustveriV1X.OzId = ustveri.OzId.ToV1XIdentifierType();
                ustveriV1X.Tarih = ustveri.Tarih;
                return ustveriV1X;
            }
        }

        public static Api.V2X.CT_Ustveri V2XCT_Ustveri(this Ustveri ustveri)
        {
            if (ustveri == null)
                return null;
            else
            {
                var ustveriV2X = new Api.V2X.CT_Ustveri();
                ustveriV2X.BelgeId = ustveri.BelgeId.ToString().ToUpperInvariant();

                if (ustveri.Dagitimlar != null && ustveri.Dagitimlar.Count > 0)
                    ustveri.Dagitimlar.ForEach((dagitim) => { ustveriV2X.DagitimListesi = ustveriV2X.DagitimListesi.Add(dagitim.ToV2XCT_Dagitim()); });

                ustveriV2X.Dil = ustveri.Dil;
                ustveriV2X.DogrulamaBilgisi = ustveri.DogrulamaBilgisi.ToV2XCT_DogrulamaBilgisi();
                ustveriV2X.DosyaAdi = ustveri.DosyaAdi;

                if (ustveri.Ekler != null && ustveri.Ekler.Count > 0)
                    ustveri.Ekler.ForEach((ek) => { ustveriV2X.Ekler = ustveriV2X.Ekler.Add(ek.ToV2XCT_Ek()); });

                ustveriV2X.GuvenlikKodu = (Api.V2X.ST_KodGuvenlikKodu)Convert.ToInt32(ustveri.GuvenlikKodu);
                if (ustveriV2X.GuvenlikKoduGecerlilikTarihiSpecified)
                    ustveriV2X.GuvenlikKoduGecerlilikTarihi = ustveri.GuvenlikKoduGecerlilikTarihi;

                if (ustveri.HEYSKodlari != null && ustveri.HEYSKodlari.Count > 0)
                    ustveri.HEYSKodlari.ForEach((heysk) => { ustveriV2X.HeyskListesi = ustveriV2X.HeyskListesi.Add(heysk.ToV2XCT_HEYSK()); });

                if (ustveri.Ilgiler != null && ustveri.Ilgiler.Count > 0)
                    ustveri.Ilgiler.ForEach((ilgi) => { ustveriV2X.Ilgiler = ustveriV2X.Ilgiler.Add(ilgi.ToV2XCT_Ilgi()); });

                if (ustveri.Ilgililer != null && ustveri.Ilgililer.Count > 0)
                    ustveri.Ilgililer.ForEach((ilgili) => { ustveriV2X.IlgiliListesi = ustveriV2X.IlgiliListesi.Add(ilgili.ToV2XCT_Ilgili()); });

                ustveriV2X.Konu = ustveri.Konu.ToV2XTextType();
                ustveriV2X.MimeTuru = ustveri.MimeTuru;
                ustveriV2X.Olusturan = ustveri.Olusturan.V2XCT_Olusturan();
                ustveriV2X.OzId = ustveri.OzId.ToV2XIdentifierType();
                ustveriV2X.SdpBilgisi = ustveri.SdpBilgisi.ToV2XCT_SDPBilgisi();
                return ustveriV2X;
            }
        }

        public static Ustveri ToUstveri(this Api.V1X.CT_Ustveri ustveriV1X)
        {
            if (ustveriV1X == null)
                return null;
            else
            {
                var ustveri = new Ustveri();
                ustveri.BelgeId = Guid.Parse(ustveriV1X.BelgeId);
                ustveri.BelgeNo = ustveriV1X.BelgeNo;

                if (ustveriV1X.DagitimListesi != null && ustveriV1X.DagitimListesi.Length > 0)
                {
                    ustveri.Dagitimlar = new List<Dagitim>();
                    Array.ForEach(ustveriV1X.DagitimListesi, (dagitim) => { ustveri.Dagitimlar.Add(dagitim.ToDagitim()); });
                }

                ustveri.Dil = ustveriV1X.Dil;
                ustveri.DosyaAdi = ustveriV1X.DosyaAdi;

                if (ustveriV1X.Ekler != null && ustveriV1X.Ekler.Length > 0)
                {
                    ustveri.Ekler = new List<Ek>();
                    Array.ForEach(ustveriV1X.Ekler, (ek) => { ustveri.Ekler.Add(ek.ToEk()); });
                }

                ustveri.GuvenlikKodu = (GuvenlikKoduTuru)Convert.ToInt32(ustveriV1X.GuvenlikKodu);

                if (ustveriV1X.GuvenlikKoduGecerlilikTarihiSpecified)
                    ustveri.GuvenlikKoduGecerlilikTarihi = ustveriV1X.GuvenlikKoduGecerlilikTarihi;

                if (ustveriV1X.Ilgiler != null && ustveriV1X.Ilgiler.Length > 0)
                {
                    ustveri.Ilgiler = new List<Ilgi>();
                    Array.ForEach(ustveriV1X.Ilgiler, (ilgi) => { ustveri.Ilgiler.Add(ilgi.ToIlgi()); });
                }

                if (ustveriV1X.IlgiliListesi != null && ustveriV1X.IlgiliListesi.Length > 0)
                {
                    ustveri.Ilgililer = new List<Ilgili>();
                    Array.ForEach(ustveriV1X.IlgiliListesi, (ilgili) => { ustveri.Ilgililer.Add(ilgili.ToIlgili()); });
                }

                ustveri.Konu = ustveriV1X.Konu.ToMetinTip();
                ustveri.MimeTuru = ustveriV1X.MimeTuru;
                ustveri.Olusturan = ustveriV1X.Olusturan.ToOlusturan();
                ustveri.OzId = ustveriV1X.OzId.ToTanimlayiciTip();
                ustveri.Tarih = ustveriV1X.Tarih;

                return ustveri;
            }
        }

        public static Ustveri ToUstveri(this Api.V2X.CT_Ustveri ustveriV2X)
        {
            if (ustveriV2X == null)
                return null;
            else
            {
                var ustveri = new Ustveri();
                ustveri.BelgeId = Guid.Parse(ustveriV2X.BelgeId);

                if (ustveriV2X.DagitimListesi != null && ustveriV2X.DagitimListesi.Length > 0)
                {
                    ustveri.Dagitimlar = new List<Dagitim>();
                    Array.ForEach(ustveriV2X.DagitimListesi, (dagitim) => { ustveri.Dagitimlar.Add(dagitim.ToDagitim()); });
                }

                ustveri.Dil = ustveriV2X.Dil;
                ustveri.DogrulamaBilgisi = ustveriV2X.DogrulamaBilgisi.ToDogrulamaBilgisi();
                ustveri.DosyaAdi = ustveriV2X.DosyaAdi;

                if (ustveriV2X.Ekler != null && ustveriV2X.Ekler.Length > 0)
                {
                    ustveri.Ekler = new List<Ek>();
                    Array.ForEach(ustveriV2X.Ekler, (ek) => { ustveri.Ekler.Add(ek.ToEk()); });
                }

                ustveri.GuvenlikKodu = (GuvenlikKoduTuru)Convert.ToInt32(ustveriV2X.GuvenlikKodu);
                if (ustveriV2X.GuvenlikKoduGecerlilikTarihiSpecified)
                    ustveri.GuvenlikKoduGecerlilikTarihi = ustveriV2X.GuvenlikKoduGecerlilikTarihi;

                if (ustveriV2X.HeyskListesi != null && ustveriV2X.HeyskListesi.Length > 0)
                {
                    Array.ForEach(ustveriV2X.HeyskListesi, (heysk) => { ustveri.HEYSKodlari.Add(heysk.ToHEYSK()); });
                }

                if (ustveriV2X.Ilgiler != null && ustveriV2X.Ilgiler.Length > 0)
                {
                    ustveri.Ilgiler = new List<Ilgi>();
                    Array.ForEach(ustveriV2X.Ilgiler, (ilgi) => { ustveri.Ilgiler.Add(ilgi.ToIlgi()); });
                }

                if (ustveriV2X.IlgiliListesi != null && ustveriV2X.IlgiliListesi.Length > 0)
                {
                    ustveri.Ilgililer = new List<Ilgili>();
                    Array.ForEach(ustveriV2X.IlgiliListesi, (ilgili) => { ustveri.Ilgililer.Add(ilgili.ToIlgili()); });
                }

                ustveri.Konu = ustveriV2X.Konu.ToMetinTip();
                ustveri.MimeTuru = ustveriV2X.MimeTuru;
                ustveri.Olusturan = ustveriV2X.Olusturan.ToOlusturan();
                ustveri.OzId = ustveriV2X.OzId.ToTanimlayiciTip();
                ustveri.SdpBilgisi = ustveriV2X.SdpBilgisi.ToSDPBilgisi();

                return ustveri;
            }
        }

        public static Api.V1X.NameType ToV1XNameType(this IsimTip isimTip)
        {
            if (isimTip == null)
                return null;
            else
                return new Api.V1X.NameType { languageID = isimTip.DilID, Value = isimTip.Deger };
        }

        public static Api.V2X.NameType ToV2XNameType(this IsimTip isimTip)
        {
            if (isimTip == null)
                return null;
            else
                return new Api.V2X.NameType { languageID = isimTip.DilID, Value = isimTip.Deger };
        }

        public static IsimTip ToIsimTip(this Api.V1X.NameType nameTypeV1X)
        {
            if (nameTypeV1X == null)
                return null;
            else
                return new IsimTip
                {
                    DilID = nameTypeV1X.languageID,
                    Deger = nameTypeV1X.Value
                };
        }

        public static IsimTip ToIsimTip(this Api.V2X.NameType nameTypeV2X)
        {
            if (nameTypeV2X == null)
                return null;
            else
                return new IsimTip
                {
                    DilID = nameTypeV2X.languageID,
                    Deger = nameTypeV2X.Value
                };
        }

        public static Api.V1X.TextType ToV1XTextType(this MetinTip metinTip)
        {
            if (metinTip == null)
                return null;
            else
                return new Api.V1X.TextType { languageID = metinTip.DilID, Value = metinTip.Deger };
        }

        public static Api.V2X.TextType ToV2XTextType(this MetinTip metinTip)
        {
            if (metinTip == null)
                return null;
            else
                return new Api.V2X.TextType { languageID = metinTip.DilID, Value = metinTip.Deger };
        }

        public static MetinTip ToMetinTip(this Api.V1X.TextType textTypeV1X)
        {
            if (textTypeV1X == null)
                return null;
            else
                return new MetinTip
                {
                    DilID = textTypeV1X.languageID,
                    Deger = textTypeV1X.Value
                };
        }

        public static MetinTip ToMetinTip(this Api.V2X.TextType textTypeV2X)
        {
            if (textTypeV2X == null)
                return null;
            else
                return new MetinTip
                {
                    DilID = textTypeV2X.languageID,
                    Deger = textTypeV2X.Value
                };
        }

        public static Api.V1X.IdentifierType ToV1XIdentifierType(this TanimlayiciTip tanimlayiciTip)
        {
            if (tanimlayiciTip == null)
                return null;
            else
                return new Api.V1X.IdentifierType { schemeID = tanimlayiciTip.SemaID, Value = tanimlayiciTip.Deger };
        }

        public static Api.V2X.IdentifierType ToV2XIdentifierType(this TanimlayiciTip tanimlayiciTip)
        {
            if (tanimlayiciTip == null)
                return null;
            else
                return new Api.V2X.IdentifierType { schemeID = tanimlayiciTip.SemaID, Value = tanimlayiciTip.Deger };
        }

        public static TanimlayiciTip ToTanimlayiciTip(this Api.V1X.IdentifierType identifierTypeV1X)
        {
            if (identifierTypeV1X == null)
                return null;
            else
                return new TanimlayiciTip
                {
                    SemaID = identifierTypeV1X.schemeID,
                    Deger = identifierTypeV1X.Value
                };
        }

        public static TanimlayiciTip ToTanimlayiciTip(this Api.V2X.IdentifierType identifierTypeV2X)
        {
            if (identifierTypeV2X == null)
                return null;
            else
                return new TanimlayiciTip
                {
                    SemaID = identifierTypeV2X.schemeID,
                    Deger = identifierTypeV2X.Value
                };
        }
    }
}