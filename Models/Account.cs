namespace AssignmentGD1.Models

{
   
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PlayerID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        // Trong ứng dụng thực tế, nên bỏ qua việc ánh xạ cột này và sử dụng dịch vụ mã hóa
        [Required]
        [MaxLength(100)]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        public int EXP { get; set; } // Điểm kinh nghiệm tích lũy

        public int? CurrentModeID { get; set; } // Khóa ngoại, có thể là NULL

        // Navigation Properties (Khóa ngoại)
        [ForeignKey("CurrentModeID")]
        public GameMode? GameMode { get; set; }

        // Thuộc tính điều hướng cho lịch sử giao dịch
        public ICollection<TransactionHistory> Transactions { get; set; } = new List<TransactionHistory>();
    }
}
