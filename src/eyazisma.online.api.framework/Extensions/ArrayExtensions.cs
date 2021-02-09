using System;
using System.Linq;

namespace eyazisma.online.api.framework.Extensions
{
    internal static class ArrayExtensions
    {
        public static T[] Add<T>(this T[] array, T arrayItem)
        {
            if (arrayItem == null)
                throw new ArgumentNullException(nameof(arrayItem));

            if (array == null)
            {
                array = new T[1];
                array[0] = arrayItem;
            }
            else
                array = array.Concat(new T[] { arrayItem }).ToArray();

            return array;
        }

        public static T[] Remove<T>(this T[] array, T arrayItem)
        {
            if (arrayItem == null)
                throw new ArgumentNullException(nameof(arrayItem));

            if (array != null && array.Any(p => p.Equals(arrayItem)))
            {
                if (array.Length == 1)
                    array = null;
                else
                    array = Array.FindAll(array, p => !p.Equals(arrayItem)).ToArray();
            }

            return array;
        }
    }
}
