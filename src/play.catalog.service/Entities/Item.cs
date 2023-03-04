using Play.Common;

namespace play.catalog.service.Entities
{
    public class Item : IEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public DateTimeOffset CreatedDateTime { get; set; }

    }
}