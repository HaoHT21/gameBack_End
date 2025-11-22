// Controllers/ShopController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AssignmentGD1.Data; // Thay thế bằng namespace Data của bạn
using AssignmentGD1.Models; // Thay thế bằng namespace Models của bạn

namespace AssignmentGD1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly MinecraftDbContext _context;

        public ShopController(MinecraftDbContext context)
        {
            // Nhận DbContext thông qua Dependency Injection
            _context = context;
        }

        /// <summary>
        /// API để thực hiện giao dịch mua 1 Item hoặc 1 Vehicle.
        /// </summary>
        /// <param name="request">Chứa PlayerId, ItemId hoặc VehicleId</param>
        [HttpPost("purchase")]
        public async Task<IActionResult> Purchase([FromBody] PurchaseRequestDto request)
        {
            // --- 1. Xác định giá và loại vật phẩm ---
            int pricePaid;
            string purchaseType;

            // Đảm bảo chỉ có một trong hai ID được cung cấp
            if (request.ItemId.HasValue && !request.VehicleId.HasValue)
            {
                var item = await _context.Items.FindAsync(request.ItemId.Value);
                if (item == null) return NotFound("Item không tồn tại trong cửa hàng.");
                pricePaid = item.Value;
                purchaseType = item.ItemName;
            }
            else if (request.VehicleId.HasValue && !request.ItemId.HasValue)
            {
                var vehicle = await _context.Vehicles.FindAsync(request.VehicleId.Value);
                if (vehicle == null) return NotFound("Phương tiện không tồn tại trong cửa hàng.");
                pricePaid = vehicle.Value;
                purchaseType = vehicle.VehicleName;
            }
            else
            {
                return BadRequest("Yêu cầu phải mua DUY NHẤT 1 Item hoặc 1 Phương tiện.");
            }

            // --- 2. Kiểm tra tài khoản và EXP ---
            var account = await _context.Accounts.FindAsync(request.PlayerId);
            if (account == null) return NotFound("Người chơi không tồn tại.");

            if (account.EXP < pricePaid)
            {
                return BadRequest($"EXP hiện tại ({account.EXP}) không đủ để mua {purchaseType} (giá {pricePaid}).");
            }

            // --- 3. Thực hiện Giao dịch (Sử dụng Transaction) ---
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // A. Trừ EXP của người chơi
                    account.EXP -= pricePaid;
                    _context.Accounts.Update(account);

                    // B. Ghi lại lịch sử giao dịch
                    var history = new TransactionHistory
                    {
                        PlayerID = request.PlayerId,
                        PurchasedItemID = request.ItemId,
                        PurchasedVehicleID = request.VehicleId,
                        PricePaid = pricePaid,
                        TransactionDate = DateTime.Now
                    };
                    await _context.Transactions.AddAsync(history);

                    await _context.SaveChangesAsync(); // Lưu cả hai thay đổi (EXP và History)
                    await transaction.CommitAsync(); // Xác nhận giao dịch thành công

                    return Ok(new
                    {
                        Message = $"{purchaseType} đã được mua thành công!",
                        NewExp = account.EXP,
                        Time = history.TransactionDate
                    });
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync(); // Hoàn tác nếu bất kỳ bước nào thất bại
                    return StatusCode(500, "Lỗi xảy ra trong quá trình xử lý giao dịch.");
                }
            }
        }
    }
}