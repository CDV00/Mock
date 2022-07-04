using System.Collections.Generic;

namespace Entities.LinkModels
{
    public class LinkCollectionWrapper<T> : LinkResourceBase
    {
        public List<T> Data { get; set; } = new List<T>();
        public LinkCollectionWrapper()
        { }
        public LinkCollectionWrapper(List<T> value) => Data = value;
    }
}
