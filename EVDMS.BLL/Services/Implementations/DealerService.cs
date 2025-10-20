using EVDMS.BLL.Services.Abstractions;
using EVDMS.Core.Entities;
using EVDMS.DAL.Repositories.Abstractions; // Đảm bảo using repository

namespace EVDMS.BLL.Services.Implementations
{
    public class DealerService : IDealerService
    {
        private readonly IDealerRepository _dealerRepository;
        // --- BỔ SUNG KHAI BÁO FIELD ---
        private readonly IAccountRepository _accountRepository; // Cần để kiểm tra account liên quan

        // --- CẬP NHẬT CONSTRUCTOR ---
        public DealerService(IDealerRepository dealerRepository, IAccountRepository accountRepository) // Thêm IAccountRepository
        {
            _dealerRepository = dealerRepository;
            _accountRepository = accountRepository; // Gán giá trị
        }

        public async Task<IEnumerable<Dealer>> GetAllAsync()
        {
            return await _dealerRepository.GetAllAsync();
        }

        public async Task<Dealer?> GetDealerByIdAsync(Guid id)
        {
            return await _dealerRepository.GetByIdAsync(id);
        }

        public async Task<Dealer> CreateDealerAsync(Dealer newDealer)
        {
            newDealer.Id = Guid.NewGuid();
            return await _dealerRepository.AddAsync(newDealer);
        }

        public async Task UpdateDealerAsync(Dealer dealer)
        {
            await _dealerRepository.UpdateAsync(dealer);
        }

        // --- HÀM KIỂM TRA ACCOUNT (Sử dụng _accountRepository đã inject) ---
        public async Task<bool> HasAssociatedAccountsAsync(Guid dealerId)
        {
            // Giả định IAccountRepository có AnyAsync
            // Kiểm tra xem có Account nào (chưa bị xóa mềm) thuộc Dealer này không
            return await _accountRepository.AnyAsync(a => a.DealerId == dealerId && !a.IsDeleted);
        }

        // --- HÀM XÓA ĐÃ SỬA (Sử dụng HasAssociatedAccountsAsync) ---
        public async Task DeleteDealerAsync(Guid id)
        {
            if (await HasAssociatedAccountsAsync(id))
            {
                throw new InvalidOperationException("Cannot delete dealer because there are associated accounts.");
            }

            var dealer = await _dealerRepository.GetByIdAsync(id);
            if (dealer != null)
            {
                await _dealerRepository.DeleteAsync(dealer);
            }
        }

        // --- Các hàm kiểm tra trùng lặp ---
        public async Task<bool> CodeExistsAsync(string code, Guid? excludeId = null)
        {
            return await _dealerRepository.AnyAsync(d => d.Code.ToLower() == code.ToLower() && d.Id != excludeId);
        }

        public async Task<bool> NameExistsAsync(string name, Guid? excludeId = null)
        {
            return await _dealerRepository.AnyAsync(d => d.Name.ToLower() == name.ToLower() && d.Id != excludeId);
        }

        public async Task<bool> EmailExistsAsync(string email, Guid? excludeId = null)
        {
            return await _dealerRepository.AnyAsync(d => d.Email.ToLower() == email.ToLower() && d.Id != excludeId);
        }
    }
}