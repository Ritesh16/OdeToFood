using System;

namespace OdeToFood.Data
{
    public class GuidService : IGuidService
    {
        private readonly Guid guid;

        public GuidService()
        {
            guid = Guid.NewGuid();
        }
        public string GetGuid() => guid.ToString();

    }
}
