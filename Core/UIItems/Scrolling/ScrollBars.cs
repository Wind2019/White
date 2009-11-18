using System.Windows.Automation;
using White.Core.AutomationElementSearch;
using White.Core.Configuration;
using White.Core.UIItems.Actions;

namespace White.Core.UIItems.Scrolling
{
    public class ScrollBars : AbstractScrollBars
    {
        private readonly ActionListener actionListener;
        protected readonly AutomationElementFinder finder;

        protected delegate AutomationElement FindElement(AutomationSearchCondition condition);

        public ScrollBars(AutomationElement automationElement, ActionListener actionListener)
        {
            this.actionListener = actionListener;
            finder = new AutomationElementFinder(automationElement);
        }

        public override IHScrollBar Horizontal
        {
            get { return FindHorizontalBar(finder.Child); }
        }

        protected virtual IHScrollBar FindHorizontalBar(FindElement findElement)
        {
            AutomationElement horizontalScrollElement =
                findElement(
                    AutomationSearchCondition.ByAutomationId(UIItemIdAppXmlConfiguration.Instance.HorizontalScrollBar).OfControlType(
                        ControlType.ScrollBar));
            if (horizontalScrollElement == null) return new NullHScrollBar();
            return new HScrollBar(horizontalScrollElement, actionListener);
        }

        public override IVScrollBar Vertical
        {
            get { return FindVerticalBar(finder.Child); }
        }

        protected virtual IVScrollBar FindVerticalBar(FindElement findElement)
        {
            AutomationElement verticalScrollElement =
                findElement(
                    AutomationSearchCondition.ByAutomationId(UIItemIdAppXmlConfiguration.Instance.VerticalScrollBar).OfControlType(
                        ControlType.ScrollBar));
            if (verticalScrollElement == null) return new NullVScrollBar();
            return new VScrollBar(verticalScrollElement, actionListener);
        }
    }
}
