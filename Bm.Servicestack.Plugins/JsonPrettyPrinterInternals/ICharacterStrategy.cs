namespace Bm.Servicestack.Plugins.JsonPrettyPrinterInternals
{
    public interface ICharacterStrategy
    {
        void ExecutePrintyPrint(JsonPPStrategyContext context);
        char ForWhichCharacter { get; }
    }
}
