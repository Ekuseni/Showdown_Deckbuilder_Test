namespace Services.Providers.Deck
{
    using Models;

    public interface IDeckItemValidationService
    {
       public bool ValidateDeckItem(DeckItem item);
    }
}
