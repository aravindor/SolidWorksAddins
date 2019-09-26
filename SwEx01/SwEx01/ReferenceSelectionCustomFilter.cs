using CodeStack.SwEx.PMPage.Base;
using CodeStack.SwEx.PMPage.Controls;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;

namespace SwEx01
{
    public class ReferenceSelectionCustomFilter : SelectionCustomFilter<IEntity>
    {
        protected override bool Filter(IPropertyManagerPageControlEx selBox, IEntity selection, swSelectType_e selType,
            ref string itemText)
        {
            if (selType == swSelectType_e.swSelFACES)
            {
                var face = selection as IFace2;

                if (!face.IGetSurface().IsPlane())
                {
                    return false;
                }
            }

            return true;
        }
    }
}