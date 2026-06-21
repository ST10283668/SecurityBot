namespace Securitybot
{
    internal class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime ReminderDate { get; set; }
        public string Category { get; set; } = string.Empty;
        public bool IsComplete { get; set; }

        public string GetDisplayText()
        {
            string status = IsComplete ? "Complete" : "Pending";
            return $"{status} | {Category} | {Title} | Reminder: {ReminderDate:dd MMM yyyy HH:mm}";
        }
    }
}
