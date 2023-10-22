using Project.Domain;

namespace Project.Persistence.IPersistence
{
    public interface IApproverPersistence
    {
        Task<Approver[]> GetAllApproverByCardIdAsync(int cardId);
    }
}