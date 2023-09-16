using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyShop.DAL;
using MyShop.Models;
using MyShop.ViewModels;

namespace MyShop.Controllers;

public class ItemController : Controller
{
    private readonly IItemRepository _itemRepository;
    private readonly ILogger<ItemController> _logger;

    public ItemController(IItemRepository itemRepository, ILogger<ItemController> logger)
    {
        _itemRepository = itemRepository;
        _logger = logger;
    }

    public async Task<IActionResult> Table()
    {
        var items = await _itemRepository.GetAll();
        if (items == null)
        {
            _logger.LogError("[ItemController] Item list not found while executing _itemRepository.GetAll()");
            return NotFound("Item list not found");
        }
        var itemListViewModel = new ItemListViewModel(items, "Table");
        return View(itemListViewModel);
    }

    public async Task<IActionResult> Grid()
    {
        var items = await _itemRepository.GetAll();
        if (items == null)
        {
            _logger.LogError("[ItemController] Item list not found while executing _itemRepository.GetAll()");
            return NotFound("Item list not found");
        }
        var itemListViewModel = new ItemListViewModel(items, "Grid");
        return View(itemListViewModel);
    }

    public async Task<IActionResult> Details(int id)
    {
        var item = await _itemRepository.GetItemById(id);
        if (item == null)
        {
            _logger.LogError("[ItemController] Item not found for the ItemId {ItemId:0000}", id);
            return NotFound("Item not found for the ItemId");
        }
        return View(item);
    }

    [HttpGet]
    [Authorize]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create(Item item)
    {
        if (ModelState.IsValid)
        {
            bool returnOk = await _itemRepository.Create(item);
            if (returnOk)
                return RedirectToAction(nameof(Table));
        }
        _logger.LogWarning("[ItemController] Item creation failed {@item}", item);
        return View(item);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Update(int id)
    {
        var item = await _itemRepository.GetItemById(id);
        if (item == null)
        {
            _logger.LogError("[ItemController] Item not found when updating the ItemId {ItemId:0000}", id);
            return BadRequest("Item not found for the ItemId");
        }
        return View(item);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Update(Item item)
    {
        if (ModelState.IsValid)
        {
            bool returnOk = await _itemRepository.Update(item);
            if (returnOk)
                return RedirectToAction(nameof(Table));
        }
        _logger.LogWarning("[ItemController] Item update failed {@item}", item);
        return View(item);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _itemRepository.GetItemById(id);
        if (item == null)
        {
            _logger.LogError("[ItemController] Item not found for the ItemId {ItemId:0000}", id);
            return BadRequest("Item not found for the ItemId");
        }
        return View(item);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        bool returnOk = await _itemRepository.Delete(id);
        if (!returnOk)
        {
            _logger.LogError("[ItemController] Item deletion failed for the ItemId {ItemId:0000}", id);
            return BadRequest("Item deletion failed");
        }
        return RedirectToAction(nameof(Table));
    }
}

//public IActionResult Table()
//{
//    var items = GetItems();
//    ViewBag.CurrentViewName = "Table";
//    return View(items);
//}

//public IActionResult Grid()
//{
//    var items = GetItems();
//    ViewBag.CurrentViewName = "Grid";
//    return View(items);
//}

//public List<Item> GetItems()
//{
//    var items = new List<Item>();
//    var item1 = new Item
//    {
//        ItemId = 1,
//        Name = "Pizza",
//        Price = 150,
//        Description = "Delicious Italian dish with a thin crust topped with tomato sauce, cheese, and various toppings.",
//        ImageUrl = "/images/pizza.jpg"
//    };

//    var item2 = new Item
//    {
//        ItemId = 2,
//        Name = "Fried Chicken Leg",
//        Price = 20,
//        Description = "Crispy and succulent chicken leg that is deep-fried to perfection, often served as a popular fast food item.",
//        ImageUrl = "/images/chickenleg.jpg"
//    };

//    var item3 = new Item
//    {
//        ItemId = 3,
//        Name = "French Fries",
//        Price = 50,
//        Description = "Crispy, golden-brown potato slices seasoned with salt and often served as a popular side dish or snack.",
//        ImageUrl = "/images/frenchfries.jpg"
//    };

//    var item4 = new Item
//    {
//        ItemId = 4,
//        Name = "Grilled Ribs",
//        Price = 250,
//        Description = "Tender and flavorful ribs grilled to perfection, usually served with barbecue sauce.",
//        ImageUrl = "/images/ribs.jpg"
//    };

//    var item5 = new Item
//    {
//        ItemId = 5,
//        Name = "Tacos",
//        Price = 150,
//        Description = "Tortillas filled with various ingredients such as seasoned meat, vegetables, and salsa, folded into a delicious handheld meal.",
//        ImageUrl = "/images/tacos.jpg"
//    };

//    var item6 = new Item
//    {
//        ItemId = 6,
//        Name = "Fish and Chips",
//        Price = 180,
//        Description = "Classic British dish featuring battered and deep-fried fish served with thick-cut fried potatoes.",
//        ImageUrl = "/images/fishandchips.jpg"
//    };

//    var item7 = new Item
//    {
//        ItemId = 7,
//        Name = "Cider",
//        Price = 50,
//        Description = "Refreshing alcoholic beverage made from fermented apple juice, available in various flavors.",
//        ImageUrl = "/images/cider.jpg"
//    };

//    var item8 = new Item
//    {
//        ItemId = 8,
//        Name = "Coke",
//        Price = 30,
//        Description = "Popular carbonated soft drink known for its sweet and refreshing taste.",
//        ImageUrl = "/images/coke.jpg"
//    };

//    items.Add(item1);
//    items.Add(item2);
//    items.Add(item3);
//    items.Add(item4);
//    items.Add(item5);
//    items.Add(item6);
//    items.Add(item7);
//    items.Add(item8);
//    return items;
//}
//}