using System;

namespace Services.Providers.Deck
{
    using Models;

    public class TestDeckProvider : IDeckProviderService, IDeckItemValidationService
    {
        private const string DefaultName = "Deck";

        private readonly Random m_random;
        private readonly int m_size;
        private readonly int m_min;
        private readonly int m_max;

        public TestDeckProvider(int seed, int size, int min, int max)
        {
            //Again my preference, System.Random > Unity.Random because of the ability to set seed
            //and get reproducible results
            m_random = new Random(seed);
            m_size = size;
            m_min = min;
            m_max = max;
        }
        
        
        public (string name, DeckItem[] deckItems) GetDeck()
        {
            var cards = new DeckItem[m_size];
            
            for (int i = 0; i < m_size; i++)
            {
                cards[i] = new DeckItem(Guid.NewGuid().ToString(), m_random.Next(m_min, m_max), this);
            }
            
            return (DefaultName, cards);
        }
        
        
        public bool ValidateDeckItem(DeckItem item)
        {
            return item.Count >= m_min && item.Count <= m_max;
        }
    }
}