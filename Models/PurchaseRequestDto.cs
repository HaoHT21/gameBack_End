// Models/PurchaseRequestDto.cs
using System.ComponentModel.DataAnnotations;

namespace AssignmentGD1.Models
{
    /// <summary>
    /// Đối tượng DTO dùng để nhận yêu cầu mua hàng từ client.
    /// </summary>
    public class PurchaseRequestDto
    {
        [Required(ErrorMessage = "Mã người chơi là bắt buộc.")]
        public int PlayerId { get; set; }

        /// <summary>
        /// Mã vật phẩm. Chỉ điền nếu mua Item.
        /// </summary>
        public int? ItemId { get; set; }

        /// <summary>
        /// Mã phương tiện. Chỉ điền nếu mua Vehicle.
        /// </summary>
        public int? VehicleId { get; set; }
    }
}