using System;
using System.Collections.Generic;
using Raylib_cs;

namespace NovemberProjekt
{
    class Program
    {

        static void Main(string[] args)
        {
            string room = "hs";
            int money = 200;
            int totalBooks = 0; //total books ska inte minska när man säljer en bok - det är hur många man haft någonsin
            bool locked = true;

            var books = new List<Book>();

            Game g1 = new Game();

                // Jag vill att den ska köra om härifrån varje gång man går tillbaka till hs / Kunna lägga
                //in att man skapar ett raylib window i hs. Nu funkar inte det för windowshouldclose måste innefatta hs, s och a.
                //just nu error message om man kör om det 2 gånger för andra gången finns det inget window. 

                Raylib.InitWindow(1000, 1000, "Antiquary");
                int posX = 330;
                int posY = 610;


                while (!Raylib.WindowShouldClose())
                {

                    if (room == "hs")
                    {

                        Rectangle background1 = new Rectangle(0, 0, 320, 660);
                        Rectangle background2 = new Rectangle(0, 660, 1000, 340);
                        Rectangle background3 = new Rectangle(380, 0, 620, 600);
                        Rectangle background4 = new Rectangle(320, 0, 60, 200);
                        Rectangle background5 = new Rectangle(800, 600, 200, 60);
                        Rectangle roadHorizontal = new Rectangle(320, 600, 480, 60);
                        Rectangle roadVertical = new Rectangle(320, 200, 60, 400);
                        Rectangle aDoor = new Rectangle(670, 560, 60, 50);
                        Rectangle sDoor = new Rectangle(325, 270, 60, 50);
                        Rectangle player = new Rectangle(posX, posY, 20, 20);


                        DrawHomescreen(player, background1, background2, background3, background4, background5, roadHorizontal, roadVertical, aDoor, sDoor);

                        bool isCollidingBG1 = Raylib.CheckCollisionRecs(background1, player);
                        bool isCollidingBG2 = Raylib.CheckCollisionRecs(background2, player);
                        bool isCollidingBG3 = Raylib.CheckCollisionRecs(background3, player);
                        bool isCollidingBG4 = Raylib.CheckCollisionRecs(background4, player);
                        bool isCollidingBG5 = Raylib.CheckCollisionRecs(background5, player);

                        if (isCollidingBG1 == true)
                        {
                            posX++;
                        }
                        if (isCollidingBG2 == true)
                        {
                            posY--;
                        }
                        if (isCollidingBG3 == true)
                        {
                            posX--;
                            posY++;
                        }
                        if (isCollidingBG4 == true)
                        {
                            posY++;
                        }
                        if (isCollidingBG5 == true)
                        {
                            posX--;
                        }

                        //player enters antiquary
                        bool aDoorCollide = Raylib.CheckCollisionRecs(aDoor, player);
                        if (aDoorCollide == true)
                        {
                            room = "a";
                        }
                        //player enters second hand store
                        bool sDoorCollide = Raylib.CheckCollisionRecs(sDoor, player);
                        if (sDoorCollide == true)
                        {
                            room = "s";
                        }
                        if (locked == false)
                        {
                            //player controls
                            if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
                            {
                                posX--;
                            }
                            if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT))
                            {
                                posX++;
                            }
                            if (Raylib.IsKeyDown(KeyboardKey.KEY_DOWN))
                            {
                                posY++;
                            }
                            if (Raylib.IsKeyDown(KeyboardKey.KEY_UP))
                            {
                                posY--;
                            }
                        }//locked
                        locked = false;

                    }//room is hs



                    if (room == "a")
                    {
                        Console.WriteLine("Youre in the antiquary. Would you like to drop off the books in your inventory? [y/n]");
                        string dropCheck = Console.ReadLine();
                        if (dropCheck == "y")
                        {

                            Console.WriteLine("You have dropped off ");

                            for (int i = g1.inventory.Count - 1; i > -1; i--)
                            {
                                Console.WriteLine(g1.inventory[i]);
                                g1.inventory.Remove(books[i].name);
                                g1.antiqueStorage.Add(books[i].name);
                            }

                            // Jag är medveten om att detta kodblock kommer bete sig konstigt om man har gett upp en bok tidigare, men detta är 
                            //bästa lösningen jag kan hitta just nu

                            for (int i = g1.antiqueStorage.Count - 1; i > -1; i--)
                            {
                                Console.WriteLine("Set a price to sell " + g1.antiqueStorage[i] + " for.");
                                Console.ReadLine();
                            }
                        }//dropcheck y
                        else if (dropCheck == "n")
                        {

                        }//dropcheck n
                    }//room a


                    if (room == "s")
                    {

                        //Raylib.CloseWindow();
                        Console.Clear();
                        int currentInventory = g1.CountInventory();

                        while (currentInventory < 3)
                        {
                            if (currentInventory > 0)
                            {
                                Console.Clear();
                                Console.WriteLine("You have " + currentInventory + " book(s) in your inventory");
                            }//current inventory > 0
                            else
                            {
                                Console.WriteLine("You don't have anything in your inventory");
                            }


                            Console.WriteLine("Welcome to the second hand store. Would you like to buy a book? [ y / n ]. Your balance is " + money + "$");
                            Console.WriteLine("You can have a maximum of three books in your inventory, after that you have to drop them off at the antiquary");
                            string input = Console.ReadLine();

                            if (input == "y")
                            {
                                Console.Clear();

                                Console.WriteLine("You found a new book!");
                                int decisions = 0;
                                books.Add(new Book());

                                while (decisions == 0)
                                {
                                    Console.WriteLine("Here are your options:");
                                    Console.WriteLine(" [print info] to see the rarity level, price to buy, and cathegory of the book");
                                    Console.WriteLine(" [buy] to buy the book, [leave it] or anything else to look at other books ");
                                    Console.WriteLine("");
                                    string chooseBook = Console.ReadLine();

                                    if (chooseBook == "print info")
                                    {
                                        Console.Clear();
                                        books[1].PrintInfo();
                                    }  //print info

                                    else if (chooseBook == "buy")
                                    {
                                        Console.Clear();
                                        Console.WriteLine("You pay the second hand shop for the book, and put it in your bag");
                                        if (totalBooks == 0)
                                        {
                                            money = money - books[1].priceToBuy;
                                            g1.inventory.Add(books[1].name);
                                        }//totalbooks ==0

                                        currentInventory++;
                                        totalBooks++;
                                        decisions++;
                                        System.Threading.Thread.Sleep(5000);
                                    }//buy

                                    else if (chooseBook == "leave it")
                                    {
                                        Console.Clear();
                                        int confirmValid = 0;
                                        while (confirmValid == 0)
                                        {
                                            Console.WriteLine("Do you want to leave this forever, or come back for it later? (Note, that if you say yes, you will never find this book again) [leave it] or [come back later]");
                                            string confirm = Console.ReadLine();
                                            if (confirm == "leave it")
                                            {
                                                confirmValid++;
                                                Console.Clear();
                                                totalBooks++;
                                                Console.WriteLine("You leave the book. Soon, another collector walks  in and buys it. This particular copy is now unavailable");
                                            }//leave it
                                            else if (confirm == "come back later")
                                            {
                                                confirmValid++;
                                                Console.Clear();
                                                Console.WriteLine("You leave the book on it's shelf, noting to come back to it next time you're here");
                                            }//come back later
                                            else
                                            {
                                                Console.Clear();
                                                Console.WriteLine("You need to confirm your decision");
                                            }//else
                                        }//confirm valid
                                        decisions++;
                                    }//leave it

                                    else
                                    {
                                        Console.Clear();
                                        Console.WriteLine("You must decide what to do with this book");
                                    }   //else decisions

                                }//decisions
                            }//if input y 
                            else if (input == "n")
                            {
                                Console.Clear();
                                Console.WriteLine("Do you want to leave the second hand store? [y/n]");
                                string leave = Console.ReadLine();
                                if (leave == "y")
                                {
                                    locked = true;
                                    room = "hs";
                                    //Raylib.InitWindow(1000,1000,"Antiquary");   
                                    posX = 330;
                                    posY = 610;
                                    currentInventory = currentInventory + 4;
                                }
                            }//if input n

                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Please only answer with [y] or [n]");
                            }

                        }//while less than 3 or = 3
                    }// if room s
                }//while not closed

        }//static void main

        // Second hand


        //Console.WriteLine("Welcome to your first day of many at the antiquary");
        //Planen är att man ska kunna gå från rum till rum i en affär, och kunna plocka upp böcker som man kan köpa.
        //Sedan andra delen av spelet kommer man tillbaka till sin egen affär och ska sälja den till customers
        //Man får en rapport hur mycket man tjänar i slutet av dagen. Om man har kvar ett cursed item
        //får man problem under nästa dag. Om man säljer en cursed item till en customer som inte letar efter det
        //kommer man få en lawsuit, som man måste kunna betala av
        //Console.WriteLine();




        //Console.ReadLine();

        static void DrawHomescreen(Rectangle player, Rectangle background1, Rectangle background2, Rectangle background3,
        Rectangle background4, Rectangle background5, Rectangle roadVertical, Rectangle roadHorizontal, Rectangle aDoor, Rectangle sDoor)
        {

            Raylib.BeginDrawing();

            Raylib.ClearBackground(Color.WHITE);

            Raylib.DrawRectangleRec(roadVertical, Color.DARKGRAY);
            Raylib.DrawRectangleRec(roadHorizontal, Color.DARKGRAY);

            Raylib.DrawRectangleRec(background1, Color.GRAY);
            Raylib.DrawRectangleRec(background2, Color.GRAY);
            Raylib.DrawRectangleRec(background3, Color.GRAY);
            Raylib.DrawRectangleRec(background4, Color.GRAY);
            Raylib.DrawRectangleRec(background5, Color.GRAY);

            Raylib.DrawText("Welcome to a new day at the antiquary", 60, 80, 40, Color.BLACK);

            Raylib.DrawRectangle(200, 200, 300, 120, Color.RED);
            Raylib.DrawRectangleRec(sDoor, Color.BLACK);
            Raylib.DrawText("Second Hand Store", 215, 230, 25, Color.BLACK);

            Raylib.DrawRectangle(500, 410, 400, 200, Color.PURPLE);
            Raylib.DrawRectangleRec(aDoor, Color.BLACK);
            Raylib.DrawText("ANTIQUARY", 590, 450, 35, Color.BLACK);
            Raylib.DrawRectangleRec(player, Color.PURPLE);

            Raylib.EndDrawing();


        }
    }
}
