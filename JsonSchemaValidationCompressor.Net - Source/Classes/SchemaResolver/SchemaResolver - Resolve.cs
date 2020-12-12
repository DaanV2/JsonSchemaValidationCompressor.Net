using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;

namespace DaanV2.Json {
    public sealed partial class SchemaResolver {
        private readonly String _DefinitionsName = "definitions";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Doc"></param>
        public void Resolve(JObject Doc, String Filepath) {
            _ = this.InternalResolve(Doc, Filepath);
        }

        internal ReferenceResolver InternalResolve(JObject Doc, String Filepath, ReferenceResolver Parent = null) {
            var Resolver = new ReferenceResolver(Directory.GetParent(Filepath).FullName, Parent);

            //Check if definitions object exists, else add it
            if (Doc.ContainsKey(this._DefinitionsName)) {
                Resolver._Definitions = (JObject)Doc[this._DefinitionsName];
            }
            else {
                Resolver._Definitions = new JObject();
            }

            //Traverse all the elements looking for $ref, and resolve them
            this.Traverse(Doc, Resolver);

            //If larger then 0 make sure its present in the document
            if (Resolver._Definitions.Count > 0) {
                Doc[this._DefinitionsName] = Resolver._Definitions;
            }

            return Resolver;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Element"></param>
        /// <param name="Resolver"></param>
        private void Traverse(JObject Element, ReferenceResolver Resolver) {
            //Has a reference
            if (Element.ContainsKey("$ref")) {
                this.ResolveRef(Element, Resolver);
            }

            var Names = new List<String>(Element.Count);

            foreach (KeyValuePair<String, JToken> Child in Element) {
                Names.Add(Child.Key);
            }

            for (Int32 I = 0; I < Names.Count; I++) {
                JToken Item = Element[Names[I]];

                if (Item is JObject JChild) {
                    this.Traverse(JChild, Resolver);
                }
                else if (Item is JArray A) {
                    this.Traverse(A, Resolver);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Elements"></param>
        /// <param name="Resolver"></param>
        private void Traverse(JArray Elements, ReferenceResolver Resolver) {
            foreach (JToken Item in Elements) {
                if (Item is JObject JChild) {
                    this.Traverse(JChild, Resolver);
                }
                else if (Item is JArray A) {
                    this.Traverse(A, Resolver);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Element"></param>
        /// <param name="Resolver"></param>
        private void ResolveRef(JObject Element, ReferenceResolver Resolver) {
            String Reference;

            if (Element is JObject OElement) {
                Reference = OElement["$ref"].Value<String>();
            }
            else {
                Reference = Element["$ref"].Value<String>();
            }

            if (Reference[0] == '#') {
                return;
            }

            if (Reference.StartsWith("./")) {
                Reference = Reference[2..];
            }

            var uri = new Uri(Resolver._Basepath, Reference);

            //Is it is a file
            if (!uri.IsFile) {
                return;
            }

            String Name = Resolver.GetDefinitionsName(uri);

            if (!Resolver.HasDefinition(Name)) {
                JObject ReferenceData = File.Load(uri.AbsolutePath);

                ReferenceResolver Data = this.InternalResolve(ReferenceData, uri.AbsolutePath, Resolver);
                Resolver.Inherit(Name, ReferenceData, Data);
            }

            //Update reference
            Element["$ref"] = "#/definitions/" + Name;
        }
    }
}
