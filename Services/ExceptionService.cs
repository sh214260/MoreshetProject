using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services;

public class EmptyListException : Exception
{

}
public class EntityNotFoundExceptions : Exception
{
    public object? Entity { get; set; }
    public int EntityId { get; set; }
    public Type? EntityType { get; set; }

    public EntityNotFoundExceptions()
    {

    }

    public EntityNotFoundExceptions(string? message) : base(message)
    {

    }

    public EntityNotFoundExceptions(string? message, Exception? innerException) : base(message, innerException)
    {

    }
}
public class EntityIsExsistExceptions : Exception
{
    public object? Entity { get; set; }
    public int EntityId { get; set; }
    public Type? EntityType { get; set; }

    public EntityIsExsistExceptions()
    {

    }
    public EntityIsExsistExceptions(string? message) : base(message)
    {

    }
    public EntityIsExsistExceptions(string? message, Exception? innerException) : base(message, innerException)
    {

    }
}
public class CategoryException : Exception
{

}
