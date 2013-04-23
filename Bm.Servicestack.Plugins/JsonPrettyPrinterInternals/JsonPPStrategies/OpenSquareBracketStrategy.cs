namespace Bm.Servicestack.Plugins.JsonPrettyPrinterInternals.JsonPPStrategies
{
    public class OpenSquareBracketStrategy : ICharacterStrategy
    {
        public void ExecutePrintyPrint(JsonPPStrategyContext context)
        {
            context.AppendCurrentChar();

            if (context.IsProcessingString) return;

            context.EnterArrayScope();
            context.BuildContextIndents();
        }

        public char ForWhichCharacter
        {
            get { return '['; }
        }
    }
}
