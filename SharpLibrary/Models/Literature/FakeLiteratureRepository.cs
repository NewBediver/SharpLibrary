using System.Collections.Generic;
using System.Linq;

namespace SharpLibrary.Models
{
    public class FakeLiteratureRepository : ILiteratureRepository
    {
        public IQueryable<Literature> Literature => new List<Literature>
        {
            new Literature {Name = "Война и мир", Pages = 400},
            new Literature {Name = "Мертвые души", Pages = 350}
        }.AsQueryable();
    }
}
