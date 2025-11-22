namespace AssignmentGD1.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemID { get; set; }

        [Required]
        [MaxLength(100)]
        public string ItemName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string ItemType { get; set; } = string.Empty; // Vũ khí, Công cụ, Trang phục, Đặc biệt

        [Required]
        public int Value { get; set; } // Giá trị (EXP cần để mua)
    }
}
