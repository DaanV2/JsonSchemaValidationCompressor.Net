using System.Collections.Generic;

namespace DaanV2.Json {
    ///DOLATER <summary>add description for class: DictionaryExtension</summary>
    public static partial class DictionaryExtension {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="data"></param>
        /// <param name="value"></param>
        public static void RemoveValue<T, U>(this Dictionary<T, U> data, U value) {
            var Names = new List<T>(10);

            foreach (KeyValuePair<T, U> Item in data) {
                if (Item.Value != null && Item.Value.Equals(value)) {
                    Names.Add(Item.Key);
                }
            }

            foreach (T Item in Names) {
                data.Remove(Item);
            }
        }
    }
}
