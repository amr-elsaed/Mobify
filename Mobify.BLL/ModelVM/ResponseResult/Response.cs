namespace Mobify.BLL.ModelVM.ResponseResult
{
    public record Response<T>(T result , string? ErrorMessage , bool IsHasErrorOrNot);
}
