using System.Collections.Generic;

namespace SharpLibrary.ViewModels
{
    public class ListViewModel<T>
    {
        public IEnumerable<T> Entities { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
