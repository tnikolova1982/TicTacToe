namespace TicTacToe.Infrastructure.CustomAttributes
{
    using System.ComponentModel.DataAnnotations;
    using System.Configuration;

    public class ConfigRegularExpressionAttribute : RegularExpressionAttribute
    {
        public ConfigRegularExpressionAttribute(string patternConfigKey)
       : base(ConfigurationManager.AppSettings[patternConfigKey])
        {
        }
    }
}