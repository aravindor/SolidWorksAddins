using System;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;

namespace SwEx01
{
    public static class ModelDocx
    {
         public static void CreateCylinder(IEntity reference,ModelDoc2 model,double dia, double height)
        {
            CreateExtrusion(reference,model,skmgr=>skmgr.CreateCircleByRadius(0,0,0,dia/2)!=null,height );
        }
        
        public static void CreateBox(IEntity reference,ModelDoc2 model,double width,double length, double height)
        {
            CreateExtrusion(reference,model,skmgr=>skmgr.CreateCenterRectangle(0,0,0,width/2,length/2,0)!=null,height );
        }


        private static void CreateExtrusion(IEntity reference,ModelDoc2 model,Func<ISketchManager,bool> creator,double height)
        {
            if (!reference.SelectByMark(false,0))
            {
                throw new Exception("Failed select reference !");
            }

            model.IActiveView.EnableGraphicsUpdate = false;
            model.FeatureManager.EnableFeatureTree = false;
            try
            {
                var sketch = CreateSketch(model,creator);
                if ((sketch as IFeature).Select2(false, 0))
                {
                    var extrude = model.FeatureManager.FeatureExtrusion3(true, false, false,
                        (int) swEndConditions_e.swEndCondBlind,
                        (int) swEndConditions_e.swEndCondBlind, height, 0, false, false, false, false, 0, 0, false,
                        false,
                        false, false, false, false, false,
                        (int) swStartConditions_e.swStartSketchPlane, 0, false
                    );
                    if (extrude == null)
                    {
                        throw new NullReferenceException("Failed to create extrusion");
                    }
                }
                else
                {
                    throw new Exception("Failed to create base sketch !!");
                }

            }
            finally
            {
                model.IActiveView.EnableGraphicsUpdate = true;
                model.FeatureManager.EnableFeatureTree = true;
            }
            
        }

        private static ISketch CreateSketch(ModelDoc2 model,Func<ISketchManager,bool> creator)
        {
            
            var skMgr = model.SketchManager;
            skMgr.InsertSketch(true);
            skMgr.AddToDB = true;
            var sketch = skMgr.ActiveSketch;
            if (!creator.Invoke(skMgr))
            {
                throw new NullReferenceException("Failed to create sketch segment !");
            }

            skMgr.AddToDB = false;
            skMgr.InsertSketch(true);
            return sketch;
        }
    }
}