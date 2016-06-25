namespace ProjectRLG
{
    using System;
    using ProjectRLG.Infrastructure;

#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            //try
            //{
            using (var game = new RogueLikeGame())
            {
                game.Run();
            }
            //}
            //catch (Exception ex)
            //{
            //    ProjectRLG.Infrastructure.Logger.Error(ex, "Program.Main");
            //}
        }
    }
#endif
}
