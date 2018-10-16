using System;
namespace AnotherTaskManager.App.Models.Mappers.Interfaces
{
    public interface IMapperConverter
    {
        object Convert<S>(S source);
    }
}
