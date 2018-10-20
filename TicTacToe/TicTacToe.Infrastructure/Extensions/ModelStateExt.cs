namespace TicTacToe.Infrastructure.Extensions
{
    using System.Linq;
    using System.Web.Mvc;

    public static class ModelStateExt
    {
        public static void AddCommonError(this ModelStateDictionary controller, string message)
        {
            if (!controller.IsValid && controller.Keys.Any(key => key.IsNotNullOrEmpty()))
            {
                controller.AddFirstToKey(string.Empty, message);
            }
        }

        public static void AddFirstToKey(this ModelStateDictionary controller, string key, string message)
        {
            if (!controller.ContainsKey(key))
            {
                controller.AddModelError(key, message);
            }
            else
            {
                controller[key].Errors.Insert(0, new ModelError(message));
            }
        }
    }
}
