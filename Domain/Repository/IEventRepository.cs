using Domain.Dto.Event;

namespace Domain.Repository
{
    public interface IEventRepository
    {
        Task DeleteEventByParams(DeleteEventByParamsInputDto input);
    }
}
