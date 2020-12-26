using Snacks.Entity.Client.Attributes;
using System.Linq;
using System.Reflection;

namespace Snacks.Entity.Client
{
    public class BaseEntityModel<TKey> : IEntityModel<TKey>
    {
        private PropertyInfo KeyProperty => GetType().GetProperties()
            .FirstOrDefault(x => x.IsDefined(typeof(KeyAttribute)));

        public TKey Key
        {
            get => (TKey)KeyProperty.GetValue(this);
            set => KeyProperty.SetValue(this, value);
        }
        object IEntityModel.Key 
        { 
            get => Key; 
            set => Key = (TKey)value; 
        }
        public string IdempotencyKey { get; set; }
    }
}
