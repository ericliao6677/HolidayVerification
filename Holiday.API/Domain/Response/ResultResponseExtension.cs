using Holiday.API.Domain.Entity;

namespace Holiday.API.Domain.Response
{
    public class ResultResponseExtension
    {
        public static class Query
        {
            public static ResultResponse QuerySuccess(object data) => new ResultResponse(ReturnMessage: "查詢成功", ReturnData: data);
            //public static ResultResponse<HolidayEntity> QuerySuccessG(HolidayEntity data) => new ResultResponse<HolidayEntity>(ReturnMessage: "查詢成功", ReturnData: data);

        }
        public static class Command
        {
            public static ResultResponse SiginSuccess(string token) => new ResultResponse(ReturnMessage: "登入成功", ReturnData: token);
            public static ResultResponse InsertSuccess() => new ResultResponse(ReturnMessage: "新增成功");
            public static ResultResponse InsertFail(string? ex = null) => new ResultResponse(ReturnCode: Enum.ReturnCodeEnum.DBCommandFail, ReturnMessage: "新增失敗", ReturnData: ex);
            public static ResultResponse VaildDataError(string? content = null) => new ResultResponse(ReturnCode: Enum.ReturnCodeEnum.VaildDataError, ReturnMessage: "驗證錯誤", ReturnData: content);
            public static ResultResponse UpdateSuccess() => new ResultResponse(ReturnMessage: "修改成功");
            public static ResultResponse QueryNotFound(string id) => new ResultResponse(ReturnCode: Enum.ReturnCodeEnum.QueryNotFound, ReturnMessage: $"查無此資料 {id}");
            public static ResultResponse UpdateFail(string? ex = null) => new ResultResponse(ReturnCode: Enum.ReturnCodeEnum.DBCommandFail, ReturnMessage: "修改失敗", ReturnData: ex);
            public static ResultResponse DeleteSuccess() => new ResultResponse(ReturnMessage: "刪除成功");
            public static ResultResponse DeleteFail(string? ex = null) => new ResultResponse(ReturnMessage: "刪除失敗", ReturnData: ex);
        }
        public static class Verify
        {
            public static ResultResponse LoginVerificationError() => new ResultResponse(ReturnCode: Enum.ReturnCodeEnum.UnAuth, ReturnMessage: "登入失敗");
        }

        public static class Exception
        {
            public static ResultResponse UnexpectedException(object problemDetails) => new ResultResponse(ReturnCode: Enum.ReturnCodeEnum.Unexpected, ReturnMessage: "意外狀況", ReturnData: problemDetails);
            public static ResultResponse BadRequest(object problemDetails) => new ResultResponse(ReturnCode: Enum.ReturnCodeEnum.BadRequest, ReturnMessage: "伺服器驗證錯誤", ReturnData: problemDetails);
        }
    }
}
