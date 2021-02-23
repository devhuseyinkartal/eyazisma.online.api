using eyazisma.online.api.Classes;
using eyazisma.online.api.Enums;
using System;
using System.Collections.Generic;

namespace eyazisma.online.api.Interfaces.Fluents
{
    public interface IUstveriV1XFluent
    {
        /// <summary>
        /// Belgenin tekil numarasıdır.
        /// </summary>
        /// <param name="belgeId">Belgenin tekil numarası değeridir.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IUstveriV1XFluentBelgeId BelgeIdAta(Guid belgeId);
    }

    public interface IUstveriV1XFluentBelgeId
    {
        /// <summary>
        /// Belgenin konusudur.
        /// </summary>
        /// <param name="konu">Belgenin konu bilgisi değeridir. MetinTip tipinde olmalıdır.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IUstveriV1XFluentKonu KonuAta(MetinTip konu);
    }

    public interface IUstveriV1XFluentKonu
    {
        /// <summary>
        /// Belgenin tarihidir.
        /// </summary>
        /// <param name="tarih">Belgenin tarih değeridir.</param>
        /// <remarks>
        /// Zorunlu alandır.
        /// UTC ofset değeri ile verilmesi tavsiye edilir.
        /// </remarks>
        IUstveriV1XFluentTarih TarihAta(DateTime tarih);
    }

    public interface IUstveriV1XFluentTarih
    {
        /// <summary>
        /// Belge numarasıdır.
        /// </summary>
        /// <param name="belgeNo">Belge numarası değeridir.</param>
        /// <remarks>
        /// Zorunlu alandır.
        /// Resmi yazışmalara ilişkin mevzuatta belirtilen biçime uygun olmalıdır.
        /// </remarks>
        IUstveriV1XFluentBelgeNo BelgeNoAta(string belgeNo);
    }

    public interface IUstveriV1XFluentBelgeNo
    {
        /// <summary>
        /// Belgenin güvenlik derecesini gösterir.
        /// </summary>
        /// <param name="guvenlikKodu">Belgenin güvenlik derecesi değeridir. GuvenlikKoduTuru tipinde olmalıdır.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IUstveriV1XFluentGuvenlikKodu GuvenlikKoduAta(GuvenlikKoduTuru guvenlikKodu);
    }

    public interface IUstveriV1XFluentGuvenlikKodu
    {
        /// <summary>
        /// Belgenin güvenlik seviyesinin ortadan kalktığı tarihtir.
        /// </summary>
        ///<param name="tarih">Belgenin güvenlik seviyesinin ortadan kalktığı tarih değeridir.</param>
        IUstveriV1XFluentGuvenlikKoduGecerlilikTarihi GuvenlikKoduGecerlilikTarihiIle(DateTime tarih);
        /// <summary>
        /// Belgenin üretildiği sistemdeki tekil anahtar değeridir.
        /// </summary>
        /// <param name="ozId">TanimlayiciTip tipinde olmalıdır.</param>
        /// <remarks>
        /// Tekil anahtar değeri için kullanılan veri türü/şeması, elemanın SemaID alanında verilir.
        /// Elemanın boş bırakılması ekin elektronik bir sistemde üretilmediği anlamına gelir.
        /// OzId değeri verilmesi durumunda, SemaID değerinin verilmesi zorunludur.
        /// </remarks>
        IUstveriV1XFluentOzId OzIdIle(TanimlayiciTip ozId);
        /// <summary>
        /// Belgenin dağıtımının yapıldığı taraf bilgisidir.
        /// </summary>
        /// <param name="dagitim">Dagitim tipinde olmalıdır.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IUstveriV1XFluentDagitim DagitimAta(Dagitim dagitim);
        /// <summary>
        /// Belgenin dağıtımının yapıldığı taraf bilgileridir.
        /// </summary>
        /// <param name="dagitimlar">Belgenin dağıtımının yapıldığı taraf bilgileri değeridir. Dagitim listesi tipinde olmalıdır.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IUstveriV1XFluentDagitimlar DagitimlarAta(List<Dagitim> dagitimlar);
    }

    public interface IUstveriV1XFluentGuvenlikKoduGecerlilikTarihi
    {
        /// <summary>
        /// Belgenin üretildiği sistemdeki tekil anahtar değeridir.
        /// </summary>
        /// <param name="ozId">TanimlayiciTip tipinde olmalıdır.</param>
        /// <remarks>
        /// Tekil anahtar değeri için kullanılan veri türü/şeması, elemanın SemaID alanında verilir.
        /// Elemanın boş bırakılması ekin elektronik bir sistemde üretilmediği anlamına gelir.
        /// OzId değeri verilmesi durumunda, SemaID değerinin verilmesi zorunludur.
        /// </remarks>
        IUstveriV1XFluentOzId OzIdIle(TanimlayiciTip ozId);
        /// <summary>
        /// Belgenin dağıtımının yapıldığı taraf bilgisidir.
        /// </summary>
        /// <param name="dagitim">Dagitim tipinde olmalıdır.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IUstveriV1XFluentDagitim DagitimAta(Dagitim dagitim);
        /// <summary>
        /// Belgenin dağıtımının yapıldığı taraf bilgileridir.
        /// </summary>
        /// <param name="dagitimlar">Belgenin dağıtımının yapıldığı taraf bilgileri değeridir. Dagitim listesi tipinde olmalıdır.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IUstveriV1XFluentDagitimlar DagitimlarAta(List<Dagitim> dagitimlar);
    }

    public interface IUstveriV1XFluentOzId
    {
        /// <summary>
        /// Belgenin dağıtımının yapıldığı taraf bilgisidir.
        /// </summary>
        /// <param name="dagitim">Belgenin dağıtımının yapıldığı taraf bilgisi değeridir. Dagitim tipinde olmalıdır.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IUstveriV1XFluentDagitim DagitimAta(Dagitim dagitim);

        /// <summary>
        /// Belgenin dağıtımının yapıldığı taraf bilgileridir.
        /// </summary>
        /// <param name="dagitimlar">Belgenin dağıtımının yapıldığı taraf bilgileri değeridir. Dagitim listesi tipinde olmalıdır.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IUstveriV1XFluentDagitimlar DagitimlarAta(List<Dagitim> dagitimlar);
    }

    public interface IUstveriV1XFluentDagitim
    {
        /// <summary>
        /// Belgenin dağıtımının yapıldığı taraf bilgisidir.
        /// </summary>
        /// <param name="dagitim">Dagitim tipinde olmalıdır.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IUstveriV1XFluentDagitim DagitimAta(Dagitim dagitim);
        /// <summary>
        /// Belgeye eklenecek eke ilişkin bilgidir.
        /// </summary>
        /// <param name="ek">Belgeye eklenecek ekin değeridir. Ek tipinde olmalıdır.</param>
        IUstveriV1XFluentEk EkIle(Ek ek);
        /// <summary>
        /// Belgeye eklenecek eklere ilişkin bilgilerdir.
        /// </summary>
        /// <param name="ekler">Belgeye eklenecek eklerin değeridir. Ek listesi tipinde olmalıdır.</param>
        IUstveriV1XFluentEkler EklerIle(List<Ek> ekler);
        /// <summary>
        /// Belgeye eklenecek ilgiye ilişkin bilgidir.
        /// </summary>
        /// <param name="ilgi">Belgeye eklenecek ilginin değeridir. Ilgi tipinde olmalıdır.</param>
        IUstveriV1XFluentIlgi IlgiIle(Ilgi ilgi);
        /// <summary>
        /// Belgeye eklenecek ilgilere ilişkin bilgilerdir.
        /// </summary>
        /// <param name="ilgiler">Belgeye eklenecek ilgilerin değeridir. Ilgi listesi tipinde olmalıdır.</param>
        IUstveriV1XFluentIlgiler IlgilerIle(List<Ilgi> ilgiler);
        /// <summary>
        /// Belgenin oluşturulduğu dildir.
        /// </summary>
        /// <param name="dil">Belgenin oluşturulduğu dilin değeridir.</param>
        /// <remarks>ISO 639-3 standardına uygun dil kodu verilmelidir.</remarks>
        IUstveriV1XFluentDil DilIle(string dil);
        /// <summary>
        /// Belgeyi oluşturan taraftır.
        /// </summary>
        /// <param name="olusturan">Belgeyi oluşturan tarafın değeridir. Olusturan tipinde olmalıdır.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IUstveriV1XFluentOlusturan OlusturanAta(Olusturan olusturan);
    }

    public interface IUstveriV1XFluentDagitimlar
    {
        /// <summary>
        /// Belgeye eklenecek eke ilişkin bilgidir.
        /// </summary>
        /// <param name="ek">Belgeye eklenecek ekin değeridir. Ek tipinde olmalıdır.</param>
        IUstveriV1XFluentEk EkIle(Ek ek);
        /// <summary>
        /// Belgeye eklenecek eklere ilişkin bilgilerdir.
        /// </summary>
        /// <param name="ekler">Belgeye eklenecek eklerin değeridir. Ek listesi tipinde olmalıdır.</param>
        IUstveriV1XFluentEkler EklerIle(List<Ek> ekler);
        /// <summary>
        /// Belgeye eklenecek ilgiye ilişkin bilgidir.
        /// </summary>
        /// <param name="ilgi">Belgeye eklenecek ilginin değeridir. Ilgi tipinde olmalıdır.</param>
        IUstveriV1XFluentIlgi IlgiIle(Ilgi ilgi);
        /// <summary>
        /// Belgeye eklenecek ilgilere ilişkin bilgilerdir.
        /// </summary>
        /// <param name="ilgiler">Belgeye eklenecek ilgilerin değeridir. Ilgi listesi tipinde olmalıdır.</param>
        IUstveriV1XFluentIlgiler IlgilerIle(List<Ilgi> ilgiler);
        /// <summary>
        /// Belgenin oluşturulduğu dildir.
        /// </summary>
        /// <param name="dil">Belgenin oluşturulduğu dilin değeridir.</param>
        /// <remarks>ISO 639-3 standardına uygun dil kodu verilmelidir.</remarks>
        IUstveriV1XFluentDil DilIle(string dil);
        /// <summary>
        /// Belgeyi oluşturan taraftır.
        /// </summary>
        /// <param name="olusturan">Belgeyi oluşturan tarafın değeridir. Olusturan tipinde olmalıdır.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IUstveriV1XFluentOlusturan OlusturanAta(Olusturan olusturan);
    }

    public interface IUstveriV1XFluentEk
    {
        /// <summary>
        /// Belgeye eklenecek eke ilişkin bilgidir.
        /// </summary>
        /// <param name="ek">Belgeye eklenecek ekin değeridir. Ek tipinde olmalıdır.</param>
        IUstveriV1XFluentEk EkIle(Ek ek);
        /// <summary>
        /// Belgeye eklenecek ilgiye ilişkin bilgidir.
        /// </summary>
        /// <param name="ilgi">Belgeye eklenecek ilginin değeridir. Ilgi tipinde olmalıdır.</param>
        IUstveriV1XFluentIlgi IlgiIle(Ilgi ilgi);
        /// <summary>
        /// Belgeye eklenecek ilgilere ilişkin bilgilerdir.
        /// </summary>
        /// <param name="ilgiler">Belgeye eklenecek ilgilerin değeridir. Ilgi listesi tipinde olmalıdır.</param>
        IUstveriV1XFluentIlgiler IlgilerIle(List<Ilgi> ilgiler);
        /// <summary>
        /// Belgenin oluşturulduğu dildir.
        /// </summary>
        /// <param name="dil">Belgenin oluşturulduğu dilin değeridir.</param>
        /// <remarks>ISO 639-3 standardına uygun dil kodu verilmelidir.</remarks>
        IUstveriV1XFluentDil DilIle(string dil);
        /// <summary>
        /// Belgeyi oluşturan taraftır.
        /// </summary>
        /// <param name="olusturan">Belgeyi oluşturan tarafın değeridir. Olusturan tipinde olmalıdır.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IUstveriV1XFluentOlusturan OlusturanAta(Olusturan olusturan);
    }

    public interface IUstveriV1XFluentEkler
    {
        /// <summary>
        /// Belgeye eklenecek ilgiye ilişkin bilgidir.
        /// </summary>
        /// <param name="ilgi">Belgeye eklenecek ilginin değeridir. Ilgi tipinde olmalıdır.</param>
        IUstveriV1XFluentIlgi IlgiIle(Ilgi ilgi);
        /// <summary>
        /// Belgeye eklenecek ilgilere ilişkin bilgilerdir.
        /// </summary>
        /// <param name="ilgiler">Belgeye eklenecek ilgilerin değeridir. Ilgi listesi tipinde olmalıdır.</param>
        IUstveriV1XFluentIlgiler IlgilerIle(List<Ilgi> ilgiler);
        /// <summary>
        /// Belgenin oluşturulduğu dildir.
        /// </summary>
        /// <param name="dil">Belgenin oluşturulduğu dilin değeridir.</param>
        /// <remarks>ISO 639-3 standardına uygun dil kodu verilmelidir.</remarks>
        IUstveriV1XFluentDil DilIle(string dil);
        /// <summary>
        /// Belgeyi oluşturan taraftır.
        /// </summary>
        /// <param name="olusturan">Belgeyi oluşturan tarafın değeridir. Olusturan tipinde olmalıdır.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IUstveriV1XFluentOlusturan OlusturanAta(Olusturan olusturan);
    }

    public interface IUstveriV1XFluentIlgi
    {
        /// <summary>
        /// Belgeye eklenecek ilgiye ilişkin bilgidir.
        /// </summary>
        /// <param name="ilgi">Belgeye eklenecek ilginin değeridir. Ilgi tipinde olmalıdır.</param>
        IUstveriV1XFluentIlgi IlgiIle(Ilgi ilgi);
        /// <summary>
        /// Belgenin oluşturulduğu dildir.
        /// </summary>
        /// <param name="dil">Belgenin oluşturulduğu dilin değeridir.</param>
        /// <remarks>ISO 639-3 standardına uygun dil kodu verilmelidir.</remarks>
        IUstveriV1XFluentDil DilIle(string dil);
        /// <summary>
        /// Belgeyi oluşturan taraftır.
        /// </summary>
        /// <param name="olusturan">Belgeyi oluşturan tarafın değeridir. Olusturan tipinde olmalıdır.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IUstveriV1XFluentOlusturan OlusturanAta(Olusturan olusturan);
    }

    public interface IUstveriV1XFluentIlgiler
    {
        /// <summary>
        /// Belgenin oluşturulduğu dildir.
        /// </summary>
        /// <param name="dil">Belgenin oluşturulduğu dilin değeridir.</param>
        /// <remarks>ISO 639-3 standardına uygun dil kodu verilmelidir.</remarks>
        IUstveriV1XFluentDil DilIle(string dil);
        /// <summary>
        /// Belgeyi oluşturan taraftır.
        /// </summary>
        /// <param name="olusturan">Belgeyi oluşturan tarafın değeridir. Olusturan tipinde olmalıdır.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IUstveriV1XFluentOlusturan OlusturanAta(Olusturan olusturan);
    }

    public interface IUstveriV1XFluentDil
    {
        /// <summary>
        /// Belgeyi oluşturan taraftır.
        /// </summary>
        /// <param name="olusturan">Belgeyi oluşturan tarafın değeridir. Olusturan tipinde olmalıdır.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IUstveriV1XFluentOlusturan OlusturanAta(Olusturan olusturan);
    }

    public interface IUstveriV1XFluentOlusturan
    {
        /// <summary>
        ///  Belge ile ilgili iletişim kurulacak tarafa ait bilgidir.
        /// </summary>
        /// <param name="ilgili">Ilgili tipinde olmalıdır.</param>
        IUstveriV1XFluentIlgili IlgiliIle(Ilgili ilgili);
        /// <summary>
        ///  Belge ile ilgili iletişim kurulacak taraflara ait bilgilerdir.
        /// </summary>
        /// <param name="ilgililer">Ilgili listesi tipinde olmalıdır.</param>
        IUstveriV1XFluentIlgililer IlgililerIle(List<Ilgili> ilgililer);
        /// <summary>
        /// "Üst Yazı" bileşeninin dosya sistemindeki adıdır.
        /// </summary>
        /// <param name="dosyaAdi">"Üst Yazı" bileşeninin dosya sistemindeki adının değeridir.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IUstveriV1XFluentDosyaAdi DosyaAdiAta(string dosyaAdi);
    }

    public interface IUstveriV1XFluentIlgili
    {
        /// <summary>
        ///  Belge ile ilgili iletişim kurulacak tarafa ait bilgidir.
        /// </summary>
        /// <param name="ilgili">Ilgili tipinde olmalıdır.</param>
        IUstveriV1XFluentIlgili IlgiliIle(Ilgili ilgili);
        /// <summary>
        /// "Üst Yazı" bileşeninin dosya sistemindeki adıdır.
        /// </summary>
        /// <param name="dosyaAdi">"Üst Yazı" bileşeninin dosya sistemindeki adının değeridir.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IUstveriV1XFluentDosyaAdi DosyaAdiAta(string dosyaAdi);
    }

    public interface IUstveriV1XFluentIlgililer
    {
        /// <summary>
        /// "Üst Yazı" bileşeninin dosya sistemindeki adıdır.
        /// </summary>
        /// <param name="dosyaAdi">"Üst Yazı" bileşeninin dosya sistemindeki adının değeridir.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IUstveriV1XFluentDosyaAdi DosyaAdiAta(string dosyaAdi);
    }

    public interface IUstveriV1XFluentDosyaAdi
    {
        Ustveri Olustur();
    }

    public interface IUstveriV2XFluent
    {
        /// <summary>
        /// Belgenin tekil numarasıdır.
        /// </summary>
        /// <param name="belgeId">Belgenin tekil numarası değeridir.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IUstveriV2XFluentBelgeId BelgeIdAta(Guid belgeId);
    }

    public interface IUstveriV2XFluentBelgeId
    {
        /// <summary>
        /// Belgenin konusudur.
        /// </summary>
        /// <param name="konu">Belgenin konu bilgisi değeridir. MetinTip tipinde olmalıdır.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IUstveriV2XFluentKonu KonuAta(MetinTip konu);
    }

    public interface IUstveriV2XFluentKonu
    {
        /// <summary>
        /// Belgenin güvenlik derecesini gösterir.
        /// </summary>
        /// <param name="guvenlikKodu">Belgenin güvenlik derecesi değeridir. GuvenlikKoduTuru tipinde olmalıdır.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IUstveriV2XFluentGuvenlikKodu GuvenlikKoduAta(GuvenlikKoduTuru guvenlikKodu);
    }

    public interface IUstveriV2XFluentGuvenlikKodu
    {
        /// <summary>
        /// Belgenin güvenlik seviyesinin ortadan kalktığı tarihtir.
        /// </summary>
        ///<param name="tarih">Belgenin güvenlik seviyesinin ortadan kalktığı tarih değeridir.</param>
        IUstveriV2XFluentGuvenlikKoduGecerlilikTarihi GuvenlikKoduGecerlilikTarihiIle(DateTime tarih);
        /// <summary>
        /// Belgenin üretildiği sistemdeki tekil anahtar değeridir.
        /// </summary>
        /// <param name="ozId">TanimlayiciTip tipinde olmalıdır.</param>
        /// <remarks>
        /// Tekil anahtar değeri için kullanılan veri türü/şeması, elemanın SemaID alanında verilir.
        /// Elemanın boş bırakılması ekin elektronik bir sistemde üretilmediği anlamına gelir.
        /// OzId değeri verilmesi durumunda, SemaID değerinin verilmesi zorunludur.
        /// </remarks>
        IUstveriV2XFluentOzId OzIdIle(TanimlayiciTip ozId);
        /// <summary>
        /// Belgenin dağıtımının yapıldığı taraf bilgisidir.
        /// </summary>
        /// <param name="dagitim">Dagitim tipinde olmalıdır.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IUstveriV2XFluentDagitim DagitimAta(Dagitim dagitim);
        /// <summary>
        /// Belgenin dağıtımının yapıldığı taraf bilgileridir.
        /// </summary>
        /// <param name="dagitimlar">Belgenin dağıtımının yapıldığı taraf bilgileri değeridir. Dagitim listesi tipinde olmalıdır.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IUstveriV2XFluentDagitimlar DagitimlarAta(List<Dagitim> dagitimlar);
    }

    public interface IUstveriV2XFluentGuvenlikKoduGecerlilikTarihi
    {
        /// <summary>
        /// Belgenin üretildiği sistemdeki tekil anahtar değeridir.
        /// </summary>
        /// <param name="ozId">TanimlayiciTip tipinde olmalıdır.</param>
        /// <remarks>
        /// Tekil anahtar değeri için kullanılan veri türü/şeması, elemanın SemaID alanında verilir.
        /// Elemanın boş bırakılması ekin elektronik bir sistemde üretilmediği anlamına gelir.
        /// OzId değeri verilmesi durumunda, SemaID değerinin verilmesi zorunludur.
        /// </remarks>
        IUstveriV2XFluentOzId OzIdIle(TanimlayiciTip ozId);
        /// <summary>
        /// Belgenin dağıtımının yapıldığı taraf bilgisidir.
        /// </summary>
        /// <param name="dagitim">Dagitim tipinde olmalıdır.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IUstveriV2XFluentDagitim DagitimAta(Dagitim dagitim);
        /// <summary>
        /// Belgenin dağıtımının yapıldığı taraf bilgileridir.
        /// </summary>
        /// <param name="dagitimlar">Belgenin dağıtımının yapıldığı taraf bilgileri değeridir. Dagitim listesi tipinde olmalıdır.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IUstveriV2XFluentDagitimlar DagitimlarAta(List<Dagitim> dagitimlar);
    }

    public interface IUstveriV2XFluentOzId
    {
        /// <summary>
        /// Belgenin dağıtımının yapıldığı taraf bilgisidir.
        /// </summary>
        /// <param name="dagitim">Belgenin dağıtımının yapıldığı taraf bilgisi değeridir. Dagitim tipinde olmalıdır.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IUstveriV2XFluentDagitim DagitimAta(Dagitim dagitim);

        /// <summary>
        /// Belgenin dağıtımının yapıldığı taraf bilgileridir.
        /// </summary>
        /// <param name="dagitimlar">Belgenin dağıtımının yapıldığı taraf bilgileri değeridir. Dagitim listesi tipinde olmalıdır.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IUstveriV2XFluentDagitimlar DagitimlarAta(List<Dagitim> dagitimlar);
    }

    public interface IUstveriV2XFluentDagitim
    {
        /// <summary>
        /// Belgenin dağıtımının yapıldığı taraf bilgisidir.
        /// </summary>
        /// <param name="dagitim">Dagitim tipinde olmalıdır.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IUstveriV2XFluentDagitim DagitimAta(Dagitim dagitim);
        /// <summary>
        /// Belgeye eklenecek eke ilişkin bilgidir.
        /// </summary>
        /// <param name="ek">Belgeye eklenecek ekin değeridir. Ek tipinde olmalıdır.</param>
        IUstveriV2XFluentEk EkIle(Ek ek);
        /// <summary>
        /// Belgeye eklenecek eklere ilişkin bilgilerdir.
        /// </summary>
        /// <param name="ekler">Belgeye eklenecek eklerin değeridir. Ek listesi tipinde olmalıdır.</param>
        IUstveriV2XFluentEkler EklerIle(List<Ek> ekler);
        /// <summary>
        /// Belgeye eklenecek ilgiye ilişkin bilgidir.
        /// </summary>
        /// <param name="ilgi">Belgeye eklenecek ilginin değeridir. Ilgi tipinde olmalıdır.</param>
        IUstveriV2XFluentIlgi IlgiIle(Ilgi ilgi);
        /// <summary>
        /// Belgeye eklenecek ilgilere ilişkin bilgilerdir.
        /// </summary>
        /// <param name="ilgiler">Belgeye eklenecek ilgilerin değeridir. Ilgi listesi tipinde olmalıdır.</param>
        IUstveriV2XFluentIlgiler IlgilerIle(List<Ilgi> ilgiler);
        /// <summary>
        /// Belgenin oluşturulduğu dildir.
        /// </summary>
        /// <param name="dil">Belgenin oluşturulduğu dilin değeridir.</param>
        /// <remarks>ISO 639-3 standardına uygun dil kodu verilmelidir.</remarks>
        IUstveriV2XFluentDil DilIle(string dil);
        /// <summary>
        /// Belgeyi oluşturan taraftır.
        /// </summary>
        /// <param name="olusturan">Belgeyi oluşturan tarafın değeridir. Olusturan tipinde olmalıdır.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IUstveriV2XFluentOlusturan OlusturanAta(Olusturan olusturan);
    }

    public interface IUstveriV2XFluentDagitimlar
    {
        /// <summary>
        /// Belgeye eklenecek eke ilişkin bilgidir.
        /// </summary>
        /// <param name="ek">Belgeye eklenecek ekin değeridir. Ek tipinde olmalıdır.</param>
        IUstveriV2XFluentEk EkIle(Ek ek);
        /// <summary>
        /// Belgeye eklenecek eklere ilişkin bilgilerdir.
        /// </summary>
        /// <param name="ekler">Belgeye eklenecek eklerin değeridir. Ek listesi tipinde olmalıdır.</param>
        IUstveriV2XFluentEkler EklerIle(List<Ek> ekler);
        /// <summary>
        /// Belgeye eklenecek ilgiye ilişkin bilgidir.
        /// </summary>
        /// <param name="ilgi">Belgeye eklenecek ilginin değeridir. Ilgi tipinde olmalıdır.</param>
        IUstveriV2XFluentIlgi IlgiIle(Ilgi ilgi);
        /// <summary>
        /// Belgeye eklenecek ilgilere ilişkin bilgilerdir.
        /// </summary>
        /// <param name="ilgiler">Belgeye eklenecek ilgilerin değeridir. Ilgi listesi tipinde olmalıdır.</param>
        IUstveriV2XFluentIlgiler IlgilerIle(List<Ilgi> ilgiler);
        /// <summary>
        /// Belgenin oluşturulduğu dildir.
        /// </summary>
        /// <param name="dil">Belgenin oluşturulduğu dilin değeridir.</param>
        /// <remarks>ISO 639-3 standardına uygun dil kodu verilmelidir.</remarks>
        IUstveriV2XFluentDil DilIle(string dil);
        /// <summary>
        /// Belgeyi oluşturan taraftır.
        /// </summary>
        /// <param name="olusturan">Belgeyi oluşturan tarafın değeridir. Olusturan tipinde olmalıdır.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IUstveriV2XFluentOlusturan OlusturanAta(Olusturan olusturan);
    }

    public interface IUstveriV2XFluentEk
    {
        /// <summary>
        /// Belgeye eklenecek eke ilişkin bilgidir.
        /// </summary>
        /// <param name="ek">Belgeye eklenecek ekin değeridir. Ek tipinde olmalıdır.</param>
        IUstveriV2XFluentEk EkIle(Ek ek);
        /// <summary>
        /// Belgeye eklenecek ilgiye ilişkin bilgidir.
        /// </summary>
        /// <param name="ilgi">Belgeye eklenecek ilginin değeridir. Ilgi tipinde olmalıdır.</param>
        IUstveriV2XFluentIlgi IlgiIle(Ilgi ilgi);
        /// <summary>
        /// Belgeye eklenecek ilgilere ilişkin bilgilerdir.
        /// </summary>
        /// <param name="ilgiler">Belgeye eklenecek ilgilerin değeridir. Ilgi listesi tipinde olmalıdır.</param>
        IUstveriV2XFluentIlgiler IlgilerIle(List<Ilgi> ilgiler);
        /// <summary>
        /// Belgenin oluşturulduğu dildir.
        /// </summary>
        /// <param name="dil">Belgenin oluşturulduğu dilin değeridir.</param>
        /// <remarks>ISO 639-3 standardına uygun dil kodu verilmelidir.</remarks>
        IUstveriV2XFluentDil DilIle(string dil);
        /// <summary>
        /// Belgeyi oluşturan taraftır.
        /// </summary>
        /// <param name="olusturan">Belgeyi oluşturan tarafın değeridir. Olusturan tipinde olmalıdır.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IUstveriV2XFluentOlusturan OlusturanAta(Olusturan olusturan);
    }

    public interface IUstveriV2XFluentEkler
    {
        /// <summary>
        /// Belgeye eklenecek ilgiye ilişkin bilgidir.
        /// </summary>
        /// <param name="ilgi">Belgeye eklenecek ilginin değeridir. Ilgi tipinde olmalıdır.</param>
        IUstveriV2XFluentIlgi IlgiIle(Ilgi ilgi);
        /// <summary>
        /// Belgeye eklenecek ilgilere ilişkin bilgilerdir.
        /// </summary>
        /// <param name="ilgiler">Belgeye eklenecek ilgilerin değeridir. Ilgi listesi tipinde olmalıdır.</param>
        IUstveriV2XFluentIlgiler IlgilerIle(List<Ilgi> ilgiler);
        /// <summary>
        /// Belgenin oluşturulduğu dildir.
        /// </summary>
        /// <param name="dil">Belgenin oluşturulduğu dilin değeridir.</param>
        /// <remarks>ISO 639-3 standardına uygun dil kodu verilmelidir.</remarks>
        IUstveriV2XFluentDil DilIle(string dil);
        /// <summary>
        /// Belgeyi oluşturan taraftır.
        /// </summary>
        /// <param name="olusturan">Belgeyi oluşturan tarafın değeridir. Olusturan tipinde olmalıdır.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IUstveriV2XFluentOlusturan OlusturanAta(Olusturan olusturan);
    }

    public interface IUstveriV2XFluentIlgi
    {
        /// <summary>
        /// Belgeye eklenecek ilgiye ilişkin bilgidir.
        /// </summary>
        /// <param name="ilgi">Belgeye eklenecek ilginin değeridir. Ilgi tipinde olmalıdır.</param>
        IUstveriV2XFluentIlgi IlgiIle(Ilgi ilgi);
        /// <summary>
        /// Belgenin oluşturulduğu dildir.
        /// </summary>
        /// <param name="dil">Belgenin oluşturulduğu dilin değeridir.</param>
        /// <remarks>ISO 639-3 standardına uygun dil kodu verilmelidir.</remarks>
        IUstveriV2XFluentDil DilIle(string dil);
        /// <summary>
        /// Belgeyi oluşturan taraftır.
        /// </summary>
        /// <param name="olusturan">Belgeyi oluşturan tarafın değeridir. Olusturan tipinde olmalıdır.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IUstveriV2XFluentOlusturan OlusturanAta(Olusturan olusturan);
    }

    public interface IUstveriV2XFluentIlgiler
    {
        /// <summary>
        /// Belgenin oluşturulduğu dildir.
        /// </summary>
        /// <param name="dil">Belgenin oluşturulduğu dilin değeridir.</param>
        /// <remarks>ISO 639-3 standardına uygun dil kodu verilmelidir.</remarks>
        IUstveriV2XFluentDil DilIle(string dil);
        /// <summary>
        /// Belgeyi oluşturan taraftır.
        /// </summary>
        /// <param name="olusturan">Belgeyi oluşturan tarafın değeridir. Olusturan tipinde olmalıdır.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IUstveriV2XFluentOlusturan OlusturanAta(Olusturan olusturan);
    }

    public interface IUstveriV2XFluentDil
    {
        /// <summary>
        /// Belgeyi oluşturan taraftır.
        /// </summary>
        /// <param name="olusturan">Belgeyi oluşturan tarafın değeridir. Olusturan tipinde olmalıdır.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IUstveriV2XFluentOlusturan OlusturanAta(Olusturan olusturan);
    }

    public interface IUstveriV2XFluentOlusturan
    {
        /// <summary>
        ///  Belge ile ilgili iletişim kurulacak tarafa ait bilgidir.
        /// </summary>
        /// <param name="ilgili">Ilgili tipinde olmalıdır.</param>
        IUstveriV2XFluentIlgili IlgiliIle(Ilgili ilgili);
        /// <summary>
        ///  Belge ile ilgili iletişim kurulacak taraflara ait bilgilerdir.
        /// </summary>
        /// <param name="ilgililer">Ilgili listesi tipinde olmalıdır.</param>
        IUstveriV2XFluentIlgililer IlgililerIle(List<Ilgili> ilgililer);
        /// <summary>
        /// "Üst Yazı" bileşeninin dosya sistemindeki adıdır.
        /// </summary>
        /// <param name="dosyaAdi">"Üst Yazı" bileşeninin dosya sistemindeki adının değeridir.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IUstveriV2XFluentDosyaAdi DosyaAdiAta(string dosyaAdi);
    }

    public interface IUstveriV2XFluentIlgili
    {
        /// <summary>
        ///  Belge ile ilgili iletişim kurulacak tarafa ait bilgidir.
        /// </summary>
        /// <param name="ilgili">Ilgili tipinde olmalıdır.</param>
        IUstveriV2XFluentIlgili IlgiliIle(Ilgili ilgili);
        /// <summary>
        /// "Üst Yazı" bileşeninin dosya sistemindeki adıdır.
        /// </summary>
        /// <param name="dosyaAdi">"Üst Yazı" bileşeninin dosya sistemindeki adının değeridir.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IUstveriV2XFluentDosyaAdi DosyaAdiAta(string dosyaAdi);
    }

    public interface IUstveriV2XFluentIlgililer
    {
        /// <summary>
        /// "Üst Yazı" bileşeninin dosya sistemindeki adıdır.
        /// </summary>
        /// <param name="dosyaAdi">"Üst Yazı" bileşeninin dosya sistemindeki adının değeridir.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IUstveriV2XFluentDosyaAdi DosyaAdiAta(string dosyaAdi);
    }

    public interface IUstveriV2XFluentDosyaAdi
    {
        /// <summary>
        /// Belgenin Standart Dosya Planı bilgisidir.
        /// </summary>
        /// <param name="sdpBilgisi">Belgenin Standart Dosya Planı bilgisi değeridir. SDPBilgisi tipinde olmalıdır.</param>
        IUstveriV2XFluentSdp SdpBilgisiIle(SDPBilgisi sdpBilgisi);
        /// <summary>
        /// Belgeye ilişkin Hizmet Envanter Yönetim Sistemi kodu bilgisidir.
        /// </summary>
        /// <param name="heysKodu">HEYSK tipinde olmalıdır.</param>
        IUstveriV2XFluentHeysk HEYSKoduIle(HEYSK heysKodu);
        /// <summary>
        /// Belgenin doğrulama yapılacağı web adresi bilgisidir.
        /// </summary>
        /// <param name="dogrulamaBilgisi">Belgenin doğrulama yapılacağı web adresi bilgisi değeridir.</param>
        /// <remarks>
        /// Zorunlu alandır.
        /// </remarks>
        IUstveriV2XFluentDogrulamaBilgisi DogrulamaBilgisiAta(DogrulamaBilgisi dogrulamaBilgisi);
    }

    public interface IUstveriV2XFluentSdp
    {
        /// <summary>
        /// Belgeye ilişkin Hizmet Envanter Yönetim Sistemi kodu bilgisidir.
        /// </summary>
        /// <param name="heysKodu">HEYSK tipinde olmalıdır.</param>
        IUstveriV2XFluentHeysk HEYSKoduIle(HEYSK heysKodu);
        /// <summary>
        /// Belgenin doğrulama yapılacağı web adresi bilgisidir.
        /// </summary>
        /// <param name="dogrulamaBilgisi">Belgenin doğrulama yapılacağı web adresi bilgisi değeridir.</param>
        /// <remarks>
        /// Zorunlu alandır.
        /// </remarks>
        IUstveriV2XFluentDogrulamaBilgisi DogrulamaBilgisiAta(DogrulamaBilgisi dogrulamaBilgisi);
    }

    public interface IUstveriV2XFluentHeysk
    {
        /// <summary>
        /// Belgeye ilişkin Hizmet Envanter Yönetim Sistemi kodu bilgisidir.
        /// </summary>
        /// <param name="heysKodu">HEYSK tipinde olmalıdır.</param>
        IUstveriV2XFluentHeysk HEYSKoduIle(HEYSK heysKodu);
        /// <summary>
        /// Belgenin doğrulama yapılacağı web adresi bilgisidir.
        /// </summary>
        /// <param name="dogrulamaBilgisi">Belgenin doğrulama yapılacağı web adresi bilgisi değeridir.</param>
        /// <remarks>
        /// Zorunlu alandır.
        /// </remarks>
        IUstveriV2XFluentDogrulamaBilgisi DogrulamaBilgisiAta(DogrulamaBilgisi dogrulamaBilgisi);
    }

    public interface IUstveriV2XFluentDogrulamaBilgisi
    {
        Ustveri Olustur();
    }
}
