namespace Securitybot
{
    internal class ActivityLogEntry
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string Summary { get; set; } = string.Empty;

        public string GetDisplayText()
        {
            return $"{CreatedAt:HH:mm} - {Summary}";
        }
    }
}
