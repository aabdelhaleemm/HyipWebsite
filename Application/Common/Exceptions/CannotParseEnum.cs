using System;

namespace Application.Common.Exceptions
{
    public class CannotParseEnum : Exception
    {
        public CannotParseEnum(string msg) : base(msg)
        {
            
        }
    }
}