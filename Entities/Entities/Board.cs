using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.Domain.Entities
{
    public class Board
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Column> Columns { get; set; }
    }

}
