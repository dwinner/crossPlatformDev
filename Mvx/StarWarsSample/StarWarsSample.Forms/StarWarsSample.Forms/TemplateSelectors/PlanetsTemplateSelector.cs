using StarWarsSample.Core.Models;
using Xamarin.Forms;

namespace StarWarsSample.Forms.UI.TemplateSelectors
{
   public class PlanetsTemplateSelector : DataTemplateSelector
   {
      public DataTemplate NameTemplate { get; set; }

      public DataTemplate WhiteNameTemplate { get; set; }

      protected override DataTemplate OnSelectTemplate(object item, BindableObject container) =>
         item.GetType() == typeof(Planet)
            ? NameTemplate
            : WhiteNameTemplate;
   }
}