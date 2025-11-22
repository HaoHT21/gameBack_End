namespace AssignmentGD1.Models
{
    // Models/TransactionHistory.cs
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class TransactionHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionID { get; set; }

        [Required]
        public int PlayerID { get; set; } // Khóa ngoại

        public int? PurchasedItemID { get; set; } // Khóa ngoại, NULL nếu mua Vehicle

        public int? PurchasedVehicleID { get; set; } // Khóa ngoại, NULL nếu mua Item

        [Required]
        public int PricePaid { get; set; } // Giá đã trả (EXP)

        [Required]
        public DateTime TransactionDate { get; set; }

        // Navigation Properties (Điều hướng)
        [ForeignKey("PlayerID")]
        public Account? Player { get; set; }

        [ForeignKey("PurchasedItemID")]
        public Item? Item { get; set; }

        [ForeignKey("PurchasedVehicleID")]
        public Vehicle? Vehicle { get; set; }
    }
}
