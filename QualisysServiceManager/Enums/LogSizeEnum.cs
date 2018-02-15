using System.ComponentModel;

namespace QualisysServiceManager.Enums
{
    public enum LogSizeEnum : int
    {
        [DescriptionAttribute("10")]
        XS = 10,
        [DescriptionAttribute("100")]
        SM = 100,
        [DescriptionAttribute("1000")]
        MD = 1000,
        [DescriptionAttribute("10000")]
        LG = 10000,
    }
}
