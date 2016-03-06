using System.Data.Entity;
using Telephony.VritualNumberService.Entities;
using Telephony.VritualNumberService.Entities.Purpose;
using Telephony.VritualNumberService.Entities.States;
using Telephony.VritualNumberService.Entities.VirtualNumber;

namespace Telephony.VritualNumberService.Data.Persistence
{
    public class VirtualNumberContext : DbContext
    {
        public VirtualNumberContext()
            : base("Babajob")
        { }

        public VirtualNumberContext(string connectionStringName)
            : base(connectionStringName)
        { }

        public DbSet<VirtualNumber> VirtualNumbers { get; set; }

        public DbSet<VirtualNumberAssociation> VirtualNumberAssociations { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Purpose> Purposes { get; set; }

        public DbSet<State> States { get; set; }

        public DbSet<Provider> Providers { get; set; } 

        public DbSet<PhoneNumber> PhoneNumbers { get; set; }   

        public DbSet<BabajobUserType> BabajobUserTypes { get; set; } 
    }
}