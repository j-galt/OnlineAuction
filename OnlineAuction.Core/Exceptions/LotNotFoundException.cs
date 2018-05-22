using System;

namespace OnlineAuction.Core.Exceptions
{
    /// <summary>
    /// Exception class. Thrown when lot is null or there's no lot with put id.
    /// </summary>
    public class LotNotFoundException : Exception
    {
        public LotNotFoundException(int lotID) : base($"No lot found with id {lotID}")
        {
        }

        protected LotNotFoundException(System.Runtime.Serialization.SerializationInfo info, 
            System.Runtime.Serialization.StreamingContext context) 
            : base(info, context)
        {
        }

        public LotNotFoundException(string message) : base(message)
        {
        }

        public LotNotFoundException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }
    }
}
