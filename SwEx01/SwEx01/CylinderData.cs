using System.ComponentModel;
using CodeStack.SwEx.Common.Attributes;
using CodeStack.SwEx.PMPage.Attributes;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using SwEx01.Properties;

namespace SwEx01
{
    [PageOptions(swPropertyManagerPageOptions_e.swPropertyManagerOptions_CancelButton|swPropertyManagerPageOptions_e.swPropertyManagerOptions_OkayButton)]
    [Title("My Addon")]
    [Icon(typeof(Resources), nameof(Resources.primitives_icon))]
    public class CylinderData
    {
        
        private IEntity reference;
        private double dia;
        private double height;

        [SelectionBox(typeof(ReferenceSelectionCustomFilter),swSelectType_e.swSelDATUMPLANES,swSelectType_e.swSelFACES)]
        [Description("Face or Plane")]
        [ControlOptions(height:12)]
        [ControlAttribution(swControlBitmapLabelType_e.swBitmapLabel_SelectFace)]
        public IEntity Reference
        {
            get => reference;
            set => reference = value;
        }
        
        [Description("Diameter")]
        [ControlAttribution(swControlBitmapLabelType_e.swBitmapLabel_Diameter)]
        [NumberBoxOptions(swNumberboxUnitType_e.swNumberBox_Length,0,1000,0.01,false,0.1,0.01,swPropMgrPageNumberBoxStyle_e.swPropMgrPageNumberBoxStyle_ComboEditBox)]
        public double Dia
        {
            get => dia;
            set => dia = value;
        }
        
        [Description("Height")]
        [Icon(typeof(Resources), nameof(Resources.primitives_icon))]
        [NumberBoxOptions(swNumberboxUnitType_e.swNumberBox_Length,0,1000,0.01,false,0.1,0.01,swPropMgrPageNumberBoxStyle_e.swPropMgrPageNumberBoxStyle_Thumbwheel)]
        public double Height
        {
            get => height;
            set => height = value;
        }
    }
}