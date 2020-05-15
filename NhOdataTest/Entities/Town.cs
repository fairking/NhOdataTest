using System;

namespace NhOdataTest.Entities
{
    public class Town
    {
        protected Town()
        {
        }

        public Town(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            Name = name;
        }

        public virtual Guid Id { get; protected set; }
        public virtual string Name { get; protected set; }
    }
}
