using System.Data;
using System.Data.SqlClient;
using Dapper;
using FinancialPreferenceSystem.Models;
using FinancialPreferenceSystem.Models.ViewModels;

namespace FinancialPreferenceSystem.Repositories
{
    public class LikeRepository
    {
        private readonly string _connectionString;

        public LikeRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        // 新增喜好商品
        public void AddLike(string userId, int productId, int qty, string account)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Execute("sp_AddLikeProduct", new
            {
                UserID = userId,
                Nom = productId,
                OrderQty = qty,
                Account = account
            }, commandType: CommandType.StoredProcedure);
        }

        // 查詢喜好清單
        public IEnumerable<LikeListViewModel> GetLikeList(string userId)
        {
            using var conn = new SqlConnection(_connectionString);
            return conn.Query<LikeListViewModel>(
                "sp_GetLikeList",
                new { UserID = userId },
                commandType: CommandType.StoredProcedure);
        }

        // 取得使用者資料
        public User GetUserById(string userId)
        {
            using var conn = new SqlConnection(_connectionString);
            return conn.QueryFirstOrDefault<User>(
                "SELECT * FROM [User] WHERE UserID = @UserID",
                new { UserID = userId });
        }

        // 取得所有產品
        public IEnumerable<Product> GetAllProducts()
        {
            using var conn = new SqlConnection(_connectionString);
            return conn.Query<Product>("SELECT * FROM Product");
        }

        // 取得單筆修改前
        public LikeListViewModel GetLikeById(int sn)
        {
            using var conn = new SqlConnection(_connectionString);
            return conn.QueryFirstOrDefault<LikeListViewModel>(
                "sp_GetLikeById",
                new { SN = sn },
                commandType: CommandType.StoredProcedure);
        }

        public void UpdateLike(LikeListViewModel model)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Execute("sp_UpdateLikeProduct", new
            {
                SN = model.SN,
                Nom = model.Nom,
                OrderQty = model.OrderQty,
                Account = model.Account
            }, commandType: CommandType.StoredProcedure);
        }

        public void DeleteLike(int sn)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Execute("sp_DeleteLike", new { SN = sn }, commandType: CommandType.StoredProcedure);
        }
    }
}
