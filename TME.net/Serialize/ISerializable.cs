using System;
namespace TME.Serialize
{
    public interface ISerializableLoad
    {
        bool Load(ISerializeContext context);
    }
    
    public interface ISerializableSave
    {
        bool Save(ISerializeContext context);
    }
    
    public interface ISerializable : ISerializableLoad, ISerializableSave
    {
    }
}
