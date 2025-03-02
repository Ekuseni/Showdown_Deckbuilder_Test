namespace Game.Installers
{
    using Controllers.Deck;
    using Models;
    using Services.Providers.Deck;
    using Services.Serialization.Deck;
    using UnityEngine;
    using Views.Deck;
    using Zenject;

    public class DeckInstaller : MonoInstaller
    {
        [SerializeField] private DeckView m_deckView;
        [SerializeField] private string m_deckProviderSeed = "1234";
        [SerializeField] private int m_deckSize = 6;
        [SerializeField] private int m_minCardCount = 0;
        [SerializeField] private int m_maxCardCount = 3;

        public override void InstallBindings()
        {
            var testDeckProvider = new TestDeckProvider(m_deckProviderSeed.GetHashCode(), m_deckSize, m_minCardCount, m_maxCardCount);
            Container.BindInterfacesAndSelfTo<TestDeckProvider>().FromInstance(testDeckProvider).AsSingle();
            Container.Bind<DeckView>().FromInstance(m_deckView).AsSingle();
            Container.Bind<DeckController>().AsSingle();
            Container.Bind<Deck>().FromNew().AsTransient();
            Container.Bind<DeckItem>().FromNew().AsTransient();
            Container.BindInterfacesAndSelfTo<JSONContractResolver>().AsSingle();
            Container.BindInterfacesAndSelfTo<JSONDeckSerializer>().AsSingle();
            Container.Resolve(typeof(DeckController));
        }
    }
}