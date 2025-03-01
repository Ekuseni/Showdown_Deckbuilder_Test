namespace Services.Serialization.Deck
{
    using Models;

    public interface IDeckSerializationService
    {
        void Serialize(Deck deck);
        Deck Deserialize();
    }
}
