using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Domain.Exceptions;

internal class ValidationException : Exception
{
    public ValidationException(string message) : base(message) { }

}
