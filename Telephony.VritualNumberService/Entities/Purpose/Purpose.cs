using System.Collections.Generic;

namespace Telephony.VritualNumberService.Entities.Purpose
{
    public abstract class Purpose
    {
        public abstract int Id { get; }

        public abstract string Name { get; }

        public abstract string Description { get; }
    }
}