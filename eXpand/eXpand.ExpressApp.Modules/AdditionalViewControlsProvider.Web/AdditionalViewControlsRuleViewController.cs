﻿using System.Web.UI;
using eXpand.ExpressApp.AdditionalViewControlsProvider.Logic;
using eXpand.ExpressApp.Logic;

namespace eXpand.ExpressApp.AdditionalViewControlsProvider.Web {
    public class AdditionalViewControlsRuleViewController : Logic.AdditionalViewControlsRuleViewController{
        protected override Control AddControl(object viewSiteControl, object control, LogicRuleInfo<IAdditionalViewControlsRule> additionalViewControlsRule, AdditionalViewControlsProviderCalculator calculator, ExecutionContext context) {
            ControlCollection collection = ((Control)viewSiteControl).Controls;
            object o = GetControl(collection, control, calculator, additionalViewControlsRule);
            ((Control) o).Visible = true;
            if (additionalViewControlsRule.Rule.AdditionalViewControlsProviderPosition == AdditionalViewControlsProviderPosition.Top)
                collection.AddAt(0, (Control)o);
            else
                collection.Add((Control)o);
        }
    }
}