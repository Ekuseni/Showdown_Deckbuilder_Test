using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class DeckItemView : MonoBehaviour
    {
        [SerializeField] private TMPro.TMP_Text m_cardIDText;
        [SerializeField] private TMPro.TMP_Text m_cardCountText;
        [SerializeField] private Button m_incrementButton;
        [SerializeField] private Button m_decrementButton;

        public void AddListener(Controllers.DeckItem.DeckItemController controller)
        {
            m_incrementButton.onClick.AddListener(controller.OnIncrementButtonClicked);
            m_decrementButton.onClick.AddListener(controller.OnDecrementButtonClicked);
        }

        public void UpdateView(Models.DeckItem model)
        {
            m_cardIDText.text = model.Id;
            m_cardCountText.text = model.Count.ToString();
        }
    }
}


