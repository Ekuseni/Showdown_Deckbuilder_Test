using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Services.Providers.Deck;
using UnityEngine;
using Zenject;

namespace Services.Serialization.Deck
{
    using Models;

    public class JSONDeckSerializer : IDeckSerializationService
    {
        //TODO: use a more robust serialization library like Newtonsoft.Json
        //TODO: as well as a proper loading/saving path prompt for the user

        private const string FileName = "deck.json";
        private string FilePath => $"{Application.persistentDataPath}/{FileName}";

        private IDeckItemValidationService m_validator;
        private JSONContractResolver m_resolver;
        public JSONDeckSerializer(IDeckItemValidationService validator, JSONContractResolver resolver)
        {
            m_validator = validator;
            m_resolver = resolver;
        }

        //And newtonsoft works just perfect as always
        public void Serialize(Deck deck)
        {
            File.WriteAllText(FilePath, JsonConvert.SerializeObject(deck, Formatting.Indented));
        }

        public Deck Deserialize()
        {
            try
            {
                return JsonConvert.DeserializeObject<Deck>(File.ReadAllText(FilePath), new JsonSerializerSettings
                {
                    ContractResolver = m_resolver
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }

    public class JSONContractResolver : DefaultContractResolver
    {
        private readonly DiContainer m_container;

        public JSONContractResolver(DiContainer container)
        {
            m_container = container;
        }

        protected override JsonObjectContract CreateObjectContract(Type objectType)
        {
            if (m_container.HasBinding(objectType))
            {
                JsonObjectContract contract = base.CreateObjectContract(objectType);
                contract.DefaultCreator = () => m_container.Instantiate(objectType);

                return contract;
            }

            throw new ArgumentException("No binding found for type " + objectType);
        }
    }
}