namespace Services.Providers.Deck
{
    using Models;

    public interface IDeckProviderService
    {
        (string name, DeckItem[] deckItems) GetDeck();
    }
}