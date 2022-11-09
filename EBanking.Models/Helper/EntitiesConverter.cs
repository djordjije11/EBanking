namespace EBanking.Models.Helper
{
    public class EntitiesConverter<T> where T : IEntity
    {
        public static List<T> ConvertList(List<IEntity> entities)
        {
            var list = new List<T>();
            foreach (IEntity entity in entities)
            {
                if (entity is T)
                {
                    list.Add((T)entity);
                } 
                else throw new IncompatibleEntity("Incompatible type of entity while converting!");
            }
            return list;
        }
    }
    public class IncompatibleEntity : InvalidCastException
    {
        public IncompatibleEntity(string error) : base(error) { }
    }
}
