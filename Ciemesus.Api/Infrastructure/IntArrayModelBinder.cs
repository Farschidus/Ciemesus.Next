using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System.Linq;
using System.Threading.Tasks;

namespace Ciemesus.Api.Extensions
{
    public class IntArrayModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (value == null || string.IsNullOrEmpty(value.FirstValue))
            {
                return Task.CompletedTask;
            }

            var result = value
                .FirstValue
                .Split(',')
                .Select(int.Parse)
                .ToArray();

            bindingContext.Result = ModelBindingResult.Success(result);

            return Task.CompletedTask;
        }
    }

    public class IntArrayModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType == typeof(int[]))
            {
                return new BinderTypeModelBinder(typeof(IntArrayModelBinder));
            }

            return null;
        }
    }
}
