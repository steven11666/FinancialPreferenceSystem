using FinancialPreferenceSystem.Repositories;
using FinancialPreferenceSystem.Models;
using FinancialPreferenceSystem.Models.ViewModels;

namespace FinancialPreferenceSystem.Services
{
    public class LikeService
    {
        private readonly LikeRepository _repo;

        public LikeService(LikeRepository repo)
        {
            _repo = repo;
        }

        public void AddLike(string userId, int productId, int qty, string account)
        {
            _repo.AddLike(userId, productId, qty, account);
        }

        public IEnumerable<LikeListViewModel> GetLikeList(string userId)
        {
            return _repo.GetLikeList(userId);
        }

        public User GetUserById(string userId)
        {
            return _repo.GetUserById(userId);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _repo.GetAllProducts();
        }
        
        public LikeListViewModel GetLikeById(int sn)
        {
            return _repo.GetLikeById(sn);
        }

        public void UpdateLike(LikeListViewModel model)
        {
            _repo.UpdateLike(model);
        }

        public void DeleteLike(int sn)
        {
            _repo.DeleteLike(sn);
        }

    }
}
