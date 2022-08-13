namespace DHHelper.Models.Base
{

    public class ResponseBase<T>
    {
        public virtual string Root => "body";

        public T? Result { get; set; }

    }
}