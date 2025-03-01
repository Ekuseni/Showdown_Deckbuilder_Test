using System;
using System.IO;
using Services.Providers.Deck;
using UnityEngine;

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
        public JSONDeckSerializer(IDeckItemValidationService validator)
        {
            m_validator = validator;
        }
        
        //AND I remembered why no one uses Unity's JsonUtility...
        //It's because it's awful
        //Like it can't serialize/deserialize properties, only fields
        
        [Serializable]
        private class SerializableDeck
        {
            public string Name;
            public SerializableDeckItem[] Cards;
        }
        
        [Serializable]
        private class SerializableDeckItem
        {
            public string Name;
            public int Count;
        }
        
        public void Serialize(Deck deck)
        {
            var serializableDeck = new SerializableDeck
            {
                Name = deck.Name,
                Cards = new SerializableDeckItem[deck.Cards.Length]
            };
            
            for (int i = 0; i < deck.Cards.Length; i++)
            {
                var card = deck.Cards[i];
                serializableDeck.Cards[i] = new SerializableDeckItem
                {
                    Name = card.Id,
                    Count = card.Count
                };
            }
            
            File.WriteAllText(FilePath, JsonUtility.ToJson(serializableDeck));
        }

        public Deck Deserialize()
        {
            try
            {
                var json = File.ReadAllText(FilePath);
                var serializableDeck = JsonUtility.FromJson<SerializableDeck>(json);
                var cards = new DeckItem[serializableDeck.Cards.Length];
                
                for (int i = 0; i < serializableDeck.Cards.Length; i++)
                {
                    var card = serializableDeck.Cards[i];
                    cards[i] = new DeckItem(card.Name, card.Count, m_validator);
                }
                
                return new Deck(serializableDeck.Name, cards);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}



