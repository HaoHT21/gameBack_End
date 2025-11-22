using Microsoft.EntityFrameworkCore;
using AssignmentGD1.Models;

namespace AssignmentGD1.Data
{
    // Đảm bảo namespace này khớp với namespace bạn đang sử dụng
    public class MinecraftDbContext : DbContext
    {
        // Khai báo các DbSet
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<TransactionHistory> Transactions { get; set; }
        public DbSet<GameMode> GameModes { get; set; }


        public MinecraftDbContext(DbContextOptions<MinecraftDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Ghi đè phương thức OnModelCreating để cấu hình tên bảng
        /// Đảm bảo tên CSDL trong SQL Server (Ví dụ: "Item", "Vehicle") khớp với tên model (số ít).
        /// Điều này giải quyết lỗi "Invalid object name 'Items'" bằng cách buộc EF Core sử dụng tên bảng đã cho.
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Thiết lập tên bảng chính xác (loại bỏ quy ước tự động thêm 's' của EF Core)
            modelBuilder.Entity<Account>().ToTable("Account");
            modelBuilder.Entity<Item>().ToTable("Item");
            modelBuilder.Entity<Vehicle>().ToTable("Vehicle");
            modelBuilder.Entity<TransactionHistory>().ToTable("TransactionHistory");
            modelBuilder.Entity<GameMode>().ToTable("GameMode");

            base.OnModelCreating(modelBuilder);
        }
    }
}