using System.Collections.Generic;
using Spark.Engine.Core;

namespace Spark.Engine.Interfaces
{
    public interface ITransfer
    {
        void Internalize(Interaction interaction);
        void Internalize(IEnumerable<Interaction> interactions);
        void Externalize(Interaction interaction);
        void Externalize(IEnumerable<Interaction> interactions);
    }
}