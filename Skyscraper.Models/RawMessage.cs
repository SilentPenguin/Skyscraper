namespace Skyscraper.Models
{
    public class RawMessage
    {
        public string Message { get; private set; }
        public RawMessageDirection Direction { get; private set; }

        public RawMessage(string message, RawMessageDirection direction)
        {
            this.Message = message;
            this.Direction = direction;
        }
    }
}