using System;

namespace Application.Common.Exceptions
{
    public class CantAddEntityException : Exception
    {

        public CantAddEntityException(string msg) : base(msg)
        {
        }

        
        
    }
}