using System;
using Newtonsoft.Json.Linq;

namespace JsonSchemaValidationCompressor.Net {
    ///DOLATER <summary>Add description for struct: JsonCompressorContext</summary>
    internal partial struct JsonCompressorContext {

        /// <summary></summary>
        internal String _SourceFilepath;

        /// <summary></summary>
        internal String _BaseFolder;

        /// <summary></summary>
        internal String _DestinationFilepath;

        /// <summary></summary>
        internal JObject _Definitions;

        /// <summary>Creates a new instance of <see cref="JsonCompressorContext"/></summary>
        /// <param name="sourceFilepath"></param>
        /// <param name="baseFolder"></param>
        /// <param name="destinationFilepath"></param>
        /// <param name="definitions"></param>
        public JsonCompressorContext(String sourceFilepath, String baseFolder, String destinationFilepath, JObject definitions) {
            this._SourceFilepath = sourceFilepath;
            this._BaseFolder = baseFolder;
            this._DestinationFilepath = destinationFilepath;
            this._Definitions = definitions;
        }
    }
}
