using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using CodeStack.SwEx.AddIn;
using CodeStack.SwEx.AddIn.Attributes;
using CodeStack.SwEx.AddIn.Enums;
using CodeStack.SwEx.Common.Attributes;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using SwEx01.Properties;

namespace SwEx01
{
    [ComVisible(true), Guid("93FA6291-9967-46D7-A534-93C07962ABAE")]
    [AutoRegister("My First sw Addin", "Addin for beginning", true)]
    public class Class1 : SwAddInEx
    {
        [Title("My Addon")]
        [Description("My first addon for sw")]
       [Icon(typeof(Resources),nameof(Resources.primitives_icon))]
        private enum commands_enum
        {
            [Title("Create Cylinder")]
            [Description("Create cylinder")]
            [Icon(typeof(Resources),nameof(Resources.cylinder_icon))]
            [CommandItemInfo(true,true,swWorkspaceTypes_e.Part,true)]
            CreateCylinder,
            
            [Title("Create Box")]
            [Description("Create Box")]
            [Icon(typeof(Resources),nameof(Resources.cylinder_icon))]
            [CommandItemInfo(true,true,swWorkspaceTypes_e.Part,true)]
            CreateBox
        }

        public override bool OnConnect()
        {
            AddCommandGroup<commands_enum>(OnButtonClick, OnButtonEnabled);
            return true;
        }


        private void OnButtonClick(commands_enum cmd)
        {
            try
            {
                switch (cmd)
                {
                    case commands_enum.CreateCylinder:
                        ModelDocx.CreateCylinder(App.IActiveDoc2,0.01, 0.01);
                        break;
                    case commands_enum.CreateBox:
                        ModelDocx.CreateBox(App.IActiveDoc2,10,20,50);
                        break;
                }
            }
            catch (Exception e)
            {
                App.SendMsgToUser2(e.Message, (int) swMessageBoxIcon_e.swMbStop, (int) swMessageBoxBtn_e.swMbOk);
            }
        }

        private void OnButtonEnabled(commands_enum cmd, ref CommandItemEnableState_e state)
        {
            switch (cmd)
            {
                case commands_enum.CreateBox:
                case commands_enum.CreateCylinder:
                    var model = App.IActiveDoc2;
                    state = CommandItemEnableState_e.DeselectDisable;
                    if (model is PartDoc)
                    {
                        var select_type =(swSelectType_e) model.ISelectionManager.GetSelectedObjectType3(1, -1);
                        if (select_type== swSelectType_e.swSelDATUMPLANES)
                        {
                            state = CommandItemEnableState_e.DeselectEnable;
                        }
                        else if(select_type==swSelectType_e.swSelFACES)
                        {
                            var face = model.ISelectionManager.GetSelectedObject6(1,-1) as IFace2;

                            if (face.IGetSurface().IsPlane())
                            {
                                state = CommandItemEnableState_e.DeselectEnable;
                            }
                        }
                    }
                    
                    break;
            }
        }

       
    }
}