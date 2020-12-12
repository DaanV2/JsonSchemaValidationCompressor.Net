using System;

namespace DaanV2.Json.Specification {
    public sealed partial class CompressingSpecification {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="BaseFolder"></param>
        public void Resolve(String BaseFolder) {
            foreach (FileSpecification Item in this.Files) {
                Item.Resolve(BaseFolder);
            }
        }
     }
}
