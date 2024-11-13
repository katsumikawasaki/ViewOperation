#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.Creation;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Visual;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#endregion

namespace ViewOperation
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            try
            {
                UIApplication uiapp = commandData.Application;
                UIDocument uidoc = uiapp.ActiveUIDocument;
                Autodesk.Revit.DB.Document doc = uidoc.Document;
                Autodesk.Revit.DB.View view = doc.ActiveView;

                //ビューの種類を判断する
                String prompt = "現在作業中のビュー：\n";
                if (view is Autodesk.Revit.DB.View3D)
                {
                    prompt += "3Dビュー";
                }
                else if (view is Autodesk.Revit.DB.ViewSection)
                {
                    prompt += "断面図ビュー";
                }
                else if (view is Autodesk.Revit.DB.ViewSheet)
                {
                    prompt += "シートビュー";
                }
                else if (view is Autodesk.Revit.DB.ViewDrafting)
                {
                    prompt += "製図ビュー";
                }
                else
                {
                    prompt += "プラン他 \nビューの名前＝ " + view.Name;
                }

                TaskDialog.Show("ビュー", prompt);

                Form1 form1 = new Form1();

                if (view.Name.Contains("動力コンセント"))
                {
                    form1.SetLabel("動力コンセント");
                    form1.SetTextbox("動力コンセント平面図では以下の点を確認しましょう。\r\n" +
                        "・電源種別は正しいか\r\n" +
                        "・容量は正しいか\r\n" +
                        "・機械設備と機器記号など情報は合っているか\r\n" +
                        "・正しいファミリ、タイプが選択されているか\r\n" +
                        "・盤は正しいか\r\n" +
                        "・距離が長い回路は電線サイズアップしたか\r\n");
                }

                form1.ShowDialog();

                return Result.Succeeded;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return Result.Failed;
            }
        }
    }
}