using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace Arcus.EventGrid.Proxy.Tests.Unit.Stubs
{
    public class ConfigurationStub : IConfiguration
    {
        private readonly string _key;
        private readonly string _valueToReturn;

        public ConfigurationStub(string key, string valueToReturn)
        {
            _key = key;
            _valueToReturn = valueToReturn;
        }

        public ConfigurationStub()
        {
        }

        public IConfigurationSection GetSection(string key)
        {
            return null;
        }

        public IEnumerable<IConfigurationSection> GetChildren()
        {
            yield break;
        }

        public IChangeToken GetReloadToken()
        {
            return null;
        }

        public string this[string key]
        {
            get => _key == key ? _valueToReturn : null;
            set { }
        }
    }
}