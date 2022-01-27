namespace MvvmCrossDemo.Core.Models
{
   public class ResponseMessage<T> : BaseResponseMessage
   {
      public T Result { get; set; }
   }
}