namespace HRSystem.BLL.ResponseResult
{
    // record for respone
    public record Response<T>(T Value, string? ErrorMessage, bool IsSuccess);
}
