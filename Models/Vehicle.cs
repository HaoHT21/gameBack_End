namespace AssignmentGD1.Models
{
    // Models/Vehicle.cs
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Vehicle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VehicleID { get; set; }

        [Required]
        [MaxLength(100)]
        public string VehicleName { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(4, 2)")] // Ánh xạ tới kiểu DECIMAL(4, 2) trong SQL Server
        public decimal SpeedBonus { get; set; }

        [Required]
        public int Value { get; set; } // Giá trị (EXP cần để mua)
    }
}
