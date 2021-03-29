using System;
using eyazisma.online.api.Interfaces.Fluents;

namespace eyazisma.online.api.Classes
{
    /// <summary>
    ///     Belgeye ilişkin Hizmet Envanter Yönetim Sistemi kodu bilgisidir.
    /// </summary>
    /// <remarks>Only for version 2.0</remarks>
    public sealed class HEYSK
    {
        public HEYSK()
        {
        }

        private HEYSK(int kod, string ad, string tanim)
        {
            Kod = kod;
            Ad = ad;
            Tanim = tanim;
        }

        /// <summary>
        ///     Hizmet Envanteri Yönetim Sisteminde tanımlanmış hizmetin kodudur.
        /// </summary>
        /// <remarks>Zorunlu alandır.</remarks>
        public int Kod { get; set; }

        /// <summary>
        ///     Hizmet Envanteri Yönetim Sisteminde tanımlanmış hizmetin adıdır.
        /// </summary>
        /// <remarks>Zorunlu alandır.</remarks>
        public string Ad { get; set; }

        /// <summary>
        ///     Hizmet Envanteri Yönetim Sisteminde yer alan hizmetin tanımıdır.
        /// </summary>
        public string Tanim { get; set; }

        public sealed class Kilavuz : IHEYSKFluent
        {
            private string _ad, _tanim;
            private readonly int _kod;

            public Kilavuz(int kod)
            {
                _kod = kod;
            }

            /// <summary>
            ///     Hizmet Envanteri Yönetim Sisteminde tanımlanmış hizmetin adıdır.
            /// </summary>
            /// <param name="ad">HEYS hizmet adı değeridir.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public IHEYSKFluentAd AdAta(string ad)
            {
                _ad = ad;
                return this;
            }

            /// <summary>
            ///     Hizmet Envanteri Yönetim Sisteminde yer alan hizmetin tanımıdır.
            /// </summary>
            /// <param name="tanim">HEYS hizmet tanımı değeridir.</param>
            public IHEYSKFluentTanim TanimIle(string tanim)
            {
                _tanim = tanim;
                return this;
            }

            public HEYSK Olustur()
            {
                return new HEYSK(_kod, _ad, _tanim);
            }

            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }

            /// <summary>
            ///     Hizmet Envanteri Yönetim Sisteminde tanımlanmış hizmetin kodudur.
            /// </summary>
            /// <param name="kod">HEYS hizmet kodu değeridir.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public static IHEYSKFluentKod KodAta(int kod)
            {
                return new Kilavuz(kod);
            }
        }
    }
}