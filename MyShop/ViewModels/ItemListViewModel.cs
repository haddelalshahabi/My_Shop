using System;
using System.Collections.Generic;
using MyShop.Models;

namespace MyShop.ViewModels
{
	public class ItemListViewModel
	{
		public IEnumerable<Item> Items;
		public string? CurrentViewName;

		public ItemListViewModel(IEnumerable<Item> items, string? currentViewName)
		{
			Items = items;
			CurrentViewName = currentViewName; 
		}
	}
}