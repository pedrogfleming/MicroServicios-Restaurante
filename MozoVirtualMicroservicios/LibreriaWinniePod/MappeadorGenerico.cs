using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaWinniePod
{
    /// <summary>
    /// Maps any object to another when the type and the name of the property of origin match with any of destiny
    /// If a obj with different properties is mapping, doesn´t throw and exception,only ignores the property to map
    /// leaving the property with null or default value
    /// </summary>
    public class MappeadorGenerico
    {
        /// <summary>
        /// Use only with an entity with no inner list property
        /// Avoid with Order!!!
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="f"></param>
        /// <returns></returns>
        public static T Map<T>(object f) where T : class, new()
        {
            try
            {
                int TotalNotFoundProperties = 0;
                PropertyInfo[] fromProps = f.GetType().GetProperties();
                PropertyInfo[] toProps = typeof(T).GetProperties();

                var result = new T();
                foreach (var from in fromProps)
                {
                    var to = toProps.FirstOrDefault(x => x.Name == from.Name);
                    if (to == null)
                    {
                        TotalNotFoundProperties++;
                        continue;
                    }
                    var val = from.GetMethod.Invoke(f, null);
                    to.SetMethod.Invoke(result, new[] { val });

                }
                if(TotalNotFoundProperties > (fromProps.Length / 2+1)) 
                {
                    throw new IncompletedMapperException($"TotalNotFoundProperties: {TotalNotFoundProperties} ." +
                        $"Too many properties  set to default value(check the types passing between <T>)");
                }
                return result;
            }
            catch (TargetParameterCountException tparEx)
            {
                throw new IncompletedMapperException($"Error mapping multiple entities from {f.GetType().FullName}" +
                    $" to list {typeof(T).FullName}." +
                    $"The mapping return empty objects", tparEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error mapping from {f.GetType().Name} " +
                    $"to {typeof(T).Name}", ex);
            }

        }
        /// <summary>
        /// To use with classes that have an inner list to map
        /// </summary>
        /// <typeparam name="T">T is the type to convert the entity</typeparam>
        /// <typeparam name="U">U is the type from the inner list of the entity</typeparam>
        /// <typeparam name="V">V is the type to convert the inner list of the entity</typeparam>
        /// <param name="f"></param>
        /// <returns></returns>
        public static T MapEntityWithInnerList<T, U, V>(object f)
            where T : class, new()
            where U : class, new()
            where V : class, new()
        {
            try
            {
                var fromProps = f.GetType().GetProperties();
                var toProps = typeof(T).GetProperties();

                var result = new T();
                //List<V> innerResultListTo = new();
                foreach (var from in fromProps)
                {
                    var to = toProps.FirstOrDefault(x => x.Name == from.Name);
                    if (to == null)
                    {
                        continue;
                    }
                    if (to.PropertyType.FullName.Contains("System.Collections.Generic.List"))
                    {
                        #region no va
                        //var l = from.GetGetMethod();
                        ////Getting the inner list property type of my entity => List<product>
                        //var plistFrom = fromProps.FirstOrDefault(x => x.Name == from.Name);
                        ////Getting the inner list values of my entity, ex => products
                        //var innerListValuesFrom = plistFrom.GetValue(f);

                        //var plistTo = toProps.FirstOrDefault(x => x.Name == to.Name);
                        ////var innerListValuesTo = plistTo.
                        //var innerListTypeTo = plistTo.PropertyType;
                        //var innerListToProps = innerListTypeTo.GetProperties();
                        //List<U> innerListValuesTo = new List<U>();
                        //foreach (var item in innerListToProps)
                        //{
                        //    var innerListPropto = innerListToProps.FirstOrDefault(x => x.Name == item.Name);
                        //    if (innerListPropto == null)
                        //    {
                        //        continue;
                        //    }
                        //    var innerVal = item.GetMethod.Invoke(innerListValuesFrom, null);
                        //    to.SetMethod.Invoke(innerListValuesTo, new[] { innerVal });
                        //}
                        #endregion
                        //Getting the inner list property type of my entity => List<product>
                        var plistFrom = fromProps.FirstOrDefault(x => x.Name == from.Name);
                        //Getting the inner list values of my entity, ex => products
                        var innerListValuesFrom = plistFrom.GetValue(f);
                        var innerListValuesTo = MapEntities<V>((IEnumerable<object>)innerListValuesFrom);
                        //var val = from.GetMethod.Invoke(f, null);
                        to.SetMethod.Invoke(result, new[] { innerListValuesTo });
                    }
                    else
                    {

                        var val = from.GetMethod.Invoke(f, null);
                        to.SetMethod.Invoke(result, new[] { val });
                    }

                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error mapping from the {f.GetType().Name}" +
                    $" to {typeof(T).Name} " +
                    $"and from the inner list of {f.GetType().Name}," +
                    $"{typeof(U).Name} to {typeof(U).Name}", ex);
            }

        }

        public static List<T> MapEntities<T>(IEnumerable<object> f) where T : class, new()
        {
            try
            {
                var toProps = typeof(T).GetProperties();
                List<T> result = new();
                foreach (var item in f)
                {
                    var mappedObj = Map<T>(item);                    
                    foreach (var prop in toProps)
                    {
                        //Getting the inner list property type of my entity => List<product>
                        var plistFrom = toProps.FirstOrDefault(x => x.Name == prop.Name);
                        //Getting the inner list values of my entity, ex => products
                        //If goes wrong the mapping, here throws TargetParameterCountException
                        var innerListValuesFrom = plistFrom.GetValue(mappedObj);
                    }
                    result.Add(mappedObj);
                }
                return result;
            }
            catch (TargetParameterCountException tparEx)
            {
                throw new IncompletedMapperException($"Error mapping multiple entities from {f.GetType().FullName}" +
                    $" to list {typeof(T).FullName}." +
                    $"The mapping return empty objects", tparEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error mapping multiple entities from {f.GetType().FullName}" +
                    $" to list {typeof(T).FullName}", ex);
            }
        }
        public static T CreateEntityDTOWithError<T>(List<string> errors) where T : ErrorsHandler, new()
        {
            return new T
            {
                Errors = errors
            };
        }

        //No puedo usarlo por los dtos que heredan de ErrorHandler y tienen siempre 1 propiedad extra
        //private static bool PropertiesMatch(PropertyInfo[] aProps,PropertyInfo[] bProps)
        //{
        //    try
        //    {
        //        if (aProps.Length != bProps.Length) { throw new Exception("Differs the total number of properties of each mapping object"); }
        //        foreach (PropertyInfo a in aProps)
        //        {
        //            bProps.Single(x => x.Name == a.Name && x.PropertyType == a.PropertyType);
        //        }
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new PropertiesDontMatchException("Properties not match in type or name", ex);
        //    }
        //}
    }
}
