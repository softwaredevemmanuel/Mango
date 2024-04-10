using Mango.Web.Models.Dto;

namespace Mango.Web.Service.IService
{
    public interface IBaseService
    {
        Task <ResponseDto?> SendAsync(RequestDto requestDto);
    }
}
