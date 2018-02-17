using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PicFavWebApp.Utils
{
    public class ObjectConverter
    {
        public static List<T> ModelsToDtos<T,U>(List<U> models)
        {
            try
            {
                List<T> dtos = new List<T>();
                foreach (var model in models)
                {
                    dtos.Add((T) Activator.CreateInstance(typeof(T), model));
                }
                return dtos;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public static T ModelToDto<T, U>(U model)
        {
            try
            {
                return (T)Activator.CreateInstance(typeof(T), model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return default(T);
            }
        }

        public static long GetUnixDate(DateTime date)
        {
            return (long)(date - new DateTime(1970, 1, 1)).TotalSeconds;
        }

        public static DateTime GetDateFromUnix(long longdate)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(longdate).ToLocalTime();
            return dtDateTime;
        }
    }
}