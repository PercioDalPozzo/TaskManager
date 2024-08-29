using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commands.TaskQuery
{
    public record TaskQueryResponse(IEnumerable<Entities.Task> Records)
    {    
    }
}
