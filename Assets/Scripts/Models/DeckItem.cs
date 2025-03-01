using System;
using Services.Providers.Deck;

namespace Models
{
    public class DeckItem
    {
        public string Id { get; set; }
        public int Count { get; set; }

        //Was thinking abut keeping model as clean as possible, but since I was asked to add dummy deck provider service
        //I decided to add validation service to the model as well
        
        private readonly IDeckItemValidationService m_validationService;
        public DeckItem(string id, int count, IDeckItemValidationService validationService)
        {
            Id = id;
            Count = count;
            
            m_validationService = validationService;
            
            if (!m_validationService.ValidateDeckItem(this))
            {
                throw new ArgumentException("Invalid deck item");
            }
        }
        
        public void SetCount(int count)
        {
            var lastCount = Count;
            
            Count = count;
            
            
            if (!m_validationService.ValidateDeckItem(this))
            {
                Count = lastCount;
            }
        }
        
        public override string ToString()
        {
            return $"{Id} x{Count}";
        }
    }
}