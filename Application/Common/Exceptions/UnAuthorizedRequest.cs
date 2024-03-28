using System;

namespace Application.Common.Exceptions
{
    public class UnAuthorizedRequest : Exception

    {
        public UnAuthorizedRequest(string msg) : base(msg)
        {
        }
    }
}