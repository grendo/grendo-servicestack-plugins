namespace Bm.Servicestack.Plugins.JsonPrettyPrinterInternals.JsonPPStrategies
{
    public class OpenBracketStrategy : ICharacterStrategy
    {
        public void ExecutePrintyPrint(JsonPPStrategyContext context)
        {
            if (context.IsProcessingString)
            {
                context.AppendCurrentChar();
                return;
            }

            context.AppendCurrentChar();
            context.EnterObjectScope();

            if (!IsBeginningOfNewLineAndIndentionLevel(context)) return;

            context.BuildContextIndents();
        }

        private static bool IsBeginningOfNewLineAndIndentionLevel(JsonPPStrategyContext context)
        {
            return context.IsProcessingVariableAssignment || (!context.IsStart && !context.IsInArrayScope);
        }

        public char ForWhichCharacter
        {
            get { return '{'; }
        }
    }
}
