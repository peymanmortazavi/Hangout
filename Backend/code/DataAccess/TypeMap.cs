using System;
using System.Collections.Generic;

namespace CAILMobility.DataAccess
{
   public class TypeMap
   {

      private static readonly Dictionary<string, TypeMap> TypeMapsDictionary;

      static TypeMap()
      {
         TypeMapsDictionary = new Dictionary<string, TypeMap>();
      }

      public static bool IsTypeMapRegistered(Type type)
      {
         return TypeMapsDictionary.ContainsKey(type.FullName);
      }

      public static void For<T>(Action<TypeMap> map)
      {

         var newEntityMap = new TypeMap();

         map(newEntityMap);

         TypeMapsDictionary[typeof(T).FullName] = newEntityMap;

      }

      public static TypeMap GetTypeMap(Type type)
      {
         return IsTypeMapRegistered(type) ? TypeMapsDictionary[type.FullName] : default(TypeMap);
      }

      public string CollectionName { get; set; }

   }

}
