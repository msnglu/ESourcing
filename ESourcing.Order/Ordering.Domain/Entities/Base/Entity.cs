using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Entities.Base
{
#pragma warning disable CS0436 // Type conflicts with imported type
    public abstract class Entity : IEntityBase
#pragma warning restore CS0436 // Type conflicts with imported type
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; protected set; }

#pragma warning disable CS0436 // Type conflicts with imported type
        public Entity Clone()
#pragma warning restore CS0436 // Type conflicts with imported type
        {
#pragma warning disable CS0436 // Type conflicts with imported type
            return (Entity)this.MemberwiseClone();
#pragma warning restore CS0436 // Type conflicts with imported type
        }
    }
}
