using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.Domain.Entities
{
    public class Column
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BoardId { get; set; }
        public Board Board { get; set; }
        public ICollection<Card> Cards { get; set; }
    }
}
