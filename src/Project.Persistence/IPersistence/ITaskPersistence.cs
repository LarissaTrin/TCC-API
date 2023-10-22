using Project.Domain;

namespace Project.Persistence.IPersistence
{
    public interface ITaskPersistence
    {
        Task<TaskProject[]> GetAllTasksByCardIdAsync(int cardId);
    }
}