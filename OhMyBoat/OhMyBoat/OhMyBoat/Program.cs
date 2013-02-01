using System;

namespace OhMyBoat
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            bool server = false;
            string ip = "127.0.0.1";
            //affichage de la textbox avec choix

            

            using (var game = new Application(server, ip))
            {
                game.Run();
            }
        }
    }
#endif
}

