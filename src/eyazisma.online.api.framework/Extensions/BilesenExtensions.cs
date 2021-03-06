﻿using System.Collections.Generic;
using System.Linq;
using eyazisma.online.api.Classes;

namespace eyazisma.online.api.Extensions
{
    public static class BilesenExtensions
    {
        public static Hedef ToHedef(this Dagitim dagitim)
        {
            if (dagitim == null)
                return null;

            if (dagitim.Oge is GercekSahis)
                return Hedef.Kilavuz.OgeAta((GercekSahis) dagitim.Oge).Olustur();
            if (dagitim.Oge is TuzelSahis)
                return Hedef.Kilavuz.OgeAta((TuzelSahis) dagitim.Oge).Olustur();
            if (dagitim.Oge is KurumKurulus)
                return Hedef.Kilavuz.OgeAta((KurumKurulus) dagitim.Oge).Olustur();
            return null;
        }

        public static List<Hedef> ToHedefler(this List<Dagitim> dagitimlar)
        {
            if (dagitimlar == null || dagitimlar.Count == 0)
                return null;

            return dagitimlar.Select(d => d.ToHedef()).ToList();
        }
    }
}