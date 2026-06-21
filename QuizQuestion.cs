namespace Securitybot
{
    internal class QuizQuestion
    {
        public string Question { get; set; } = string.Empty;
        public List<string> Options { get; set; } = new List<string>();
        public int CorrectIndex { get; set; }
        public string Explanation { get; set; } = string.Empty;

        public string GetOptionText(int index)
        {
            string letter = index switch
            {
                0 => "A",
                1 => "B",
                2 => "C",
                3 => "D",
                _ => "?"
            };

            return $"{letter}) {Options[index]}";
        }
    }
}
