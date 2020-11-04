using System.Collections.Generic;

namespace Streetwise.Api.Models
{
    public class Department : BaseStringIdModel
    {
        public string Name { get; set; }
        public IList<DepartmentGroup> Groups { get; set; }
    }
}
