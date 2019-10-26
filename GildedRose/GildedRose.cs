using System;
using System.Collections.Generic;

namespace GildedRose
{
    public class GildedRose
    {
        readonly IList<Item> _items;
        
        private const string AGED_BRIE = "Aged Brie";
        private const string SULFURAS_HAND_RAGNAROS = "Sulfuras, Hand of Ragnaros";
        private const string BACKSTAGE_PASSES = "Backstage passes to a TAFKAL80ETC concert";
        private const string CONJURED = "Conjured";

        public GildedRose(IList<Item> items)
        {
            this._items = items;
        }
        public void UpdateQuality()
        {
            foreach (Item item in _items)
            {
                //Regular Items
                if (item.Name != AGED_BRIE && item.Name != BACKSTAGE_PASSES)
                {
                    if (item.Quality > 0)
                    {
                        if (item.Name != SULFURAS_HAND_RAGNAROS)
                        {
                            if (item.Name == CONJURED)
                            {
                                item.Quality = item.Quality - 2;
                            }

                            item.Quality = item.Quality - 1;
                        }
                    }
                }
                else
                {
                    if (item.Quality < 50)
                    {
                        item.Quality = item.Quality + 1;

                        if (item.Name == BACKSTAGE_PASSES)
                        {
                            if (item.SellIn < 11)
                            {
                                if (item.Quality < 50)
                                {
                                    item.Quality = item.Quality + 1;
                                }
                            }

                            if (item.SellIn < 6)
                            {
                                if (item.Quality < 50)
                                {
                                    item.Quality = item.Quality + 1;
                                }
                            }
                        }
                    }
                }

                if (item.Name != SULFURAS_HAND_RAGNAROS)
                {
                    item.SellIn = item.SellIn - 1;
                }

                if (item.SellIn >= 0) continue;
                if (item.Name != AGED_BRIE)
                {
                    if (item.Name != BACKSTAGE_PASSES)
                    {
                        if (item.Quality <= 0) continue;
                        if (item.Name != SULFURAS_HAND_RAGNAROS)
                        {
                            if (item.Name == CONJURED) item.Quality = item.Quality - 2;

                            item.Quality = item.Quality - 1;

                        }
                    }
                    else
                    {
                        item.Quality = item.Quality - item.Quality;
                    }
                }
                else
                {
                    if (item.Quality < 50)
                    {
                        item.Quality = item.Quality + 1;
                    }
                }
            }
        }
    }

}