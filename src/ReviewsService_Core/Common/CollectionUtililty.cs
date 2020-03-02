using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;

namespace ReviewsService_Core.Common
{
    public static class CollectionUtililty
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> list)
        {
            if (list == null)
            {
                return true;
            }

            if (!list.Any())
            {
                return true;
            }

            return false;
        }

        public static object ShapeList<TSource>(this IList<TSource> obj, string fields)
        {
            List<string> lstOfFields = new List<string>();
            if (string.IsNullOrEmpty(fields))
            {
                return obj;
            }
            lstOfFields = fields.Split(',').ToList();
            List<string> lstOfFieldsToWorkWith = new List<string>(lstOfFields);

            List<System.Dynamic.ExpandoObject> lsobjectToReturn = new List<System.Dynamic.ExpandoObject>();
            if (!lstOfFieldsToWorkWith.Any())
            {
                return obj;
            }
            else
            {



                foreach (var kj in obj)
                {

                    System.Dynamic.ExpandoObject objectToReturn = new System.Dynamic.ExpandoObject();

                    foreach (var field in lstOfFieldsToWorkWith)
                    {
                        try
                        {
                            var fieldValue = kj.GetType()
                            .GetProperty(field.Trim(), BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)
                            .GetValue(kj, null);
                            ((IDictionary<String, Object>)objectToReturn).Add(field.Trim(), fieldValue);
                        }
                        catch
                        {
                        }

                    }

                    lsobjectToReturn.Add(objectToReturn);
                }
            }
            var ls = new List<System.Text.Json.JsonElement>();
            foreach (var b in lsobjectToReturn)
            {
                var v = System.Text.Json.JsonDocument.Parse(SerializeUtility.SerializeJSON(b));
                ls.Add(v.RootElement);
            }

            return ls;
        }


        public static IQueryable<T> ApplySort<T>(this IQueryable<T> source, string sort)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (string.IsNullOrEmpty(sort))
            {
                return source;
            }

            // split the sort string
            var lstSort = sort.Split(',');



            //run through the sorting options and apply them - in reverse
            //order, otherwise results will come out sorted by the last
            // item in the string first!
            foreach (var sortOption in lstSort.Reverse())
            {
                // if the sort option starts with "-", we order
                // descending, ortherwise ascending



                if (sortOption.StartsWith("-"))
                {
                    source = source.OrderBy(sortOption.Remove(0, 1) + " descending");
                }
                else
                {
                    source = source.OrderBy(sortOption);
                }



            }



            return source;
        }
    }
}
