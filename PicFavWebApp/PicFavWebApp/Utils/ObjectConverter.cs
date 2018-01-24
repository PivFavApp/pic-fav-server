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
    }
}