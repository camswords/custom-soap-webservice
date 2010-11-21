using System.Collections.Generic;
using System;
using System.Reflection;
using System.IO;
using Antlr3.ST;

namespace SoapWebservices.Text
{
    public class Template
    {
        private readonly StringTemplate template;

        public Template(string name)
        {
            var assembly = Assembly.GetCallingAssembly();
            var stream = assembly.GetManifestResourceStream(name);

            if (stream == null)
            {
                var candidates = string.Join(", ", assembly.GetManifestResourceNames()); 
                throw new FileNotFoundException(string.Format("cannot find embedded resource {0} in assembly {1}, candidate templates include [{2}]", name, assembly.GetName(), candidates));
            }

            var contents = new StreamReader(stream).ReadToEnd();
            template = new StringTemplate(contents);
        }

        public string Render(IDictionary<string, object> parameters)
        {
            foreach (var propertyName in parameters.Keys)
            {
                template.SetAttribute(propertyName, parameters[propertyName]);
            }
            return template.ToString();
        }
    }
}
