using System;
using System.ComponentModel;

namespace Infrastructure.Pipeline.DataProviders
{
    public interface IDataProvider
    {
        public Type ModelType { get; }
    }
}
