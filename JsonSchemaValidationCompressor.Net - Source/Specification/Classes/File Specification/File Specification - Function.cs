using System;
using System.IO;

namespace DaanV2.Json.Specification {
    public sealed partial class FileSpecification {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="BaseFolder"></param>
        public void Resolve(String BaseFolder) {
            if (!Path.IsPathRooted(this.Destination)) {
                this.Destination = Path.GetFullPath(this.Destination, BaseFolder);
            }

            if (!Path.IsPathRooted(this.Source)) {
                this.Source = Path.GetFullPath(this.Source, BaseFolder);
            }
        }
    }
}
