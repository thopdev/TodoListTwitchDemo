using System;
using Todo.Blazor.Factories.Interfaces;
using Todo.Blazor.Models;

namespace Todo.Blazor.Factories
{
    public class LoaderItemFactory : ILoaderItemFactory
    {
        public LoaderItem CreateRandomItem()
        {
            var random = new Random();
            var randomNumber = random.Next(0, 6);

            return randomNumber switch
            {
                0 => new LoaderItem("Just grabbing a coffee while is loads!",
                    "https://assets4.lottiefiles.com/packages/lf20_0E84Ic.json"),
                1 => new LoaderItem("Are you TACO'ing to me?",
                    "https://assets4.lottiefiles.com/packages/lf20_yjL4ri.json"),
                2 => new LoaderItem("A burger a day keeps the stress away!",
                    "https://assets4.lottiefiles.com/packages/lf20_FXggV8.json"),
                3 => new LoaderItem("Run away doo doo doo doo doo doo", "https://assets5.lottiefiles.com/temporary_files/aZ3MXG.json", "lisejojo"),
                4 => new LoaderItem("I love blazor, it's LAMAzing!", "https://assets6.lottiefiles.com/packages/lf20_4ll9qg6q.json", "JPReumerman"),
                5 => new LoaderItem("Beeee patient I'm bzzzzy loading ", "https://assets5.lottiefiles.com/packages/lf20_raijrjlw.json", "Irish_TeaLeaf"),
                _ => new LoaderItem("Don't mind me. Just grabbing a coffee while is loads!",
                    "https://assets4.lottiefiles.com/packages/lf20_0E84Ic.json")
            };
        }
    }
}