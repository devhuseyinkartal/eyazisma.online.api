# eyazisma.online.api
## Genel Bakış

Türkiye Cumhuriyeti Cumhurbaşkanlığı Dijital Dönüşüm Ofisi tarafından yürütülmekte olan **e-Yazışma Projesi** (https://cbddo.gov.tr/projeler/e-yazisma) kapsamında yayınlanan,

* E-Yazışma Teknik Rehberi Sürüm 1.2 
* E-Yazışma Teknik Rehberi Sürüm 1.3
* E-Yazışma Teknik Rehberi Sürüm 2.0 

sürümlerinde belirtilen kriterlere uygun şekilde e-yazışma paketi oluşturmak, güncellemek ve okumak için **.NET teknolojileri** kullanılarak hazırlanmış bir API (uygulama programlama arayüzü) dir.

## Derlemeler

**eyazisma.online.api** aşağıda belirtilen derlemelere sahiptir.

|Derleme| Teknoloji|
|--|--|
| eyazisma.online.api.framework | .NET Framework 4.6 |
| eyazisma.online.api.standard | .NET Standard 2.0 |
| eyazisma.online.api.net5 | .NET 5.0 |

## Tasarım Modeli

**eyazisma.online.api** yazılım geliştiricilerin E-Yazışma Teknik Rehberi kurallarını derinlemesine bilmelerine gerek kalmadan, e-yazışma paketi ile ilgili işlemleri gerçekleştirebilmelerini sağlamak amacıyla Akıcı Arayüz Tasarım Modeli (Fluent Interface Design Pattern) kullanılarak hazırlanmıştır.

## Kullanım

**eyazisma.online.api** e-yazışma paketi işlemlerinin yapılabilmesini sağlamak amacıyla paket sürüm bilgisine dayalı farklı seçenekler sunmaktadır:
* Eğer işlem yapılacak paketin sürüm bilgisi biliniyor ise ilgili sürüm grubuna ilişkin ```PaketV1X``` ve ```PaketV2X``` şeklinde direkt kullanım imkanı bulunmaktadır. 
* Eğer sürüm bilgisi bilinmiyor ise ```Paket ``` şeklinde kullanım sunulmaktadır.

#### Örnek (Paket Sürümü Bilindiği Durum)
```c#
PaketV1X.Oku("[Paketin bulunduğu dosya yolu]")
        .BilesenleriAl((kritikDogrulamaHatasiVarMi, bilesenler, tumDogrulamaHatalari) => 
        {
	        if (!kritikDogrulamaHatasiVarMi)
	        {
	            var konu = bilesenler.Ustveri.Konu.Deger;
	            var imza = bilesenler.ImzaAl();
	        }
	        else
	        {
	            throw new ApplicationException(tumDogrulamaHatalari.Count + " adet kritik hata bulunmuştur.");
	        }
       })
       .Kapat();
```
#### Örnek (Paket Sürümü Bilinmediği Durum)
```c#
Paket.Oku("[Paketin bulunduğu dosya yolu]")
     .Versiyon1XIse((kritikDogrulamaHatasiVarMi, bilesenler, tumDogrulamaHatalari) =>
     {
         if (!kritikDogrulamaHatasiVarMi)
         {
             var konu = bilesenler.Ustveri.Konu.Deger;
             var imza = bilesenler.ImzaAl();
         }
         else
         {
             throw new ApplicationException(tumDogrulamaHatalari.Count + " adet kritik hata bulunmuştur.");
         }
     })
     .Versiyon2XIse((kritikDogrulamaHatasiVarMi, bilesenler, tumDogrulamaHatalari) => {
         if (!kritikDogrulamaHatasiVarMi)
         {
             var konu = bilesenler.Ustveri.Konu.Deger;
             var imza = bilesenler.ImzaAl();
         }
         else
         {
             throw new ApplicationException(tumDogrulamaHatalari.Count + " adet kritik hata bulunmuştur.");
         }
     });
```
### Bileşen Klavuzları
E-Yazışma Paketi üstyazı, üstveri, ek vb. bileşenlerden oluşmaktadır. Her bir bileşene ait detaylı açıklamalar E-Yazışma Teknik Rehberlerlerinde (https://cbddo.gov.tr/projeler/e-yazisma ulaşılabilir) verilmiştir. Yazılım geliştiricilerin bileşenleri doğru şekilde oluşturabilmesi için söz konusu bileşenleri ve detaylarını derinlemesine bilmeleri gerekmektedir. 

Örneğin bir e-yazışma paketi, Dahili Elektronik Dosya, Fiziksel ve Harici Referans olmak üzere 3 farklı ek bileşeni tanımlamasına sahip olabilir. E-Yazışma Teknik Rehberinde bu üç farklı tipe karşılık referans olan eleman ***CT_Ek*** dir. Bu sebepten *fiziksel* tipinde bir eleman oluştururken, *dahili elektronik dosya* tipine ait elaman alanlarının kullanılmaması gerekmektedir.

Ayrıca, E-Yazışma Teknik Rehberine uygun şekilde hazırlanan mevcut apiler kullanılırken yazılım geliştirme esnasında hangi eleman alanlarının zorunlu, hangilerinin opsiyonel olduğu bilinememektedir. 

Bu durumların iyileştirilmesi amacıyla **eyazisma.online.api** de bileşen kılavuzları oluşturulmuştur. Bileşen kılavuzları ilgili elemanın ```[Class].Kilavuz``` şeklinde ulaşılabilir. Kılavuzlar üzerinden ilerlendiğinde ***Ata*** kelimesi ile biten alanlar zorunlu, ***Ile*** ile kelimesi ile biten alanlar opsiyoneldir.

#### Örnek (Dağıtım Elemanı Oluşturulması)
```c#
Dagitim.Kilavuz
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
       .IvedilikTuruAta(IvedilikTuru.NRM)
       .DagitimTuruAta(DagitimTuru.GRG)
       .Olustur();
```
## Uyuşmazlıklar
**eyazisma.online.api** e-Yazışma Projesi kapsamında hazırlanan E-Yazışma Teknik Rehberleri üzerinde yapılan detaylı incelemeler ışığında hazırlanmıştır. eyazisma.online.api ile işlem yapılan e-yazışma paketlerinin, e-Yazışma Projesi kapsamında hazırlanan apiler ile uyuşmazlık gösterdiği durumlarda (örneğin eyazisma.online.api ile hazırlanan bir paketin e-Yazışma Projesi kapsamında hazırlanan apiler ile açılamaması vb.) öncelikle ilgili paket sürümüne ilişkin E-Yazışma Teknik Rehberi kurallarına bakılmalıdır. Bu kurallar ışığında eğer eyazisma.online.api de bulunan bir eksiklik söz konusu ise lütfen en kısa sürede **Issues** bölümünden konuyu bildiriniz. Şayet e-Yazışma Projesi kapsamında hazırlanan apilerde sorun olduğunu düşünüyorsanız **e-Yazışma Projesi** sitesinde bulunan iletişim kanalları üzerinden sorununuzu bildiriniz.

## Lisans

eyazisma.online.api **Apache Lisansı, Sürüm 2.0** koşullarına riayet ederek kullanılabilir.
