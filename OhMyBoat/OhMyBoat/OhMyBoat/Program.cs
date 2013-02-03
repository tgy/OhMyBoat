using System;

namespace OhMyBoat
{
#if WINDOWS || XBOX
    static class Program
    {
        static void Main(string[] args)
        {
            bool server = false;
            string ip = "127.0.0.1";
            //affichage de la textbox avec choix

            using (var game = new Application())
            {
                game.Run();
            }
        }
    }
#endif
}

