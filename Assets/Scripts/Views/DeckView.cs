using Controllers.Deck;
using UnityEngine;
using UnityEngine.UI;

namespace Views.Deck
{
    public class DeckView : MonoBehaviour
    {
        [SerializeField] private Button m_newDeckButton;
        [SerializeField] private Button m_saveDeckButton;
        [SerializeField] private Button m_loadDeckButton;

        [SerializeField] private TMPro.TMP_InputField m_deckNameInputField;

        [SerializeField] private DeckItemView m_deckItemPrefab;
        [SerializeField] private Transform m_deckItemsParent;
        
        public void AddListener(DeckController controller)
        {
            //This is my personal preference, I just don't like to use Unity's event system
            //in my opinion it's easier to just pass a reference to a button and handle the click event in script
            //that way it's much easier to switch button to another one and not forget to add a listener in inspector
            //mostly because that way you get a runtime null reference exception
            m_newDeckButton.onClick.AddListener(controller.OnNewDeckButtonClicked);
            m_saveDeckButton.onClick.AddListener(controller.OnSaveDeckButtonClicked);
            m_loadDeckButton.onClick.AddListener(controller.OnLoadDeckButtonClicked);
            m_deckNameInputField.onEndEdit.AddListener(controller.OnDeckNameInputFieldEndEdit);
        }

        public void UpdateView(Models.Deck deck)
        {
            m_deckNameInputField.text = deck.Name;
        }
        
        private int m_lastSetIndex = 0;
        
        public DeckItemView CreateDeckItemView(int index)
        {
            m_lastSetIndex = index;
            
            if(m_deckItemsParent.childCount > index)
            {
                return m_deckItemsParent.GetChild(index).GetComponent<DeckItemView>();
            }
            else
            {
                return Instantiate(m_deckItemPrefab, m_deckItemsParent);
            }
        }
        
        public void RemoveRedundantDeckItemViews()
        {
            for (int i = m_deckItemsParent.childCount; i > m_lastSetIndex; i--)
            {
                Destroy(m_deckItemsParent.GetChild(i).gameObject);
            }
        }
    }
}