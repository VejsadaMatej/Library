namespace LibraryGUI.ViewModels
{
    public class User
    {
        public int UserId { get; set; }  // Primární klíč
        public string Name { get; set; } = string.Empty;  // Jméno uživatele
        public string Email { get; set; } = string.Empty;  // Email uživatele
    }

}
