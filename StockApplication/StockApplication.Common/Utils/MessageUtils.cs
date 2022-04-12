using StockApplication.Common.Messages;

namespace StockApplication.Common.Utils
{
    public static class MessageUtils
    {
        public static (string, string) GetStock(string message)
        {
            var stringArray = message.Split('=');
            if (stringArray[0].Contains(Commands.GetStock))
            {
                return (stringArray[0], stringArray[1]);
            }
            return (null, null);
        }
    }
}
