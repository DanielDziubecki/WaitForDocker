﻿using Newtonsoft.Json;
using YamlDotNet.Serialization;

namespace WaitForDocker.ComposeProcessing
{
    internal class JsonComposeConverter : IComposeConverter<string>
    {
        public string Convert(string yamlCompose)
        {
            var yamlObject = new Deserializer().Deserialize<dynamic>(yamlCompose);
            var json = JsonConvert.SerializeObject(yamlObject);
            return json;
        }
    }
}