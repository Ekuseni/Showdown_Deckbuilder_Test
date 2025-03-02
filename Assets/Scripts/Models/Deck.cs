using System.Linq;
using Services.Providers.Deck;
using Services.Serialization.Deck;

namespace Models
{
    public class Deck
    {
        public DeckItem[] Cards { get; set; }
        public string Name { get; set; }
        
        private readonly IDeckProviderService m_deckProvider;
        
        private readonly IDeckSerializationService m_serializationService;
        
        //Wasn't sure if this should be in a model but in task description it was mentioned that I should add a dummy deck provider,
        //and it was not stated that I can't extend the model with additional functionality,
        //and canonical way is to link model with external services such as data providers I decided to add this constructor
        public Deck(IDeckProviderService provider, IDeckSerializationService serializationService)
        {
            m_deckProvider = provider;
            m_serializationService = serializationService;
            (Name, Cards) = m_deckProvider.GetDeck();
        }
        
        // public Deck(string name, DeckItem[] cards)
        // {
        //     Name = name;
        //     Cards = cards;
        // }
        
        public override string ToString()
        {
            return $"{Name} ({Cards.Sum(c => c.Count)})";
        }
        
        public void GenerateDeck()
        {
            (Name, Cards) = m_deckProvider.GetDeck();
        }
        
        public void SaveDeck()
        {
            m_serializationService.Serialize(this);
        }
        
        public void LoadDeck()
        {
            var deck = m_serializationService.Deserialize();
            Name = deck.Name;
            Cards = deck.Cards;
        }
    }
}