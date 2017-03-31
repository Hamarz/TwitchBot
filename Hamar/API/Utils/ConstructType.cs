namespace Hamar.API.Utils
{
    /// <summary>
    /// Determines if script has a constructor parameter requirement for these.
    /// </summary>
    public enum ConstructType
    {
        RequireNothing = 1,
        RequireIrcControl = 2, // with loggingcontrol 10
        RequireDatabaseControl = 4, // With ircControl 6
    }
}
