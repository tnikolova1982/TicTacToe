namespace TicTacToe.Infrastructure.ModelBinders
{
    using System;
    using System.Globalization;
    using System.Web.Mvc;

    public class DateTimeBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var modelName = bindingContext.ModelName;
            var value = bindingContext.ValueProvider.GetValue(modelName);

            try
            {
                if (bindingContext.ModelMetadata.IsNullableValueType && (value == null || string.IsNullOrWhiteSpace(value.AttemptedValue)))
                {
                    return null;
                }

                return value != null ? value.ConvertTo(typeof(DateTime), CultureInfo.CurrentCulture) : null;
            }
            catch
            {
                bindingContext.ModelState.AddModelError(modelName, "Некоректен формат на датата!");
            }

            return value;
        }
    }
}
