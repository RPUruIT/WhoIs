using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoIs.Configs;
using Xamarin.Forms;

namespace WhoIs.Behaviors
{
    public class HuntIndicatorBehavior:BehaviorBase<Label>
    {
   
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            string indicator = BindingContext as string;

            if (!String.IsNullOrEmpty(indicator))
            {
                string[] indicatorSplited = indicator.Split('/');

                if (indicatorSplited.Length == 2)
                {
                    double countHunted = Int64.Parse(indicatorSplited[0]);
                    double totalCount = Int64.Parse(indicatorSplited[1]);

                    double percentaje = countHunted / totalCount;

                    AssociatedObject.Style = ((percentaje < 0.3 ?
                        Application.Current.Resources["ListTitleIndicatorMinorAdvance"] : percentaje < 0.7 ?
                        Application.Current.Resources["ListTitleIndicatorMediumAdvance"] : 
                        Application.Current.Resources["ListTitleIndicatorMaxAdvance"])) as Style;
                }
            }
        }



    }
}
