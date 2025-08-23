namespace FinancialPreferenceSystem.Models
{
    public class LikeList
    {
        public int SN { get; set; }
        public string UserID { get; set; }
        public int Nom { get; set; }
        public int OrderQty { get; set; }
        public string Account { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalFee { get; set; }
    }
}
