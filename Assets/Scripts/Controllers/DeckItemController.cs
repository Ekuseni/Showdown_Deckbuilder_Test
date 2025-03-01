using Views;

namespace Controllers.DeckItem
{
    using Models;

    public class DeckItemController
    {
        private readonly DeckItem m_model;
        private readonly DeckItemView m_view;

        public DeckItemController(DeckItem model, DeckItemView view)
        {
            m_model = model;
            m_view = view;
            m_view.AddListener(this);
            m_view.UpdateView(m_model);
        }

        public void OnIncrementButtonClicked()
        {
            m_model.SetCount(m_model.Count + 1);
            m_view.UpdateView(m_model);
        }
        
        public void OnDecrementButtonClicked()
        {
            m_model.SetCount(m_model.Count - 1);
            m_view.UpdateView(m_model);
        }
    }
}