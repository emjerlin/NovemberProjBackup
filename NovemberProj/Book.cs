using System;
using System.Collections.Generic;

namespace NovemberProjekt
{
   public class Book
    {
        public int price;
        int actualValue;
        public int priceToBuy;
        public int priceToSell;
        public string name;
        int rarity;
        string category;
        bool cursed;
        Random generator = new Random();
        List<string> nameList = new List<string>(){"Ex Altiora", "The Boneturner's Tale","Book of The Dead","The Key of Solomon","The Tale of a Field Hospital"};
        //kategorin kommer alltid ha samma plats i listan som boken som har den kategorin
        List<string> categoryList = new List<string>(){"Poetry","Fantasy","Horror","Horror","Non-fiction"};
        public Book(){

            actualValue = generator.Next(1,101);
            priceToBuy = generator.Next(1,101);
            rarity = generator.Next(1,11);

            int b = generator.Next(2);
            if (b == 0){
                cursed = true;
            }
            else{
                cursed = false;
            }
            int length = nameList.Count;
            int y = length--;
            int x = generator.Next(0,y);
            name = nameList[x];
            category = categoryList[x];
        }
        public void PrintInfo(){
            Console.WriteLine(name);
            Console.WriteLine("Rarity level: " + rarity);
            Console.WriteLine("Category: " + category);
            Console.WriteLine("Price: " + price);
            Console.WriteLine("Price: " + priceToBuy);
        }
        }
        }
