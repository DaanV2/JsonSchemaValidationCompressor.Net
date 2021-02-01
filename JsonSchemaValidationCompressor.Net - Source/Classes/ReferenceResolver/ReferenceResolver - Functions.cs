using System;

namespace DaanV2.Json {
    internal sealed partial class ReferenceResolver {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Reference"></param>
        /// <returns></returns>
        public String GetDefinitionsName(Uri Reference) {
            String Total = Reference.ToString();

            //if exists return it
            if (this._ReferenceConverter.TryGetValue(Total, out String Value)) {
                return Value;
            }

            //if not then create it
            UInt32 Number = (UInt32)this._ReferenceConverter.Count;

            String Name = CreateName(Number);
            this._ReferenceConverter[Total] = Name;
            return Name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Base"></param>
        /// <returns></returns>
        public static String CreateName(UInt32 Number) {
            String Temp = Number.ToString();
            Char[] Chars = new Char[Temp.Length];

            for (Int32 I = 0; I < Chars.Length; I++) {
                Chars[I] = (Char)((Temp[I] - '0') + 'A');
            }

            return new String(Chars);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public Boolean HasDefinition(String Name) {
            return this._Definitions.ContainsKey(Name);
        }
    }
}
