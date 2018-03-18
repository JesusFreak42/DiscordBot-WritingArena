using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot_WritingArena.Modules
{
    public class Ping : ModuleBase<SocketCommandContext>
    {
        Random die = new Random();

        [Command("ping")]
        public async Task pingAsync()
        {
            await ReplyAsync("Hello world!");
        }

        [Command("enter")]
        public async Task enterAsync()
        {
            int whichVillain = die.Next(1, 3); //shall not exceed the number of items in villainList below
            int wordGoal = die.Next(50, 500);
            string[] villainList = new string[] { "fierce battle goat", "giant armored slug", "poison-spitting llama" };
            
            await ReplyAsync("You step into the arena and confront a " + villainList[whichVillain] + ". Spring 10 minutes and write " + wordGoal + " words to defeat it!");

        }

    }
}

/*List of things to defeat in the arena:
 * battle goat
 * giant armored slug
 * poison-spitting llama
 * 
 */


