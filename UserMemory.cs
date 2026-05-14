namespace Securitybot
{
    internal class UserMemory
    {
        public string UserName { get; set; } = string.Empty;
        public string FavouriteTopic { get; set; } = string.Empty;

        public bool HasName()
        {
            return !string.IsNullOrWhiteSpace(UserName);
        }

        public bool HasFavouriteTopic()
        {
            return !string.IsNullOrWhiteSpace(FavouriteTopic);
        }
    }
}
