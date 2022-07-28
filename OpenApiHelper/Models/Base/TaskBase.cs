namespace OpenApiHelper.Models.Base
{

    public class TaskBase<T>
    {
        public bool IsSuccess { get; set; }

        public string? Message { get; set; }

        public T? Result { get; set; }

        public Exception? Exception { get; set; }
    }


}