namespace AssignmentGD1.Models
{
    using System.ComponentModel.DataAnnotations;

    public class GameMode
    {
        [Key]
        public int ModeID { get; set; }

        [Required]
        [MaxLength(50)]
        public string ModeName { get; set; } = string.Empty;

        // Navigation Properties (Điều hướng ngược)
        public ICollection<Account> Accounts { get; set; } = new List<Account>();
    }
}
