using System;

namespace MP.Core.ObjetosDeDominio
{
    public interface IRepository<T> : IDisposable where T  : IAggregateRoot
    {
    }
}
