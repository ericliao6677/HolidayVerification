using Holiday.API.Domain.Enum;

namespace Holiday.API.Domain.Response;

public record ResultResponse<TResponse>(ReturnCodeEnum ReturnCode = ReturnCodeEnum.Success, string ReturnMessage = "", TResponse? ReturnData = default);
