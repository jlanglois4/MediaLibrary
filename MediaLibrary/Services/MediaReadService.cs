using System;
using System.Collections.Generic;

namespace MediaLibrary
{
    public class MediaReadService
    {
        public void ListMedia(List<string> list)
        {
            if (list.Count != 0)
            {
                int index = 0;
                int anotherTen = 10;
                while (list.Count != index)
                {
                    if (index < (list.Count - 10))
                    {
                        for (int i = index; i < (anotherTen); i++)
                        {
                            Console.WriteLine(list[i]);
                            index += 1;
                        }

                        anotherTen = index + 10;
                        Console.WriteLine("Enter 1 to exit. Enter anything else to continue.");
                        var lineRead = Console.ReadLine();
                        if (lineRead.Equals("1"))
                        {
                            index = list.Count;
                            Console.WriteLine("Exit.");
                        }
                        else
                        {
                            Console.WriteLine("Continue.");
                        }
                    }
                    else
                    {
                        anotherTen = (list.Count - index);
                        for (int i = 0; i < anotherTen; i++)
                        {
                            Console.WriteLine(list[i + index]);
                        }

                        index = list.Count;
                    }
                }
            }
        }
    }
}