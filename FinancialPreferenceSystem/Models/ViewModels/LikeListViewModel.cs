namespace FinancialPreferenceSystem.Models.ViewModels
{
    public class LikeListViewModel
    {
        public int SN { get; set; }              // 對應 LikeList.SN
        public string UserID { get; set; }       // 使用者ID
        public int Nom { get; set; }             // 對應 Product.Nom
        public string ProductName { get; set; }  // 商品名稱
        public decimal Price { get; set; }       // 商品價格
        public decimal FeeRate { get; set; }     // 手續費率
        public int OrderQty { get; set; }        // 購買數量
        public string Account { get; set; }      // 扣款帳號
        public decimal TotalAmount { get; set; } // 預計扣款總金額
        public decimal TotalFee { get; set; }    // 總手續費
        public string Email { get; set; }        // 使用者電子信箱
    }
}
