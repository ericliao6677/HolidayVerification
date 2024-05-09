using Holiday.API.Domain.Enum;

namespace Holiday.API.Domain.Response
{
    public record ResultResponse(ReturnCodeEnum ReturnCode = ReturnCodeEnum.Success, string ReturnMessage = "", object? ReturnData = default);
    
}
