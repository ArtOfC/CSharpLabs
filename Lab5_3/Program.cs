using System;
using System.Windows.Forms;

namespace Lab5_4
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.Run(new MainWindow());
        }
    }
    
    class MainWindow: Form
    {
        private StatusStrip statStrip;
    
        public MainWindow()
        {
            MenuStrip menu = new MenuStrip();

            ToolStripMenuItem FileIt = new ToolStripMenuItem("File");
            ToolStripMenuItem workIt = new ToolStripMenuItem("Work");
            ToolStripMenuItem AboutIt = new ToolStripMenuItem("About");
            ToolStripMenuItem ExitIt = new ToolStripMenuItem("Exit");
            
            FileIt.DropDownItems.Add(ExitIt);
            
            AboutIt.Click += AbountF;
            ExitIt.Click += ExitF;
            workIt.Click += WorkF;
            
            menu.Items.Add(FileIt);
            menu.Items.Add(workIt);
            menu.Items.Add(AboutIt);
            menu.Items.Add(ExitIt);
            
            statStrip = new StatusStrip();
            
            ToolStripStatusLabel statLabel = new ToolStripStatusLabel();
            statStrip.Items.Add(statLabel);
            
            statLabel = new ToolStripStatusLabel();
            statStrip.Items.Add(statLabel);
            
            Controls.Add(statStrip);

            statStrip.Items[0].Text = "³��� ������";
            statStrip.Items[1].Text = "������� ���� ����";

            Controls.Add(menu);
        }

        private void AbountF(object sender, EventArgs a)
        {
            Console.WriteLine("About");
            statStrip.Items[1].Text = "³������ ���� About";
            MessageBox.Show("�� �������� �������� ��� ����������� ������ 5.4");
            statStrip.Items[1].Text = "������� ���� ����";
        }
        private void WorkF(object sender, EventArgs a)
        {
            Console.WriteLine("Work");
            statStrip.Items[1].Text = "³��� ������ �� ������";
        }

        private void ExitF(object sender, EventArgs x)
        {
            Console.WriteLine("FormClosing");
            //ClickEvent FormClosing = FormClosedEventHandler;
            MessageBox.Show("Exit");
            Close();
        }
    }
}
