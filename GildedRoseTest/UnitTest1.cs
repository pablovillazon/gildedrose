using System;
using System.Collections.Generic;
using GildedRose;
using Microsoft.VisualStudio.TestTools.UnitTesting;



namespace GildedRoseTest
{
    [TestClass]
    public class UnitTest1
    {
        private const string AGED_BRIE = "Aged Brie";
        private const string SULFURAS_HAND_RAGNAROS = "Sulfuras, Hand of Ragnaros";
        private const string BACKSTAGE_PASSES = "Backstage passes to a TAFKAL80ETC concert";
        private const string CONJURED = "Conjured";
        [TestMethod]
        public void TestItem()
        {
            //Given
            IList<Item> Items = new List<Item> { new Item { Name = "item1", SellIn = 0, Quality = 0 } };
            //When
            GildedRose.GildedRose app = new GildedRose.GildedRose(Items);
            //Then
            Assert.AreEqual("item1", Items[0].Name);
        }

        [TestMethod]
        public void TestUpdateQuality()
        {
            IList<Item> Items = new List<Item>();
            Items.Add(new Item { Name = "foo", SellIn = 5, Quality = 3 });
            Items.Add(new Item { Name = "foo1", SellIn = 7, Quality = 4 });
            Items.Add(new Item { Name = "foo2", SellIn = 6, Quality = 5 });
            Items.Add(new Item { Name = "Aged Brie", SellIn = 10, Quality = 20 });
            Items.Add(new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 50, Quality = 10 });
            Items.Add(new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 5, Quality = 20 });

            GildedRose.GildedRose app = new GildedRose.GildedRose(Items);

            //1.- First day update
            app.UpdateQuality();

            //foo - Then
            Assert.AreEqual(4, Items[0].SellIn);
            Assert.AreEqual(2, Items[0].Quality);

            //Aged Brie - Then
            Assert.AreEqual(9, Items[3].SellIn);
            Assert.AreEqual(21, Items[3].Quality);

            //Backstage passes - Then
            Assert.AreEqual(49, Items[4].SellIn);
            Assert.AreEqual(11, Items[4].Quality);


            //2.- Second day update
            app.UpdateQuality();

            //foo - Then
            Assert.AreEqual(3, Items[0].SellIn);
            Assert.AreEqual(1, Items[0].Quality);

            //Aged Brie - Then
            Assert.AreEqual(8, Items[3].SellIn);
            Assert.AreEqual(22, Items[3].Quality);

            //Backstage passes - Then
            Assert.AreEqual(48, Items[4].SellIn);
            Assert.AreEqual(12, Items[4].Quality);

        }
        
        [TestMethod]
        public void TestAgedBrieIncreaseQuality()
        {
            //- "Aged Brie" actually increases in Quality the older it gets

            IList<Item> Items = new List<Item>();
            Items.Add(new Item { Name = AGED_BRIE, SellIn = 10, Quality = 20 });

            GildedRose.GildedRose app = new GildedRose.GildedRose(Items);

            var cont = 0;
            while (cont < 5)
            {
                app.UpdateQuality();
                cont++;
            }

            Assert.AreEqual(5, Items[0].SellIn);
            Assert.AreEqual(25, Items[0].Quality);
        }

        [TestMethod]
        public void TestQualityNoNegative()
        {
            //- The Quality of an item is never negative
            IList<Item> Items = new List<Item>();
            
            Items.Add(new Item { Name = "foo", SellIn = 6, Quality = 5 });

            GildedRose.GildedRose app = new GildedRose.GildedRose(Items);

            var cont = 0;
            while (cont < 7)
            {
                app.UpdateQuality();
                cont++;
            }

            Assert.AreEqual(0, Items[0].Quality);

        }

        [TestMethod]
        public void TestQualityDegradesTwiceFast()
        {
            //- Once the sell by date has passed, Quality degrades twice as fast
            IList<Item> Items = new List<Item>();

            Items.Add(new Item { Name = "foo", SellIn = 5, Quality = 20 });

            GildedRose.GildedRose app = new GildedRose.GildedRose(Items);

            var cont = 0;
            while (cont < 7)
            {
                app.UpdateQuality();
                cont++;
            }

            Assert.AreEqual(11, Items[0].Quality);

        }

        [TestMethod]
        public void TestQualityNoMoreThan50()
        {
            //- The Quality of an item is never more than 50
            IList<Item> Items = new List<Item>();

            Items.Add(new Item { Name = AGED_BRIE, SellIn = 10, Quality = 45 });

            GildedRose.GildedRose app = new GildedRose.GildedRose(Items);

            var cont = 0;
            while (cont < 7)
            {
                app.UpdateQuality();
                cont++;
            }

            Assert.AreEqual(50, Items[0].Quality);
        }

        [TestMethod]
        public void TestSulfurasNoDecreaseQuality()
        {
            //- "Sulfuras", being a legendary item, never has to be sold or decreases in Quality
            IList<Item> Items = new List<Item>();

            Items.Add(new Item { Name = SULFURAS_HAND_RAGNAROS, SellIn = 10, Quality = 45 });

            GildedRose.GildedRose app = new GildedRose.GildedRose(Items);

            var cont = 0;
            while (cont < 7)
            {
                app.UpdateQuality();
                cont++;
            }

            Assert.AreEqual(10, Items[0].SellIn);
            Assert.AreEqual(45, Items[0].Quality);
        }

        [TestMethod]
        public void TestBackstageIncreaseQuality()
        {
            //- "Backstage passes", like aged brie, increases in Quality as its SellIn value approaches;

            IList<Item> Items = new List<Item>();
            Items.Add(new Item { Name = BACKSTAGE_PASSES, SellIn = 10, Quality = 20 });

            GildedRose.GildedRose app = new GildedRose.GildedRose(Items);

            var cont = 0;
            while (cont < 5)
            {
                app.UpdateQuality();
                cont++;
            }

            Assert.AreEqual(5, Items[0].SellIn);
            Assert.AreEqual(25, Items[0].Quality);
        }

        [TestMethod]
        public void TestConjured()
        {
            //- "Conjured" items degrade in Quality twice as fast as normal items

            IList<Item> Items = new List<Item>();
            Items.Add(new Item { Name = CONJURED, SellIn = 10, Quality = 20 });

            GildedRose.GildedRose app = new GildedRose.GildedRose(Items);

            var cont = 0;
            while (cont < 5)
            {
                app.UpdateQuality();
                cont++;
            }

            Assert.AreEqual(5, Items[0].SellIn);
            Assert.AreEqual(10, Items[0].Quality);
        }

    }
}
