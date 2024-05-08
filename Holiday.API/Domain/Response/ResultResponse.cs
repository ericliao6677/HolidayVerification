using Holiday.API.Domain.Enum;

namespace Holiday.API.Domain.Response
{
    public record ResultResponse(ReturnCodeEnum ReturnCode = ReturnCodeEnum.Success, string ReturnMessage = "", object ReturnData = null);
    public record ResultResponse<TResult>(TResult ReturnData, ReturnCodeEnum ReturnCode = ReturnCodeEnum.Success, string ReturnMessage = "");
}
