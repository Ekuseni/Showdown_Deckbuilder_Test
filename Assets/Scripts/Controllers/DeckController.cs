using Controllers.DeckItem;
using Services.Serialization.Deck;
using UnityEngine;
using Views.Deck;

namespace Controllers.Deck
{
    using Models;

    public class DeckController
    {
        private readonly Deck m_model;
        private readonly DeckView m_view;
        
        public DeckController(Deck model, DeckView view)
        {
            m_model = model;
            m_view = view;
            m_view.AddListener(this);
            UpdateDeckItems();
            m_view.UpdateView(m_model);
        }
        
        public void OnDeckNameInputFieldEndEdit(string newName)
        {
            Debug.Log($"RequestingNameChange: {newName}");
            m_model.Name = newName;
        }

        public void OnNewDeckButtonClicked()
        {
            m_model.GenerateDeck();
            UpdateDeckItems();
            m_view.UpdateView(m_model);
            
        }

        public void OnSaveDeckButtonClicked()
        {
            m_model.SaveDeck();
        }

        public void OnLoadDeckButtonClicked()
        {
            m_model.LoadDeck();
            UpdateDeckItems();
            m_view.UpdateView(m_model);
        }

        private void UpdateDeckItems()
        {
            for(int i = 0; i < m_model.Cards.Length; i++)
            {
                var card = m_model.Cards[i];
                new DeckItemController(card, m_view.CreateDeckItemView(i));
            }
        }
    }
}