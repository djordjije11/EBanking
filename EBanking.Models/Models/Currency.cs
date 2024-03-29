﻿namespace EBanking.Models
{
    public class Currency : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public override bool Equals(object? obj)
        {
            if (obj is Currency currency)
            {
                return currency.Id == this.Id;
            }
            else return false;
        }
        public override string ToString() { return Code; }
        public int GetIdentificator() { return Id; }
        public void SetIdentificator(int id) { Id = id; }
        public string SinglePrint()
        {
            return $"\nИд: {Id}\nИме валуте: {Name}\nКод валуте: {Code}";
        }
    }
}