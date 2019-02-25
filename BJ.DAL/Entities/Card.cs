namespace BJ.DAL.Entities
{
    public class Card:BaseEntity
    {
        public Sqit Sqit { get; set; }
        public Value Value { get; set; }

    }

    public enum Sqit
    {
        Pyke,
        Tref,
        Bub,
        Cherv
    }

    public enum Value
    {
        six = 6,
        seven,
        eight,
        nine,
        ten,
        jack,
        lady,
        king,
        ace
    }
}
