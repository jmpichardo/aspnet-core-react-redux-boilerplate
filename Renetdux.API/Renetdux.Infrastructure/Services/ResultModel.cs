namespace Renetdux.Infrastructure.Services
{
    public struct ResultModel<T> : IResultModel<T>
    {
        public bool IsSuccess { get; private set; }
        public string ErrorMessage { get; private set; }
        public T Result { get; }

        public ResultModel(T result, bool isSuccess, string errorMessage = null)
        {
            Result = result;
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
        }

        public ResultModel(T result)
        {
            Result = result;
            IsSuccess = true;
            ErrorMessage = null;
        }

        public ResultModel(bool isSuccess, string errorMessage = null)
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
            Result = default(T);
        }
    }
}
