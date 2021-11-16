using System;
namespace TME.Serialize
{
    public interface ISerializable
    {
        bool Load(ISerializeContext context);
        bool Save();
    }
}
