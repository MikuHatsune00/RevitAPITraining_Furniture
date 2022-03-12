using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Prism.Commands;
using RevitAPITrainingLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPITraining_Furniture
{
    public class MainViewViewModel
    {
        private ExternalCommandData _commandData;


        public List<Level> Levels { get; } = new List<Level>();
        public List<FamilySymbol> FamilyTypes { get; } = new List<FamilySymbol>();
        public DelegateCommand SaveCommand { get; }
        public Level SelectedLevel { get; set; }
        public FamilySymbol SelectedFamilyType {get;set;}
        public List<XYZ> Points { get; } = new List<XYZ>();
        public Element SelectElement(UIDocument uidoc, Document doc)
        {
            Reference reference = uidoc.Selection.PickObject(ObjectType.Element);
            Element element = uidoc.Document.GetElement(reference);
            return element;
        }

        public MainViewViewModel(ExternalCommandData commandData)
        {
            _commandData = commandData;           
            Levels = LevelsUtils.GetLevels(commandData);
            FamilyTypes = FamilySymbolUtils.GetFamilySymbols(commandData);
            SaveCommand = new DelegateCommand(OnSaveCommand);   
           
            Points = SelectionUtils.GetPoints(_commandData, "Выберите точки", ObjectSnapTypes.Endpoints);
        }

        private void OnSaveCommand()
        {
            UIApplication uiapp = _commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

           
            
            var point0 = new List<Point>();
            var points = new List<Point>();
            for (int i = 0; i < Points.Count; i++)
            {
                if (i == 0)
                    continue;
                var Point1 = Points[0];
               


            }
            FamilyInstanceUtils.CreateFamilyInstance(_commandData,  SelectedFamilyType, Points[0], SelectedLevel);
           

            RaiseCloseRequest();
        }


        public event EventHandler CloseRequest;
        public void RaiseCloseRequest()
        {
            CloseRequest?.Invoke(this, EventArgs.Empty);
        }
    }
}
