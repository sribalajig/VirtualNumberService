namespace Telephony.VritualNumberService.Entities
{
    public class Provider
    {
        public Provider(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; protected set; }

        public string Name { get; protected set; }
    }
}