namespace Renetdux.Infrastructure.Services
{
    public interface IResultModel<T>
    {
        bool IsSuccess { get; }
        string ErrorMessage { get; }
        T Result { get; }
    }
}
