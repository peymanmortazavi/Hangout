using System;
using System.Collections.Generic;

namespace CAILMobility.DataAccess
{
   public class TypeMap
   {

      private static readonly Dictionary<string, TypeMap> TypeMapsDictionary;

        /// <summary>
        /// Initializes the <see cref="TypeMap"/> class.
        /// </summary>
        static TypeMap()
      {
         TypeMapsDictionary = new Dictionary<string, TypeMap>();
      }

        /// <summary>
        /// Determines whether [is type map registered] [the specified type].
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static bool IsTypeMapRegistered(Type type)
      {
         return TypeMapsDictionary.ContainsKey(type.FullName);
      }

        /// <summary>
        /// Fors the specified map.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="map">The map.</param>
        public static void For<T>(Action<TypeMap> map)
      {

         var newEntityMap = new TypeMap();

         map(newEntityMap);

         TypeMapsDictionary[typeof(T).FullName] = newEntityMap;

      }

        /// <summary>
        /// Gets the type map.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static TypeMap GetTypeMap(Type type)
      {
         return IsTypeMapRegistered(type) ? TypeMapsDictionary[type.FullName] : default(TypeMap);
      }

        /// <summary>
        /// Gets or sets the name of the collection.
        /// </summary>
        /// <value>
        /// The name of the collection.
        /// </value>
        public string CollectionName { get; set; }

   }

}
