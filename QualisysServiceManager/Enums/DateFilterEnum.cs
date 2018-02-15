using System.ComponentModel;

namespace QualisysServiceManager.Enums
{
    public enum DateFilterEnum : int
    {
        [DescriptionAttribute("Hoy")]
        TODAY = 1,
        [DescriptionAttribute("Esta semana")]
        CURRENT_WEEK = 2,
        [DescriptionAttribute("Este mes")]
        CURRENT_MONTH = 3,
        [DescriptionAttribute("Este año")]
        CURRENT_YEAR = 4,
        [DescriptionAttribute("Última semana")]
        LAST_WEEK = 5,
        [DescriptionAttribute("Último mes")]
        LAST_MONTH = 6,
        [DescriptionAttribute("Último año")]
        LAST_YEAR = 7,
        [DescriptionAttribute("Rango")]
        RANGE = 8,
    }
}
