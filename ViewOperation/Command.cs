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

                //�r���[�̎�ނ𔻒f����
                String prompt = "���ݍ�ƒ��̃r���[�F\n";
                if (view is Autodesk.Revit.DB.View3D)
                {
                    prompt += "3D�r���[";
                }
                else if (view is Autodesk.Revit.DB.ViewSection)
                {
                    prompt += "�f�ʐ}�r���[";
                }
                else if (view is Autodesk.Revit.DB.ViewSheet)
                {
                    prompt += "�V�[�g�r���[";
                }
                else if (view is Autodesk.Revit.DB.ViewDrafting)
                {
                    prompt += "���}�r���[";
                }
                else
                {
                    prompt += "�v������ \n�r���[�̖��O�� " + view.Name;
                }

                TaskDialog.Show("�r���[", prompt);

                Form1 form1 = new Form1();

                if (view.Name.Contains("���̓R���Z���g"))
                {
                    form1.SetLabel("���̓R���Z���g");
                    form1.SetTextbox("���̓R���Z���g���ʐ}�ł͈ȉ��̓_���m�F���܂��傤�B\r\n" +
                        "�E�d����ʂ͐�������\r\n" +
                        "�E�e�ʂ͐�������\r\n" +
                        "�E�@�B�ݔ��Ƌ@��L���ȂǏ��͍����Ă��邩\r\n" +
                        "�E�������t�@�~���A�^�C�v���I������Ă��邩\r\n" +
                        "�E�Ղ͐�������\r\n" +
                        "�E������������H�͓d���T�C�Y�A�b�v������\r\n");
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