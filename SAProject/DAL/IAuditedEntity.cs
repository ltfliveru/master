using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAProject.DAL
{
    public interface IAuditedEntity
    {

          DateTime CreatedAt { get; set; }

          DateTime ? UpdatedAt { get; set; }
    }
}