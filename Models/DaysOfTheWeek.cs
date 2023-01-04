namespace Mythos.Common.Models;

[Flags]
public enum DaysOfTheWeek
{
    /// <summary>
    /// Indicates no days.
    /// </summary>
    Unknown = 1 << 0,
    /// <summary>
    ///     Indicates Sunday.
    /// </summary>
    Sunday = 1 << 1,
    /// <summary>
    ///     Indicates Monday.
    /// </summary>
    Monday = 1 << 2,
    /// <summary>
    ///     Indicates Tuesday.
    /// </summary>
    Tuesday = 1 << 3,
    /// <summary>
    ///     Indicates Wednesday.
    /// </summary>
    Wednesday = 1 << 4,
    /// <summary>
    ///     Indicates Thursday.
    /// </summary>
    Thursday = 1 << 5,
    /// <summary>
    ///     Indicates Friday.
    /// </summary>
    Friday = 1 << 6,
    /// <summary>
    ///     Indicates Saturday.
    /// </summary>
    Saturday = 1 << 7,
}
