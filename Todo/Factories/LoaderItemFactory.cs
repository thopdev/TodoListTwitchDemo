using System;
using System.Collections.Generic;
using System.Linq;
using Todo.Factories.Interfaces;
using Todo.Models;

namespace Todo.Factories
{
    public class LoaderItemFactory : ILoaderItemFactory
    {
        public LoaderItem CreateRandomItem()
        {
            var random = new Random();
            var randomNumber = random.Next(0, 3);

            return randomNumber switch
            {
                0 => new LoaderItem("Just grabbing a coffee while is loads!",
                    "https://assets4.lottiefiles.com/packages/lf20_0E84Ic.json"),
                1 => new LoaderItem("Are you TACO'ing to me?",
                    "https://assets4.lottiefiles.com/packages/lf20_yjL4ri.json"),
                2 => new LoaderItem("A burger a day keeps the stress away!",
                    "https://assets4.lottiefiles.com/packages/lf20_FXggV8.json"),
                _ => new LoaderItem("Don't mind me. Just grabbing a coffee while is loads!",
                    "https://assets4.lottiefiles.com/packages/lf20_0E84Ic.json")
            };
        }
    }
}