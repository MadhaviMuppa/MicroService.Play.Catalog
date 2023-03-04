using Microsoft.AspNetCore.Mvc;
using play.catalog.service.Dtos;
using play.catalog.service.Entities;
using Play.Common;

namespace play.catalog.service.Controllers
{
    [ApiController]
    [Route("Items")]
    public class ItemsController : ControllerBase
    {
        /*private static List<ItemDto> _items = new()
       {
        new ItemDto(Guid.NewGuid(),"Test1","Testing Book",20,DateTimeOffset.UtcNow),
        new ItemDto(Guid.NewGuid(),"Test2","Testing Book",20,DateTimeOffset.UtcNow),
        new ItemDto(Guid.NewGuid(),"Test3","Testing Book",20,DateTimeOffset.UtcNow)
       };*/
        private readonly IRepository<Item> _items;
        public ItemsController(IRepository<Item> itemRepo)
        {
            _items = itemRepo;
        }

        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetAsync()
        {
            var items = (await _items.GetAllAsync()).Select(Item => Item.AsDto());
            return items;
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<ItemDto>> GetByIdAsync(Guid Id)
        {
            var item = (await _items.GetAsync(Id)).AsDto();

            return item;
        }

        [HttpPost]
        public async Task<ActionResult<ItemDto>> PostAsync(CreateItemDto item)
        {
            var itemDto = new Item
            {
                Name = item.Name,
                Price = item.Price,
                Description = item.Description,
                CreatedDateTime = DateTimeOffset.UtcNow
            };
            await _items.CreateAsync(itemDto);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = itemDto.Id }, item);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(Guid id, UpdateItemDto item)
        {
            var _ExistedItem = (await _items.GetAsync(id));

            if (_ExistedItem == null)
                return NotFound();
            _ExistedItem.Name = item.Name;
            _ExistedItem.Price = item.Price;
            _ExistedItem.Description = item.Description;
            //_ExistedItem.CreatedDate=item.

            var index = _items.UpdateAsync(_ExistedItem);

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await _items.DeleteAsync(id);
            return NoContent();
        }

    }
}