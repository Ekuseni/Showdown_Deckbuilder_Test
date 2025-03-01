using Controllers.Deck;
using Services.Providers.Deck;
using Services.Serialization.Deck;
using UnityEngine;
using Views.Deck;

namespace Game.Installers
{
    public class SimpleMainInstaller : MonoBehaviour
    {
        //TODO: use some kind of dependency injection framework like Zenject
        [SerializeField] private string m_deckProviderSeed = "1234";
        [SerializeField] private int m_deckSize = 6;
        [SerializeField] private int m_minCardCount = 0;
        [SerializeField] private int m_maxCardCount = 3;
        
        [SerializeField] private DeckView m_deckView;
        
        private IDeckProviderService m_deckProvider;
        public IDeckItemValidationService m_validator;
        
        void Start()
        {
            m_deckProvider = new TestDeckProvider(m_deckProviderSeed.GetHashCode(), m_deckSize, m_minCardCount, m_maxCardCount);
            m_validator = m_deckProvider as IDeckItemValidationService;
            //Maybe I got too used to Zenject vut without it stuff tends to get messy IMO
            var deck = new Models.Deck(m_deckProvider, new JSONDeckSerializer(m_validator));
            
            new DeckController(deck, m_deckView);
        }
    }
}