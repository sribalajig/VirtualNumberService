using System.ComponentModel.DataAnnotations;

namespace Telephony.VritualNumberService.Entities
{
    public class Provider
    {
        public Provider() { }

        public Provider(int id, string name)
        {
            Id = id;
            Name = name;
        }

        [Key]
        public int Id { get; protected set; }

        public string Name { get; protected set; }
    }
}